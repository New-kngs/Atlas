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
    public partial class frmEquipment_Add : Form
    {
        ServiceHelper service = null;
        public EquipmentVO equip { get; set; }
        public frmEquipment_Add(EquipmentVO equip)
        {
            InitializeComponent();
            this.equip = equip;
        }

        private void frmEquipment_Add_Load(object sender, EventArgs e)
        {
            cboCategory.Items.AddRange(new string[] { "선택", "생산설비", "조립설비", "포장설비" });
            cboCategory.SelectedIndex = 0;
        }

        private void frmEquipment_Add_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEquip.Text))
            {
                MessageBox.Show("설비명을 입력해주세요");
                return;
            }
            if(cboCategory.SelectedIndex == 0)
            {
                MessageBox.Show("설비 유형을 선택하여주시기 바랍니다.");
                return;
            }

            service = new ServiceHelper("api/Equipment");
            string chk = string.Empty;

            EquipmentVO process = new EquipmentVO
            {
                EquipName = txtEquip.Text,
                EquipCategory = cboCategory.Text,
                CreateUser = this.equip.CreateUser
            };

            ResMessage<List<EquipmentVO>> result = service.PostAsync<EquipmentVO, List<EquipmentVO>>("SaveEquip", process);

            if (result.ErrCode == 0)
            {
                MessageBox.Show("성공적으로 등록되었습니다.");
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show(result.ErrMsg);
        }

        private void frmEquipment_Add_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(service != null)
            {
                service.Dispose();
            }
            
        }
    }
}
