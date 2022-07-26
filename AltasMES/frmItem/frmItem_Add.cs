﻿using AtlasDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AltasMES
{
    public partial class frmItem_Add : Form
    {
        public ItemVO item { get; set; }

        ServiceHelper srv = null;
        List<ItemVO> itemList;          // 전체 아이템
        List<ComboItemVO> comboList;    // 아이템 카테고리 콤보
        List<WareHouseVO> whcomboList;  // 창고 콤보       
        List<CustomerVO> cusList;       // 입고 거래처 콤보

        string ItemCode = string.Empty;        

        public frmItem_Add(ItemVO item)
        {
            InitializeComponent();
            this.item = item;
        }

        private void frmItem_Add_Load(object sender, EventArgs e)
        {            
            cboCategory1.Items.AddRange(new string[] { "선택", "완제품", "반제품", "자재" });
            cboCategory1.SelectedIndex = 0;

            cboSize.Items.AddRange(new string[] { "선택", "S", "D", "Q", "K" });
            cboSize.SelectedIndex = 0;

            srv = new ServiceHelper("");

            cusList = srv.GetAsync<List<CustomerVO>>("api/Customer/AllCustomer").Data;
            whcomboList = srv.GetAsync<List<WareHouseVO>>("api/WareHouse/AllWareHouse").Data;
            itemList = srv.GetAsync<List<ItemVO>>("api/Item/AllItem").Data;
            comboList = srv.GetAsync<List<ComboItemVO>>("api/Item/AllItemCategory").Data;             
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cboCategory2.SelectedIndex < 1)
            {
                MessageBox.Show("제품유형 정확하게 선택을 선택해주세요.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtName.Text.Trim()))
            {
                MessageBox.Show("제품명을 입력해주세요", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string itemName = txtName.Text;
            List<ItemVO> resultName = itemList.FindAll(p => p.ItemName == itemName);
            if (resultName.Count > 0)
            {
                MessageBox.Show("이미 존재하는 제품명 입니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Clear();
                return;
            }            
            if (cboSize.SelectedIndex < 1)
            {
                MessageBox.Show("제품 규격을 선택해주세요.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPrice.Text.Trim()))
            {
                MessageBox.Show("제품 단가를 입력해주세요", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (nmrSafeQty.Value < 1)
            {
                MessageBox.Show("제품 안전재고량을 입력해주세요", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboCusID.Enabled == true && cboCusID.SelectedIndex == 0)
            {
                MessageBox.Show("거래처를 선택해주세요", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboWhID.Enabled == true && cboWhID.SelectedIndex == 0)
            {
                MessageBox.Show("창고를 선택해주세요", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ItemVO item = new ItemVO
            {
                ItemCategory = cboCategory1.Text,
                p_ItemCode = txtID.Text, // LastNumID 를 만들기 위해 // MT
                ItemName = txtName.Text,
                ItemSize = cboSize.Text,
                ItemPrice = Convert.ToInt32(txtPrice.Text),
                SafeQty = Convert.ToInt32(nmrSafeQty.Value),
                CurrentQty = Convert.ToInt32(nmrQty.Value),
                CustomerID = cboCusID.SelectedValue.ToString(),
                WHID = cboWhID.SelectedValue.ToString(),
                ItemExplain = txtExplain.Text,
                CreateUser = this.item.CreateUser
            };
            ResMessage result = srv.ServerFileUpload("api/Item/SaveItem", txtImage.Text, item);

            if (result.ErrCode == 0)
            {
                MessageBox.Show("성공적으로 등록되었습니다", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show(result.ErrMsg);
        }        

        private void cboCategory1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategory1.SelectedIndex < 1)
            {
                cboCategory2.DataSource = cboWhID.DataSource = cboCusID.DataSource = null;               
                cboSize.Enabled = false; // 수정해야함
                return;
            }

            cboSize.Enabled = true;
            CommonUtil.ComboBinding<CustomerVO>(cboCusID, cusList.FindAll(p => p.Category.Equals("입고")), "CustomerName", "CustomerID", blankText: "선택");            

            //선택된 카테고리에 적합한 제품유형 및 창고 바인딩
            string selCategory = cboCategory1.Text.Trim();
            if (comboList != null && whcomboList != null)
            {
                //제품유형
                cboCategory2.DataSource = null;
                CommonUtil.ComboBinding(cboCategory2, comboList, selCategory, blankText: "선택");
                //창고 
                CommonUtil.ComboBinding<WareHouseVO>(cboWhID, whcomboList.FindAll(p => p.ItemCategory.Equals(selCategory)), "WHName", "WHID", blankText: "선택");
            }

            //선택된 카테고리가 자재인 경우 거래처 바인딩
            if (selCategory.Equals("자재") && cusList != null)
            {
                cboCusID.Enabled = true;
            }
            else
            {
                cboCusID.Enabled = false;
                cboCusID.SelectedIndex = 0;
            }
        }      

        private void cboCategory2_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtID.Text = null;                      
            if (cboCategory2.SelectedIndex > 0)
            {
                ItemCode = comboList.Find((c) => c.CodeName.Equals(cboCategory2.Text)).Code;
                txtID.Text = ItemCode;
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmItem_Add_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (srv != null)
            {
                srv.Dispose();
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
