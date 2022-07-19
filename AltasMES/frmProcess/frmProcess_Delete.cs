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
    public partial class frmProcess_Delete : PopUpBase
    {
        public ProcessVO process { get; set; }
        public frmProcess_Delete(ProcessVO process)
        {
            InitializeComponent();
            this.process = process;
            txtProcess.Text = process.ProcessName;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmProcess_Delete_Load(object sender, EventArgs e)
        {

        }
    }
}
