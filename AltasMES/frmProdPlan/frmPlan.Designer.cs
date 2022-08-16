namespace AltasMES
{
    partial class frmPlan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPlan));
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.dgvMaterial = new System.Windows.Forms.DataGridView();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dgvSemi = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.BasePanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.Basepanel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Basepanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterial)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSemi)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // BasePanel1
            // 
            this.BasePanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel2);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            // 
            // panel1
            // 
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            // 
            // Basepanel4
            // 
            this.Basepanel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Controls.Add(this.dtpTo);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dtpFrom);
            this.groupBox2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox2.Text = "검색조건";
            // 
            // Basepanel3
            // 
            this.Basepanel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            // 
            // Basepanel2
            // 
            this.Basepanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            // 
            // groupBox1
            // 
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(720, 20);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAdd.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnAdd.Size = new System.Drawing.Size(148, 37);
            this.btnAdd.Text = "작업지시(주문)";
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
            // btnExecl
            // 
            this.btnExecl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecl.Location = new System.Drawing.Point(461, 20);
            this.btnExecl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExecl.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnExecl.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.ImageIndex = 1;
            this.btnDelete.Location = new System.Drawing.Point(636, 18);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelete.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnDelete.Text = "출하";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(174, 51);
            this.lblTitle.Text = "생산계획";
            // 
            // btnModify
            // 
            this.btnModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModify.ImageIndex = 0;
            this.btnModify.Location = new System.Drawing.Point(874, 20);
            this.btnModify.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnModify.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnModify.Size = new System.Drawing.Size(148, 37);
            this.btnModify.Text = "작업지시(추가)";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(19, 39);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(123, 29);
            this.dtpFrom.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "~";
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(176, 39);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(123, 29);
            this.dtpTo.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.ImageIndex = 3;
            this.btnSearch.ImageList = this.imageList1;
            this.btnSearch.Location = new System.Drawing.Point(305, 39);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnSearch.Size = new System.Drawing.Size(29, 29);
            this.btnSearch.TabIndex = 15;
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tableLayoutPanel1);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox5.Location = new System.Drawing.Point(520, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(511, 485);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "구성품현황";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox7, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox6, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(505, 457);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.dgvMaterial);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(3, 140);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(499, 314);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "자재";
            // 
            // dgvMaterial
            // 
            this.dgvMaterial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaterial.Location = new System.Drawing.Point(3, 25);
            this.dgvMaterial.Name = "dgvMaterial";
            this.dgvMaterial.RowTemplate.Height = 23;
            this.dgvMaterial.Size = new System.Drawing.Size(493, 286);
            this.dgvMaterial.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dgvSemi);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(3, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(499, 131);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "반제품";
            // 
            // dgvSemi
            // 
            this.dgvSemi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSemi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSemi.Location = new System.Drawing.Point(3, 25);
            this.dgvSemi.Name = "dgvSemi";
            this.dgvSemi.RowTemplate.Height = 23;
            this.dgvSemi.Size = new System.Drawing.Size(493, 103);
            this.dgvSemi.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox5, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 491F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1034, 491);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.groupBox3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox4, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(511, 485);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvList);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(505, 236);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "주문서목록";
            // 
            // dgvList
            // 
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.Location = new System.Drawing.Point(3, 25);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.Size = new System.Drawing.Size(499, 208);
            this.dgvList.TabIndex = 0;
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgvDetail);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox4.Location = new System.Drawing.Point(3, 245);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(505, 237);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "주문서상세";
            // 
            // dgvDetail
            // 
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetail.Location = new System.Drawing.Point(3, 25);
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.RowTemplate.Height = 23;
            this.dgvDetail.Size = new System.Drawing.Size(499, 209);
            this.dgvDetail.TabIndex = 0;
            this.dgvDetail.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetail_CellClick);
            // 
            // frmPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 682);
            this.Name = "frmPlan";
            this.ShowIcon = false;
            this.Text = "생산계획";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPlan_FormClosing);
            this.Load += new System.EventHandler(this.frmPlan_Load);
            this.Shown += new System.EventHandler(this.frmPlan_Shown);
            this.BasePanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.Basepanel4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.Basepanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterial)).EndInit();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSemi)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox6;
        protected System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvMaterial;
        private System.Windows.Forms.DataGridView dgvSemi;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgvDetail;
    }
}