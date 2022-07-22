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
        List<ItemVO> itemList = null;
        List<ItemVO> citemList = null;

        public frmItem()
        {
            InitializeComponent();  
        }

        private void frmItem_Load(object sender, EventArgs e)
        {
            srv = new ServiceHelper("api/Item");
            ResMessage<List<ItemVO>> result = srv.GetAsync<List<ItemVO>>("AllItem");
            if (result != null)
            {
                itemList = result.Data;
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
                return;
            }

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
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "이미지", "ItmeImage", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter, visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "설명", "ItemExplain", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter, visibility:false);

            cboCategory.Items.AddRange(new string[] { "선택", "완제품", "반제품", "자재" });
            cboCategory.SelectedIndex = 0; // Loadata();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<ItemVO> list;

            if (string.IsNullOrWhiteSpace(txtSearch.Text)) // 텍스트 조건 없이 콤보박스만
            {
                cboCategory_SelectedIndexChanged(this, e); 
            }

            else
            {
                if (cboCategory.SelectedIndex == 0) // 텍스트 조건만
                {
                    citemList = itemList.FindAll(p => p.ItemName.ToLower().Contains(txtSearch.Text.ToLower()));

                    dgvItem.DataSource = null;
                    dgvItem.DataSource = new AdvancedList<ItemVO>(citemList);
                }
                else //
                {
                    list = citemList.FindAll(p => p.ItemName.ToLower().Contains(txtSearch.Text.ToLower()));
                    dgvItem.DataSource = null;
                    dgvItem.DataSource = new AdvancedList<ItemVO>(list);
                }
            } 
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategory.SelectedIndex == 0)
            {
                dgvItem.DataSource = null;
                dgvItem.DataSource = new AdvancedList<ItemVO>(itemList);
            }
            else
            {
                if (itemList != null)
                {
                    citemList = itemList.FindAll(p => p.ItemCategory.Equals(cboCategory.Text));
                    dgvItem.DataSource = null;
                    dgvItem.DataSource = new AdvancedList<ItemVO>(citemList);
                }
            }
            dgvItem.ClearSelection();
        }

        private void dgvItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmItem_Add pop = new frmItem_Add();
            if (pop.ShowDialog() == DialogResult.OK)
            {
                //LoadData();
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

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void frmItem_Shown(object sender, EventArgs e)
        {
            dgvItem.ClearSelection();
        }        

        private void frmItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (srv != null)
            {
                srv.Dispose();
            }
        }
    }
}
