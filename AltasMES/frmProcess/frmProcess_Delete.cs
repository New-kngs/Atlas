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
        ServiceHelper service = null;
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDeleteChk.Text.Trim()))
            {
                MessageBox.Show("문구를 입력해주세요");
                return;
            }

            if (txtProcess.Text.Equals(txtDeleteChk.Text))
            {
                service = new ServiceHelper("api/Process");

                ProcessVO process = new ProcessVO
                {
                    ProcessID = this.process.ProcessID,
                    ModifyUser = this.process.ModifyUser
                };

                ResMessage<List<ProcessVO>> result = service.PostAsync<ProcessVO, List<ProcessVO>>("DeleteProcess", process);

                if (result.ErrCode == 0)
                {
                    MessageBox.Show("성공적으로 삭제되었습니다.");
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

        private void frmProcess_Delete_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(service != null) 
                service.Dispose();
        }

        private void frmProcess_Delete_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtDeleteChk_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtProcess_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
