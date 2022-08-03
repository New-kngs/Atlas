using AtlasDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlasPOP
{
    public partial class AtlasPOP : Form
    {


        public string EmpID { get; set; }
        public string EmpName { get; set; }
        public string DeptID { get; set; }
        public OperationVO Oper { get; set; }
        public string OperID { get; set; }

        string CustomerID;
        int process_id;

        popServiceHelper service = null;
        ResMessage<List<ItemVO>> itemList;
        ResMessage<List<OperationVO>> operList;
        ResMessage<List<OrderVO>> oderList;
        ResMessage<List<CustomerVO>> customerList;

        frmPerformance frm;
        frmOperation frmoper = null;


        public AtlasPOP()
        {
            InitializeComponent();
        }

        private void AtlasPOP_Load(object sender, EventArgs e)
        {
            //panel1.Visible = false;
            frmoper = new frmOperation();
            frmoper.MdiParent = this;
            frmoper.WindowState = FormWindowState.Maximized;
            frmoper.DataSendEvent += new DataGetEventHandler(this.DataGet);
            frmoper.Show();

            service = new popServiceHelper("");
            itemList = service.GetAsync<List<ItemVO>>("api/pop/getItem");
            operList = service.GetAsync<List<OperationVO>>("api/pop/AllOperation");
            oderList = service.GetAsync<List<OrderVO>>("api/pop/GetCustomer");
            customerList = service.GetAsync<List<CustomerVO>>("api/pop/GetCustomerName");

            tableLayoutPanel1.Visible = false;

        }

        public void ChangeValue()
        {
            tableLayoutPanel1.Visible = true;

            CustomerID = oderList.Data.Find((n) => n.OrderID == Oper.OrderID).CustomerID;

            lblOper.Text = Oper.OpID;
            lblOder.Text = Oper.OrderID;
            lblOperDate.Text = Oper.OpDate;
            lblProcess.Text = Oper.ProcessName;
            lblItem.Text = Oper.ItemName;
            lblCustomer.Text = customerList.Data.Find((n) => n.CustomerID == CustomerID).CustomerName;
            lblQty.Text = Oper.PlanQty.ToString();
            lblStatus.Text = Oper.OpState;
            lblResource.Text = Oper.resourceYN;
            lblBegin.Text = Oper.BeginDate;
            lblEnd.Text = Oper.EndDate;
            lblEmp.Text = Oper.EmpID;
        }

        private void DataGet(OperationVO data)
        {
            this.Oper = data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmOperation frm = new frmOperation();
            frm.MdiParent = this;
            frm.DataSendEvent += new DataGetEventHandler(this.DataGet);
            frm.Show();
        }

        private void btnOperStatus_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (Oper == null)
            {
                MessageBox.Show("작업을 선택해주세요");
                return;
            }
            if (Oper.OpState.Equals("작업중"))
            {
                MessageBox.Show("이미 작업중입니다.");
                return;
            }
            if (Oper.OpState.Equals("작업종료"))
            {
                MessageBox.Show("이미 종료된 작업입니다.");
                return;
            }
            if (Oper.resourceYN.Equals("N"))
            {
                MessageBox.Show("자재가 투입되지 않았습니다.");
                return;
            }

            string server = Application.StartupPath + "\\VirtualPLCMachin.exe";

            string ip = "127.0.0.1";
            string port = Oper.port;
            string name = Oper.ProcessName;


            Process pro = Process.Start(server, $"{name} {ip} {port} {Oper.PlanQty.ToString()}");
            process_id = pro.Id;

            frm = new frmPerformance(name, ip, port);
            frm.MdiParent = this;
            frm.Show();
            frm.Hide();
            frmoper.MdiParent = this;
            frmoper.WindowState = FormWindowState.Maximized;

            //IsTaskEnabled = true;
        }

        public void Finish()
        {
            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.Id.Equals(process_id))
                {
                    proc.Kill();
                    MessageBox.Show("종료되었다");
                }
            }
            
            frm.TaskExit = true;
            frm.Close();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.Id.Equals(process_id))
                {
                    proc.Kill();
                }
            }

            frm.TaskExit = true;
            frm.Close();
            
            // IsTaskEnabled = false;
        }

        private void btnResource_Click(object sender, EventArgs e)
        {

        }

        private void btnFail_Click(object sender, EventArgs e)
        {
            if (Oper == null)
            {
                MessageBox.Show("작업을 선택해주세요");
                return;
            }
            Oper.FailQty = 4;
            frmFail frm = new frmFail(Oper);
            frm.Show();
        }

        private void btnLaping_Click(object sender, EventArgs e)
        {
            if (Oper == null)
            {
                MessageBox.Show("작업을 선택해주세요");
                return;
            }
            frmLaping frm = new frmLaping(Oper);
            frm.Show();
        }

        private void btnResource_Click_1(object sender, EventArgs e)
        {
            if (Oper == null)
            {
                MessageBox.Show("작업을 선택해주세요");
                return;
            }
            frmResource frm = new frmResource(Oper);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                frmOperation frmop = new frmOperation();
                frmop.DialogResult = DialogResult.OK;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //frm.TaskExit = true;
            // frm.Close();
            this.Close();
        }
    }
}
