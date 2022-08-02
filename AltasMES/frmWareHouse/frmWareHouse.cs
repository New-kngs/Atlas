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
    public partial class frmWarehouse : BaseForm
    {
        ServiceHelper service = null;
        public frmWarehouse()
        {
            InitializeComponent();
        }
        private void frmWarehouse_Load(object sender, EventArgs e)
        {
            DataGridUtil.SetInitGridView(dgvWH);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "창고ID", "WHID", colwidth: 205, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "제품유형", "ItemCategory", colwidth: 180, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "창고이름", "WHName", colwidth: 205, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "생성날짜", "CreateDate", colwidth: 275, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "생성사원", "CreateUser", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "변경날짜", "ModifyDate", colwidth: 275, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "변경사원", "ModifyUser", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "사용여부", "StateYN", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            dgvWH.DefaultCellStyle.Font = new Font("맑은고딕", 12, FontStyle.Regular);

            //ItemID, ItemName, CurrentQty, WHID, ItemCategory, ItemSize
            DataGridUtil.SetInitGridView(dgvPDT);
            DataGridUtil.AddGridTextBoxColumn(dgvPDT, "창고ID", "WHID", colwidth: 300, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPDT, "제품ID", "ItemID", colwidth: 250, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPDT, "제품유형", "ItemCategory", colwidth: 250, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPDT, "제품이름", "ItemName", colwidth: 300, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvPDT, "제품사이즈", "ItemSize", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPDT, "현재재고", "CurrentQty", colwidth: 200, align: DataGridViewContentAlignment.MiddleRight);
            dgvPDT.DefaultCellStyle.Font = new Font("맑은고딕", 12, FontStyle.Regular);

            service = new ServiceHelper("");
            cboWH.Items.AddRange(new string[] { "전체", "완제품", "반제품", "자재" });
            cboWH.SelectedIndex = 0;
                        
            DataLoad();
        }
        private void DataLoad()
        {
            cboWH.SelectedIndex = 0;
            
            ResMessage<List<WareHouseVO>> result = service.GetAsync<List<WareHouseVO>>("api/WareHouse/AllWareHouse");
            if (result != null)
            {
                dgvWH.DataSource = new AdvancedList<WareHouseVO>(result.Data);
                //dgvWH.DataSource = result.Data;
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            WareHouseVO wareHouse = new WareHouseVO()
            {
                CreateUser = ((Main)this.MdiParent).EmpName.ToString()
            };

            frmWareHouse_Add frm = new frmWareHouse_Add(wareHouse);
            //frm.wareHouse = wareHouse;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DataLoad();
            }
        }

        private void frmWarehouse_FormClosing(object sender, FormClosingEventArgs e)
        {
            service.Dispose();
        }


        private void dgvWH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string whid = dgvWH[0, e.RowIndex].Value.ToString();

                ResMessage<List<ItemVO>> resResult = service.GetAsync<List<ItemVO>>($"api/WareHouse/WareHouseInfo/{whid}");
                dgvPDT.DataSource = null;
                dgvPDT.DataSource = resResult.Data;
            }
            else
            {
                return;
            }

        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            WareHouseVO wareHouse = new WareHouseVO()
            {
                WHID = dgvWH.SelectedRows[0].Cells["WHID"].Value.ToString(),
                WHName = (dgvWH.SelectedRows[0].Cells["WHName"].Value).ToString(),
                StateYN = (dgvWH.SelectedRows[0].Cells["StateYN"].Value).ToString(),
                ModifyUser = ((Main)this.MdiParent).EmpName.ToString()
            };

            //frmWareHouse_Delete frm = new frmWareHouse_Delete(wareHouse);
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    DataLoad();
            //}
            if ((dgvWH.SelectedRows[0].Cells["StateYN"].Value).ToString() == "Y")
            {
                frmWareHouse_Delete frm = new frmWareHouse_Delete(wareHouse);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    DataLoad();
                }
            }
            else
            {
                //frmWareHouse_Modify frm = new frmWareHouse_Modify(wareHouse);
                //if (frm.ShowDialog() == DialogResult.OK)
                //{
                //    DataLoad();
                //}
                MessageBox.Show("이미 삭제(미사용)처리된 창고입니다.");
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            WareHouseVO wareHouse = new WareHouseVO()
            {
                WHID = dgvWH.SelectedRows[0].Cells["WHID"].Value.ToString(),
                WHName = (dgvWH.SelectedRows[0].Cells["WHName"].Value).ToString(),
                StateYN = (dgvWH.SelectedRows[0].Cells["StateYN"].Value).ToString(),
                ModifyUser = ((Main)this.MdiParent).EmpName.ToString()
            };

            if ((dgvWH.SelectedRows[0].Cells["StateYN"].Value).ToString() == "N")
            {
                frmWareHouse_Using frmusing = new frmWareHouse_Using(wareHouse);
                if (frmusing.ShowDialog() == DialogResult.OK)
                {
                    DataLoad();
                }
            }
            else
            {
                //frmWareHouse_Modify frm = new frmWareHouse_Modify(wareHouse);
                //if (frm.ShowDialog() == DialogResult.OK)
                //{
                //    DataLoad();
                //}
                MessageBox.Show("이미 사용중인 창고입니다.");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ResMessage<List<WareHouseVO>> volist = service.GetAsync<List<WareHouseVO>>("api/WareHouse/AllWareHouse");

            string category = cboWH.Text;
            string name = txtWH.Text;
            List<WareHouseVO> resultVO = volist.Data.FindAll((r) => r.ItemCategory == category);
            List<WareHouseVO> resultVO1 = volist.Data.FindAll((r) => r.WHName.Contains(name));
            List<WareHouseVO> resultVO2 = resultVO.FindAll((r) => r.WHName.Contains(name));
                        
            if (string.IsNullOrWhiteSpace(txtWH.Text))
            {
                if (cboWH.SelectedIndex == 0)
                {
                    DataLoad();
                }
                else
                {
                    dgvWH.DataSource = new AdvancedList<WareHouseVO>(resultVO);
                }
            }
            else
            {
                if (cboWH.SelectedIndex == 0)
                {
                    dgvWH.DataSource = new AdvancedList<WareHouseVO>(resultVO1);
                }
                else
                {
                    dgvWH.DataSource = new AdvancedList<WareHouseVO>(resultVO2);
                }
            }

        }

        private void cboWH_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvPDT.DataSource = null;
            txtWH.Text = null;
            if (cboWH.SelectedIndex == 0)
            {
                dgvWH.DataSource = null;
                DataLoad();
            }
            else
            {
                ResMessage<List<WareHouseVO>> volist = service.GetAsync<List<WareHouseVO>>("api/WareHouse/AllWareHouse");

                string category = cboWH.Text;
                List<WareHouseVO> resultVO = volist.Data.FindAll((r) => r.ItemCategory == category);
                dgvWH.DataSource = new AdvancedList<WareHouseVO>(resultVO);  
            }

            dgvWH.ClearSelection();
        }

        private void txtWH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSearch_Click(this, e);
            }
        }
    }
}
