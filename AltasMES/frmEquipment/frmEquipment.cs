﻿using AtlasDTO;
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
            DataGridUtil.SetInitGridView(dgvEquip);
            DataGridUtil.AddGridTextBoxColumn(dgvEquip, "설비ID", "EquipID", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvEquip, "설비명", "EquipName", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvEquip, "설비유형", "EquipCategory", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvEquip, "생성날짜", "CreateDate", colwidth: 200);
            DataGridUtil.AddGridTextBoxColumn(dgvEquip, "생성사용자", "CreateUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvEquip, "변경날짜", "ModifyDate", colwidth: 200);
            DataGridUtil.AddGridTextBoxColumn(dgvEquip, "변경사용자", "ModifyUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvEquip, "사용여부", "StateYN", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);

            LoadData();
        }
        public void LoadData()
        {
            service = new ServiceHelper("api/Equipment");
            ResMessage<List<EquipmentVO>> result = service.GetAsync<List<EquipmentVO>>("AllEquipment");
            if (result.Data != null)
            {
                dgvEquip.DataSource = new AdvancedList<EquipmentVO>(result.Data);
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
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
            service.Dispose();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            EquipmentVO equip = new EquipmentVO()
            {
                EquipID = Convert.ToInt32(dgvEquip.SelectedRows[0].Cells["EquipID"].Value),
                EquipName = (dgvEquip.SelectedRows[0].Cells["EquipName"].Value).ToString(),
                EquipCategory = (dgvEquip.SelectedRows[0].Cells["EquipCategory"].Value).ToString(),
                ModifyUser = ((Main)this.MdiParent).EmpName.ToString()
            };

            frmEquipment_Modify frm = new frmEquipment_Modify(equip);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
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
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ResMessage<List<EquipmentVO>> result = service.GetAsync<List<EquipmentVO>>("AllEquipment");
            if (result.Data != null)
            {
                List<EquipmentVO> list = result.Data.FindAll((p) => p.EquipName.Contains(txtEquip.Text));
                if (list.Count <= 0)
                {
                    MessageBox.Show("검색된 설비가 없습니다. 다시 확인하여 주세요");
                    txtEquip.Text = string.Empty;
                    LoadData();
                    return;
                }
                txtEquip.Text = string.Empty;
                dgvEquip.DataSource = list;
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void txtEquip_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSearch_Click(sender, e);
            }
        }
    }
}
