
namespace AltasMES
{
    partial class frmShip
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShip));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvShip = new System.Windows.Forms.DataGridView();
            this.BasePanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.Basepanel4.SuspendLayout();
            this.Basepanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShip)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox3);
            // 
            // groupBox2
            // 
            this.groupBox2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(866, 20);
            this.btnAdd.Text = "상세";
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
            this.btnDelete.Location = new System.Drawing.Point(663, 20);
            this.btnDelete.Visible = false;
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(751, 20);
            this.btnModify.Size = new System.Drawing.Size(109, 37);
            this.btnModify.Text = "출하처리";
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvShip);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1034, 491);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "출하대기목록";
            // 
            // dgvShip
            // 
            this.dgvShip.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvShip.Location = new System.Drawing.Point(3, 25);
            this.dgvShip.Name = "dgvShip";
            this.dgvShip.RowTemplate.Height = 23;
            this.dgvShip.Size = new System.Drawing.Size(1028, 463);
            this.dgvShip.TabIndex = 1;
            // 
            // frmShip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 682);
            this.Name = "frmShip";
            this.Text = "출하";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmShip_FormClosing);
            this.Load += new System.EventHandler(this.frmShip_Load);
            this.Shown += new System.EventHandler(this.frmShip_Shown);
            this.BasePanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.Basepanel4.ResumeLayout(false);
            this.Basepanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShip)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvShip;
    }
}