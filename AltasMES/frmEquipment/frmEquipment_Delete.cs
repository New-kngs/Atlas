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
    public partial class frmEquipment_Delete : Form
    {
        ServiceHelper service = null;
        public EquipmentVO equip { get; set; }
        public frmEquipment_Delete(EquipmentVO equip)
        {
            InitializeComponent();
            this.equip = equip;
            txtEquip.Text = equip.EquipName;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDeleteChk.Text.Trim()))
            {
                MessageBox.Show("문구를 입력해주세요");
                return;
            }

            if (txtEquip.Text.Equals(txtDeleteChk.Text))
            {
                service = new ServiceHelper("api/Equipment");

                EquipmentVO equip = new EquipmentVO
                {
                    EquipID = this.equip.EquipID,
                    ModifyUser = this.equip.ModifyUser
                };

                ResMessage<List<EquipmentVO>> result = service.PostAsync<EquipmentVO, List<EquipmentVO>>("DeleteEquip", equip);

                if (result.ErrCode == 0)
                {
                    MessageBox.Show("성공적으로 삭제되었습니다.");
                    this.DialogResult = DialogResult.OK;
                }
                else
                    MessageBox.Show(result.ErrMsg);
            }
            else
            {
                MessageBox.Show("문구를 다시 확인해주세요");
                return;
            }
        }

        private void frmEquipment_Delete_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }

        private void frmEquipment_Delete_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (service != null)
            {
                service.Dispose();
            }
        }
    }
}
