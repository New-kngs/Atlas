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
    public partial class frmPurchase_Add : Form
    {
        ServiceHelper srv = null;
        List<CustomerVO> cusList = null;
        List<ItemVO> purItemList = null;

        public PurchaseVO item { get; set; }
        public frmPurchase_Add(PurchaseVO item)
        {
            InitializeComponent();
            this.item = item;
        }        

        private void frmPurchase_Add_Load(object sender, EventArgs e)
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
            DataGridUtil.AddGridTextBoxColumn(dgvPurItem, "자재명", "ItemName", colwidth: 160, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPurItem, "수량", "Qty", colwidth: 100, Readonly: false, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvPurItem, "총 가격", "PurTotPrice", visibility: false);
            

            DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
            btn2.HeaderText = "자재삭제";
            btn2.Text = "삭제";
            btn2.Width = 80;
            btn2.DefaultCellStyle.Padding = new Padding(5, 1, 5, 1);
            btn2.UseColumnTextForButtonValue = true;
            dgvPurItem.Columns.Add(btn2);

            
            LoadData();

            cusList = srv.GetAsync<List<CustomerVO>>("api/Customer/AllCustomer").Data;
            CommonUtil.ComboBinding<CustomerVO>(cboCustomer, cusList.FindAll(p => p.Category.Equals("입고")), "CustomerName", "CustomerID", blankText: "전체");

        }

        public void LoadData()
        {            
            purItemList = srv.GetAsync<List<ItemVO>>("api/Item/PurChaseItem").Data;            

            if (purItemList == null)
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }

            dgvItem.DataSource = null;
            dgvItem.DataSource = new AdvancedList<ItemVO>(purItemList);
        }               

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Clear();
            dgvItem.DataSource = null;

            if (cboCustomer.SelectedIndex == 0)
            {
                dgvItem.DataSource = new AdvancedList<ItemVO>(purItemList);
            }
            else
            {
                List<ItemVO> sitemList = purItemList.FindAll(p => p.CustomerName.Equals(cboCustomer.Text));
                dgvItem.DataSource = new AdvancedList<ItemVO>(sitemList);
            }
            dgvItem.ClearSelection();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text.Trim()) && cboCustomer.SelectedIndex == 0)
            {
                MessageBox.Show("거래처를 선택하거나 자재명을 입력해 주세요");
            }
            if (string.IsNullOrWhiteSpace(txtSearch.Text.Trim()))
            {
                cboCustomer_SelectedIndexChanged(this, e);
            }
            else
            {
                if (cboCustomer.SelectedIndex == 0)
                {
                    List<ItemVO> pitemList = purItemList.FindAll(p => p.ItemName.ToLower().Contains(txtSearch.Text.ToLower().Trim()));
                    dgvItem.DataSource = null;
                    dgvItem.DataSource = new AdvancedList<ItemVO>(pitemList);
                }
                else
                {
                    List<ItemVO> citemList = purItemList.FindAll(p => p.ItemName.ToLower().Contains(txtSearch.Text.ToLower().Trim()) && p.CustomerName.Equals(cboCustomer.Text));
                    dgvItem.DataSource = null;
                    dgvItem.DataSource = new AdvancedList<ItemVO>(citemList);
                }
            }

        }

        private void dgvItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 6)
            {
                string itemId = dgvItem["ItemID", e.RowIndex].Value.ToString();
                string itemName = dgvItem["ItemName", e.RowIndex].Value.ToString();

                foreach (DataGridViewRow item in dgvPurItem.Rows)
                {
                    string purItemId = item.Cells[0].Value.ToString();

                    if (itemId.Equals(purItemId))
                    {
                        MessageBox.Show($"이미 추가된 제품입니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                dgvPurItem.Rows.Add(itemId, itemName, 0, 0);
            }  
        }


        private void dgvPurItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 4)
            {
                DataGridViewRow dgvRow = dgvPurItem.Rows[e.RowIndex];
                dgvPurItem.Rows.Remove(dgvRow);
            }
        }

        private void frmPurchase_Add_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (srv != null)
            {
                srv.Dispose();
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSearch_Click(this, e);
            }
        }

       
    }
}
