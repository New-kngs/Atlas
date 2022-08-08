
namespace AtlasPOP
{
    partial class frmLaping
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
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.btnCreateLOT = new System.Windows.Forms.Button();
            this.btnLaping = new System.Windows.Forms.Button();
            this.btnPutIN = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvList
            // 
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Location = new System.Drawing.Point(14, 15);
            this.dgvList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowHeadersWidth = 51;
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.Size = new System.Drawing.Size(870, 515);
            this.dgvList.TabIndex = 0;
            // 
            // btnCreateLOT
            // 
            this.btnCreateLOT.BackColor = System.Drawing.Color.White;
            this.btnCreateLOT.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCreateLOT.ForeColor = System.Drawing.Color.Black;
            this.btnCreateLOT.Location = new System.Drawing.Point(125, 579);
            this.btnCreateLOT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCreateLOT.Name = "btnCreateLOT";
            this.btnCreateLOT.Size = new System.Drawing.Size(137, 76);
            this.btnCreateLOT.TabIndex = 1;
            this.btnCreateLOT.Text = "LOT생성";
            this.btnCreateLOT.UseVisualStyleBackColor = false;
            this.btnCreateLOT.Click += new System.EventHandler(this.btnCreateLOT_Click);
            // 
            // btnLaping
            // 
            this.btnLaping.BackColor = System.Drawing.Color.White;
            this.btnLaping.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLaping.ForeColor = System.Drawing.Color.Black;
            this.btnLaping.Location = new System.Drawing.Point(393, 579);
            this.btnLaping.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLaping.Name = "btnLaping";
            this.btnLaping.Size = new System.Drawing.Size(137, 76);
            this.btnLaping.TabIndex = 1;
            this.btnLaping.Text = "포장";
            this.btnLaping.UseVisualStyleBackColor = false;
            this.btnLaping.Click += new System.EventHandler(this.btnLaping_Click);
            // 
            // btnPutIN
            // 
            this.btnPutIN.BackColor = System.Drawing.Color.White;
            this.btnPutIN.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPutIN.ForeColor = System.Drawing.Color.Black;
            this.btnPutIN.Location = new System.Drawing.Point(661, 579);
            this.btnPutIN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnPutIN.Name = "btnPutIN";
            this.btnPutIN.Size = new System.Drawing.Size(137, 76);
            this.btnPutIN.TabIndex = 1;
            this.btnPutIN.Text = "창고입고";
            this.btnPutIN.UseVisualStyleBackColor = false;
            this.btnPutIN.Click += new System.EventHandler(this.btnPutIN_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AtlasPOP.Properties.Resources.fast_forward_double_right_arrows_symbol;
            this.pictureBox1.Location = new System.Drawing.Point(305, 598);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(45, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AtlasPOP.Properties.Resources.fast_forward_double_right_arrows_symbol;
            this.pictureBox2.Location = new System.Drawing.Point(573, 598);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(45, 40);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // frmLaping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(896, 689);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnPutIN);
            this.Controls.Add(this.btnLaping);
            this.Controls.Add(this.btnCreateLOT);
            this.Controls.Add(this.dgvList);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmLaping";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "포장";
            this.Load += new System.EventHandler(this.frmLaping_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Button btnCreateLOT;
        private System.Windows.Forms.Button btnLaping;
        private System.Windows.Forms.Button btnPutIN;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}