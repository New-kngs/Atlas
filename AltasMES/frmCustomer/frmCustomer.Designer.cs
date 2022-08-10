
namespace AltasMES
{
    partial class frmCustomer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCustomer));
            this.label1 = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSerach = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvCus = new System.Windows.Forms.DataGridView();
            this.BasePanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.Basepanel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Basepanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCus)).BeginInit();
            this.SuspendLayout();
            // 
            // BasePanel1
            // 
            this.BasePanel1.Size = new System.Drawing.Size(1182, 852);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Size = new System.Drawing.Size(1182, 613);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cboCategory);
            this.groupBox2.Controls.Add(this.lblSearch);
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Controls.Add(this.txtSerach);
            this.groupBox2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            // 
            // btnAdd
            // 
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.Images.SetKeyName(0, "Create.png");
            this.imageList1.Images.SetKeyName(1, "Modify.png");
            this.imageList1.Images.SetKeyName(2, "Delete.png");
            this.imageList1.Images.SetKeyName(3, "Serach.png");
            this.imageList1.Images.SetKeyName(4, "Execl.png");
            // 
            // btnDelete
            // 
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnModify
            // 
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(19, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 28);
            this.label1.TabIndex = 19;
            this.label1.Text = "거래처 구분";
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Items.AddRange(new object[] {
            "전체",
            "입고",
            "출고"});
            this.cboCategory.Location = new System.Drawing.Point(135, 54);
            this.cboCategory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(125, 36);
            this.cboCategory.TabIndex = 18;
            this.cboCategory.SelectedIndexChanged += new System.EventHandler(this.cboCategory_SelectedIndexChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.ImageIndex = 3;
            this.btnSearch.ImageList = this.imageList1;
            this.btnSearch.Location = new System.Drawing.Point(625, 54);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnSearch.Size = new System.Drawing.Size(40, 36);
            this.btnSearch.TabIndex = 17;
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSerach
            // 
            this.txtSerach.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtSerach.Location = new System.Drawing.Point(368, 54);
            this.txtSerach.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSerach.Name = "txtSerach";
            this.txtSerach.Size = new System.Drawing.Size(245, 34);
            this.txtSerach.TabIndex = 16;
            this.txtSerach.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSerach_KeyPress);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSearch.Location = new System.Drawing.Point(274, 58);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(92, 28);
            this.lblSearch.TabIndex = 15;
            this.lblSearch.Text = "거래처명";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvCus);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(1182, 613);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // dgvCus
            // 
            this.dgvCus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCus.Location = new System.Drawing.Point(3, 31);
            this.dgvCus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvCus.Name = "dgvCus";
            this.dgvCus.RowHeadersWidth = 51;
            this.dgvCus.RowTemplate.Height = 23;
            this.dgvCus.Size = new System.Drawing.Size(1176, 578);
            this.dgvCus.TabIndex = 0;
            this.dgvCus.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvCus_ColumnHeaderMouseClick);
            // 
            // frmCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 852);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmCustomer";
            this.Text = "거래처";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCustomer_FormClosing);
            this.Load += new System.EventHandler(this.frmCustomer_Load);
            this.Shown += new System.EventHandler(this.frmCustomer_Shown);
            this.BasePanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.Basepanel4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.Basepanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label lblSearch;
        protected System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSerach;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvCus;
    }
}