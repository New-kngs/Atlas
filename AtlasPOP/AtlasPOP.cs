using AtlasDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlasPOP
{
    public partial class btnPerformance : Form
    {
        public string EmpID { get; set; }
        public string EmpName { get; set; }
        public string DeptID { get; set; }
        public OperationVO Oper { get; set; }
        public string OperID { get; set; }

        string CustomerID;

        popServiceHelper service = null;
        ResMessage<List<ItemVO>> itemList;
        ResMessage<List<OperationVO>> operList;
        ResMessage<List<OrderVO>> oderList;
        ResMessage<List<CustomerVO>> customerList;
        public btnPerformance()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AtlasPOP_Load(object sender, EventArgs e)
        {
            //panel1.Visible = false;
            frmOperation frm = new frmOperation();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.DataSendEvent += new DataGetEventHandler(this.DataGet);
            frm.Show();

            service = new popServiceHelper("");
            itemList = service.GetAsync<List<ItemVO>>("api/pop/getItem");
            operList = service.GetAsync<List<OperationVO>>("api/pop/AllOperation");
            oderList = service.GetAsync<List<OrderVO>>("api/pop/GetCustomer");
            customerList = service.GetAsync<List<CustomerVO>>("api/pop/GetCustomerName");


        }

        public void ChangeValue()
        {
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
            if(Oper == null)
            {
                MessageBox.Show("작업을 선택해주세요");
                return;
            }
            if (Oper.resourceYN.Equals("N"))
            {
                MessageBox.Show("자재가 투입되지 않았습니다.");
                return;
            }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {

        }

        private void btnResource_Click(object sender, EventArgs e)
        {

        }

        private void btnFail_Click(object sender, EventArgs e)
        {
            if(Oper == null)
            {
                MessageBox.Show("작업을 선택해주세요");
                return;
            }

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
            frm.Show();
        }      
        
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
