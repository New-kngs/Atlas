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
        public frmMain()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {



            timer1.Interval = 1000;
            toolStripLblTime.Text = "현재 시간 : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            timer1.Start();
            timer1.Tick += Timer1_Tick;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            toolStripLblTime.Text = "현재 시간 : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
