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

namespace AtlasPOP
{
    public partial class Monitoring : UserControl
    {
        public Monitoring(OperationVO oper)
        {
            InitializeComponent();
            lblOper.Text = oper.OpID.ToString();
            lblItem.Text = oper.ItemID;
            lblProc.Text = oper.ProcessName;
            lblProcType.Text = oper.ProcessID.ToString();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
