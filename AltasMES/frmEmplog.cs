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
    public partial class frmEmplog : BaseForm
    {
        ServiceHelper service = null;

        public frmEmplog()
        {
            InitializeComponent();
        }

        private void frmEmplog_Load(object sender, EventArgs e)
        {
            btnAdd.Visible = btnModify.Visible = btnDelete.Visible = false;
            lblTitle.Text = "사용이력";
            groupBox2.Text = "검색조건";
            groupBox3.Text = "이력현황";
            lblSearch.Text = "사용자명";
            this.Text = "사용이력";


            DataGridUtil.SetInitGridView(dgvEmp);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "사용자ID", "EmpID", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "사용자명", "EmpName", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "부서명", "DeptName", colwidth: 150, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "이력", "LogText", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "날짜", "LogDate", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);

            dgvEmp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            service = new ServiceHelper("");
            dgvEmp.DataSource = new AdvancedList<EmplogVO>(service.GetAsync<List<EmplogVO>>("api/Emplog/GetEmplog").Data);
        }

        private void frmEmplog_Shown(object sender, EventArgs e)
        {
            dgvEmp.ClearSelection();
        }

        private void dgvEmp_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvEmp.ClearSelection();
        }
    }
}
