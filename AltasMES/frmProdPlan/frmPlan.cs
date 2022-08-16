using AtlasDTO;
using System;
using System.Collections.Generic;
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
            DataGridUtil.AddGridTextBoxColumn(dgvList, "주문ID", "OrderID", colwidth: 160, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "거래처명", "CustomerName", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "생성사용자", "CreateUser", colwidth: 180, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "주문날짜", "CreateDate", colwidth: 280, align: DataGridViewContentAlignment.MiddleCenter);

            //ItemID, ItemName, Qty, CurrentQty, SafeQty
            DataGridUtil.SetInitGridView(dgvDetail);
            DataGridUtil.AddGridTextBoxColumn(dgvDetail, "제품ID", "ItemID", colwidth: 160, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvDetail, "제품명", "ItemName", colwidth: 220, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvDetail, "주문수량", "Qty", colwidth: 110, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvDetail, "현재재고", "CurrentQty", colwidth: 110, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvDetail, "안전재고", "SafeQty", colwidth: 110, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvDetail, "필요수량", "NeedQty", colwidth: 110, align: DataGridViewContentAlignment.MiddleRight);

            //ItemID, ItemName, PlanQty, CurrentQty, SafeQty
            DataGridUtil.SetInitGridView(dgvSemi);
            DataGridUtil.AddGridTextBoxColumn(dgvSemi, "제품ID", "ItemID", colwidth: 120, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvSemi, "제품명", "ItemName", colwidth: 215, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvSemi, "소요수량", "PlanQty", colwidth: 120, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvSemi, "현재재고", "CurrentQty", colwidth: 120, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvSemi, "안전재고", "SafeQty", colwidth: 120, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvSemi, "필요수량", "NeedQty", colwidth: 120, align: DataGridViewContentAlignment.MiddleRight);


            //ItemID, ItemName, PlanQty, CurrentQty, SafeQty
            DataGridUtil.SetInitGridView(dgvMaterial);
            DataGridUtil.AddGridTextBoxColumn(dgvMaterial, "제품ID", "ItemID", colwidth: 120, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvMaterial, "제품명", "ItemName", colwidth: 215, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvMaterial, "소요수량", "PlanQty", colwidth: 120, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvMaterial, "현재재고", "CurrentQty", colwidth: 120, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvMaterial, "안전재고", "SafeQty", colwidth: 120, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvMaterial, "필요수량", "NeedQty", colwidth: 120, align: DataGridViewContentAlignment.MiddleRight);


            dtpTo.Value = DateTime.Now;
            dtpFrom.Value = DateTime.Now.AddDays(-7);

            LoadData();
        }

        public void LoadData()
        {
            ResMessage<List<OrderVO>> result = srv.GetAsync<List<OrderVO>>("api/Plan/ReadyList/" + dtpFrom.Value.ToShortDateString() + "/" + dtpTo.Value.AddDays(1).ToShortDateString());

            if (result != null)
            {
                dgvList.DataSource = new AdvancedList<OrderVO>(result.Data);
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            dgvDetail.DataSource = null;
            dgvSemi.DataSource = null;
            dgvMaterial.DataSource = null;

            string order = dgvList["OrderID", e.RowIndex].Value.ToString();

            ResMessage<List<PlanVO>> resource = srv.GetAsync<List<PlanVO>>($"api/Plan/OrderDetail/{order}");

            dgvDetail.DataSource = resource.Data;
            dgvDetail.ClearSelection();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int from = Convert.ToInt32(dtpFrom.Value.ToString("yyyyMMdd"));
            int to = Convert.ToInt32(dtpTo.Value.ToString("yyyyMMdd"));

            if (from > to)
            {
                MessageBox.Show("검색날짜를 확인해주십시오.");
                dtpTo.Value = DateTime.Now;
                dtpFrom.Value = DateTime.Now.AddDays(-7);
                return;
            }
            else
            {
                LoadData();
                dgvList.ClearSelection();
                dgvList.ClearSelection();
                dgvDetail.DataSource = null;
                dgvSemi.DataSource = null;
                dgvMaterial.DataSource = null;
            }
        }

        private void dgvDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;           

            dgvSemi.DataSource = null;
            dgvMaterial.DataSource = null;

            string order = dgvList["OrderID", dgvList.CurrentRow.Index].Value.ToString();
            string item = dgvDetail["ItemID", e.RowIndex].Value.ToString();
            //int qty = Convert.ToInt32(dgvDetail["Qty", e.RowIndex].Value);
            //int currentQty = Convert.ToInt32(dgvDetail["CurrentQty", e.RowIndex].Value);
            //int safeQty = Convert.ToInt32(dgvDetail["SafeQty", e.RowIndex].Value);
            //int planQty = 0;
            //if (currentQty - qty > safeQty)
            //{
            //    planQty = 0;
            //}
            //else
            //{
            //    planQty = qty + safeQty - currentQty;
            //}

            ResMessage<List<PlanVO>> resource = srv.GetAsync<List<PlanVO>>($"api/Plan/Components/{order}/{item}");

            List<PlanVO> semiList = resource.Data.FindAll((f) => f.ItemCategory.Equals("반제품"));
            List<PlanVO> mList = resource.Data.FindAll((f) => f.ItemCategory.Equals("자재"));

            dgvSemi.DataSource = semiList;
            dgvMaterial.DataSource = mList;
            dgvSemi.ClearSelection();
            dgvMaterial.ClearSelection();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {            
            frmPlan_Add frm = new frmPlan_Add();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                dgvList.ClearSelection();
                dgvDetail.DataSource = null;
                dgvSemi.DataSource = null;
                dgvMaterial.DataSource = null;
            }
        }

        private void frmPlan_FormClosing(object sender, FormClosingEventArgs e)
        {
            srv.Dispose();
        }

        private void frmPlan_Shown(object sender, EventArgs e)
        {
            dgvList.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgvSemi.DataSource == null)
            {
                MessageBox.Show("작업지시를 생성할 주문서와 제품을 선택하여 주십시오.");
                return;
            }
            else
            {
                PlanOptVO plan = new PlanOptVO()
                {
                    OrderID = dgvList["OrderID", dgvList.CurrentRow.Index].Value.ToString(),
                    ProductID = dgvDetail["ItemID", dgvDetail.CurrentRow.Index].Value.ToString(),
                    ProductName = dgvDetail["ItemName", dgvDetail.CurrentRow.Index].Value.ToString(),
                    ProductQty = Convert.ToInt32(dgvDetail["NeedQty", dgvDetail.CurrentRow.Index].Value),
                    Semi1ID = dgvSemi.Rows[0].Cells["ItemID"].Value.ToString(),
                    Semi1Name = dgvSemi.Rows[0].Cells["ItemName"].Value.ToString(),
                    Semi1Qty = Convert.ToInt32(dgvSemi.Rows[0].Cells["NeedQty"].Value),
                    Semi2ID = dgvSemi.Rows[1].Cells["ItemID"].Value.ToString(),
                    Semi2Name = dgvSemi.Rows[1].Cells["ItemName"].Value.ToString(),
                    Semi2Qty = Convert.ToInt32(dgvSemi.Rows[1].Cells["NeedQty"].Value),
                    Material1ID = dgvMaterial.Rows[0].Cells["ItemID"].Value.ToString(),
                    Material1Name = dgvMaterial.Rows[0].Cells["ItemName"].Value.ToString(),
                    Material1Qty = Convert.ToInt32(dgvMaterial.Rows[0].Cells["NeedQty"].Value),
                    Material2ID = dgvMaterial.Rows[1].Cells["ItemID"].Value.ToString(),
                    Material2Name = dgvMaterial.Rows[1].Cells["ItemName"].Value.ToString(),
                    Material2Qty = Convert.ToInt32(dgvMaterial.Rows[1].Cells["NeedQty"].Value),
                    Material3ID = dgvMaterial.Rows[2].Cells["ItemID"].Value.ToString(),
                    Material3Name = dgvMaterial.Rows[2].Cells["ItemName"].Value.ToString(),
                    Material3Qty = Convert.ToInt32(dgvMaterial.Rows[2].Cells["NeedQty"].Value),
                    Material4ID = dgvMaterial.Rows[3].Cells["ItemID"].Value.ToString(),
                    Material4Name = dgvMaterial.Rows[3].Cells["ItemName"].Value.ToString(),
                    Material4Qty = Convert.ToInt32(dgvMaterial.Rows[3].Cells["NeedQty"].Value),
                    Material5ID = dgvMaterial.Rows[4].Cells["ItemID"].Value.ToString(),
                    Material5Name = dgvMaterial.Rows[4].Cells["ItemName"].Value.ToString(),
                    Material5Qty = Convert.ToInt32(dgvMaterial.Rows[4].Cells["NeedQty"].Value),
                    Material6ID = dgvMaterial.Rows[5].Cells["ItemID"].Value.ToString(),
                    Material6Name = dgvMaterial.Rows[5].Cells["ItemName"].Value.ToString(),
                    Material6Qty = Convert.ToInt32(dgvMaterial.Rows[5].Cells["NeedQty"].Value),
                    Material7ID = dgvMaterial.Rows[6].Cells["ItemID"].Value.ToString(),
                    Material7Name = dgvMaterial.Rows[6].Cells["ItemName"].Value.ToString(),
                    Material7Qty = Convert.ToInt32(dgvMaterial.Rows[6].Cells["NeedQty"].Value),
                    Material8ID = dgvMaterial.Rows[7].Cells["ItemID"].Value.ToString(),
                    Material8Name = dgvMaterial.Rows[7].Cells["ItemName"].Value.ToString(),
                    Material8Qty = Convert.ToInt32(dgvMaterial.Rows[7].Cells["NeedQty"].Value)
                };
                frmPlan_Plan frm = new frmPlan_Plan(plan);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                    dgvList.ClearSelection();
                    dgvDetail.DataSource = null;
                    dgvSemi.DataSource = null;
                    dgvMaterial.DataSource = null;
                }
            }
        }
    }
}
