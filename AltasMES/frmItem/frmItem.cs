using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AtlasDTO;

namespace AltasMES
{
    public partial class frmItem : BaseForm
    {
        ServiceHelper srv = null;
        ResMessage<List<ItemVO>> itemList;        

        public frmItem()
        {
            InitializeComponent();  
        }

        private void frmItem_Load(object sender, EventArgs e)
        {
            
            //ItmeImage, ItemExplain

            cboCategory.Items.AddRange(new string[] { "선택", "완제품", "반제품", "자재" });
            cboCategory.SelectedIndex = 0;

            DataGridUtil.SetInitGridView(dgvItem);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "제품ID", "ItemID", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "제품명", "ItemName", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "유형", "ItemCategory", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "규격", "ItemSize", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "단가", "ItemPrice", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "거래처명", "CustomerName", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "재고수량", "CurrentQty", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "안전재고량", "SafeQty", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "창고", "WHName", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "생성날짜", "CreateDate", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "생성사용자", "CreateUser", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "변경날짜", "ModifyDate", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "변경사용자", "ModifyUser", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "사용여부", "StateYN", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);

            LoadData();
        }

        public void LoadData()
        {

            srv = new ServiceHelper("api/Item");
            itemList = srv.GetAsync<List<ItemVO>>("AllItem");

            if (itemList != null)
                dgvItem.DataSource = new AdvancedList<ItemVO>(itemList.Data);
            else
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");            
        }

        private void frmItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (srv != null)
                srv.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmItem_Add pop = new frmItem_Add();
            if (pop.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }        

        private void btnModify_Click(object sender, EventArgs e)
        {

            //frmItem_Modify pop = new frmItem_Modify();
            //if (pop.ShowDialog() == DialogResult.OK)
            //{
            //    LoadDate();
            //}
        }

        private void dgvItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();

            if (cboCategory.SelectedIndex > 0)
                itemList.Data = itemList.Data.FindAll((p) => p.ItemCategory.Contains(cboCategory.Text));

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                itemList.Data = itemList.Data.FindAll((p) => p.ItemName.Contains(txtSearch.Text));

            dgvItem.DataSource = itemList.Data;
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
