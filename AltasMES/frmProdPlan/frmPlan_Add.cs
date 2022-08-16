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
    public partial class frmPlan_Add : Form
    {
        ServiceHelper srv = null;
        public frmPlan_Add()
        {
            InitializeComponent();            
        }

        private void frmPlan_Add_Load(object sender, EventArgs e)
        {
            srv = new ServiceHelper("");

            cboCategory.Items.AddRange(new string[] { "선택", "완제품", "반제품", "자재" });
            cboCategory.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cboCategory.SelectedIndex == 0)
            {
                cboItemName.Items.Clear();
            }
            else
            {
                cboItemName.Items.Clear();
                ResMessage<List<ItemVO>> volist = srv.GetAsync<List<ItemVO>>("api/Item/AllItem");

                string category = cboCategory.Text;
                List<ItemVO> resultvo = volist.Data.FindAll((r) => r.ItemCategory == category);
                cboItemName.Items.Add("선택");
                cboItemName.SelectedIndex = 0;
                foreach (var item in resultvo)
                {
                    cboItemName.Items.Add(item.ItemName);
                }
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 13)
                e.Handled = true;
        }
    }
}
