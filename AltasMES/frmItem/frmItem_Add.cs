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

            itemList = srv.GetAsync<List<ItemVO>>("AllItem");
            comboList = srv.GetAsync<List<ComboItemVO>>("AllItemCategory");

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

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

                //MessageBox.Show(ItemCode);

                txtID.Text = ItemCode;
            }
            

            

        }
    }
}
