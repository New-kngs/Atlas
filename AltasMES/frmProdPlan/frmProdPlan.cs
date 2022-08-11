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
    public partial class frmPlan : BaseForm
    {
        ServiceHelper srv = null;

        public frmPlan()
        {
            InitializeComponent();
        }              

        private void frmPlan_Load(object sender, EventArgs e)
        {
            srv = new ServiceHelper("");

            //OrderID, CustomerID, OrderShip, OrderEndDate, CreateDate, CreateUser, ModifyDate, ModifyUser
            DataGridUtil.SetInitGridView(dgvList);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "주문ID", "OrderID", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "거래처명", "CustomerID", colwidth: 180, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "생성사용자", "CreateUser", colwidth: 180, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "주문날짜", "CreateDate", colwidth: 300, align: DataGridViewContentAlignment.MiddleCenter);

            //ItemID, ItemName, PlanQty, CurrentQty, SafeQty
            DataGridUtil.SetInitGridView(dgvDetail);
            DataGridUtil.AddGridTextBoxColumn(dgvDetail, "제품ID", "ItemID", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvDetail, "제품명", "ItemName", colwidth: 210, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvDetail, "주문수량", "PlanQty", colwidth: 150, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvDetail, "현재재고", "CurrentQty", colwidth: 150, align: DataGridViewContentAlignment.MiddleRight); 
            DataGridUtil.AddGridTextBoxColumn(dgvDetail, "안전재고", "SafeQty", colwidth: 150, align: DataGridViewContentAlignment.MiddleRight);

            //ItemID, ItemName, PlanQty, CurrentQty, SafeQty
            DataGridUtil.SetInitGridView(dgvSemi);
            DataGridUtil.AddGridTextBoxColumn(dgvSemi, "제품ID", "ItemID", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvSemi, "제품명", "ItemName", colwidth: 215, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvSemi, "주문수량", "PlanQty", colwidth: 150, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvSemi, "현재재고", "CurrentQty", colwidth: 150, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvSemi, "안전재고", "SafeQty", colwidth: 150, align: DataGridViewContentAlignment.MiddleRight);

            //ItemID, ItemName, PlanQty, CurrentQty, SafeQty
            DataGridUtil.SetInitGridView(dgvMaterial);
            DataGridUtil.AddGridTextBoxColumn(dgvMaterial, "제품ID", "ItemID", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvMaterial, "제품명", "ItemName", colwidth: 215, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvMaterial, "주문수량", "PlanQty", colwidth: 150, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvMaterial, "현재재고", "CurrentQty", colwidth: 150, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvMaterial, "안전재고", "SafeQty", colwidth: 150, align: DataGridViewContentAlignment.MiddleRight);

            dtpTo.Value = DateTime.Now;
            dtpFrom.Value = DateTime.Now.AddDays(-7);

            LoadData();
        }

        public void LoadData()
        {

        }
    }
}
