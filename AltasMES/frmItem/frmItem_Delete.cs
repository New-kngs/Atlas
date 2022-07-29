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
    public partial class frmItem_Delete : Form
    {
        public ItemVO item { get; set; }
        ServiceHelper srv = null;

        public frmItem_Delete(ItemVO item)
        {
            InitializeComponent();

            srv = new ServiceHelper("");

            this.item = item;
            txtCategory.Text = item.ItemCategory;
            txtID.Text = item.ItemID;
            txtName.Text = item.ItemName;
            txtSize.Text = item.ItemSize;
            txtPrice.Text = item.ItemPrice.ToString();
            txtQty.Text = item.CurrentQty.ToString();
            txtSafeQty.Text = Convert.ToString(item.SafeQty);
            txtCusName.Text = item.CustomerName;
            txtWhName.Text = item.WHName;
            txtExplain.Text = item.ItemExplain;            
            txtImage.Text = item.ItemImage;
            pictureBox1.ImageLocation = $"{srv.BaseServiceURL}Uploads/{item.ItemImage}";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDeleteChk.Text.Trim()))
            {
                MessageBox.Show("문구를 입력해주세요");
                return;
            }
            if (txtID.Text.Equals(txtDeleteChk.Text.Trim()))
            {
                ItemVO item = new ItemVO()
                {
                    ItemID = this.item.ItemID,
                    ModifyUser = this.item.ModifyUser
                };
                ResMessage<List<ItemVO>> result = srv.PostAsync<ItemVO, List<ItemVO>>("api/Item/DeleteItem", item);
                if (result.ErrCode == 0)                {
                    
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

        private void frmItem_Delete_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (srv != null)
            {
                srv.Dispose();
            }
        }        
    }
}
