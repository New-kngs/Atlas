using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using AtlasDTO;

namespace AltasMES
{

    public delegate void ReadCompletedHandler(object sender, frmShip_End.ReadEventArgs e);

    public partial class frmShip_End : Form
    {

        public event ReadCompletedHandler ReadCompleted;

        SerialPort _port;
        bool IsOpen = false;
        ServiceHelper service = null;
       
        List<ShipVO> shipList = null;
        List<OrderDetailVO> orderList = null;
        List<CustomerVO> cusList = null;
        string modUser = string.Empty;


        public frmShip_End(string moduser)
        {
            modUser = moduser;
            InitializeComponent();
        }

        private void frmShip_End_Load(object sender, EventArgs e)
        {
            service = new ServiceHelper("");
            shipList = service.GetAsync<List<ShipVO>>("api/Ship/GetAllShip").Data;
            orderList = service.GetAsync<List<OrderDetailVO>>("api/Order/GetAllOrderDetail").Data;
            cusList = service.GetAsync<List<CustomerVO>>("api/Customer/GetCustomerlist").Data;

            DataGridUtil.SetInitGridView(dgvOrderDetail);
            DataGridUtil.AddGridTextBoxColumn(dgvOrderDetail, "제품ID", "ItemID", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvOrderDetail, "제품명", "ItemName", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvOrderDetail, "수량", "Qty", colwidth: 100, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvOrderDetail, "주문번호", "OrderID", visibility: false);

           

            SerialPortConnect();
            ReadCompleted += FrmShip_End_ReadCompleted;
        }

        private void FrmShip_End_ReadCompleted(object sender, ReadEventArgs e)
        {

            textBox1.Text = e.ReadMessage;

            string BarID = e.ReadMessage.Trim();
            int chBarID = 0;


            if (!int.TryParse(BarID,out chBarID))
            {
                MessageBox.Show("출하 대기 목록에 존재 하지 않습니다.","정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOrderID.Text = "";
                txtName.Text = "";
                txtCreateDate.Text = "";
                txtEndDate.Text = "";
                txtAddr.Text = "";
                dgvOrderDetail.DataSource = null;
                return;
            }

            ShipVO shipVO = shipList.Find(n => n.BarCodeID.Equals(Convert.ToInt32(BarID)));

            if(shipVO == null)
            {
                MessageBox.Show("출하 대기 목록에 존재 하지 않습니다.","정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOrderID.Text = "";
                txtName.Text = "";
                txtCreateDate.Text = "";
                txtEndDate.Text = "";
                txtAddr.Text = "";
              
                dgvOrderDetail.DataSource = null;
                return;
            }

            

            txtOrderID.Text = shipVO.OrderID;
            txtName.Text = shipVO.CustomerName;
            txtCreateDate.Text = shipVO.CreateDate;
            txtEndDate.Text = shipVO.EndDate;
            txtAddr.Text = cusList.Find(n => n.CustomerName.Equals(shipVO.CustomerName.Trim())).Address;

            dgvOrderDetail.DataSource = orderList.FindAll(n => n.OrderID.Equals(shipVO.OrderID)).ToList();
            dgvOrderDetail.ClearSelection();
            
        }

        private void SerialPortConnect()
        {
            if (IsOpen) return;

            if (_port == null)
            {
                _port = new SerialPort();
                _port.DataReceived += _port_DataReceived;
            }

            if(Properties.Settings.Default.PortName == null)
            {
                _port.PortName = "COM1";
            }
            _port.PortName = Properties.Settings.Default.PortName;
            _port.BaudRate = Convert.ToInt32(Properties.Settings.Default.BaudRate);
            _port.DataBits = Convert.ToInt32(Properties.Settings.Default.DataSize);

            Parity par = Parity.None;
            if (Properties.Settings.Default.Parity == "odd")
                par = Parity.Odd;
            else if (Properties.Settings.Default.Parity == "even")
                par = Parity.Even;

            _port.Parity = par;
            _port.Handshake = Handshake.None;

            try
            {
                if (_port.IsOpen)
                {
                   _port.Close();
                }
                _port.Open();
                IsOpen = true;
               // MessageBox.Show("연결성공");
            }
            catch (Exception err)
            {
                IsOpen = false;
                MessageBox.Show(err.Message + "\n바코드 연결 설정이 필요합니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            System.Threading.Thread.Sleep(500);

            string msg = _port.ReadExisting();

            this.Invoke(new EventHandler(delegate
            {
                if (ReadCompleted != null)
                {
                    ReadEventArgs args = new ReadEventArgs();
                    args.ReadMessage = msg;
                    ReadCompleted(this, args);
                }
            }));
        }

        public class ReadEventArgs : EventArgs
        {
            public string ReadMessage { get; set; }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            if (_port.IsOpen)
            {
                _port.Close();
            }

            //2.설정폼을 열고,
            frmPortSetting frm = new frmPortSetting();
            frm.ShowDialog();

            //3.변경된 설정정보로 다시 재연결
            if (!string.IsNullOrEmpty(Properties.Settings.Default.PortName))
            {
                IsOpen = false;
                SerialPortConnect();
            }
        }

       

        private void btnEnd_Click(object sender, EventArgs e)
        {

            if(string.IsNullOrWhiteSpace(txtOrderID.Text))
            {
                MessageBox.Show("출하 할 목록을 먼저 조회해주세요", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"{txtOrderID.Text} 대해 출하 처리 하시겠습니까?", "출하", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                OrderVO VO = new OrderVO
                {
                    OrderID = txtOrderID.Text,
                    ModifyUser = modUser,
                };

                ResMessage<List<OrderVO>> result = service.PostAsync<OrderVO, List<OrderVO>>("api/Order/OrderEnd", VO);

                if (result.ErrCode == 0)
                {
                    MessageBox.Show("출하 처리가 완료 되었습니다.", "출하", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
                else
                    MessageBox.Show(result.ErrMsg);
            }
        }

        private void frmShip_End_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_port.IsOpen)
            {
                _port.Close();
            }

            if (service != null)
            {
                service.Dispose();
            }

        }

        private void frmShip_End_Shown(object sender, EventArgs e)
        {
            dgvOrderDetail.ClearSelection();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
