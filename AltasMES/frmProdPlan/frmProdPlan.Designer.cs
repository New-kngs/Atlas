namespace AltasMES
{
    partial class frmProdPlan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProdPlan));
            this.BasePanel1.SuspendLayout();
            this.Basepanel4.SuspendLayout();
            this.Basepanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BasePanel1
            // 
            this.BasePanel1.Size = new System.Drawing.Size(800, 450);
            // 
            // panel2
            // 
            this.panel2.Size = new System.Drawing.Size(800, 211);
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(800, 14);
            // 
            // Basepanel4
            // 
            this.Basepanel4.Size = new System.Drawing.Size(800, 124);
            // 
            // groupBox2
            // 
            this.groupBox2.Size = new System.Drawing.Size(800, 124);
            this.groupBox2.Text = "검색조건";
            // 
            // Basepanel3
            // 
            this.Basepanel3.Size = new System.Drawing.Size(800, 12);
            // 
            // Basepanel2
            // 
            this.Basepanel2.Size = new System.Drawing.Size(800, 89);
            // 
            // groupBox1
            // 
            this.groupBox1.Size = new System.Drawing.Size(800, 89);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(419, 25);
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
            this.btnExecl.Location = new System.Drawing.Point(704, 25);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(609, 25);
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(215, 62);
            this.lblTitle.Text = "생산계획";
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(514, 25);
            // 
            // frmProdPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "frmProdPlan";
            this.Text = "frmProdPlan";
            this.BasePanel1.ResumeLayout(false);
            this.Basepanel4.ResumeLayout(false);
            this.Basepanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}