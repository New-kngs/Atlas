
namespace AltasMES
{
    partial class Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.MaintoolStrip = new System.Windows.Forms.ToolStrip();
            this.StandardStrip = new System.Windows.Forms.ToolStripButton();
            this.SalesStrip = new System.Windows.Forms.ToolStripButton();
            this.ProductionStrip = new System.Windows.Forms.ToolStripButton();
            this.LogOutStrip = new System.Windows.Forms.ToolStripButton();
            this.SystemStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.MaintoolStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(136, 578);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 72);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(136, 506);
            this.panel2.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(136, 72);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // MaintoolStrip
            // 
            this.MaintoolStrip.BackColor = System.Drawing.Color.White;
            this.MaintoolStrip.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.MaintoolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StandardStrip,
            this.SalesStrip,
            this.ProductionStrip,
            this.LogOutStrip,
            this.SystemStripButton1});
            this.MaintoolStrip.Location = new System.Drawing.Point(136, 0);
            this.MaintoolStrip.Name = "MaintoolStrip";
            this.MaintoolStrip.Size = new System.Drawing.Size(1121, 71);
            this.MaintoolStrip.TabIndex = 2;
            this.MaintoolStrip.Text = "toolStrip1";
            // 
            // StandardStrip
            // 
            this.StandardStrip.AutoSize = false;
            this.StandardStrip.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.StandardStrip.Image = ((System.Drawing.Image)(resources.GetObject("StandardStrip.Image")));
            this.StandardStrip.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.StandardStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StandardStrip.Name = "StandardStrip";
            this.StandardStrip.Size = new System.Drawing.Size(110, 65);
            this.StandardStrip.Text = "기준정보관리";
            this.StandardStrip.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.StandardStrip.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.StandardStrip.Click += new System.EventHandler(this.StandardStrip_Click);
            // 
            // SalesStrip
            // 
            this.SalesStrip.AutoSize = false;
            this.SalesStrip.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SalesStrip.Image = ((System.Drawing.Image)(resources.GetObject("SalesStrip.Image")));
            this.SalesStrip.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.SalesStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SalesStrip.Name = "SalesStrip";
            this.SalesStrip.Size = new System.Drawing.Size(110, 65);
            this.SalesStrip.Text = "영업관리";
            this.SalesStrip.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.SalesStrip.Click += new System.EventHandler(this.SalesStrip_Click);
            // 
            // ProductionStrip
            // 
            this.ProductionStrip.AutoSize = false;
            this.ProductionStrip.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ProductionStrip.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ProductionStrip.Image = ((System.Drawing.Image)(resources.GetObject("ProductionStrip.Image")));
            this.ProductionStrip.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ProductionStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ProductionStrip.Name = "ProductionStrip";
            this.ProductionStrip.Size = new System.Drawing.Size(110, 65);
            this.ProductionStrip.Text = "생산관리";
            this.ProductionStrip.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ProductionStrip.Click += new System.EventHandler(this.ProductionStrip_Click);
            // 
            // LogOutStrip
            // 
            this.LogOutStrip.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.LogOutStrip.AutoSize = false;
            this.LogOutStrip.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LogOutStrip.Image = ((System.Drawing.Image)(resources.GetObject("LogOutStrip.Image")));
            this.LogOutStrip.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LogOutStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LogOutStrip.Margin = new System.Windows.Forms.Padding(0, 5, 0, 2);
            this.LogOutStrip.Name = "LogOutStrip";
            this.LogOutStrip.Size = new System.Drawing.Size(73, 64);
            this.LogOutStrip.Text = "로그아웃";
            this.LogOutStrip.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // SystemStripButton1
            // 
            this.SystemStripButton1.AutoSize = false;
            this.SystemStripButton1.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.SystemStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("SystemStripButton1.Image")));
            this.SystemStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.SystemStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SystemStripButton1.Name = "SystemStripButton1";
            this.SystemStripButton1.Size = new System.Drawing.Size(110, 65);
            this.SystemStripButton1.Text = "시스템관리";
            this.SystemStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.SystemStripButton1.Click += new System.EventHandler(this.SystemStripButton1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.White;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(136, 556);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1121, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(121, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tabControl1.Location = new System.Drawing.Point(136, 71);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1121, 27);
            this.tabControl1.TabIndex = 5;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDown);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "close-grey.png");
            this.imageList1.Images.SetKeyName(1, "product.png");
            this.imageList1.Images.SetKeyName(2, "BOM1.png");
            this.imageList1.Images.SetKeyName(3, "storage.png");
            this.imageList1.Images.SetKeyName(4, "equipment.png");
            this.imageList1.Images.SetKeyName(5, "process.png");
            this.imageList1.Images.SetKeyName(6, "order.png");
            this.imageList1.Images.SetKeyName(7, "purchase.png");
            this.imageList1.Images.SetKeyName(8, "ship.png");
            this.imageList1.Images.SetKeyName(9, "Account.png");
            this.imageList1.Images.SetKeyName(10, "plan.png");
            this.imageList1.Images.SetKeyName(11, "workorder.png");
            this.imageList1.Images.SetKeyName(12, "fail.png");
            this.imageList1.Images.SetKeyName(13, "Employee.png");
            this.imageList1.Images.SetKeyName(14, "Department.png");
            this.imageList1.Images.SetKeyName(15, "EmpHis.png");
            // 
            // menuStrip1
            // 
            this.menuStrip1.Enabled = false;
            this.menuStrip1.Location = new System.Drawing.Point(136, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(776, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // Main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1257, 578);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.MaintoolStrip);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "AltasMES";
            this.Load += new System.EventHandler(this.Main_Load);
            this.MdiChildActivate += new System.EventHandler(this.Main_MdiChildActivate);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.MaintoolStrip.ResumeLayout(false);
            this.MaintoolStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip MaintoolStrip;
        private System.Windows.Forms.ToolStripButton StandardStrip;
        private System.Windows.Forms.ToolStripButton SalesStrip;
        private System.Windows.Forms.ToolStripButton ProductionStrip;
        private System.Windows.Forms.ToolStripButton LogOutStrip;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripButton SystemStripButton1;
    }
}

