
namespace AltasMES
{
    partial class frmEquitment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEquitment));
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Size = new System.Drawing.Size(932, 72);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(594, 20);
            // 
            // btnExecl
            // 
            this.btnExecl.Location = new System.Drawing.Point(846, 20);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(762, 20);
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(71, 37);
            this.lblTitle.Text = "설비";
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(678, 20);
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
            // groupBox2
            // 
            this.groupBox2.Size = new System.Drawing.Size(932, 62);
            this.groupBox2.Text = "검색조건";
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(932, 384);
            // 
            // frmEquitment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 518);
            this.Name = "frmEquitment";
            this.Text = "설비";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}