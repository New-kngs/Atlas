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
    public partial class frmItem_Using : Form
    {
        public ItemVO item { get; set; }
        ServiceHelper srv = null;

        public frmItem_Using(ItemVO item)
        {
            InitializeComponent();
            this.item = item;
            txtID.Text = item.ItemID;            
        }

        private void btnChk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsingChk.Text.Trim()))
            {
                MessageBox.Show("문구를 입력해주세요");
                return;
            }

            if (txtID.Text.Equals(txtUsingChk.Text.Trim()))
            {
                srv = new ServiceHelper("api/Item");
                
                ItemVO item = new ItemVO
                {
                    ItemID = this.item.ItemID,
                    ModifyUser = this.item.ModifyUser
                };
                ResMessage<List<ItemVO>> result = srv.PostAsync<ItemVO, List<ItemVO>>("UsingItem", item);

                if (result.ErrCode == 0)
                {
                    MessageBox.Show("제품을 다시 사용하실 수 있습니다.");
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

        private void frmItem_Using_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (srv != null)
            {
                srv.Dispose();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }    
}
