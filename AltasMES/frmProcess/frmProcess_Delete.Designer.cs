
namespace AltasMES
{
    partial class frmProcess_Delete
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProcess_Delete));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtProcess = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDeleteChk = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Size = new System.Drawing.Size(460, 84);
            this.lblTitle.Text = "공정삭제";
            // 
            // panel1
            // 
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Size = new System.Drawing.Size(460, 84);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.Images.SetKeyName(0, "excel.png");
            this.imageList1.Images.SetKeyName(1, "plus.png");
            this.imageList1.Images.SetKeyName(2, "trash.png");
            this.imageList1.Images.SetKeyName(3, "pencil.png");
            this.imageList1.Images.SetKeyName(4, "search.png");
            this.imageList1.Images.SetKeyName(5, "arrow-left.png");
            this.imageList1.Images.SetKeyName(6, "check.png");
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.txtDeleteChk);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtProcess);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(0, 84);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Size = new System.Drawing.Size(460, 285);
            this.panel2.Controls.SetChildIndex(this.panel3, 0);
            this.panel2.Controls.SetChildIndex(this.label1, 0);
            this.panel2.Controls.SetChildIndex(this.txtProcess, 0);
            this.panel2.Controls.SetChildIndex(this.label2, 0);
            this.panel2.Controls.SetChildIndex(this.txtDeleteChk, 0);
            this.panel2.Controls.SetChildIndex(this.btnAdd, 0);
            this.panel2.Controls.SetChildIndex(this.btnCancel, 0);
            this.panel2.Controls.SetChildIndex(this.label3, 0);
            // 
            // panel3
            // 
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.panel3.Size = new System.Drawing.Size(180, 0);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.ImageIndex = 5;
            this.btnCancel.ImageList = this.imageList1;
            this.btnCancel.Location = new System.Drawing.Point(254, 205);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnCancel.Size = new System.Drawing.Size(89, 45);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "취소";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.ImageIndex = 2;
            this.btnAdd.ImageList = this.imageList1;
            this.btnAdd.Location = new System.Drawing.Point(144, 205);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.btnAdd.Size = new System.Drawing.Size(89, 45);
            this.btnAdd.TabIndex = 21;
            this.btnAdd.Text = "삭제";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtProcess
            // 
            this.txtProcess.Location = new System.Drawing.Point(136, 47);
            this.txtProcess.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtProcess.Name = "txtProcess";
            this.txtProcess.ReadOnly = true;
            this.txtProcess.Size = new System.Drawing.Size(259, 34);
            this.txtProcess.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 52);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 28);
            this.label1.TabIndex = 17;
            this.label1.Text = "공정명";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 125);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 28);
            this.label2.TabIndex = 17;
            this.label2.Text = "삭제 확인";
            // 
            // txtDeleteChk
            // 
            this.txtDeleteChk.Location = new System.Drawing.Point(133, 120);
            this.txtDeleteChk.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtDeleteChk.Name = "txtDeleteChk";
            this.txtDeleteChk.Size = new System.Drawing.Size(259, 34);
            this.txtDeleteChk.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(128, 161);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(279, 20);
            this.label3.TabIndex = 22;
            this.label3.Text = "삭제를 하시려면 공정명을 입력해주세요";
            // 
            // frmProcess_Delete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 369);
            this.Margin = new System.Windows.Forms.Padding(6, 13, 6, 13);
            this.Name = "frmProcess_Delete";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "공정 삭제";
            this.Load += new System.EventHandler(this.frmProcess_Delete_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        protected System.Windows.Forms.Button btnCancel;
        protected System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtDeleteChk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProcess;
        private System.Windows.Forms.Label label1;
    }
}