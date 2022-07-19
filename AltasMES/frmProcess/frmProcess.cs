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
    public partial class frmProcess : BaseStyle1
    {
        ServiceHelper service = null;
        public frmProcess()
        {
            InitializeComponent();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmProcess_Add frm = new frmProcess_Add();
            frm.ShowDialog();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            frmPorcess_Modify frm = new frmPorcess_Modify();
            frm.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            frmProcess_Delete frm = new frmProcess_Delete();
            frm.ShowDialog();
        }

        private void btnExecl_Click(object sender, EventArgs e)
        {

        }

        private void frmProcess_Load(object sender, EventArgs e)
        {
            DataGridUtil.SetInitGridView(dgvProcess);
            DataGridUtil.AddGridTextBoxColumn(dgvProcess, "공정ID", "ProcessID", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvProcess, "공정명", "ProcessName", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvProcess, "불량확인여부", "FailCheck");
            DataGridUtil.AddGridTextBoxColumn(dgvProcess, "생성날짜", "CreateDate");
            DataGridUtil.AddGridTextBoxColumn(dgvProcess, "생성한사람", "CreateUser", colwidth: 300, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvProcess, "수정날짜", "ModifyDate");
            DataGridUtil.AddGridTextBoxColumn(dgvProcess, "수정한사람", "ModifyUser", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);

            service = new ServiceHelper("api/Process");
            ResMessage<List<ProcessVO>> result = service.GetAsync<List<ProcessVO>>("AllProcess");
            if (result != null)
            {
                dgvProcess.DataSource = new AdvancedList<ProcessVO>(result.Data);
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }
    }
}
