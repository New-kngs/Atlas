
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnCreateLOT = new System.Windows.Forms.Button();
            this.btnLaping = new System.Windows.Forms.Button();
            this.btnPutIN = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(761, 412);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnCreateLOT
            // 
            this.btnCreateLOT.BackColor = System.Drawing.Color.Black;
            this.btnCreateLOT.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCreateLOT.ForeColor = System.Drawing.Color.White;
            this.btnCreateLOT.Location = new System.Drawing.Point(109, 463);
            this.btnCreateLOT.Name = "btnCreateLOT";
            this.btnCreateLOT.Size = new System.Drawing.Size(120, 61);
            this.btnCreateLOT.TabIndex = 1;
            this.btnCreateLOT.Text = "LOT생성";
            this.btnCreateLOT.UseVisualStyleBackColor = false;
            // 
            // btnLaping
            // 
            this.btnLaping.BackColor = System.Drawing.Color.Black;
            this.btnLaping.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLaping.ForeColor = System.Drawing.Color.White;
            this.btnLaping.Location = new System.Drawing.Point(344, 463);
            this.btnLaping.Name = "btnLaping";
            this.btnLaping.Size = new System.Drawing.Size(120, 61);
            this.btnLaping.TabIndex = 1;
            this.btnLaping.Text = "포장";
            this.btnLaping.UseVisualStyleBackColor = false;
            // 
            // btnPutIN
            // 
            this.btnPutIN.BackColor = System.Drawing.Color.Black;
            this.btnPutIN.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnPutIN.ForeColor = System.Drawing.Color.White;
            this.btnPutIN.Location = new System.Drawing.Point(578, 463);
            this.btnPutIN.Name = "btnPutIN";
            this.btnPutIN.Size = new System.Drawing.Size(120, 61);
            this.btnPutIN.TabIndex = 1;
            this.btnPutIN.Text = "창고입고";
            this.btnPutIN.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AtlasPOP.Properties.Resources.fast_forward_double_right_arrows_symbol;
            this.pictureBox1.Location = new System.Drawing.Point(267, 478);
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
            this.pictureBox2.Location = new System.Drawing.Point(501, 478);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(39, 32);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // frmLaping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 551);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnPutIN);
            this.Controls.Add(this.btnLaping);
            this.Controls.Add(this.btnCreateLOT);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmLaping";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLaping";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnCreateLOT;
        private System.Windows.Forms.Button btnLaping;
        private System.Windows.Forms.Button btnPutIN;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}