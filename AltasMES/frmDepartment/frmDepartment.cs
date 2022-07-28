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
    public partial class frmDepartment : BaseForm
    {
        public frmDepartment()
        {
            InitializeComponent();
        }

        ServiceHelper service = null;
        List<DepartmentVO> allList = null;

        private void frmDepartment_Load(object sender, EventArgs e)
        {

            lblTitle.Text = "부서";
            groupBox2.Text = "검색조건";
            groupBox3.Text = "부서현황";

            service = new ServiceHelper("api/Department");

            allList = service.GetAsync<List<DepartmentVO>>("all").Data;

            DataGridUtil.SetInitGridView(dgvdept);
            DataGridUtil.AddGridTextBoxColumn(dgvdept, "부서ID", "DeptID");
            DataGridUtil.AddGridTextBoxColumn(dgvdept, "부서명", "DeptName");
            DataGridUtil.AddGridTextBoxColumn(dgvdept, "생성날짜", "CreateDate");
            DataGridUtil.AddGridTextBoxColumn(dgvdept, "생성사용자", "CreateUser");
            DataGridUtil.AddGridTextBoxColumn(dgvdept, "수정날짜", "ModifyDate");
            DataGridUtil.AddGridTextBoxColumn(dgvdept, "수정사용자", "ModifyUser");


            dgvdept.DataSource = null;
            dgvdept.DataSource = new AdvancedList<DepartmentVO>(allList);

        }

        private void frmDepartment_FormClosing(object sender, FormClosingEventArgs e)
        {
            service.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}
