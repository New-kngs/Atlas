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
    public partial class AtlasPOP : Form
    {
        public string EmpID { get; set; }
        public string EmpName { get; set; }
        public string DeptID { get; set; }
        public OperationVO opervo { get; set; }
        public string OperID { get; set; }

        string itemID;
        string OrderID;
        string CustomerID;
        int FailQty = 2;

        popServiceHelper service = null;
        ResMessage<List<ItemVO>> itemList;
        ResMessage<List<OperationVO>> operList;
        ResMessage<List<OrderVO>> oderList;
        ResMessage<List<CustomerVO>> customerList;
        public AtlasPOP()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AtlasPOP_Load(object sender, EventArgs e)
        {
            
            frmOperation frm = new frmOperation();
            frm.MdiParent = this;
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
            if (OperID != null)
            {
                itemID = operList.Data.Find((n) => n.OpID == OperID).ItemID;
                OrderID = operList.Data.Find((n) => n.OpID == OperID).OrderID;
                CustomerID = oderList.Data.Find((n) => n.OrderID == OrderID).CustomerID;

                lblOper.Text = OperID;
                lblOder.Text = OrderID;
                lblOperDate.Text = operList.Data.Find((n) => n.OpID == OperID).OpDate;
                lblProcess.Text = operList.Data.Find((n) => n.OpID == OperID).ProcessName;
                lblItem.Text = itemList.Data.Find((n) => n.ItemID == itemID).ItemName;
                lblCustomer.Text = customerList.Data.Find((n) => n.CustomerID == CustomerID).CustomerName;
                lblQty.Text = operList.Data.Find((n) => n.OpID == OperID).PlanQty.ToString();
                lblStatus.Text = operList.Data.Find((n) => n.OpID == OperID).OpState;
                lblResource.Text = operList.Data.Find((n) => n.OpID == OperID).resourceYN;
                lblBegin.Text = operList.Data.Find((n) => n.OpID == OperID).BeginDate;
                lblEnd.Text = operList.Data.Find((n) => n.OpID == OperID).EndDate;
                lblEmp.Text = operList.Data.Find((n) => n.OpID == OperID).EmpID;

            }

        }

        private void DataGet(string data)
        {
            this.OperID = data;
        }

        private void AtlasPOP_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblCustomer_Click(object sender, EventArgs e)
        {

        }

        private void lblEmp_Click(object sender, EventArgs e)
        {

        }

        private void lblEnd_Click(object sender, EventArgs e)
        {

        }

        private void lblBegin_Click(object sender, EventArgs e)
        {

        }

        private void lblResource_Click(object sender, EventArgs e)
        {

        }
    }
}
