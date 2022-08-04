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
    public partial class frmBOM : BaseForm
    {
        ServiceHelper service = null;

        public frmBOM()
        {
            InitializeComponent();
        }

        private void frmBOM_Load(object sender, EventArgs e)
        {
            //ItemID, ItemName, CurrentQty, WHID, ItemCategory, ItemSize
            DataGridUtil.SetInitGridView(dgvPdt);
            DataGridUtil.AddGridTextBoxColumn(dgvPdt, "제품ID", "ItemID", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPdt, "제품유형", "ItemCategory", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvPdt, "제품이름", "ItemName", colwidth: 265, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvPdt, "제품사이즈", "ItemSize", colwidth: 160, align: DataGridViewContentAlignment.MiddleCenter);

            DataGridUtil.SetInitGridView(dgvA);
            DataGridUtil.AddGridTextBoxColumn(dgvA, "제품ID", "ItemID", colwidth: 175, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvA, "제품유형", "ItemCategory", colwidth: 170, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvA, "제품이름", "ItemName", colwidth: 255, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvA, "수량", "UnitQty", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvA, "제품사이즈", "ItemSize", colwidth: 120, align: DataGridViewContentAlignment.MiddleCenter);

            DataGridUtil.SetInitGridView(dgvD);
            DataGridUtil.AddGridTextBoxColumn(dgvD, "제품ID", "ItemID", colwidth: 175, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvD, "제품유형", "ItemCategory", colwidth: 170, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvD, "제품이름", "ItemName", colwidth: 255, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvD, "수량", "UnitQty", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvD, "제품사이즈", "ItemSize", colwidth: 120, align: DataGridViewContentAlignment.MiddleCenter);

            service = new ServiceHelper("");

            cboPdt.Items.AddRange(new string[] { "전체", "완제품", "반제품", "자재" });
            cboPdt.SelectedIndex = 0;
            //DataLoad();
        }
        private void DataLoad()
        {
            ResMessage<List<ItemVO>> result = service.GetAsync<List<ItemVO>>("api/Item/AllItem");

            if (result != null)
            {
                dgvPdt.DataSource = new AdvancedList<ItemVO>(result.Data);
                //dgvPdt.DataSource = result.Data;
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void AllClear()
        {
            cboPdt.SelectedIndex = 0;
            dgvA.DataSource = null;
            dgvD.DataSource = null;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            ResMessage<List<ItemVO>> volist = service.GetAsync<List<ItemVO>>("api/Item/AllItem");

            string category = cboPdt.Text;
            string name = txtPdt.Text;
            List<ItemVO> resultVO = volist.Data.FindAll((r) => r.ItemCategory == category);
            List<ItemVO> resultVO2 = volist.Data.FindAll((r) => r.ItemName.Contains(name));
            List<ItemVO> resultVO1 = resultVO.FindAll((r) => r.ItemName.Contains(name));

            if (string.IsNullOrWhiteSpace(txtPdt.Text))
            {
                if (cboPdt.SelectedIndex == 0)
                {
                    DataLoad();
                }
                else
                {
                    dgvPdt.DataSource = new List<ItemVO>(resultVO);
                }
            }
            else
            {
                if (cboPdt.SelectedIndex == 0)
                {
                    dgvPdt.DataSource = new List<ItemVO>(resultVO2);
                }
                else
                {
                    dgvPdt.DataSource = new List<ItemVO>(resultVO1);
                }
            }
        }
        private void cboPdt_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvA.DataSource = null;
            dgvD.DataSource = null;
            txtPdt.Text = null;
            if (cboPdt.SelectedIndex == 0)
            {
                dgvPdt.DataSource = null;
                DataLoad();
            }
            else
            {
                ResMessage<List<ItemVO>> volist = service.GetAsync<List<ItemVO>>("api/Item/AllItem");

                string category = cboPdt.Text;
                List<ItemVO> resultvo = volist.Data.FindAll((r) => r.ItemCategory == category);
                dgvPdt.DataSource = new List<ItemVO>(resultvo);
            }
            dgvPdt.ClearSelection();
        }

        private void dgvPdt_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvA.DataSource = null;
            dgvD.DataSource = null;

            string itemID = dgvPdt["ItemID", e.RowIndex].Value.ToString();
            string category = dgvPdt["ItemCategory", e.RowIndex].Value.ToString();

            ResMessage<List<BOMVO>> resource = service.GetAsync<List<BOMVO>>($"api/BOM/BOMFoward/{itemID}");
            ResMessage<List<BOMVO>> resource1 = service.GetAsync<List<BOMVO>>($"api/BOM/BOMReward/{itemID}");
            ResMessage<List<BOMVO>> resResult = service.GetAsync<List<BOMVO>>("api/BOM/AllBOMItem");

            if (e.RowIndex < 0) return;

            if (category == "완제품")
            {               
                dgvA.DataSource = resource.Data;
            }
            else if(category =="반제품")
            {
                List<BOMVO> listF = resResult.Data.FindAll((r) => r.ParentID == itemID);
                dgvA.DataSource = listF;

                List<BOMVO> listR = resResult.Data.FindAll((r) => r.ChildID == itemID);
                dgvD.DataSource = listR;
            }
            else
            {
                dgvD.DataSource = resource1.Data;
            }

        }

        private void frmBOM_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (service != null)
            {
                service.Dispose();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            BOMVO bom = new BOMVO()
            {
                CreateUser = ((Main)this.MdiParent).EmpName.ToString()
            };

            frmBOM_Add frm = new frmBOM_Add(bom);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DataLoad();
            }
        }

        private void txtPdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSearch_Click(this, e);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ResMessage<List<BOMVO>> resResult = service.GetAsync<List<BOMVO>>("api/BOM/AllBOMItem");

            string id = dgvPdt["ItemID", dgvPdt.CurrentRow.Index].Value.ToString();
            int list = resResult.Data.FindIndex((r) => r.ItemID == id);
            if (list < 0)
            {
                MessageBox.Show("삭제할 BOM대상이 없습니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (MessageBox.Show("선택하신 BOM구성이 삭제됩니다.", "삭제확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    ResMessage<List<BOMVO>> del = service.GetAsync<List<BOMVO>>($"api/BOM/DeleteBOM/" + id);
                    if (del.ErrCode == 0)
                    {
                        AllClear();
                        MessageBox.Show("삭제되었습니다.");                        
                    }
                    else
                    {
                        MessageBox.Show("삭제가 실패하였습니다. 다시 시도하여 주십시오.");
                    }
                }
            }


        }
    }
}
