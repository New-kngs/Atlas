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
    public partial class frmItem_Add : Form
    {
        ServiceHelper srv = null;
        ResMessage<List<ComboItemVO>> comboList;
        ResMessage<List<ItemVO>> itemList;

        string ItemCode = string.Empty;
        string Item = "";
        public frmItem_Add()
        {
            InitializeComponent();
        }

        private void frmItem_Add_Load(object sender, EventArgs e)
        {
            srv = new ServiceHelper("api/Item");

            cboCategory1.Items.AddRange(new string[] { "선택", "완제품", "반제품", "자재" });
            cboCategory1.SelectedIndex = 0;

            cboSize.Items.AddRange(new string[] { "선택", "S", "D", "Q", "K" });
            cboSize.SelectedIndex = 0;

            cboCusName.Items.AddRange(new string[] { "선택", "테스트1" });
            cboCusName.SelectedIndex = 0;

            cboWhName.Items.AddRange(new string[] { "선택", "테스트2" });
            cboWhName.SelectedIndex = 0;

            itemList = srv.GetAsync<List<ItemVO>>("AllItem");
            comboList = srv.GetAsync<List<ComboItemVO>>("AllItemCategory");

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("제품명을 입력해주세요");
                return;
            }

            srv = new ServiceHelper("api/Item");
            ResMessage<List<ItemVO>> result = srv.GetAsync<List<ItemVO>>("AllItem");

            string itemName = txtName.Text;
            List<ItemVO> resultName = result.Data.FindAll(p => p.ItemName == itemName);
            if (resultName.Count > 0)
            {
                MessageBox.Show("존재하는 제품명 입니다.");
                txtName.Clear();
                return;
            }
            if (cboCategory1.SelectedIndex == 0 && cboCategory2.SelectedIndex == 0)
            {
                MessageBox.Show("제품유형을 선택하여주시기 바랍니다.");
                return;
            }

            ItemVO item = new ItemVO
            {
                ItemCategory = cboCategory1.Text,
                ItemID = txtID.Text,
                ItemName = txtName.Text,
                ItemSize = cboSize.Text,
                ItemPrice = Convert.ToInt32(txtPrice.Text),
                SafeQty = Convert.ToInt32(txtQty.Text),
                CurrentQty = Convert.ToInt32(txtQty.Text),
                CustomerName = cboCusName.Text,
                WHName = cboWhName.Text,
                ItemExplain = txtExplain.Text,
                ItemImage = txtImage.Text                
            };

            ResMessage<List<ItemVO>> resultItem = srv.PostAsync<ItemVO, List<ItemVO>>("saveItem", item);

            if (result.ErrCode == 0)
            {
                MessageBox.Show("성공적으로 등록되었습니다.");
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show(result.ErrMsg);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboCategory1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (cboCategory1.SelectedIndex == 1)
            {                
                if (comboList != null)
                    CommonUtil.ComboBinding(cboCategory2, comboList.Data, "완제품", blankText: "선택");
            }
            
            if (cboCategory1.SelectedIndex == 2)
            {                
                if (comboList != null)
                    CommonUtil.ComboBinding(cboCategory2, comboList.Data, "반제품", blankText: "선택");
            }
            
            if (cboCategory1.SelectedIndex == 3)
            {               
                if (comboList != null)
                    CommonUtil.ComboBinding(cboCategory2, comboList.Data, "자재", blankText: "선택");
            }            
        }

        private void cboCategory2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboCategory2.SelectedIndex != 0)
            {
                ItemCode = comboList.Data.Find((c) => c.CodeName.Equals(cboCategory2.Text)).Code;
                Item = itemList.Data.Find((c) => c.ItemID.Contains(ItemCode)).ItemID;
                int list =Convert.ToInt32(itemList.Data.Find((c) => c.ItemID.Contains(ItemCode)).ItemID.Substring(2,3).Max());
                
                

                int num = Convert.ToInt32(Item.Substring(2, Item.Length - 2));

                
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
    }
}
