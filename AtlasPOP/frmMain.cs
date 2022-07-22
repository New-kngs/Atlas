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
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            toolStripLblTime.Text = "현재 시간 : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            label1.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        private void button6_Click(object sender, EventArgs e)
        {
            frmOperation frm = new frmOperation();
            frm.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmLaping frm = new frmLaping();
            frm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmPerformance frm = new frmPerformance();
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmFail frm = new frmFail();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmResource frm = new frmResource();
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmOperStatus frm = new frmOperStatus();
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("공정이 시작되었습니다.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("공정이 종료되었습니다");
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

               
            
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if(btnLogout.Text != "로그아웃")
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
    }
}
