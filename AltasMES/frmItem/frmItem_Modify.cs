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

            srv = new ServiceHelper("");

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
            txtImage.Text = item.ItemImage;
            pictureBox1.ImageLocation = $"{srv.BaseServiceURL}Uploads/{item.ItemImage}";
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
                ItemExplain = txtExplain.Text,
                ModifyUser = this.item.ModifyUser
            };
            ResMessage result = srv.ServerFileUpload("api/Item/UpdateItem", txtImage.Text, item);

            if (result.ErrCode == 0)
            {
                MessageBox.Show("성공적으로 수정되었습니다.");
                this.DialogResult = DialogResult.OK;
                this.Close();
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

        private void btnImgFind_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtImage.Text = dlg.FileName;
                pictureBox1.ImageLocation = dlg.FileName;
            }
        }
    }
}
