namespace AltasMES
{
    partial class frmPlan_Ship
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPlan_Ship));
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOrderID = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.txtQty = new System.Windows.Forms.TextBox();
            this.수량 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 63);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(350, 229);
            this.panel2.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.수량);
            this.panel3.Controls.Add(this.txtQty);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.txtProduct);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.txtOrderID);
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnAdd);
            this.panel3.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel3.Location = new System.Drawing.Point(12, 6);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(326, 211);
            this.panel3.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 21);
            this.label2.TabIndex = 25;
            this.label2.Text = "완제품";
            // 
            // txtProduct
            // 
            this.txtProduct.Enabled = false;
            this.txtProduct.Location = new System.Drawing.Point(70, 43);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(240, 29);
            this.txtProduct.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(7, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 21);
            this.label1.TabIndex = 23;
            this.label1.Text = "주문ID";
            // 
            // txtOrderID
            // 
            this.txtOrderID.Enabled = false;
            this.txtOrderID.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtOrderID.Location = new System.Drawing.Point(70, 8);
            this.txtOrderID.Name = "txtOrderID";
            this.txtOrderID.Size = new System.Drawing.Size(240, 29);
            this.txtOrderID.TabIndex = 22;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.ImageIndex = 5;
            this.btnCancel.ImageList = this.imageList1;
            this.btnCancel.Location = new System.Drawing.Point(178, 151);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnCancel.Size = new System.Drawing.Size(73, 34);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "취소";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.ImageIndex = 6;
            this.btnAdd.ImageList = this.imageList1;
            this.btnAdd.Location = new System.Drawing.Point(70, 151);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnAdd.Size = new System.Drawing.Size(73, 34);
            this.btnAdd.TabIndex = 20;
            this.btnAdd.Text = "등록";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(350, 63);
            this.panel1.TabIndex = 8;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(350, 63);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "출하지시";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "excel.png");
            this.imageList1.Images.SetKeyName(1, "plus.png");
            this.imageList1.Images.SetKeyName(2, "trash.png");
            this.imageList1.Images.SetKeyName(3, "pencil.png");
            this.imageList1.Images.SetKeyName(4, "search.png");
            this.imageList1.Images.SetKeyName(5, "arrow-left.png");
            this.imageList1.Images.SetKeyName(6, "check.png");
            this.imageList1.Images.SetKeyName(7, "close.png");
            // 
            // txtQty
            // 
            this.txtQty.Enabled = false;
            this.txtQty.Location = new System.Drawing.Point(70, 78);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(115, 29);
            this.txtQty.TabIndex = 26;
            // 
            // 수량
            // 
            this.수량.AutoSize = true;
            this.수량.Location = new System.Drawing.Point(23, 82);
            this.수량.Name = "수량";
            this.수량.Size = new System.Drawing.Size(42, 21);
            this.수량.TabIndex = 27;
            this.수량.Text = "수량";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(65, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 21);
            this.label4.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(282, 21);
            this.label3.TabIndex = 30;
            this.label3.Text = "상기내용으로 출하지시를 등록합니다.";
            // 
            // frmPlan_Ship
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 292);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPlan_Ship";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "출하지시";
            this.Load += new System.EventHandler(this.frmPlan_Ship_Load);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panel2;
        protected System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOrderID;
        protected System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        protected System.Windows.Forms.Panel panel1;
        protected System.Windows.Forms.Label lblTitle;
        protected System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label 수량;
        private System.Windows.Forms.TextBox txtQty;
    }
}