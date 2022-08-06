
namespace AtlasPOP
{
    partial class frmPerformance
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtFail = new System.Windows.Forms.TextBox();
            this.lblState = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.txtTotQty = new System.Windows.Forms.TextBox();
            this.txtReadPLC = new System.Windows.Forms.TextBox();
            this.timer_Connec = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.txtFail);
            this.panel1.Controls.Add(this.lblState);
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Controls.Add(this.txtTotQty);
            this.panel1.Controls.Add(this.txtReadPLC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(886, 526);
            this.panel1.TabIndex = 0;
            // 
            // txtFail
            // 
            this.txtFail.Location = new System.Drawing.Point(257, 14);
            this.txtFail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFail.Name = "txtFail";
            this.txtFail.Size = new System.Drawing.Size(114, 25);
            this.txtFail.TabIndex = 3;
            // 
            // lblState
            // 
            this.lblState.Location = new System.Drawing.Point(14, 205);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(104, 75);
            this.lblState.TabIndex = 2;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(392, 13);
            this.listBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(462, 139);
            this.listBox1.TabIndex = 1;
            // 
            // txtTotQty
            // 
            this.txtTotQty.Location = new System.Drawing.Point(135, 15);
            this.txtTotQty.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTotQty.Name = "txtTotQty";
            this.txtTotQty.Size = new System.Drawing.Size(114, 25);
            this.txtTotQty.TabIndex = 0;
            // 
            // txtReadPLC
            // 
            this.txtReadPLC.Location = new System.Drawing.Point(14, 15);
            this.txtReadPLC.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtReadPLC.Name = "txtReadPLC";
            this.txtReadPLC.Size = new System.Drawing.Size(114, 25);
            this.txtReadPLC.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(31, 173);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(823, 276);
            this.panel2.TabIndex = 4;
            // 
            // frmPerformance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(886, 526);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPerformance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "현황";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPerformance_FormClosing);
            this.Load += new System.EventHandler(this.frmPerformance_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer_Connec;
        private System.Windows.Forms.TextBox txtTotQty;
        private System.Windows.Forms.TextBox txtReadPLC;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.TextBox txtFail;
        private System.Windows.Forms.Panel panel2;
    }
}