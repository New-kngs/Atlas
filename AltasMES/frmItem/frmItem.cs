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
        List<ItemVO> citemList = null; // 콤보 선택

        string selId = string.Empty;

        public frmItem()
        {
            InitializeComponent();  
        }

        private void frmItem_Load(object sender, EventArgs e)
        {
            srv = new ServiceHelper("");
            ResMessage<List<ItemVO>> result = srv.GetAsync<List<ItemVO>>("api/Item/AllItem");
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
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "제품ID", "ItemID", colwidth: 90, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "제품명", "ItemName", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "유형", "ItemCategory", colwidth: 65, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "규격", "ItemSize", colwidth: 65, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "거래처명", "CustomerName", colwidth: 190, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "창고", "WHName", colwidth: 100, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "단가", "ItemPrice", colwidth: 95, align: DataGridViewContentAlignment.MiddleRight);            
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "재고수량", "CurrentQty", colwidth: 100, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "안전재고량", "SafeQty", colwidth: 120, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "생성사용자", "CreateUser", colwidth: 120, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "생성날짜", "CreateDate", colwidth: 160, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "변경사용자", "ModifyUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "변경날짜", "ModifyDate", colwidth: 160, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "사용여부", "StateYN", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "이미지", "ItmeImage", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter, visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "설명", "ItemExplain", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter, visibility:false);
            
            dgvItem.Columns["ItemPrice"].DefaultCellStyle.Format = "###,##0";            

            cboCategory.Items.AddRange(new string[] { "선택", "완제품", "반제품", "자재" });
            cboCategory.SelectedIndex = 0; // Loadata();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<ItemVO> list;

            if (string.IsNullOrWhiteSpace(txtSearch.Text.Trim())) // 텍스트 조건 없이 콤보박스만
            {
                cboCategory_SelectedIndexChanged(this, e); 
            }

            else
            {
                if (cboCategory.SelectedIndex == 0) // 텍스트 조건만
                {
                    citemList = itemList.FindAll(p => p.ItemName.ToLower().Contains(txtSearch.Text.ToLower().Trim()));

                    dgvItem.DataSource = null;
                    dgvItem.DataSource = new AdvancedList<ItemVO>(citemList);
                }
                else //
                {
                    list = citemList.FindAll(p => p.ItemName.ToLower().Contains(txtSearch.Text.ToLower().Trim()));
                    dgvItem.DataSource = null;
                    dgvItem.DataSource = new AdvancedList<ItemVO>(list);
                }
            } 
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategory.SelectedIndex == 0)
            {
                txtSearch.Clear();
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

       


        private void btnAdd_Click(object sender, EventArgs e)
        {
            ItemVO item = new ItemVO()
            {
                CreateUser = ((Main)this.MdiParent).EmpName.ToString()
            };
            frmItem_Add pop = new frmItem_Add(item);
            if (pop.ShowDialog() == DialogResult.OK)
            {
                cboCategory_SelectedIndexChanged(this, e);
            }
        }

        private void dgvItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            selId = (dgvItem[0, e.RowIndex].Value).ToString();
            
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (selId == string.Empty)
            {
                MessageBox.Show("수정할 제품을 선택해 주세요");
                return;
            }

            else
            {
                ItemVO item = srv.GetAsync<ItemVO>($"api/Item/{selId}").Data;
                item.ModifyUser = ((Main)this.MdiParent).EmpName.ToString();            

                frmItem_Modify pop = new frmItem_Modify(item);
                if (pop.ShowDialog() == DialogResult.OK)
                {
                
                }
            }
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

        //public void LoadData()
        //{
        //    //srv = new ServiceHelper("");
        //    ResMessage<List<ItemVO>> result = srv.GetAsync<List<ItemVO>>("api/Item/AllItem");
        //    if (result != null)
        //    {
        //        itemList = result.Data;
        //    }
        //    else
        //    {
        //        MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
        //        return;
        //    }
        //}

        private void frmItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (srv != null)
            {
                srv.Dispose();
            }
        }
    }
}
