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
    public partial class frmBOM_Add : Form
    {
        ServiceHelper service = null;

        public BOMVO bom { get; set; }
        public frmBOM_Add(BOMVO bom)
        {
            InitializeComponent();
            this.bom = bom;
        }

        private void frmBOM_Add_Load(object sender, EventArgs e)
        {
            //BOMID, B.ItemID, ParentID, ChildID, UnitQty, B.CreateDate, B.CreateUser, B.ModifyDate, B.ModifyUser, ItemName, ItemCategory, ItemSize
            label2.Visible = cboPdt.Visible = false;

            DataGridUtil.SetInitGridView(dgvUnreg);
            DataGridUtil.AddGridTextBoxColumn(dgvUnreg, "제품ID", "ItemID", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvUnreg, "제품이름", "ItemName", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvUnreg, "제품사이즈", "ItemSize", visibility: false);

            DataGridUtil.SetInitGridView(dgvParts);
            DataGridUtil.AddGridTextBoxColumn(dgvParts, "제품ID", "ItemID", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvParts, "제품이름", "ItemName", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridViewButtonColumn btnA = new DataGridViewButtonColumn();
            btnA.HeaderText = "추가";
            btnA.Text = "추가";
            btnA.Width = 70;
            btnA.DefaultCellStyle.Padding = new Padding(5, 1, 5, 1);
            btnA.UseColumnTextForButtonValue = true;
            dgvParts.Columns.Add(btnA);
            foreach (DataGridViewRow row in dgvParts.Rows)
            {
                row.Cells[2].Value = "ItemID";
            }
            //제품 추가 버튼 이벤트 등록
            dgvParts.CellClick += DgvParts_CellClick;

            DataGridUtil.SetInitGridView(dgvNew);
            DataGridUtil.AddGridTextBoxColumn(dgvNew, "제품ID", "ItemID", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvNew, "제품이름", "ItemName", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvNew, "수량", "UnitQty", colwidth: 70, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridViewButtonColumn btnB = new DataGridViewButtonColumn();
            btnB.HeaderText = "삭제";
            btnB.Text = "삭제";
            btnB.Width = 70;
            btnB.DefaultCellStyle.Padding = new Padding(5, 1, 5, 1);
            btnB.UseColumnTextForButtonValue = true;
            dgvNew.Columns.Add(btnB);

            service = new ServiceHelper("");

            cboCategory.Items.AddRange(new string[] { "전체", "완제품", "반제품" });
            cboCategory.SelectedIndex = 0;
        }
        //제품추가이벤트
        private void DgvParts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            List<BOMVO> list;
            DataGridViewCell dgvCell;
            DataGridViewCellEventArgs cellEventArgs;

            list = dgvNew.DataSource as List<BOMVO>;

            if (list == null) { list = new List<BOMVO>(); }

            if (e.RowIndex < 0) { return; }

            if (e.ColumnIndex == dgvParts.Columns[""].Index)
            {
                string partID = dgvParts.Rows[e.RowIndex].Cells[0].Value.ToString();
                string partName = dgvParts.Rows[e.RowIndex].Cells[1].Value.ToString();
                int partQty = 0;
                foreach (DataGridViewRow item in dgvNew.Rows)
                {
                    string newID = item.Cells[0].Value.ToString();
                    if (partID.Equals(newID))
                    {
                        MessageBox.Show("이미 추가된 제품입니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (dgvNew.DataSource != null)
                        {
                            int findIndex = list.FindIndex((f) => f.ItemID.Equals(newID));

                            dgvCell = dgvNew.Rows[findIndex].Cells[2];
                            dgvNew.FirstDisplayedCell = dgvCell;
                            dgvNew.CurrentCell = dgvCell;

                            cellEventArgs = new DataGridViewCellEventArgs(dgvCell.ColumnIndex, dgvCell.RowIndex);
                            dgvNew_CellClick(this, cellEventArgs);
                        }
                        return;
                    }
                }

                BOMVO bom = new BOMVO()
                {
                    ItemID = partID,
                    ItemName = partName,
                    UnitQty = partQty
                };

                list.Add(bom);

                dgvNew.DataSource = null;
                dgvNew.DataSource = list;

                int select = list.FindIndex((f) => f.ItemID.Equals(partID));

                if (select != 0)
                {
                    dgvCell = dgvNew.Rows[select].Cells[2];
                    dgvNew.FirstDisplayedCell = dgvCell;
                    dgvNew.CurrentCell = dgvCell;

                    cellEventArgs = new DataGridViewCellEventArgs(dgvCell.ColumnIndex, dgvCell.RowIndex);
                    dgvNew_CellClick(this, cellEventArgs);
                }
            }
        }

        private void dgvNew_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvNew.Columns[""].Index)
            {
                if (e.RowIndex < 0) { return; }

                List<BOMVO> list = dgvNew.DataSource as List<BOMVO>;

                list.RemoveAt(e.RowIndex);

                dgvNew.DataSource = null;
                dgvNew.DataSource = list;

                dgvNew.ClearSelection();
            }
        }

        private void frmBOM_Add_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (service != null)
            {
                service.Dispose();
            }
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategory.SelectedIndex == 0)
            {
                dgvUnreg.DataSource = null;

                //dgvPdt.DataSource = volist.Data;
                DataLoad();
            }

            else
            {
                ResMessage<List<BOMVO>> volist = service.GetAsync<List<BOMVO>>("api/BOM/UnregiItem");

                string category = cboCategory.Text;
                List<BOMVO> resultVO = volist.Data.FindAll((r) => r.ItemCategory == category);

                dgvUnreg.DataSource = new AdvancedList<BOMVO>(resultVO);
            }

            dgvUnreg.ClearSelection();
        }

        private void DataLoad()
        {
            ResMessage<List<BOMVO>> result = service.GetAsync<List<BOMVO>>("api/BOM/UnregiItem");

            if (result != null)
            {
                dgvUnreg.DataSource = new AdvancedList<BOMVO>(result.Data);
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void dgvUnreg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string size = dgvUnreg["ItemSize", e.RowIndex].Value.ToString();

                ResMessage<List<ItemVO>> result = service.GetAsync<List<ItemVO>>("api/Item/AllItem");

                List<ItemVO> listA = result.Data.FindAll((r) => r.ItemSize == size);

                List<ItemVO> listB = listA.FindAll((r) => r.ItemCategory == "반제품");

                dgvParts.DataSource = listB;
            }
            else
            {
                return;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (cboCategory.SelectedIndex == 0)
            {
                MessageBox.Show("우선 제품유형을 완제품/반제품 중 선택하십시오.");
                return;
            } 
            else
            {
                dgvParts.DataSource = dgvNew.DataSource = null;
                label2.Visible = cboPdt.Visible = true;
                string choice = cboCategory.SelectedItem.ToString();
                
                ResMessage<List<BOMVO>> volist = service.GetAsync<List<BOMVO>>("api/BOM/RegiItem");

                List<BOMVO> list = volist.Data.FindAll((f) => f.ItemCategory == choice);

                cboPdt.Items.Add("선택");
                cboPdt.SelectedIndex = 0;
                foreach (var lst in list)
                {
                    cboPdt.Items.Add(lst.ItemName);
                }
            }



        }
    }
}
