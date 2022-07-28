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
    public partial class frmOrder_Detail : Form
    {
        public OrderVO order { get; set; }
        ServiceHelper srv = null;

        public frmOrder_Detail(OrderVO order)
        {
            InitializeComponent();
            this.order = order;

        }

        private void frmOrder_Detail_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
