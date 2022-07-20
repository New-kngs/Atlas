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
    public partial class frmProcess_Setting : Form
    {
        public ProcessVO process { get; set; }
        public frmProcess_Setting(ProcessVO process)
        {
            InitializeComponent();

            this.process = process;
            txtProcessName.Text = process.ProcessName;
        }
        private void fmrProcess_Setting_Load(object sender, EventArgs e)
        {
            //설비목록 가져오기
        }
        public void LoadData()
        {
            //필요할려나..?
        }

        


    }
}
