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
    public partial class frmWareHouse_Delete : Form
    {
        public WareHouseVO wareHouse { get; set; }
        ServiceHelper service = null;
        public frmWareHouse_Delete(WareHouseVO wareHouse)
        {
            InitializeComponent();
            this.wareHouse = wareHouse;
            txtWH.Text = wareHouse.WHName;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDeleteChk.Text.Trim()))
            {
                MessageBox.Show("창고명을 입력해주세요");
                return;
            }
            
            if (txtWH.Text.Equals(txtDeleteChk.Text))
            {
                service = new ServiceHelper("api/WareHouse");

                WareHouseVO wareHouse = new WareHouseVO
                {
                    WHID = this.wareHouse.WHID
                };

                ResMessage<List<WareHouseVO>> result = service.PostAsync<WareHouseVO, List<WareHouseVO>>("DeleteWareHouse", wareHouse);

                if (result.ErrCode == 0)
                {
                    MessageBox.Show("성공적으로 처리되었습니다.");
                    this.DialogResult = DialogResult.OK;
                }
                else
                    MessageBox.Show(result.ErrMsg);
            }
            else
            {
                MessageBox.Show("창고명을 다시 확인해주세요");
                return;
            }
        }

        private void frmWareHouse_Delete_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (service != null)
                service.Dispose();
        }



        private void frmWareHouse_Delete_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }
    }
}
