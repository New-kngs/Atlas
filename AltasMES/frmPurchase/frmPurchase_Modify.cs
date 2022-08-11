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
    public partial class frmPurchase_Modify : Form
    {
        public PurchaseVO purchase { get; set; }
        ServiceHelper srv = null;
        List<ItemVO> purList = null;
        List<PurchaseDetailsVO> purchaseList = null;
        List<PurchaseDetailsVO> lastPurList = null;


        public frmPurchase_Modify(PurchaseVO purchase)
        {
            InitializeComponent();

            srv = new ServiceHelper("");
            this.purchase = purchase;
            txtCusName.Text = purchase.CustomerName;
            textBox1.Text = purchase.PurchaseID;
            
            
            
        }

        private void frmPurchase_Modify_Load(object sender, EventArgs e)
        {
            srv = new ServiceHelper("");

            // 자재 리스트
            DataGridUtil.SetInitGridView(dgvItem);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "자재ID", "ItemID", colwidth: 90, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "자재명", "ItemName", colwidth: 160, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "규격", "ItemSize", colwidth: 70, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "재고수량", "CurrentQty", colwidth: 100, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "단가", "ItemPrice", colwidth: 100, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "거래처명", "CustomerName", colwidth: 120, align: DataGridViewContentAlignment.MiddleLeft);
            dgvItem.Columns["ItemPrice"].DefaultCellStyle.Format = "###,##0";
            dgvItem.ClearSelection();

            DataGridViewButtonColumn btn1 = new DataGridViewButtonColumn();
            btn1.HeaderText = "자재추가";
            btn1.Text = "추가";
            btn1.Width = 80;
            btn1.DefaultCellStyle.Padding = new Padding(5, 1, 5, 1);
            btn1.UseColumnTextForButtonValue = true;
            dgvItem.Columns.Add(btn1);

            // 발주 리스트
            DataGridUtil.SetInitGridView(dgvPurItem);
            DataGridUtil.AddGridTextBoxColumn(dgvPurItem, "자재ID", "ItemID", colwidth: 90, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPurItem, "자재명", "ItemName", colwidth: 330, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvPurItem, "규격", "ItemSize", colwidth: 70, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPurItem, "수량", "Qty", colwidth: 70, Readonly: false, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvPurItem, "단가", "ItemPrice", visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvPurItem, "거래처명", "CustomerName", visibility: false);

            DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
            btn2.HeaderText = "자재삭제";
            btn2.Text = "삭제";
            btn2.Width = 80;
            btn2.DefaultCellStyle.Padding = new Padding(5, 1, 5, 1);
            btn2.UseColumnTextForButtonValue = true;
            dgvPurItem.Columns.Add(btn2);

            LoadData();

            txtCount.Text = dgvPurItem.Rows.Count.ToString();
        }

        private void LoadData()
        {
            purList = srv.GetAsync<List<ItemVO>>("api/Item/PurChaseItem").Data;
            List<ItemVO> purItemList = purList.FindAll(p => p.CustomerName.Equals(txtCusName.Text));

            purchaseList = srv.GetAsync<List<PurchaseDetailsVO>>("api/Purchase/GetAllPurchaseDetail").Data;
            List<PurchaseDetailsVO> resultPurList = purchaseList.FindAll(p => p.PurchaseID.Equals(textBox1.Text));

            if (resultPurList == null || purItemList == null)
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }

            dgvPurItem.DataSource = dgvItem.DataSource = null;
            dgvItem.DataSource = new AdvancedList<ItemVO>(purItemList);
            dgvPurItem.DataSource = new AdvancedList<PurchaseDetailsVO>(resultPurList);
        }

        private void dgvItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 6)
            {
                string itemId = dgvItem["ItemID", e.RowIndex].Value.ToString();
                string itemName = dgvItem["ItemName", e.RowIndex].Value.ToString();
                string itemSize = dgvItem["ItemSize", e.RowIndex].Value.ToString();
                string cusName = dgvItem["CustomerName", e.RowIndex].Value.ToString();
                int price = Convert.ToInt32(dgvItem["ItemPrice", e.RowIndex].Value);                

                foreach (DataGridViewRow item in dgvPurItem.Rows)
                {
                    string purItemId = item.Cells[0].Value.ToString();                    

                    if (itemId.Equals(purItemId))
                    {
                        MessageBox.Show($"이미 추가된 자재입니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }                    
                }
                // null
                lastPurList.Add( new PurchaseDetailsVO { ItemID = itemId, ItemName = itemName, ItemSize = itemSize, CustomerName = cusName, PurTotPrice = price, Qty = 0, }); // price);
                dgvItem.ClearSelection();
                dgvPurItem.DataSource = null;
                dgvPurItem.DataSource = lastPurList;
                //txtCusName.Text = cusName;
                //txtPrice.Text = "0";


                int sum = 0;
                for (int i = 0; i < dgvPurItem.Rows.Count; i++)
                {
                    sum += Convert.ToInt32(dgvPurItem.Rows[i].Cells[3].Value);
                }
                txtCount.Text = dgvPurItem.Rows.Count.ToString();
            }
        }
    }
}
