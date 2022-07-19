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
        ServiceHelper service = null;
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
            if (string.IsNullOrWhiteSpace(txtProcess.Text))
            {
                MessageBox.Show("공정명을 입력해주세요");
                return;
            }
            if (rdY.Checked == false && rdN.Checked == false)
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
                ProcessID = this.process.ProcessID,
                ProcessName = txtProcess.Text,
                FailCheck = chk
            };

            ResMessage<List<ProcessVO>> result = service.PostAsync<ProcessVO, List<ProcessVO>>("UpdateProcess", process);

            if (result.ErrCode == 0)
            {
                MessageBox.Show("성공적으로 수정되었습니다.");
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show(result.ErrMsg);
        }
    }
}
