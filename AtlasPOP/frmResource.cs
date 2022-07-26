﻿using AltasMES;
using AltasPOP;
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

namespace AtlasPOP
{
    public partial class frmResource : Form
    {
        ServiceHelper service = null;
        public string itemID { get; set; }
        
        public string OperID { get; set; }

        ResMessage<List<BOMVO>> resource;
        public frmResource()
        {
            InitializeComponent();
        }
        public frmResource(string itemID, string oper)
        {
            InitializeComponent();
            this.itemID = itemID;
            this.OperID = oper;
        }

        private void frmResource_Load(object sender, EventArgs e)
        {
            popDataGridUtil.SetInitGridView(dgvList);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "BOMID", "BOMID", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter, visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "제품ID", "ItemID", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter, visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "부모ID", "ParentID", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter,visibility:false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "제품ID", "ChildID", colwidth: 150);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "제품명", "ItemName", colwidth: 250);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "필요갯수", "UnitQty", colwidth: 130, align: DataGridViewContentAlignment.MiddleRight);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "계획수량", "PlanQty", colwidth: 130, align: DataGridViewContentAlignment.MiddleRight);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "총 수량", "Qty", colwidth: 130, align: DataGridViewContentAlignment.MiddleRight);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "재고량", "CurrentQty", colwidth: 130, align: DataGridViewContentAlignment.MiddleRight);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "생성일시", "CreateDate", colwidth: 200, visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "생성사용자", "CreateUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter, visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "변경일지", "ModifyDate", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter, visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "변경사용자", "ModifyUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter, visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "사용유무", "StateYN", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter, visibility: false);

            service = new ServiceHelper("");
            LoadData();

            
        }
        public void LoadData()
        {
            resource = service.GetAsync<List<BOMVO>>("api/pop/GetResourceBOM");
            if (resource.Data != null)
            {
                resource.Data = resource.Data.FindAll((r) => r.ItemID == itemID && r.OpID == OperID );
                dgvList.DataSource = resource.Data;
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }

            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int CurrentQty = resource.Data.Find((r) => r.ItemID == itemID).CurrentQty;
            int totQty = resource.Data.Find((r) => r.ItemID == itemID).Qty;
            ResMessage<List<OperationVO>> result = service.GetAsync<List<OperationVO>>("api/pop/AllOperation");
            string YN = result.Data.Find((n) => n.OpID == OperID).resourceYN;
            if (YN.Equals("Y")){
                MessageBox.Show("자재가 투입되어 있습니다.");
                return;

            }

            if (totQty > CurrentQty)
            {
                MessageBox.Show("투입할 재고가 부족합니다.");
                return;
            }
            //1. 자재투입 여부 업데이트
            ResMessage<List<OperationVO>> operList = service.PostAsync<string, List<OperationVO>>("api/pop/UpdateResourceYN/"+OperID, OperID);

            if (result.ErrCode == 0)
            {
                ResMessage<List<BOMVO>> resultQty = service.PostAsync<List<BOMVO>, List<BOMVO>>("api/pop/UpdateResourceQty", resource.Data);
                if (resultQty.ErrCode == 0)
                {
                   
                }
                else
                {
                    MessageBox.Show(resultQty.ErrMsg);
                }

                MessageBox.Show("재고 투입이 완료되었습니다.");
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show(result.ErrMsg);

            //2. 자재 재고 업데이트
            
            




            //   만약 재고가 부족하다면? 근데 재고 업데이트는....생산계획에서 해야하지 않을까...?그게 맞는거같은데 

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
