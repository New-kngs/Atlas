
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
            this.lblState = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.txtTotQty = new System.Windows.Forms.TextBox();
            this.txtReadPLC = new System.Windows.Forms.TextBox();
            this.timer_Connec = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblState);
            this.panel1.Controls.Add(this.listBox1);
            this.panel1.Controls.Add(this.txtTotQty);
            this.panel1.Controls.Add(this.txtReadPLC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(934, 504);
            this.panel1.TabIndex = 0;
            // 
            // lblState
            // 
            this.lblState.Location = new System.Drawing.Point(712, 185);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(117, 116);
            this.lblState.TabIndex = 2;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(183, 143);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(405, 304);
            this.listBox1.TabIndex = 1;
            // 
            // txtTotQty
            // 
            this.txtTotQty.Location = new System.Drawing.Point(401, 66);
            this.txtTotQty.Name = "txtTotQty";
            this.txtTotQty.Size = new System.Drawing.Size(100, 21);
            this.txtTotQty.TabIndex = 0;
            this.txtTotQty.TextChanged += new System.EventHandler(this.txtTotQty_TextChanged);
            // 
            // txtReadPLC
            // 
            this.txtReadPLC.Location = new System.Drawing.Point(183, 66);
            this.txtReadPLC.Name = "txtReadPLC";
            this.txtReadPLC.Size = new System.Drawing.Size(100, 21);
            this.txtReadPLC.TabIndex = 0;
            // 
            // frmPerformance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(934, 504);
            this.Controls.Add(this.panel1);
            this.Name = "frmPerformance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "모니터링";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
    }
}