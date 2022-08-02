using AtlasDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltasMES
{
    public partial class frmPurchase_Detail : Form
    {
        public PurchaseVO purchase { get; set; }
        ServiceHelper srv = null;
        

        public frmPurchase_Detail(PurchaseVO purchase)
        {
            srv = new ServiceHelper("");

            InitializeComponent();

            this.purchase = purchase;
            txtPurchaseID.Text = purchase.PurchaseID;
            txtName.Text = purchase.CustomerName;
            txtCreateDate.Text = purchase.CreateDate;
            txtMdfDate.Text = purchase.ModifyDate;
            txtEndDate.Text = purchase.PurchaseEndDate;
            txtInState.Text = purchase.InState;

        }

        private void frmPurchase_Detail_Load(object sender, EventArgs e)
        {
            DataGridUtil.SetInitGridView(dgvPurchaseDetail);
            DataGridUtil.AddGridTextBoxColumn(dgvPurchaseDetail, "제품ID", "ItemID", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPurchaseDetail, "제품명", "ItemName", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvPurchaseDetail, "수량", "Qty", colwidth: 100, align: DataGridViewContentAlignment.MiddleRight);

            LoadData();
        }
        public void LoadData()
        {
            
        }
    }
}
