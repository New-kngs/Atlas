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
            DataGridUtil.AddGridTextBoxColumn(dgvUnreg, "제품유형", "ItemCategory", visibility: false);

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
            //foreach (DataGridViewRow row in dgvParts.Rows)
            //{
            //    row.Cells[2].Value = "ItemID";
            //}
            //제품 추가 버튼 이벤트 등록
            dgvParts.CellClick += DgvParts_CellClick;

            DataGridUtil.SetInitGridView(dgvNew);
            DataGridUtil.AddGridTextBoxColumn(dgvNew, "제품ID", "ItemID", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvNew, "제품이름", "ItemName", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvNew, "수량", "UnitQty", colwidth: 70, align: DataGridViewContentAlignment.MiddleRight, Readonly :false);
            DataGridViewButtonColumn btnB = new DataGridViewButtonColumn();
            btnB.HeaderText = "삭제";
            btnB.Text = "삭제";
            btnB.Width = 70;
            btnB.DefaultCellStyle.Padding = new Padding(5, 1, 5, 1);
            btnB.UseColumnTextForButtonValue = true;
            dgvNew.Columns.Add(btnB);
            dgvNew.CellClick += DgvNew_CellClick;

            service = new ServiceHelper("");

            cboCategory.Items.AddRange(new string[] { "전체", "완제품", "반제품" });
            cboCategory.SelectedIndex = 0;
        }

        private void dgvUnreg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (cboPdt.Visible == false)
            {
                dgvParts.DataSource = null;
                dgvNew.DataSource = null;


                if (e.RowIndex > -1)
                {
                    string size = dgvUnreg["ItemSize", e.RowIndex].Value.ToString();
                    string category = dgvUnreg["ItemCategory", e.RowIndex].Value.ToString();
                    string id = dgvUnreg["ItemID", e.RowIndex].Value.ToString();

                    ResMessage<List<ItemVO>> result = service.GetAsync<List<ItemVO>>("api/Item/AllItem");

                    List<ItemVO> listA = result.Data.FindAll((r) => r.ItemSize == size);
                    List<ItemVO> listB;
                    List<ItemVO> listC;

                    if (category == "완제품")
                    {
                        listC = listA.FindAll((r) => r.ItemCategory == "반제품");
                    }
                    else
                    {
                        listB = listA.FindAll((r) => r.ItemCategory == "자재");
                        if (id.Contains("FR"))
                        {
                            listC = listB.FindAll((r) => r.ItemID.Contains("F"));
                        }
                        else
                        {
                            listC = listB.FindAll((r) => !r.ItemID.Contains("F"));
                        }
                    }
                    dgvParts.DataSource = listC;
                }
                else
                {
                    return;
                }
            }
            else
            {                
                dgvParts.DataSource = null;
                dgvNew.DataSource = null;
                cboPdtListLoad();
            }
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
                            DgvNew_CellClick(this, cellEventArgs);
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
                    DgvNew_CellClick(this, cellEventArgs);
                }
            }
        }

        private void DgvNew_CellClick(object sender, DataGridViewCellEventArgs e)
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
            dgvUnreg.DataSource = null;
            dgvParts.DataSource = null;
            dgvNew.DataSource = null;
            if (cboPdt.Visible == false)
            {                
                if (cboCategory.SelectedIndex == 0)
                {
                    DataLoad();
                }
                else
                {
                    ResMessage<List<BOMVO>> volist = service.GetAsync<List<BOMVO>>("api/BOM/UnregiItem");

                    string category = cboCategory.Text;
                    List<BOMVO> resultVO = volist.Data.FindAll((r) => r.ItemCategory == category);

                    dgvUnreg.DataSource = new AdvancedList<BOMVO>(resultVO);
                }
            }
            else
            {
                cboPdt.Items.Clear();
                cboPdtListLoad();
                if (cboCategory.SelectedIndex == 0)
                {
                    DataLoad();
                }
                else
                {
                    ResMessage<List<BOMVO>> volist = service.GetAsync<List<BOMVO>>("api/BOM/UnregiItem");

                    string category = cboCategory.Text;
                    List<BOMVO> resultVO = volist.Data.FindAll((r) => r.ItemCategory == category);

                    dgvUnreg.DataSource = new AdvancedList<BOMVO>(resultVO);
                }
            }
            dgvUnreg.ClearSelection();
        }

        private void DataLoad()
        {
            ResMessage<List<BOMVO>> result = service.GetAsync<List<BOMVO>>("api/BOM/UnregiItem");
            List<BOMVO> list = result.Data.FindAll((r) => r.ItemCategory.Contains("제품"));

            if (result != null)
            {
                dgvUnreg.DataSource = new AdvancedList<BOMVO>(list);
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (cboCategory.SelectedIndex == 0 )
            {
                MessageBox.Show("복사할 제품유형을 선택하십시오.");
                return;
            }
            //else if (dgvUnreg.SelectedRows.Count < 1)
            //{
            //    MessageBox.Show("복사대상 미등록제품을 선택하십시오.");
            //    return;
            //}
            else
            {
                dgvParts.DataSource = dgvNew.DataSource = null;
                label2.Visible = cboPdt.Visible = true;
                cboPdtListLoad();
            }            
        }
        private void cboPdtListLoad()
        {            
            string category = cboCategory.SelectedItem.ToString();
            //string id = dgvUnreg["ItemID", dgvUnreg.CurrentRow.Index].Value.ToString();

            //ResMessage<List<ItemVO>> result = service.GetAsync<List<ItemVO>>("api/Item/AllItem");
            ResMessage<List<BOMVO>> volist = service.GetAsync<List<BOMVO>>($"api/BOM/RegiItem/{category}");

            //string size = result.Data.Find((f) => f.ItemID.Equals(id)).ItemSize;
            //List<BOMVO> list = volist.Data.FindAll((f) => f.ItemSize == size);

            cboPdt.Items.Clear();
            cboPdt.Items.Add("선택");
            cboPdt.SelectedIndex = 0;
            foreach (var lst in volist.Data)
            {
                cboPdt.Items.Add(lst.ItemName);
            }
        }

        private void cboPdt_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvNew.DataSource = null;
            dgvParts.DataSource = null;
            if (cboPdt.SelectedIndex == 0)
            {
                return;
            }
            else
            {
                ResMessage<List<ItemVO>> result = service.GetAsync<List<ItemVO>>("api/Item/AllItem");

                string name = cboPdt.SelectedItem.ToString();
                string id = result.Data.Find((b) => b.ItemName.Equals(name)).ItemID;
                string category = result.Data.Find((b) => b.ItemName.Equals(name)).ItemCategory;
                string size = result.Data.Find((b) => b.ItemName.Equals(name)).ItemSize;
                List<ItemVO> list = result.Data.FindAll((f) => f.ItemSize == size);

                ResMessage<List<BOMVO>> resResult = service.GetAsync<List<BOMVO>>("api/BOM/AllBOMItem");
                ResMessage<List<BOMVO>> volist = service.GetAsync<List<BOMVO>>($"api/BOM/RegiItem/{"반제품"}");

                if (category == "완제품")
                {
                    dgvNew.DataSource = volist.Data.FindAll((r) => r.ParentID == id);
                    dgvParts.DataSource = resResult.Data.FindAll((f) => f.ItemCategory == "반제품");
                }
                else
                {
                    dgvNew.DataSource = resResult.Data.FindAll((r) => r.ParentID == id);
                    List<ItemVO> listA = list.FindAll((f) => f.ItemCategory == "자재");
                    if (id.Contains("FR"))
                    {
                        dgvParts.DataSource = listA.FindAll((r) => r.ItemID.Contains("F"));
                    }
                    else
                    {
                        dgvParts.DataSource = listA.FindAll((r) => !r.ItemID.Contains("F"));
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            var msg = MessageBox.Show($"현재 BOM설정이 초기화됩니다.", "초기화", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (msg == DialogResult.Yes)
            {
                AllClear();
            }
        }
        private void AllClear()
        {
            label2.Visible = cboPdt.Visible = false;
            dgvNew.DataSource = null;
            dgvParts.DataSource = null;
            cboCategory.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<BOMVO> bomList1 = new List<BOMVO>();
            List<BOMVO> bomList2 = new List<BOMVO>();

            int count = 0;
            if (dgvUnreg.SelectedRows.Count < 1)
            {
                MessageBox.Show("등록할 BOM제품을 확인하여 주십시오.");
                return;
            }
            if (dgvNew.Rows.Count <1)
            {
                MessageBox.Show("등록할 BOM구성을 확인하여 주십시오.");
                return;
            }
                      
            foreach (DataGridViewRow item in dgvNew.Rows)
            {    
                if (item.Cells[2].Value != null)
                {
                    bool check = int.TryParse(item.Cells[2].Value.ToString(), out count);
                    if (!check)
                    {
                        MessageBox.Show("수량은 숫자만 입력하십시오.");
                        item.Cells[2].Value = 0;
                        return;
                    }
                    else if (count < 1)
                    {
                        MessageBox.Show("구성제품의 수량을 확인해 주십시오.");
                        return;
                    }
                }
                
            }

            string category = cboCategory.Text;
            string itemId = dgvUnreg.SelectedRows[0].Cells["ItemID"].Value.ToString();

            if (category == "완제품")
            {
                foreach (DataGridViewRow dr in dgvNew.Rows)
                {
                    BOMVO itemsF = new BOMVO
                    {
                        ItemID = itemId,
                        ParentID = "*",
                        ChildID = dr.Cells["ItemID"].Value.ToString(),
                        UnitQty = 1,
                        CreateUser = this.bom.CreateUser
                    };
                    bomList1.Add(itemsF);

                    BOMVO itemsR = new BOMVO
                    {
                        ItemID = dr.Cells["ItemID"].Value.ToString(),
                        ParentID = itemId,
                        ChildID = "*",
                        UnitQty = 1,
                        CreateUser = this.bom.CreateUser
                    };
                    bomList2.Add(itemsR);
                }

            }
            else
            {
                foreach (DataGridViewRow dr in dgvNew.Rows)
                {
                    BOMVO itemsF = new BOMVO
                    {
                        ItemID = itemId,
                        ParentID = "*",
                        ChildID = dr.Cells["ItemID"].Value.ToString(),
                        UnitQty = 1,
                        CreateUser = this.bom.CreateUser
                    };
                    bomList1.Add(itemsF);

                    BOMVO itemsR = new BOMVO
                    {
                        ItemID = dr.Cells["ItemID"].Value.ToString(),
                        ParentID = itemId,
                        ChildID = "*",
                        UnitQty = Convert.ToInt32(dr.Cells["UnitQty"].Value),
                        CreateUser = this.bom.CreateUser
                    };
                    bomList2.Add(itemsR);
                }
            }

            List<List<BOMVO>> list = new List<List<BOMVO>>();
            list.Add(bomList1);
            list.Add(bomList2);

            ResMessage result = service.PostAsyncNone<List<List<BOMVO>>>("api/BOM/SaveBOM", list);

            if (result.ErrCode == 0)
            {
                MessageBox.Show("성공적으로 등록되었습니다.");
                //this.DialogResult = DialogResult.OK;
                AllClear();
                cboCategory.SelectedIndex = 0;
            }
            else
                MessageBox.Show(result.ErrMsg);
        }
    }
}
