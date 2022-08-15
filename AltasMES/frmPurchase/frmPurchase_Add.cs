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
        //List<ItemVO> purCusList = null;
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
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "거래처ID", "CustomerID", visibility: false);
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
            DataGridUtil.AddGridTextBoxColumn(dgvPurItem, "거래처명", "CustomerName", visibility: false); //colwidth: 120, align: DataGridViewContentAlignment.MiddleLeft); //visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvPurItem, "거래처ID", "CustomerID", visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvPurItem, "단가", "ItemPrice", visibility: false);
            

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
            dgvItem.ClearSelection();

        }

        private void dgvItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 7)
            {             
                string itemId = dgvItem["ItemID", e.RowIndex].Value.ToString();
                string itemName = dgvItem["ItemName", e.RowIndex].Value.ToString();
                string itemSize = dgvItem["ItemSize", e.RowIndex].Value.ToString();
                string cusName = dgvItem["CustomerName", e.RowIndex].Value.ToString();
                string cusId = dgvItem["CustomerID", e.RowIndex].Value.ToString();
                int price = Convert.ToInt32(dgvItem["ItemPrice", e.RowIndex].Value);
                //int purQty = Convert.ToInt32(dgvPurItem["Qty", e.RowIndex].Value);

                foreach (DataGridViewRow item in dgvPurItem.Rows)
                {
                    string purItemId = item.Cells[0].Value.ToString();                    

                    if (itemId.Equals(purItemId))
                    {
                        MessageBox.Show($"이미 추가된 자재입니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }         
                    else
                    {
                        string purCusName = item.Cells[4].Value.ToString();
                        if (cusName != purCusName)
                        {
                            MessageBox.Show($"{purCusName} 자재만 발주 가능합니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cboCustomer.SelectedIndex = 0;
                            return;
                        }
                    }
                }
                dgvPurItem.Rows.Add(itemId, itemName, itemSize, 0, cusName, cusId, price);
                dgvItem.ClearSelection();
                dgvPurItem.ClearSelection();
                txtCusName.Text = cusName;
                txtPrice.Text = "0 원";

                int sum = 0;
                for (int i = 0; i < dgvPurItem.Rows.Count; i++)
                {
                    sum += Convert.ToInt32(dgvPurItem.Rows[i].Cells[3].Value);
                }
                txtCount.Text = dgvPurItem.Rows.Count.ToString();                
            }  
        }

        private void dgvPurItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 7)
            {
                DataGridViewRow dgvRow = dgvPurItem.Rows[e.RowIndex];
                dgvPurItem.Rows.Remove(dgvRow);
                dgvPurItem.ClearSelection();

                int sum = 0;
                int price = 0;
                int qty = 0;
                for (int i = 0; i < dgvPurItem.Rows.Count; i++)
                {
                    sum += qty = Convert.ToInt32(dgvPurItem.Rows[i].Cells[3].Value); // 수량
                    price += qty * Convert.ToInt32(dgvPurItem.Rows[i].Cells[6].Value); // 단가
                }
                txtCount.Text = dgvPurItem.Rows.Count.ToString();
                txtPrice.Text = price.ToString("#,##0") + " 원";

                if (dgvPurItem.Rows.Count < 1)
                {
                    txtCusName.Text = string.Empty;
                }
            }
        }


        private void dgvPurItem_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int sum = 0;
            int price = 0;
            int qty = 0;           

            for (int i = 0; i < dgvPurItem.Rows.Count; i++)
            {
                if (int.TryParse(dgvPurItem.Rows[i].Cells[3].Value.ToString(), out qty))
                {
                    sum += qty;
                    price += qty * Convert.ToInt32(dgvPurItem.Rows[i].Cells[6].Value);
                }
                else
                {
                    MessageBox.Show("수량은 숫자를 입력해 주세요");
                    dgvPurItem.Rows[i].Cells[3].Value = 0;
                    return;
                }

            }
            txtCount.Text = dgvPurItem.Rows.Count.ToString();
            txtPrice.Text = price.ToString("#,##0") + " 원";

        }        

        

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //purCusList = srv.GetAsync<List<ItemVO>>("api/Item/AllItem").Data;

            if (dgvPurItem.Rows.Count < 1)
            {
                MessageBox.Show("등록된 발주 목록이 없습니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<PurchaseDetailsVO> purDetailList = new List<PurchaseDetailsVO>();
            for (int i = 0; i < dgvPurItem.Rows.Count; i++)
            {
                if (Convert.ToInt32(dgvPurItem.Rows[i].Cells[3].Value) == 0)
                {
                    MessageBox.Show("발주 수량을 확인해 주세요", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (Convert.ToInt32(dgvPurItem.Rows[i].Cells[3].Value) > 300)
                {
                    MessageBox.Show("한번에 발주가 가능한 자재 수량은 300개 입니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgvPurItem.CurrentCell = dgvPurItem.Rows[i].Cells[3];
                    return;
                }
                PurchaseDetailsVO item = new PurchaseDetailsVO();
                item.ItemID = dgvPurItem.Rows[i].Cells[0].Value.ToString();
                item.Qty = Convert.ToInt32(dgvPurItem.Rows[i].Cells[3].Value.ToString()); 
                
                purDetailList.Add(item);
            }           
            PurchaseVO purList = new PurchaseVO()
            {
                CreateUser = this.item.CreateUser,
                CustomerID = dgvPurItem["CustomerID", 0].Value.ToString(),
            };
            ResMessage result = srv.SavePurchase("api/Purchase/SavePurchase", purList, purDetailList);


            if (result.ErrCode == 0)
            {
                MessageBox.Show("등록이 완료되었습니다.", "발주 등록", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("오류가 발생하였습니다. 다시 시도 하여 주십시오.");
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

        private void frmPurchase_Add_Shown(object sender, EventArgs e)
        {
            dgvItem.ClearSelection();
            dgvPurItem.ClearSelection();
        }

        private void dgvItem_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvItem.ClearSelection();
        }

        private void dgvPurItem_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvPurItem.ClearSelection();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
