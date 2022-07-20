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
        //ResMessage<List<ComboItemVO>> categoryList = null;

        public frmItem_Add()
        {
            InitializeComponent();
        }

        private void frmItem_Add_Load(object sender, EventArgs e)
        {
            string[] category = { "완제품", "반제품", "자재" };

            srv = new ServiceHelper("api/Item");
            //categoryList = srv.GetAsync<List<ComboItemVO>>("AllItemCategory");
            //CommonUtil.ComboBinding(cboCategory, categoryList, "완제품", blankText: "전체");
           

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
