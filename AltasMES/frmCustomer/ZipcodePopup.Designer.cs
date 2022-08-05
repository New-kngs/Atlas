
namespace AltasMES
{
    partial class ZipcodePopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZipcodePopup));
            this.txtJibunAddr2 = new System.Windows.Forms.TextBox();
            this.txtJibunZipCode = new System.Windows.Forms.TextBox();
            this.txtJibunAddr1 = new System.Windows.Forms.TextBox();
            this.txtRoadAddr2 = new System.Windows.Forms.TextBox();
            this.txtRoadZipcode = new System.Windows.Forms.TextBox();
            this.txtRoadAddr1 = new System.Windows.Forms.TextBox();
            this.btnJibun = new System.Windows.Forms.Button();
            this.btnRoad = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvZip = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSerach = new System.Windows.Forms.Button();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvZip)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtJibunAddr2
            // 
            this.txtJibunAddr2.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.txtJibunAddr2.Location = new System.Drawing.Point(252, 456);
            this.txtJibunAddr2.Name = "txtJibunAddr2";
            this.txtJibunAddr2.Size = new System.Drawing.Size(307, 27);
            this.txtJibunAddr2.TabIndex = 62;
            // 
            // txtJibunZipCode
            // 
            this.txtJibunZipCode.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.txtJibunZipCode.Location = new System.Drawing.Point(163, 456);
            this.txtJibunZipCode.Name = "txtJibunZipCode";
            this.txtJibunZipCode.Size = new System.Drawing.Size(83, 27);
            this.txtJibunZipCode.TabIndex = 61;
            // 
            // txtJibunAddr1
            // 
            this.txtJibunAddr1.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.txtJibunAddr1.Location = new System.Drawing.Point(163, 422);
            this.txtJibunAddr1.Name = "txtJibunAddr1";
            this.txtJibunAddr1.Size = new System.Drawing.Size(396, 27);
            this.txtJibunAddr1.TabIndex = 60;
            // 
            // txtRoadAddr2
            // 
            this.txtRoadAddr2.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.txtRoadAddr2.Location = new System.Drawing.Point(252, 386);
            this.txtRoadAddr2.Name = "txtRoadAddr2";
            this.txtRoadAddr2.Size = new System.Drawing.Size(307, 27);
            this.txtRoadAddr2.TabIndex = 59;
            // 
            // txtRoadZipcode
            // 
            this.txtRoadZipcode.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.txtRoadZipcode.Location = new System.Drawing.Point(163, 386);
            this.txtRoadZipcode.Name = "txtRoadZipcode";
            this.txtRoadZipcode.Size = new System.Drawing.Size(83, 27);
            this.txtRoadZipcode.TabIndex = 58;
            // 
            // txtRoadAddr1
            // 
            this.txtRoadAddr1.Font = new System.Drawing.Font("맑은 고딕", 11F);
            this.txtRoadAddr1.Location = new System.Drawing.Point(163, 355);
            this.txtRoadAddr1.Name = "txtRoadAddr1";
            this.txtRoadAddr1.Size = new System.Drawing.Size(396, 27);
            this.txtRoadAddr1.TabIndex = 57;
            // 
            // btnJibun
            // 
            this.btnJibun.BackColor = System.Drawing.Color.White;
            this.btnJibun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJibun.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.btnJibun.ForeColor = System.Drawing.Color.Black;
            this.btnJibun.Location = new System.Drawing.Point(31, 421);
            this.btnJibun.Name = "btnJibun";
            this.btnJibun.Size = new System.Drawing.Size(126, 62);
            this.btnJibun.TabIndex = 54;
            this.btnJibun.Text = "지번주소 확인";
            this.btnJibun.UseVisualStyleBackColor = false;
            this.btnJibun.Click += new System.EventHandler(this.btnJibun_Click);
            // 
            // btnRoad
            // 
            this.btnRoad.BackColor = System.Drawing.Color.White;
            this.btnRoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRoad.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.btnRoad.ForeColor = System.Drawing.Color.Black;
            this.btnRoad.Location = new System.Drawing.Point(31, 355);
            this.btnRoad.Name = "btnRoad";
            this.btnRoad.Size = new System.Drawing.Size(126, 60);
            this.btnRoad.TabIndex = 53;
            this.btnRoad.Text = "도로명 주소확인";
            this.btnRoad.UseVisualStyleBackColor = false;
            this.btnRoad.Click += new System.EventHandler(this.btnRoad_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.label1.Location = new System.Drawing.Point(29, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 21);
            this.label1.TabIndex = 56;
            this.label1.Text = "도로명(지번) 주소검색";
            // 
            // dgvZip
            // 
            this.dgvZip.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvZip.Location = new System.Drawing.Point(31, 62);
            this.dgvZip.Name = "dgvZip";
            this.dgvZip.RowHeadersWidth = 51;
            this.dgvZip.RowTemplate.Height = 23;
            this.dgvZip.Size = new System.Drawing.Size(525, 287);
            this.dgvZip.TabIndex = 55;
            this.dgvZip.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvZip_CellClick);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("맑은 고딕", 12F);
            this.txtSearch.Location = new System.Drawing.Point(203, 27);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(311, 29);
            this.txtSearch.TabIndex = 51;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnSerach);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(563, 488);
            this.panel1.TabIndex = 119;
            // 
            // btnSerach
            // 
            this.btnSerach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSerach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSerach.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSerach.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSerach.ImageIndex = 4;
            this.btnSerach.ImageList = this.imageList2;
            this.btnSerach.Location = new System.Drawing.Point(509, 14);
            this.btnSerach.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSerach.Name = "btnSerach";
            this.btnSerach.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnSerach.Size = new System.Drawing.Size(34, 30);
            this.btnSerach.TabIndex = 120;
            this.btnSerach.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSerach.UseVisualStyleBackColor = true;
            this.btnSerach.Click += new System.EventHandler(this.btnSerach_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "excel.png");
            this.imageList2.Images.SetKeyName(1, "plus.png");
            this.imageList2.Images.SetKeyName(2, "trash.png");
            this.imageList2.Images.SetKeyName(3, "pencil.png");
            this.imageList2.Images.SetKeyName(4, "search.png");
            this.imageList2.Images.SetKeyName(5, "arrow-left.png");
            this.imageList2.Images.SetKeyName(6, "check.png");
            this.imageList2.Images.SetKeyName(7, "close.png");
            // 
            // ZipcodePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(591, 517);
            this.Controls.Add(this.txtJibunAddr2);
            this.Controls.Add(this.txtJibunZipCode);
            this.Controls.Add(this.txtJibunAddr1);
            this.Controls.Add(this.txtRoadAddr2);
            this.Controls.Add(this.txtRoadZipcode);
            this.Controls.Add(this.txtRoadAddr1);
            this.Controls.Add(this.btnJibun);
            this.Controls.Add(this.btnRoad);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvZip);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ZipcodePopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "주소 검색";
            this.Load += new System.EventHandler(this.ZipcodePopup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvZip)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtJibunAddr2;
        private System.Windows.Forms.TextBox txtJibunZipCode;
        private System.Windows.Forms.TextBox txtJibunAddr1;
        private System.Windows.Forms.TextBox txtRoadAddr2;
        private System.Windows.Forms.TextBox txtRoadZipcode;
        private System.Windows.Forms.TextBox txtRoadAddr1;
        private System.Windows.Forms.Button btnJibun;
        private System.Windows.Forms.Button btnRoad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvZip;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel panel1;
        protected System.Windows.Forms.Button btnSerach;
        protected System.Windows.Forms.ImageList imageList2;
    }
}