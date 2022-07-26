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
            txtPrice.Text = Convert.ToString(item.ItemPrice);
            txtQty.Text = Convert.ToString(item.CurrentQty);
            txtSafeQty.Text = Convert.ToString(item.SafeQty);
            txtCusName.Text = item.CustomerName;
            txtWhName.Text = item.WHName;
            txtExplain.Text = item.ItemExplain;
            txtCUser.Text = item.CreateUser;
            txtMUser.Text = item.ModifyUser;
            txtCDate.Text = item.CreateDate;
            txtMDate.Text = item.ModifyDate;
            txtImage.Text = item.ItemImage;       
        }        

        private void btnUpdate_Click(object sender, EventArgs e)
        {

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
