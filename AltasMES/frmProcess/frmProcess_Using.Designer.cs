
namespace AltasMES
{
    partial class frmProcess_Using
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProcess_Using));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnChk = new System.Windows.Forms.Button();
            this.txtProcess = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsingChk = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(376, 63);
            this.lblTitle.Text = "공정 사용";
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(376, 63);
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
            this.panel2.Controls.Add(this.btnChk);
            this.panel2.Controls.Add(this.txtUsingChk);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtProcess);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Size = new System.Drawing.Size(376, 214);
            this.panel2.Controls.SetChildIndex(this.panel3, 0);
            this.panel2.Controls.SetChildIndex(this.label1, 0);
            this.panel2.Controls.SetChildIndex(this.txtProcess, 0);
            this.panel2.Controls.SetChildIndex(this.label2, 0);
            this.panel2.Controls.SetChildIndex(this.txtUsingChk, 0);
            this.panel2.Controls.SetChildIndex(this.btnChk, 0);
            this.panel2.Controls.SetChildIndex(this.btnCancel, 0);
            this.panel2.Controls.SetChildIndex(this.label3, 0);
            // 
            // panel3
            // 
            this.panel3.Size = new System.Drawing.Size(0, 0);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.ImageIndex = 5;
            this.btnCancel.ImageList = this.imageList1;
            this.btnCancel.Location = new System.Drawing.Point(208, 154);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnCancel.Size = new System.Drawing.Size(73, 34);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "취소";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnChk
            // 
            this.btnChk.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnChk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChk.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnChk.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnChk.ImageIndex = 6;
            this.btnChk.ImageList = this.imageList1;
            this.btnChk.Location = new System.Drawing.Point(118, 154);
            this.btnChk.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnChk.Name = "btnChk";
            this.btnChk.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnChk.Size = new System.Drawing.Size(73, 34);
            this.btnChk.TabIndex = 1;
            this.btnChk.Text = "확인";
            this.btnChk.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChk.UseVisualStyleBackColor = true;
            this.btnChk.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtProcess
            // 
            this.txtProcess.Location = new System.Drawing.Point(111, 35);
            this.txtProcess.Margin = new System.Windows.Forms.Padding(2);
            this.txtProcess.Name = "txtProcess";
            this.txtProcess.ReadOnly = true;
            this.txtProcess.Size = new System.Drawing.Size(213, 29);
            this.txtProcess.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 21);
            this.label1.TabIndex = 17;
            this.label1.Text = "공정명";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 94);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 21);
            this.label2.TabIndex = 17;
            this.label2.Text = "확인";
            // 
            // txtUsingChk
            // 
            this.txtUsingChk.Location = new System.Drawing.Point(109, 90);
            this.txtUsingChk.Margin = new System.Windows.Forms.Padding(2);
            this.txtUsingChk.Name = "txtUsingChk";
            this.txtUsingChk.Size = new System.Drawing.Size(213, 29);
            this.txtUsingChk.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(105, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(223, 15);
            this.label3.TabIndex = 22;
            this.label3.Text = "사용을 하시려면 공정명을 입력해주세요";
            // 
            // frmProcess_Using
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 277);
            this.Margin = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.Name = "frmProcess_Using";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "공정 재사용";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmProcess_Using_FormClosing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmProcess_Using_KeyPress);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        protected System.Windows.Forms.Button btnCancel;
        protected System.Windows.Forms.Button btnChk;
        private System.Windows.Forms.TextBox txtUsingChk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProcess;
        private System.Windows.Forms.Label label1;
    }
}