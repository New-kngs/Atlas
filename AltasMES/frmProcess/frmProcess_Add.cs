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
    
    public partial class frmProcess_Add : PopUpBase
    {
        ServiceHelper service = null;
        public frmProcess_Add()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProcess.Text))
            {
                MessageBox.Show("공정명을 입력해주세요");
                return;
            }
            if(rdY.Checked == false && rdN.Checked == false)
            {
                MessageBox.Show("불량여부를 체크해주세요");
                return;
            }
            
            service = new ServiceHelper("api/Process");
            string chk = string.Empty;
            if (rdY.Checked)
                chk = rdY.Text;
            else
                chk = rdN.Text;
            ProcessVO process = new ProcessVO
            {
                ProcessName = txtProcess.Text,
                FailCheck = chk
            };

            ResMessage<List<ProcessVO>> result = service.PostAsync<ProcessVO, List<ProcessVO>>("SaveProcess", process);

            if (result.ErrCode == 0)
            {
                MessageBox.Show("성공적으로 등록되었습니다.");
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show(result.ErrMsg);
        }

        private void frmProcess_Add_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(service!=null)
            service.Dispose();
        }

        private void frmProcess_Add_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }
    }
}
