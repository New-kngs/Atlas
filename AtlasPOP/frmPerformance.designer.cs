﻿
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtProc = new System.Windows.Forms.TextBox();
            this.txtOrder = new System.Windows.Forms.TextBox();
            this.txtItem = new System.Windows.Forms.TextBox();
            this.txtOp = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pgState = new System.Windows.Forms.ProgressBar();
            this.txtFail = new System.Windows.Forms.TextBox();
            this.lblState = new System.Windows.Forms.Label();
            this.txtTotQty = new System.Windows.Forms.TextBox();
            this.txtReadPLC = new System.Windows.Forms.TextBox();
            this.timer_Connec = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.txtFail);
            this.panel1.Controls.Add(this.lblState);
            this.panel1.Controls.Add(this.txtTotQty);
            this.panel1.Controls.Add(this.txtReadPLC);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1527, 822);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Silver;
            this.panel3.Controls.Add(this.txtQty);
            this.panel3.Controls.Add(this.txtProc);
            this.panel3.Controls.Add(this.txtOrder);
            this.panel3.Controls.Add(this.txtItem);
            this.panel3.Controls.Add(this.txtOp);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Location = new System.Drawing.Point(16, 13);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(393, 795);
            this.panel3.TabIndex = 5;
            // 
            // txtQty
            // 
            this.txtQty.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtQty.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.txtQty.Location = new System.Drawing.Point(139, 205);
            this.txtQty.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(249, 30);
            this.txtQty.TabIndex = 35;
            // 
            // txtProc
            // 
            this.txtProc.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtProc.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.txtProc.Location = new System.Drawing.Point(139, 159);
            this.txtProc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtProc.Name = "txtProc";
            this.txtProc.Size = new System.Drawing.Size(249, 30);
            this.txtProc.TabIndex = 35;
            // 
            // txtOrder
            // 
            this.txtOrder.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtOrder.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.txtOrder.Location = new System.Drawing.Point(139, 115);
            this.txtOrder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOrder.Name = "txtOrder";
            this.txtOrder.Size = new System.Drawing.Size(249, 30);
            this.txtOrder.TabIndex = 35;
            // 
            // txtItem
            // 
            this.txtItem.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtItem.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.txtItem.Location = new System.Drawing.Point(139, 70);
            this.txtItem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(249, 30);
            this.txtItem.TabIndex = 35;
            // 
            // txtOp
            // 
            this.txtOp.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtOp.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold);
            this.txtOp.Location = new System.Drawing.Point(139, 25);
            this.txtOp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOp.Name = "txtOp";
            this.txtOp.Size = new System.Drawing.Size(249, 30);
            this.txtOp.TabIndex = 35;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold);
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(8, 148);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 45);
            this.label9.TabIndex = 30;
            this.label9.Text = "공정";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold);
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(8, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 45);
            this.label7.TabIndex = 28;
            this.label7.Text = "주문ID";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(8, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 45);
            this.label3.TabIndex = 26;
            this.label3.Text = "지시수량";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(8, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 45);
            this.label1.TabIndex = 26;
            this.label1.Text = "작업지시ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold);
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(8, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 45);
            this.label5.TabIndex = 26;
            this.label5.Text = "제품명";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.pgState);
            this.panel2.Location = new System.Drawing.Point(416, 13);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1097, 795);
            this.panel2.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 642);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            this.label2.Visible = false;
            // 
            // pgState
            // 
            this.pgState.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pgState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pgState.Location = new System.Drawing.Point(0, 708);
            this.pgState.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgState.Name = "pgState";
            this.pgState.Size = new System.Drawing.Size(1095, 85);
            this.pgState.TabIndex = 0;
            // 
            // txtFail
            // 
            this.txtFail.Location = new System.Drawing.Point(259, 2);
            this.txtFail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFail.Name = "txtFail";
            this.txtFail.Size = new System.Drawing.Size(114, 25);
            this.txtFail.TabIndex = 3;
            this.txtFail.Visible = false;
            // 
            // lblState
            // 
            this.lblState.Location = new System.Drawing.Point(14, 205);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(104, 75);
            this.lblState.TabIndex = 2;
            // 
            // txtTotQty
            // 
            this.txtTotQty.Location = new System.Drawing.Point(137, 4);
            this.txtTotQty.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTotQty.Name = "txtTotQty";
            this.txtTotQty.Size = new System.Drawing.Size(114, 25);
            this.txtTotQty.TabIndex = 0;
            this.txtTotQty.Visible = false;
            // 
            // txtReadPLC
            // 
            this.txtReadPLC.Location = new System.Drawing.Point(16, 4);
            this.txtReadPLC.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtReadPLC.Name = "txtReadPLC";
            this.txtReadPLC.Size = new System.Drawing.Size(114, 25);
            this.txtReadPLC.TabIndex = 0;
            this.txtReadPLC.Visible = false;
            // 
            // timer_Connec
            // 
            this.timer_Connec.Tick += new System.EventHandler(this.timer_Connec_Tick);
            // 
            // frmPerformance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1527, 822);
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
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer_Connec;
        private System.Windows.Forms.TextBox txtTotQty;
        private System.Windows.Forms.TextBox txtReadPLC;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.TextBox txtFail;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ProgressBar pgState;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox txtProc;
        private System.Windows.Forms.TextBox txtOrder;
        private System.Windows.Forms.TextBox txtItem;
        private System.Windows.Forms.TextBox txtOp;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}