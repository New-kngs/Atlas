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
        public string User { get; set; }
        public OperationVO Oper { get; set; }
        int process_id = 0;
        int qty;
        int failqty;

        popServiceHelper service = null;
        ResMessage<List<OrderVO>> oderList;
        ResMessage<List<CustomerVO>> customerList;
        frmPerformance frmPerf = null;
        frmOperation frmoper = null;


        public AtlasPOP()
        {
            InitializeComponent();
        }

        private void AtlasPOP_Load(object sender, EventArgs e)
        {
            this.User = "강지모";
            service = new popServiceHelper("");
            oderList = service.GetAsync<List<OrderVO>>("api/pop/GetCustomer");
            customerList = service.GetAsync<List<CustomerVO>>("api/pop/GetCustomerName");
            tableLayoutPanel1.Visible = false;

            frmOper();
        }
        public void frmOper()
        {
            frmoper = new frmOperation();
            frmoper.MdiParent = this;
            frmoper.WindowState = FormWindowState.Maximized;
            frmoper.DataSendEvent += new DataGetEventHandler(this.DataGet);
            frmoper.Show();
        }

        public void ChangeValue()
        {
            tableLayoutPanel1.Visible = true;

            string CustomerID = oderList.Data.Find((n) => n.OrderID == Oper.OrderID).CustomerID;

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
        /// <summary>
        /// 작업지시폼에서 데이터 받아오기
        /// </summary>
        /// <param name="data"></param>
        private void DataGet(OperationVO data)
        {
            this.Oper = data;
        }

        /// <summary>
        /// 시작버튼 클릭 -> 작업시작
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            ResMessage<List<OperationVO>> start = service.PostAsync<OperationVO, List<OperationVO>>("api/pop/UdateState", Oper);
            if (start.ErrCode == 0)
            {
                MessageBox.Show($"{Oper.OpID} - 작업시작");
            }
            else
            {
                MessageBox.Show("시스템에 오류가 발생하였습니다.");
            }
            string server = Application.StartupPath + "\\VirtualPLCMachin.exe";

            string ip = "127.0.0.1";
            string port = Oper.port;
            string name = Oper.ProcessID.ToString();


            Process pro = Process.Start(server, $"{name} {ip} {port} {Oper.PlanQty.ToString()}");
            process_id = pro.Id;
            

            frmPerf = new frmPerformance(name, ip, port, Oper, process_id);
            frmPerf.MdiParent = this;
            
            frmPerf.Show();
            frmPerf.Hide();
            frmoper.WindowState = FormWindowState.Maximized;
            frmoper.LoadData();
        }

        public void Finish(int qty, int failqty, int processID)
        {
            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.Id.Equals(processID))
                {
                    proc.Kill();
                }
            }
            Oper.CompleteQty = qty;
            Oper.FailQty = failqty;
            this.qty = qty;
            this.failqty = failqty;

        }

        /// <summary>
        /// 종료버튼 클릭 -> 작업종료
        /// </summary>
        /// <param name="qty"></param>
        /// <param name="failqty"></param>
        private void btnEnd_Click(object sender, EventArgs e)
        {
            if (Oper.PutInYN.Equals("Y"))
            {
                MessageBox.Show("이미 종료된 작업입니다.");
                return;
            }

            ResMessage<List<ItemVO>> itemList = service.GetAsync<List<ItemVO>>("api/Item/AllItem");
            ItemVO Item = new ItemVO()
            {
                CurrentQty = itemList.Data.Find((f) => f.ItemID == Oper.ItemID).CurrentQty + Oper.CompleteQty,
                ModifyUser = "강지모",
                ItemID = Oper.ItemID
            };

            ResMessage<List<OperationVO>> finish = service.PostAsync<OperationVO, List<OperationVO>>("api/pop/UdateFinish", Oper);
            if (finish.ErrCode == 0)
            {
                MessageBox.Show($"총{qty} 개의 제품이 생산되었고 {failqty}개의 불량이 발생하였습니다.");
            }
            else
            {
                MessageBox.Show("종료 중 문제 발생");
            }


            ResMessage<List<ItemVO>> putIn = service.PostAsync<ItemVO, List<ItemVO>>("api/pop/PutInItem", Item);
            if (putIn.ErrCode == 0)
            {
                ResMessage<List<OperationVO>> State = service.PostAsync<string, List<OperationVO>>($"api/pop/UpdatePutInYN/{Oper.OpID}", Oper.OpID);
                if(State.ErrCode == 0)
                    MessageBox.Show("창고에 입고가 완료 되었습니다.");
                else
                {
                    MessageBox.Show("입고 중 문제가 발생하였습니다.");
                    return;
                }
            }

            frmoper.LoadData();
        }

        private void btnFail_Click(object sender, EventArgs e)
        {
            if (Oper == null)
            {
                MessageBox.Show("작업을 선택해주세요");
                return;
            }

            ResMessage<List<OperationVO>> fail = service.GetAsync<List<OperationVO>>("api/pop/AllOperation");
            if (fail.Data != null)
            {
                Oper.FailQty = fail.Data.Find((n) => n.OpID.Equals(Oper.OpID)).FailQty;
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }

            frmFail frm = new frmFail(Oper);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                frmoper.LoadData();
            }
        }

        private void btnLaping_Click(object sender, EventArgs e)
        {

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
                frmoper.LoadData();
            }
        }
        /// <summary>
        /// 종료 버튼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            foreach (Process proc in Process.GetProcesses())
            {
                if (process_id != 0)
                {
                    if (proc.Id.Equals(process_id))
                    {
                        proc.Kill();

                    }
                }
            }
            this.Close();
        }

        private void btnState_Click(object sender, EventArgs e)
        {
            if (Oper == null)
            {
                MessageBox.Show("작업을 선택해주세요");
            }
            frmPerf.Show();

            frmPerf.WindowState = FormWindowState.Normal;
            frmoper.WindowState = FormWindowState.Maximized;
            frmPerf.BringToFront();
        }
    }
}
