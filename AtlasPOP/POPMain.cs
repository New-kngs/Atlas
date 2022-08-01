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
        public POPMain()
        {
            this.EmpID = "EMP_0004";
            this.EmpName = "강지모";
            this.DeptID = "Product";

            InitializeComponent();
        }

        private void POPMain_Load(object sender, EventArgs e)
        {
            service = new popServiceHelper("");
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
            
            itemList = service.GetAsync<List<ItemVO>>("api/pop/getItem");
            operList = service.GetAsync<List<OperationVO>>("api/pop/AllOperation");
            oderList = service.GetAsync<List<OrderVO>>("api/pop/GetCustomer");
            customerList = service.GetAsync<List<CustomerVO>>("api/pop/GetCustomerName");
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
        /// <summary>
        /// 작업지시 폼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOperation_Click(object sender, EventArgs e)
        {
            if (btnLogout.Text != "로그아웃")
            {
                MessageBox.Show("로그인을 해주세요");
                return;
            }

            frmOperation frm = new frmOperation();
            frm.MdiParent = this;
            frm.MaximizeBox = false;
            frm.MinimizeBox = false;
            frm.ControlBox = false;
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


        /// <summary>
        /// 로그인/로그아웃
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// 자재투입
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

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
            frm.MaximizeBox = false;
            frm.MinimizeBox = false;
            frm.ControlBox = false;
            frm.Show();
        }
        /// <summary>
        /// 실적고나리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnPerfomance_Click(object sender, EventArgs e)
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
            frmOperStatus frm = new frmOperStatus(itemID, OperID);
            frm.MdiParent = this;
            frm.MaximizeBox = false;
            frm.MinimizeBox = false;
            frm.ControlBox = false;
            frm.Show();
        }

        private void btnOperSatus_Click(object sender, EventArgs e)
        {
            frmPerformance frm = new frmPerformance();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnFail_Click(object sender, EventArgs e)
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

            frmFail frm = new frmFail(FailQty, OperID, itemID, EmpID);
            frm.MdiParent = this;
            frm.MaximizeBox = false;
            frm.MinimizeBox = false;
            frm.ControlBox = false;
            frm.Show();
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
            OperationVO oper = new OperationVO()
            {
                ModifyUser = EmpName,
                OpID = OperID
            };
            string operState = operList.Data.Find((s) => s.OpID == OperID).OpState;
            switch (operState)
            {
                case "작업중": MessageBox.Show("이미 작업중입니다.");  break;
                case "작업종료": MessageBox.Show("작업이 종료된 작업지시입니다.");  break;
                case "작업대기":
                    ResMessage<List<OperationVO>> UdateState = service.PostAsync<OperationVO, List<OperationVO>>("api/pop/UdateState", oper);
                    if (UdateState.ErrCode == 0)
                    {
                        MessageBox.Show("작업이 시작되었습니다.");
                    }
                    else
                    {
                        MessageBox.Show("시작dksehla");
                    }
                    break;
            }

            

           
            
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
            OperationVO oper = new OperationVO()
            {
                ModifyUser = EmpName,
                OpID = OperID
            };
            string operState = operList.Data.Find((s) => s.OpID == OperID).OpState;
            switch (operState)
            {
                case "작업대기": MessageBox.Show("작업중이 아닙니다."); break;
                case "작업종료": MessageBox.Show("이미 작업이 종료된 작업지시입니다."); break;
                case "작업중":
                    ResMessage<List<OperationVO>> UdateState = service.PostAsync<OperationVO, List<OperationVO>>("api/pop/UdateFinish", oper);
                    if (UdateState.ErrCode == 0)
                    {
                        MessageBox.Show("작업이 종료되었습니다.");
                    }
                    else
                    {
                        MessageBox.Show("시작dksehla");
                    } break;
            }

            

            
        }
    }
}
