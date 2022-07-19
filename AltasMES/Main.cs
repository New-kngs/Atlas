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

        private void Main_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
          

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

            for (int i = 0; i < btnName.Length; i++)
            {

                Button btn = new Button();
                btn.Text = btnName[i];
                btn.Font = new Font("맑은 고딕", 12, style: FontStyle.Bold);
                btn.Location = new Point(15, 50 + i * 50);
                btn.Size = new Size(100, 40);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.TabStop = false;
                btn.ImageAlign = ContentAlignment.MiddleLeft;
                btn.Image = imageList1.Images[i+1];
                btn.TextAlign = ContentAlignment.MiddleRight;
                btn.Padding = new Padding(5, 0, 5, 0);
                btn.Click += Btn_Click;
                panel2.Controls.Add(btn);
            }


            //string[] labelName = { "제품", "BOM", "창고", "설비", "공정", "사용자", "부서" };
            //string frmName = "frmEmployee";

            //for (int i = 0; i < labelName.Length; i++)
            //{

            //    Label lbl = new Label();
            //    lbl.Font = new Font("맑은 고딕", 12, style:FontStyle.Bold);
            //    lbl.Location = new Point(25, 50 + i * 40);
            //    lbl.Name = "label" + i.ToString();
            //    lbl.Size = new Size(80, 23);
            //    lbl.TextAlign = ContentAlignment.MiddleCenter;
            //    lbl.TabIndex = i;
            //    lbl.Text = labelName[i];
            //    if(i == 5) { lbl.Tag = frmName; }
            //    lbl.Click += Lbl_Click;
            //    lbl.MouseHover += Lbl_MouseHover;
            //    lbl.MouseLeave += Lbl_MouseLeave;
            //    panel2.Controls.Add(lbl);

            //}


        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            OpenCreateForm("frmItem");
            OpenCreateForm("frmEmployee");

        }

        private void SalesStrip_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();

            Label mainlbl = new Label();
            mainlbl.Text = "영업관리";
            mainlbl.Font = new Font("맑은 고딕", 14, style: FontStyle.Bold);
            mainlbl.BackColor = Color.FromArgb(230, 230, 230);
            mainlbl.Location = new Point(5, 0);
            mainlbl.Size = new Size(130, 28);
            mainlbl.TextAlign = ContentAlignment.MiddleCenter;
            panel2.Controls.Add(mainlbl);


            string[] labelName = { "주문", "발주", "출하", "거래처" };

            for (int i = 0; i < labelName.Length; i++)
            {

                Label lbl = new Label();
                lbl.Font = new Font("맑은 고딕", 12, style: FontStyle.Bold);
                lbl.Location = new Point(25, 50 + i * 40);
                lbl.Name = "label" + i.ToString();
                lbl.Size = new Size(80, 23);
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.TabIndex = i;
                lbl.Text = labelName[i];
                lbl.MouseHover += Lbl_MouseHover;
                lbl.MouseLeave += Lbl_MouseLeave;
                panel2.Controls.Add(lbl);

            }

        }

        private void ProductionStrip_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();

            Label mainlbl = new Label();
            mainlbl.Text = "생산관리";
            mainlbl.Font = new Font("맑은 고딕", 14, style: FontStyle.Bold);
            mainlbl.BackColor = Color.FromArgb(230, 230, 230);
            mainlbl.Location = new Point(5, 0);
            mainlbl.Size = new Size(130, 28);
            mainlbl.TextAlign = ContentAlignment.MiddleCenter;
            panel2.Controls.Add(mainlbl);

            string[] labelName = { "생산계획", "작업지시", "불량현황" };


            for (int i = 0; i < labelName.Length; i++)
            {

                Label lbl = new Label();
                lbl.Font = new Font("맑은 고딕", 12, style: FontStyle.Bold);
                lbl.Location = new Point(25, 50 + i * 40);
                lbl.Name = "label" + i.ToString();
                lbl.Size = new Size(80, 23);
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.TabIndex = i;
                lbl.Text = labelName[i];
                lbl.MouseHover += Lbl_MouseHover;
                lbl.MouseLeave += Lbl_MouseLeave;
                panel2.Controls.Add(lbl);

            }

        }

        private void Lbl_MouseLeave(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            lbl.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void Lbl_MouseHover(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            lbl.BackColor = Color.FromArgb(179, 215, 243);
        }

        private void Lbl_Click(object sender, EventArgs e)
        {
            
            
        }

        private void OpenCreateForm(string prgName)
        {
            string appName = Assembly.GetEntryAssembly().GetName().Name;
            Type frmType = Type.GetType($"{appName}.{prgName}");

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
    }
}
