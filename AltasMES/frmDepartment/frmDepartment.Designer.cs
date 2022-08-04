
namespace AltasMES
{
    partial class frmDepartment
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDepartment));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvdept = new System.Windows.Forms.DataGridView();
            this.DeptID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeptName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeptN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModifyDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModifyUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DBType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSerach = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.BasePanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.Basepanel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Basepanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvdept)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox3);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblSearch);
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Controls.Add(this.txtSerach);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.SetChildIndex(this.btnModify, 0);
            this.groupBox1.Controls.SetChildIndex(this.lblTitle, 0);
            this.groupBox1.Controls.SetChildIndex(this.btnDelete, 0);
            this.groupBox1.Controls.SetChildIndex(this.btnExecl, 0);
            this.groupBox1.Controls.SetChildIndex(this.btnAdd, 0);
            this.groupBox1.Controls.SetChildIndex(this.btnSave, 0);
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvdept);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1034, 491);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // dgvdept
            // 
            this.dgvdept.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvdept.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvdept.ColumnHeadersHeight = 30;
            this.dgvdept.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DeptID,
            this.DeptName,
            this.DeptN,
            this.CreateDate,
            this.CreateUser,
            this.ModifyDate,
            this.ModifyUser,
            this.DBType});
            this.dgvdept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvdept.Location = new System.Drawing.Point(3, 25);
            this.dgvdept.Name = "dgvdept";
            this.dgvdept.RowTemplate.Height = 23;
            this.dgvdept.Size = new System.Drawing.Size(1028, 463);
            this.dgvdept.TabIndex = 0;
            this.dgvdept.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvdept_ColumnHeaderMouseClick);
            this.dgvdept.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvdept_EditingControlShowing);
            // 
            // DeptID
            // 
            this.DeptID.DataPropertyName = "DeptID";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DeptID.DefaultCellStyle = dataGridViewCellStyle2;
            this.DeptID.HeaderText = "부서ID";
            this.DeptID.Name = "DeptID";
            this.DeptID.ReadOnly = true;
            this.DeptID.Visible = false;
            // 
            // DeptName
            // 
            this.DeptName.DataPropertyName = "DeptName";
            this.DeptName.HeaderText = "부서명";
            this.DeptName.Name = "DeptName";
            this.DeptName.ReadOnly = true;
            this.DeptName.Width = 130;
            // 
            // DeptN
            // 
            this.DeptN.DataPropertyName = "DeptN";
            this.DeptN.HeaderText = "부서영문명";
            this.DeptN.Name = "DeptN";
            this.DeptN.Width = 150;
            // 
            // CreateDate
            // 
            this.CreateDate.DataPropertyName = "CreateDate";
            this.CreateDate.HeaderText = "생성날짜";
            this.CreateDate.Name = "CreateDate";
            this.CreateDate.ReadOnly = true;
            this.CreateDate.Width = 180;
            // 
            // CreateUser
            // 
            this.CreateUser.DataPropertyName = "CreateUser";
            this.CreateUser.HeaderText = "생성사용자";
            this.CreateUser.Name = "CreateUser";
            this.CreateUser.ReadOnly = true;
            this.CreateUser.Width = 130;
            // 
            // ModifyDate
            // 
            this.ModifyDate.DataPropertyName = "ModifyDate";
            this.ModifyDate.HeaderText = "수정날짜";
            this.ModifyDate.Name = "ModifyDate";
            this.ModifyDate.ReadOnly = true;
            this.ModifyDate.Width = 180;
            // 
            // ModifyUser
            // 
            this.ModifyUser.DataPropertyName = "ModifyUser";
            this.ModifyUser.HeaderText = "수정사용자";
            this.ModifyUser.Name = "ModifyUser";
            this.ModifyUser.ReadOnly = true;
            this.ModifyUser.Width = 130;
            // 
            // DBType
            // 
            this.DBType.DataPropertyName = "DBType";
            this.DBType.HeaderText = "DBType";
            this.DBType.Name = "DBType";
            this.DBType.Visible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.ImageIndex = 3;
            this.btnSearch.ImageList = this.imageList1;
            this.btnSearch.Location = new System.Drawing.Point(299, 40);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnSearch.Size = new System.Drawing.Size(37, 29);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSerach
            // 
            this.txtSerach.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtSerach.Location = new System.Drawing.Point(76, 40);
            this.txtSerach.Name = "txtSerach";
            this.txtSerach.Size = new System.Drawing.Size(215, 29);
            this.txtSerach.TabIndex = 13;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSearch.Location = new System.Drawing.Point(12, 43);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(58, 21);
            this.lblSearch.TabIndex = 15;
            this.lblSearch.Text = "부서명";
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "excel.png");
            this.imageList2.Images.SetKeyName(1, "plus.png");
            this.imageList2.Images.SetKeyName(2, "trash.png");
            this.imageList2.Images.SetKeyName(3, "pencil.png");
            this.imageList2.Images.SetKeyName(4, "search.png");
            this.imageList2.Images.SetKeyName(5, "arrow-left.png");
            this.imageList2.Images.SetKeyName(6, "check.png");
            this.imageList2.Images.SetKeyName(7, "close.png");
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.ImageIndex = 6;
            this.btnSave.ImageList = this.imageList2;
            this.btnSave.Location = new System.Drawing.Point(618, 20);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnSave.Size = new System.Drawing.Size(78, 37);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "저장";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmDepartment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 682);
            this.Name = "frmDepartment";
            this.Text = "부서";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDepartment_FormClosing);
            this.Load += new System.EventHandler(this.frmDepartment_Load);
            this.Shown += new System.EventHandler(this.frmDepartment_Shown);
            this.BasePanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.Basepanel4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.Basepanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvdept)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvdept;
        protected System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSerach;
        private System.Windows.Forms.Label lblSearch;
        protected System.Windows.Forms.ImageList imageList2;
        protected System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeptID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeptName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeptN;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModifyDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModifyUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn DBType;
    }
}