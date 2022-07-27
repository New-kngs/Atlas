using AltasMES;
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
    public partial class POPMain : Form
    {
        public string EmpID { get; set; }
        public string EmpName { get; set; }
        public string DeptID { get; set; }
        public OperationVO oper { get; set; }
        public string OperID { get; set; }

        string itemID;
        string OrderID;
        string CustomerID;
        int ProcessID;
        string ProcessName;
        ServiceHelper service = null;
        ResMessage<List<ItemVO>> itemList;
        ResMessage<List<OperationVO>> operList;
        ResMessage<List<OrderVO>> oderList;
        ResMessage<List<CustomerVO>> customerList;
        public POPMain()
        {
            InitializeComponent();
        }

        private void POPMain_Load(object sender, EventArgs e)
        {
            this.EmpID = "EMP_0004";
            this.EmpName = "강지모";
            this.DeptID = "Product";

            statusStrip1.Visible = false;
            toolStripLblUser.Text = "사용자 : " + EmpName;
            toolStripLblDept.Text = "부서 : " + DeptID;

            timer1.Interval = 1000;
            toolStripLblTime.Text = "현재 시간 : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //label1.Text = DateTime.Now.ToString("HH:mm:ss");
            timer1.Start();
            timer1.Tick += Timer1_Tick;

            LoadData();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            toolStripLblTime.Text = "현재 시간 : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //label1.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        public void LoadData()
        {
            service = new ServiceHelper("api/pop");
            itemList = service.GetAsync<List<ItemVO>>("getItem");
            operList = service.GetAsync<List<OperationVO>>("AllOperation");
            oderList = service.GetAsync<List<OrderVO>>("GetCustomer");
            customerList = service.GetAsync<List<CustomerVO>>("GetCustomerName");
        }
        private void OpenCreateForm<T>() where T : Form, new()
        {
            if (btnLogout.Text != "로그아웃")
            {
                MessageBox.Show("로그인을 해주세요");
                return;
            }
            if (OperID == null)
            {
                MessageBox.Show("작업을 먼저 선택해주세요");
                return;
            }
            //폼이 열린적이 없는 경우에는 new를 하고, 열린적이 있으면 열린 폼을 활성시킨다.
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(T))
                {
                    form.Activate();
                    form.BringToFront();

                    return;
                }
            }

            T frm = new T();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnOperation_Click(object sender, EventArgs e)
        {
            if (btnLogout.Text != "로그아웃")
            {
                MessageBox.Show("로그인을 해주세요");
                return;
            }

            frmOperation frm = new frmOperation();
            frm.MdiParent = this;
            frm.DataSendEvent += new DataGetEventHandler(this.DataGet);
            frm.Show();
        }

        public void ChangeValue()
        {
            if(OperID != null)
            {
                itemID = operList.Data.Find((n) => n.OpID == OperID).ItemID;
                OrderID = operList.Data.Find((n) => n.OpID == OperID).OrderID;
                CustomerID = oderList.Data.Find((n) => n.OrderID == OrderID).CustomerID;


                lblProcessName.Text = operList.Data.Find((n) => n.OpID == OperID).ProcessName;
                lblItemName.Text = itemList.Data.Find((n) => n.ItemID == itemID).ItemName;
                lblClient.Text = customerList.Data.Find((n) => n.CustomerID == CustomerID).CustomerName;
                lblOrderQty.Text = operList.Data.Find((n) => n.OpID == OperID).PlanQty.ToString();
            }
                
        }

        private void DataGet(string data)
        {
            this.OperID = data;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (btnLogout.Text != "로그아웃")
            {
                frmLogin frm = new frmLogin();
                
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    //label1.Visible = false;
                    statusStrip1.Visible = true;
                    lblProcessName.Text = "안녕하세요";
                    btnLogout.Text = "로그아웃";
                }
            }
            else
            {
                this.Close();
            }
        }

        private void btnResource_Click(object sender, EventArgs e)
        {
            if (btnLogout.Text != "로그아웃")
            {
                MessageBox.Show("로그인을 해주세요");
                return;
            }
            if (OperID == null)
            {
                MessageBox.Show("작업을 먼저 선택해주세요");
                return;
            }

            frmResource frm = new frmResource(itemID, OperID);
            frm.MdiParent = this;
            frm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void btnPerfomance_Click(object sender, EventArgs e)
        {
            frmOperStatus frm = new frmOperStatus(itemID, OperID);
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnOperSatus_Click(object sender, EventArgs e)
        {
            OpenCreateForm<frmPerformance>();
        }

        private void btnFail_Click(object sender, EventArgs e)
        {
            OpenCreateForm<frmFail>();
        }

        private void btnLaping_Click(object sender, EventArgs e)
        {
            OpenCreateForm<frmLaping>();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnLogout.Text != "로그아웃")
            {
                MessageBox.Show("로그인을 해주세요");
                return;
            }
            if (OperID == null)
            {
                MessageBox.Show("작업을 먼저 선택해주세요");
                return;
            }
            MessageBox.Show("시작이요");
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (btnLogout.Text != "로그아웃")
            {
                MessageBox.Show("로그인을 해주세요");
                return;
            }
            if (OperID == null)
            {
                MessageBox.Show("작업을 먼저 선택해주세요");
                return;
            }
            MessageBox.Show("종료요");
        }
    }
}
