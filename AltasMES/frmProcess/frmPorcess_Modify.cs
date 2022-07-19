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
    public partial class frmPorcess_Modify : PopUpBase
    {
        public ProcessVO process { get; set; }
        public frmPorcess_Modify(ProcessVO process)
        {
            InitializeComponent();
            this.process = process;
            txtProcess.Text = process.ProcessName;
            if (process.FailCheck == "Y")
                rdY.Checked = true;
            else
                rdN.Checked = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPorcess_Modify_FormClosing(object sender, FormClosingEventArgs e)
        {
            //srv.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}
