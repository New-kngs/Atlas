
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvList
            // 
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Location = new System.Drawing.Point(15, 15);
            this.dgvList.Name = "dgvList";
            this.dgvList.RowHeadersWidth = 51;
            this.dgvList.RowTemplate.Height = 23;
            this.dgvList.Size = new System.Drawing.Size(589, 391);
            this.dgvList.TabIndex = 0;
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            // 
            // btnCreateLOT
            // 
            this.btnCreateLOT.BackColor = System.Drawing.Color.White;
            this.btnCreateLOT.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCreateLOT.ForeColor = System.Drawing.Color.Black;
            this.btnCreateLOT.Location = new System.Drawing.Point(30, 463);
            this.btnCreateLOT.Name = "btnCreateLOT";
            this.btnCreateLOT.Size = new System.Drawing.Size(120, 61);
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
            this.btnLaping.Location = new System.Drawing.Point(265, 463);
            this.btnLaping.Name = "btnLaping";
            this.btnLaping.Size = new System.Drawing.Size(120, 61);
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
            this.btnPutIN.Location = new System.Drawing.Point(499, 463);
            this.btnPutIN.Name = "btnPutIN";
            this.btnPutIN.Size = new System.Drawing.Size(120, 61);
            this.btnPutIN.TabIndex = 1;
            this.btnPutIN.Text = "창고입고";
            this.btnPutIN.UseVisualStyleBackColor = false;
            this.btnPutIN.Click += new System.EventHandler(this.btnPutIN_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AtlasPOP.Properties.Resources.fast_forward_double_right_arrows_symbol;
            this.pictureBox1.Location = new System.Drawing.Point(188, 478);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(39, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::AtlasPOP.Properties.Resources.fast_forward_double_right_arrows_symbol;
            this.pictureBox2.Location = new System.Drawing.Point(422, 478);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(39, 32);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvList);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(616, 418);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "포장 대기 목록";
            // 
            // frmLaping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(640, 544);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnPutIN);
            this.Controls.Add(this.btnLaping);
            this.Controls.Add(this.btnCreateLOT);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLaping";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "포장";
            this.Load += new System.EventHandler(this.frmLaping_Load);
            this.Shown += new System.EventHandler(this.frmLaping_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Button btnCreateLOT;
        private System.Windows.Forms.Button btnLaping;
        private System.Windows.Forms.Button btnPutIN;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}