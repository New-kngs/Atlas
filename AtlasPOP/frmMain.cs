﻿using AltasMES;
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
    public partial class frmMain : Form
    {

        public string EmpID { get; set; }
        public string EmpName { get; set; }
        public string DeptID { get; set; }
        public OperationVO oper { get; set; }
        public string OperID { get; set; }

        string itemID;
        string OrderID;
        string CustomerID;

        ServiceHelper service = null;
        ResMessage<List<ItemVO>> itemList;
        ResMessage<List<OperationVO>> operList;
        ResMessage<List<OrderVO>> oderList;
        ResMessage<List<CustomerVO>> customerList;
        public frmMain()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.EmpID = "EMP_0004";
            this.EmpName = "강지모";
            this.DeptID = "Product";

            statusStrip1.Visible = false;
            toolStripLblUser.Text = "사용자 : " + EmpName;
            toolStripLblDept.Text = "부서 : " + DeptID;

            timer1.Interval = 1000;
            toolStripLblTime.Text = "현재 시간 : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            label1.Text = DateTime.Now.ToString("HH:mm:ss");
            timer1.Start();
            timer1.Tick += Timer1_Tick;

            LoadData();
        }
        public void LoadData()
        {
            service = new ServiceHelper("api/pop");
            itemList = service.GetAsync<List<ItemVO>>("getItem");
            operList = service.GetAsync<List<OperationVO>>("AllOperation");
            oderList = service.GetAsync<List<OrderVO>>("GetCustomer");
            customerList = service.GetAsync<List<CustomerVO>>("GetCustomerName");
            if (itemList.Data != null)
            {
                
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }


        private void Timer1_Tick(object sender, EventArgs e)
        {
            toolStripLblTime.Text = "현재 시간 : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            label1.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(btnLogout.Text != "로그아웃")
            {
                MessageBox.Show("로그인을 해주세요");
                return;
            }

            frmOperation frm = new frmOperation();
            frm.DataSendEvent += new DataGetEventHandler(this.DataGet);
            if(frm.ShowDialog() == DialogResult.OK)
            {
                itemID = operList.Data.Find((n) => n.OpID == OperID).ItemID;
                OrderID = operList.Data.Find((n) => n.OpID == OperID).OrderID;
                CustomerID = oderList.Data.Find((n) => n.OrderID == OrderID).CustomerID;

                lblItemName.Text = itemList.Data.Find((n) => n.ItemID == itemID).ItemName;
                lblClient.Text = customerList.Data.Find((n) => n.CustomerID == CustomerID).CustomerName;
                lblOrderQty.Text = operList.Data.Find((n) => n.OpID == OperID).PlanQty.ToString();
            } 
        }

        private void DataGet(string data)
        {
            this.OperID = data;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (btnLogout.Text != "로그아웃")
            {
                MessageBox.Show("로그인을 해주세요");
                return;
            }
            frmLaping frm = new frmLaping();
            frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (btnLogout.Text != "로그아웃")
            {
                MessageBox.Show("로그인을 해주세요");
                return;
            }
            frmPerformance frm = new frmPerformance();
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (btnLogout.Text != "로그아웃")
            {
                MessageBox.Show("로그인을 해주세요");
                return;
            }
            frmFail frm = new frmFail();
            frm.ShowDialog();
        }



        private void button7_Click(object sender, EventArgs e)
        {
            if (btnLogout.Text != "로그아웃")
            {
                MessageBox.Show("로그인을 해주세요");
                return;
            }
            frmOperStatus frm = new frmOperStatus();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (btnLogout.Text != "로그아웃")
            {
                MessageBox.Show("로그인을 해주세요");
                return;
            }
            MessageBox.Show("공정이 시작되었습니다.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (btnLogout.Text != "로그아웃")
            {
                MessageBox.Show("로그인을 해주세요");
                return;
            }
            MessageBox.Show("공정이 종료되었습니다");
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (btnLogout.Text != "로그아웃")
            {
                frmLogin frm = new frmLogin();
                if(frm.ShowDialog() == DialogResult.OK)
                {
                    label1.Visible = false;
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
            if (string.IsNullOrWhiteSpace(itemID))
            {
                MessageBox.Show("공정을 먼저 선택해주세요");
                return;
            }


            frmResource frm = new frmResource(itemID, oper);
            frm.ShowDialog();
        }
    }
}
