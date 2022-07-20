using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltasMES
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        public  string EmpID { get; set; }
        public  string EmpName { get; set; }
        public  string DeptID  {get; set;}


        private void Main_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            EmpID = "EMP_0004";
            EmpName = "강지모";
            DeptID = "Product";

        }

        private void StandardStrip_Click(object sender, EventArgs e)
        {

            panel2.Controls.Clear();

            Label mainlbl = new Label();
            mainlbl.Text = "기준정보관리";
            mainlbl.Font = new Font("맑은 고딕", 14, style: FontStyle.Bold);
            mainlbl.BackColor = Color.FromArgb(255, 255, 255);
            mainlbl.Location = new Point(5, 0);
            mainlbl.Size = new Size(130, 28);
            mainlbl.TextAlign = ContentAlignment.MiddleLeft;
            panel2.Controls.Add(mainlbl);


            string[] btnName = { "제품", "BOM", "창고", "설비", "공정" };
            string[] btnTag = { "frmItem", "frmBOM", "frmWareHouse", "frmEquipment", "frmProcess" };


            for (int i = 0; i < btnName.Length; i++)
            {

                Button btn = new Button();
                btn.Text = btnName[i];
                btn.Font = new Font("맑은 고딕", 12, style: FontStyle.Bold);
                btn.Location = new Point(15, 50 + i * 50);
                btn.Size = new Size(110, 40);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.TabStop = false;
                btn.Tag = btnTag[i];
                btn.ImageAlign = ContentAlignment.MiddleLeft;
                btn.Image = imageList1.Images[i+1];
                btn.TextAlign = ContentAlignment.MiddleRight;
                btn.Padding = new Padding(5, 0, 5, 0);
                btn.Click += Btn_Click;
                panel2.Controls.Add(btn);
            }


        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            OpenCreateForm(btn.Tag.ToString());
           
        }

        private void SalesStrip_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();

            Label mainlbl = new Label();
            mainlbl.Text = "영업관리";
            mainlbl.Font = new Font("맑은 고딕", 14, style: FontStyle.Bold);
            mainlbl.BackColor = Color.FromArgb(255, 255, 255);
            mainlbl.Location = new Point(5, 0);
            mainlbl.Size = new Size(130, 28);
            mainlbl.TextAlign = ContentAlignment.MiddleCenter;
            panel2.Controls.Add(mainlbl);


            string[] btnName = { "주문", "발주", "출하", "거래처" };
            string[] btnTag = { "frmOrder", "frmPurchase", "frmShip", "frmAccount"};

            for (int i = 0; i < btnName.Length; i++)
            {

                Button btn = new Button();
                btn.Text = btnName[i];
                btn.Font = new Font("맑은 고딕", 12, style: FontStyle.Bold);
                btn.Location = new Point(15, 50 + i * 50);
                btn.Size = new Size(110, 40);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.TabStop = false;
                btn.Tag = btnTag[i];
                btn.ImageAlign = ContentAlignment.MiddleLeft;
                btn.Image = imageList1.Images[i + 6];
                btn.TextAlign = ContentAlignment.MiddleRight;
                btn.Padding = new Padding(5, 0, 5, 0);
                btn.Click += Btn_Click;
                panel2.Controls.Add(btn);

            }

        }

        private void ProductionStrip_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();

            Label mainlbl = new Label();
            mainlbl.Text = "생산관리";
            mainlbl.Font = new Font("맑은 고딕", 14, style: FontStyle.Bold);
            mainlbl.BackColor = Color.FromArgb(255, 255, 255);
            mainlbl.Location = new Point(5, 0);
            mainlbl.Size = new Size(130, 28);
            mainlbl.TextAlign = ContentAlignment.MiddleCenter;
            panel2.Controls.Add(mainlbl);


            string[] btnName = { "생산계획", "작업지시", "불량현황" };
            string[] btnTag = { "frmPlan", "frmOperation", "frmFail"};

            for (int i = 0; i < btnName.Length; i++)
            {

                Button btn = new Button();
                btn.Text = btnName[i];
                btn.Font = new Font("맑은 고딕", 12, style: FontStyle.Bold);
                btn.Location = new Point(5, 50 + i * 50);
                btn.Size = new Size(130, 40);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.TabStop = false;
                btn.Tag = btnTag[i];
                btn.ImageAlign = ContentAlignment.MiddleLeft;
                btn.Image = imageList1.Images[i + 10];
                btn.TextAlign = ContentAlignment.MiddleRight;
                btn.Padding = new Padding(5, 0, 5, 0);
                btn.Click += Btn_Click;
                panel2.Controls.Add(btn);

            }

        }

       
  
        private void OpenCreateForm(string prgName)
        {
            string appName = Assembly.GetEntryAssembly().GetName().Name;
            Type frmType = Type.GetType($"{appName}.{prgName}");

            if(frmType == null) {
                MessageBox.Show("폼 등록이 필요합니다.");
                return;
            }

            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == frmType)
                {
                    form.Activate();
                    form.BringToFront();
                    return;
                }
            }

            Form frm = (Form)Activator.CreateInstance(frmType);
            frm.MdiParent = this;
            frm.ControlBox = false;
            frm.Show();
        }

        private void Main_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild == null)
            {
                tabControl1.Visible = false;
            }
            else
            {   //모든 Form은 최대화
                this.ActiveMdiChild.WindowState = FormWindowState.Maximized;


                if (this.ActiveMdiChild.Tag == null) //신규
                {
                    TabPage tp = new TabPage(this.ActiveMdiChild.Text + "    ");
                    tp.Parent = tabControl1;
                    tp.Tag = this.ActiveMdiChild;
                    tabControl1.SelectedTab = tp;


                    //자식폼이 닫힐때 탭페이지도 같이 삭제
                    this.ActiveMdiChild.FormClosed += ActiveMdiChild_FormClosed;
                    this.ActiveMdiChild.Tag = tp;
                }
                else //기존에 추가되었던 탭페이지
                {
                    tabControl1.SelectedTab = (TabPage)this.ActiveMdiChild.Tag;
                }

                if (!tabControl1.Visible)
                {
                    tabControl1.Visible = true;
                }
            }
        }

        private void ActiveMdiChild_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form frm = (Form)sender;
            ((TabPage)frm.Tag).Dispose();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != null)
            {
                Form frm = (Form)tabControl1.SelectedTab.Tag;
                frm.Select();
            }
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
             for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                var r = tabControl1.GetTabRect(i);
                var closeImage = imageList1.Images[0];
                var closeRect = new Rectangle((r.Right - closeImage.Width), r.Top + (r.Height - closeImage.Height) / 2,
                    closeImage.Width, closeImage.Height);

                if (closeRect.Contains(e.Location))
                {
                    this.ActiveMdiChild.Close();
                    break;
                }
            }
        }

        private void SystemStripButton1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();

            Label mainlbl = new Label();
            mainlbl.Text = "시스템관리";
            mainlbl.Font = new Font("맑은 고딕", 14, style: FontStyle.Bold);
            mainlbl.BackColor = Color.FromArgb(255, 255, 255);
            mainlbl.Location = new Point(5, 0);
            mainlbl.Size = new Size(130, 28);
            mainlbl.TextAlign = ContentAlignment.MiddleCenter;
            panel2.Controls.Add(mainlbl);


            string[] btnName = { "사용자", "부서", "사용이력" };
            string[] btnTag = { "frmEmployee", "frmDepartment", "frmEmpHis" };

            for (int i = 0; i < btnName.Length; i++)
            {

                Button btn = new Button();
                btn.Text = btnName[i];
                btn.Font = new Font("맑은 고딕", 12, style: FontStyle.Bold);
                btn.Location = new Point(5, 50 + i * 50);
                btn.Size = new Size(125, 40);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.TabStop = false;
                btn.Tag = btnTag[i];
                btn.ImageAlign = ContentAlignment.MiddleLeft;
                btn.Image = imageList1.Images[i + 13];
                btn.TextAlign = ContentAlignment.MiddleRight;
                btn.Padding = new Padding(5, 0, 5, 0);
                btn.Click += Btn_Click;
                panel2.Controls.Add(btn);

            }
        }
    }
}
