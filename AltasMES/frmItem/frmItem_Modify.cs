﻿using System;
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
        string preItemImage = string.Empty;

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
            nmrQty.Text = item.CurrentQty.ToString();
            nmrSafeQty.Text = item.SafeQty.ToString();
            txtCusName.Text = item.CustomerName;
            txtWhName.Text = item.WHName;
            txtExplain.Text = item.ItemExplain;            
            txtImage.Text = item.ItemImage;
            preItemImage = item.ItemImage;
            if (item.ItemImage.Length > 1)
            {
                pictureBox1.ImageLocation = $"{srv.BaseServiceURL}Uploads/{item.ItemImage}";
            }
        }



        //CurrentQty SafeQty ItemPrice ItemImage ItemExplain ModifyDate ModifyUser
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ItemVO item = new ItemVO
            {
                ItemID = txtID.Text,
                CurrentQty = Convert.ToInt32(nmrQty.Text),
                SafeQty = Convert.ToInt32(nmrSafeQty.Text),
                ItemPrice = Convert.ToInt32(txtPrice.Text),                
                ItemExplain = txtExplain.Text,
                ModifyUser = this.item.ModifyUser,
                ItemImage = (preItemImage.Equals(txtImage.Text)) ? "" : txtImage.Text
            };
            ResMessage result = srv.ServerFileUpload("api/Item/UpdateItem", item.ItemImage, item);

            if (string.IsNullOrWhiteSpace(txtPrice.Text.Trim()))
            {
                MessageBox.Show("제품 단가를 입력해주세요");
                return;
            }
            if (string.IsNullOrWhiteSpace(nmrSafeQty.Text))
            {
                MessageBox.Show("제품 안전재고량을 입력해주세요");
                return;
            }
            if (string.IsNullOrWhiteSpace(nmrQty.Text))
            {
                MessageBox.Show("제품 수량을 입력해주세요");
                return;
            }

            if (result.ErrCode == 0)
            {
                //MessageBox.Show("수정 시작하시겠습니까?", "수정확인", MessageBoxButtons.YesNo) == DialogResult.Yes;
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

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }
    }
}
