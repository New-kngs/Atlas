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
    public partial class frmPurchase_Add : Form
    {
        ServiceHelper srv = null;

        public PurchaseVO item { get; set; }
        public frmPurchase_Add(PurchaseVO item)
        {
            InitializeComponent();
            this.item = item;
        }

        private void frmPurchase_Add_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (srv != null)
            {
                srv.Dispose();
            }
        }
    }
}
