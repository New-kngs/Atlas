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
    public partial class frmEquipment : BaseForm
    {
        ServiceHelper service = null;
        public frmEquipment()
        {
            InitializeComponent();
        }

        private void frmEquipment_Load(object sender, EventArgs e)
        {
            service = new ServiceHelper("");
           
            DataGridUtil.SetInitGridView(dgvEquip);
            DataGridUtil.AddGridTextBoxColumn(dgvEquip, "설비ID", "EquipID", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvEquip, "설비유형", "EquipCategory", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvEquip, "설비명", "EquipName", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);    
            DataGridUtil.AddGridTextBoxColumn(dgvEquip, "생성날짜", "CreateDate", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvEquip, "생성사용자", "CreateUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvEquip, "변경날짜", "ModifyDate", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvEquip, "변경사용자", "ModifyUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvEquip, "사용여부", "StateYN", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            LoadData();
        }
        public void LoadData()
        {
            ResMessage<List<EquipmentVO>> result = service.GetAsync<List<EquipmentVO>>("api/Equipment/AllEquipment");
            if (result.Data != null)
            {
                dgvEquip.DataSource = new AdvancedList<EquipmentVO>(result.Data);
                
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
            dgvEquip.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EquipmentVO equip = new EquipmentVO()
            {
                CreateUser = ((Main)this.MdiParent).EmpName.ToString()
            };

            frmEquipment_Add frm = new frmEquipment_Add(equip);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
            txtEquip.Clear();
        }

        private void frmEquipment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }

        private void frmEquipment_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(service != null)
                service.Dispose();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dgvEquip.CurrentCell == null) return;

            if (!dgvEquip.CurrentCell.Selected)
            {
                MessageBox.Show("수정할 설비을 선택해주세요", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            EquipmentVO equip = new EquipmentVO()
            {
                EquipID = Convert.ToInt32(dgvEquip.SelectedRows[0].Cells["EquipID"].Value),
                EquipName = (dgvEquip.SelectedRows[0].Cells["EquipName"].Value).ToString(),
                EquipCategory = (dgvEquip.SelectedRows[0].Cells["EquipCategory"].Value).ToString(),
                ModifyUser = ((Main)this.MdiParent).EmpName.ToString()
            };

            /*frmEquipment_Modify frm = new frmEquipment_Modify(equip);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }*/

            if ((dgvEquip.SelectedRows[0].Cells["StateYN"].Value).ToString() == "N")
            {
                frmEquipment_Using frmusing = new frmEquipment_Using(equip);
                if (frmusing.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            else
            {
                frmEquipment_Modify frm = new frmEquipment_Modify(equip);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            txtEquip.Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvEquip.CurrentCell == null) return;

            if (!dgvEquip.CurrentCell.Selected)
            {
                MessageBox.Show("미사용할 설비을 선택해주세요", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EquipmentVO equip = new EquipmentVO()
            {
                EquipID = Convert.ToInt32(dgvEquip.SelectedRows[0].Cells["EquipID"].Value),
                EquipName = (dgvEquip.SelectedRows[0].Cells["EquipName"].Value).ToString(),
                EquipCategory = (dgvEquip.SelectedRows[0].Cells["EquipCategory"].Value).ToString(),
                ModifyUser = ((Main)this.MdiParent).EmpName.ToString()
            };
            frmEquipment_Delete frm = new frmEquipment_Delete(equip);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
            txtEquip.Clear();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEquip.Text.Trim()))
            {
                //MessageBox.Show("설비명을 입력해주세요");
            }
            ResMessage<List<EquipmentVO>> result = service.GetAsync<List<EquipmentVO>>("api/Equipment/AllEquipment");
            if (result.Data != null)
            {
                List<EquipmentVO> list = result.Data.FindAll((p) => p.EquipName.Contains(txtEquip.Text));
                if (list.Count >= 0)
                {
                    dgvEquip.DataSource = list;
                }
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
            dgvEquip.ClearSelection();
        }

        private void txtEquip_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void frmEquipment_Shown(object sender, EventArgs e)
        {
            dgvEquip.ClearSelection();
        }

        private void dgvEquip_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvEquip.ClearSelection();
        }

        public void reset()
        {
            txtEquip.Text = string.Empty;
        }
    }
}
