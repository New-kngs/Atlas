﻿using AtlasDTO;
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
        PlanVO plan = null;
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
            dgvList.ClearSelection();
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
            int from = Convert.ToInt32( dtpFrom.Value.ToString("yyyyMMdd"));
            int to = Convert.ToInt32( dtpTo.Value.ToString("yyyyMMdd"));

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
            }            
        }

        private void dgvDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            dgvSemi.DataSource = null;
            dgvMaterial.DataSource = null;

            string order = dgvList["OrderID", e.RowIndex].Value.ToString();
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

            plan = new PlanVO()
            {
                
            };

        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            frmProdPlan_Add frm = new frmProdPlan_Add(plan);
        }

        private void frmPlan_FormClosing(object sender, FormClosingEventArgs e)
        {
            srv.Dispose();
        }
    }
}
