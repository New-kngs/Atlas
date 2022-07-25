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
    public partial class frmProcess_Using : PopUpBase
    {
        public ProcessVO process { get; set; }
        ServiceHelper service = null;
        public frmProcess_Using(ProcessVO process)
        {
            InitializeComponent();
            this.process = process;
            txtProcess.Text = process.ProcessName;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsingChk.Text.Trim()))
            {
                MessageBox.Show("문구를 입력해주세요");
                return;
            }

            if (txtProcess.Text.Equals(txtUsingChk.Text))
            {
                service = new ServiceHelper("api/Process");

                ProcessVO process = new ProcessVO
                {
                    ProcessID = this.process.ProcessID
                };

                ResMessage<List<ProcessVO>> result = service.PostAsync<ProcessVO, List<ProcessVO>>("UsingProcess", process);

                if (result.ErrCode == 0)
                {
                    MessageBox.Show("공정을 다시 사용하실 수 있습니다.");
                    this.DialogResult = DialogResult.OK;
                }
                else
                    MessageBox.Show(result.ErrMsg);
            }
            else
            {
                MessageBox.Show("문구를 다시 확인해주세요");
                return;
            }
        }



        private void frmProcess_Using_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }

        private void frmProcess_Using_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (service != null)
                service.Dispose();
        }
    }
}
