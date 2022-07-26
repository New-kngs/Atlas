using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AtlasDTO;

namespace AltasMES
{
    public partial class frmItem_Modify : Form
    {
        ServiceHelper srv = null;

        public ItemVO item { get; set; }

        public frmItem_Modify(ItemVO item)
        {
            InitializeComponent();
            this.item = item;
            txtCategory.Text = item.ItemCategory;
            txtID.Text = item.ItemID;
            txtName.Text = item.ItemName;
            txtSize.Text = item.ItemSize;
            txtPrice.Text =item.ItemPrice.ToString();
            txtQty.Text = item.CurrentQty.ToString();
            txtSafeQty.Text = Convert.ToString(item.SafeQty);
            txtCusName.Text = item.CustomerName;
            txtWhName.Text = item.WHName;
            txtExplain.Text = item.ItemExplain;
            txtCUser.Text = item.CreateUser;
            txtMUser.Text = item.ModifyUser;
            txtCDate.Text = item.CreateDate;
            txtMDate.Text = item.ModifyDate;
            txtImage.Text = item.ItemImage;

            //txtID.ReadOnly = true;
        }

        //CurrentQty SafeQty ItemPrice ItemImage ItemExplain ModifyDate ModifyUser
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ItemVO item = new ItemVO
            {
                ItemID = txtID.Text,
                CurrentQty = Convert.ToInt32(txtQty.Text),
                SafeQty = Convert.ToInt32(txtSafeQty.Text),
                ItemPrice = Convert.ToInt32(txtPrice.Text),
                ItemImage = txtImage.Text,
                ItemExplain = txtExplain.Text,
                ModifyUser = this.item.ModifyUser
            };
            srv = new ServiceHelper("");
            ResMessage<List<ItemVO>> result = srv.PostAsync<ItemVO, List<ItemVO>>("api/Item/UpdateItem", item);

            if (result.ErrCode == 0)
            {
                MessageBox.Show("성공적으로 수정되었습니다.");
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show(result.ErrMsg);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmItem_Modify_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (srv != null)
            {
                srv.Dispose();
            }
        }
    }
}
