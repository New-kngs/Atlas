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
    public partial class frmProcess : BaseForm
    {
        ServiceHelper srv = null;
        public frmProcess()
        {
            InitializeComponent();
        }

        private void frmProcess_Load(object sender, EventArgs e)
        {
            srv = new ServiceHelper("");

            DataGridUtil.SetInitGridView(dgvProcess);
            DataGridUtil.AddGridTextBoxColumn(dgvProcess, "공정ID", "ProcessID", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvProcess, "공정명", "ProcessName", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvProcess, "불량확인여부", "FailCheck", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvProcess, "생성날짜", "CreateDate", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvProcess, "생성사용자", "CreateUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvProcess, "변경날짜", "ModifyDate", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvProcess, "변경사용자", "ModifyUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvProcess, "사용여부", "StateYN", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);

            LoadData();
        }
        public void LoadData()
        {
            ResMessage<List<ProcessVO>> result = srv.GetAsync<List<ProcessVO>>("api/Process/AllProcess");
            if (result.Data != null)
            {
                dgvProcess.DataSource = new AdvancedList<ProcessVO>(result.Data);
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProcessVO process = new ProcessVO()
            {
                CreateUser = ((Main)this.MdiParent).EmpName.ToString()
            };
            frmProcess_Add frm = new frmProcess_Add(process);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            ProcessVO process = new ProcessVO()
            {
                ProcessID = Convert.ToInt32(dgvProcess.SelectedRows[0].Cells["ProcessID"].Value),
                ProcessName = (dgvProcess.SelectedRows[0].Cells["ProcessName"].Value).ToString(),
                FailCheck = (dgvProcess.SelectedRows[0].Cells["FailCheck"].Value).ToString(),
                ModifyUser = ((Main)this.MdiParent).EmpName.ToString()
            };

            if ((dgvProcess.SelectedRows[0].Cells["StateYN"].Value).ToString() == "N")
            {
                frmProcess_Using frmusing = new frmProcess_Using(process);
                if (frmusing.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
            else
            {
                frmPorcess_Modify frm = new frmPorcess_Modify(process);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if ((dgvProcess.SelectedRows[0].Cells["StateYN"].Value).ToString() == "N")
            {
                MessageBox.Show("이미 삭제된 공정입니다.");
                return;
            }

            ProcessVO process = new ProcessVO()
            {
                ProcessID = Convert.ToInt32(dgvProcess.SelectedRows[0].Cells["ProcessID"].Value),
                ProcessName = (dgvProcess.SelectedRows[0].Cells["ProcessName"].Value).ToString(),
                FailCheck = (dgvProcess.SelectedRows[0].Cells["FailCheck"].Value).ToString(),
                ModifyUser = ((Main)this.MdiParent).EmpName.ToString()
            };
            frmProcess_Delete frm = new frmProcess_Delete(process);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
        private void frmProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (srv != null)
            {
                srv.Dispose();
            }
        }


        private void btnSetting_Click(object sender, EventArgs e)
        {
            ProcessVO process = new ProcessVO()
            {
                ProcessID = Convert.ToInt32(dgvProcess.SelectedRows[0].Cells["ProcessID"].Value),
                ProcessName = (dgvProcess.SelectedRows[0].Cells["ProcessName"].Value).ToString(),
                FailCheck = (dgvProcess.SelectedRows[0].Cells["FailCheck"].Value).ToString(),
                CreateUser = ((Main)this.MdiParent).EmpName.ToString()
            };

            frmProcess_Setting frm = new frmProcess_Setting(process);
            frm.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProcessName.Text.Trim()))
            {
                MessageBox.Show("공정명을 입력해주세요.");
            }
            ResMessage<List<ProcessVO>> result = srv.GetAsync<List<ProcessVO>>("api/Process/AllProcess");
            if (result.Data != null)
            {
                List<ProcessVO> list = result.Data.FindAll((p) => p.ProcessName.Contains(txtProcessName.Text));
                if(list.Count >= 0)
                {
                    dgvProcess.DataSource = list;
                }
                
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void frmProcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b' && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }

        private void txtProcessName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtProcessName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSearch_Click(sender, e);
            }
        }
    }
}
