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
    public partial class frmEquipment_Modify : Form
    {
        ServiceHelper service = null;
        public EquipmentVO equip { get; set; }
        public frmEquipment_Modify(EquipmentVO equip)
        {
            InitializeComponent();
            this.equip = equip;

            txtEquip.Text = equip.EquipName;
            cboCategory.Text = equip.EquipCategory;
        }

        private void frmEquipment_Modify_Load(object sender, EventArgs e)
        {
            cboCategory.Items.AddRange(new string[] { "선택", "생산설비", "조립설비", "포장설비" });
            cboCategory.Text = equip.EquipCategory;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEquip.Text))
            {
                MessageBox.Show("공정명을 입력해주세요");
                return;
            }
            if (cboCategory.SelectedIndex == 0)
            {
                MessageBox.Show("설비 유형을 선택하여주시기 바랍니다.");
                return;
            }

            service = new ServiceHelper("api/Equipment");

            EquipmentVO equip = new EquipmentVO
            {
                EquipName = txtEquip.Text,
                EquipCategory = cboCategory.Text,
                EquipID = this.equip.EquipID,
                ModifyUser = this.equip.ModifyUser
            };

            ResMessage<List<EquipmentVO>> result = service.PostAsync<EquipmentVO, List<EquipmentVO>>("UpdateEquip", equip);

            if (result.ErrCode == 0)
            {
                MessageBox.Show("성공적으로 수정되었습니다.");
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show(result.ErrMsg);
        }

        private void frmEquipment_Modify_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }

        private void frmEquipment_Modify_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (service != null)
            {
                service.Dispose();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
