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

        public frmItem_Add()
        {
            InitializeComponent();
        }

        private void frmItem_Add_Load(object sender, EventArgs e)
        {
            srv = new ServiceHelper("api/Item");
            ResMessage<List<ComboItemVO>> result = srv.GetAsync<List<ComboItemVO>>("AllItemCategory");

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
