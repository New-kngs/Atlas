
namespace AltasMES
{
    partial class frmEquipment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEquipment));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvEquip = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtEquip = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BasePanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.Basepanel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.Basepanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquip)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox3);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Controls.Add(this.txtEquip);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Text = "검색조건";
            // 
            // btnAdd
            // 
            this.btnAdd.TabIndex = 0;
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
            this.btnExecl.TabIndex = 3;
            // 
            // btnDelete
            // 
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(98, 51);
            this.lblTitle.Text = "설비";
            // 
            // btnModify
            // 
            this.btnModify.TabIndex = 1;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvEquip);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1034, 491);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "설비목록";
            // 
            // dgvEquip
            // 
            this.dgvEquip.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEquip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEquip.Location = new System.Drawing.Point(3, 25);
            this.dgvEquip.Name = "dgvEquip";
            this.dgvEquip.RowTemplate.Height = 23;
            this.dgvEquip.Size = new System.Drawing.Size(1028, 463);
            this.dgvEquip.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.ImageIndex = 3;
            this.btnSearch.ImageList = this.imageList1;
            this.btnSearch.Location = new System.Drawing.Point(304, 41);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnSearch.Size = new System.Drawing.Size(78, 29);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "조회";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtEquip
            // 
            this.txtEquip.Location = new System.Drawing.Point(83, 42);
            this.txtEquip.Name = "txtEquip";
            this.txtEquip.Size = new System.Drawing.Size(215, 29);
            this.txtEquip.TabIndex = 8;
            this.txtEquip.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEquip_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "설비명";
            // 
            // frmEquipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 682);
            this.Name = "frmEquipment";
            this.Text = "설비";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEquipment_FormClosing);
            this.Load += new System.EventHandler(this.frmEquipment_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmEquipment_KeyPress);
            this.BasePanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.Basepanel4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.Basepanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquip)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvEquip;
        protected System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtEquip;
        private System.Windows.Forms.Label label1;
    }
}