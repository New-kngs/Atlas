using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using AtlasDTO;


namespace AltasMES
{
    public partial class frmEmployee : BaseForm
    {

        ServiceHelper service = null;

        public frmEmployee()
        {
            InitializeComponent();
        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {


            DataGridUtil.SetInitGridView(dgvEmp);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "사원ID", "EmpID");
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "사원명", "EmpName");
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "비밀번호", "EmpPwd");
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "연락처", "EmpPhone");
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "이메일", "EmpEmail");
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "부서명", "DeptName");
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "생성날짜", "CreateDate");
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "생성사용자", "CreateUser");
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "수정날짜", "ModifyDate");
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "수정사용자", "ModifyUser");
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "미사용여부", "StateYN");


            lblTitle.Text = "사용자";
            groupBox2.Text = "검색조건";
            groupBox3.Text = "사용자 현황";

            service = new ServiceHelper("api/Employee");
            ResMessage<List<EmployeeVO>> result = service.GetAsync<List<EmployeeVO>>("AllEmployee");
            if (result != null)
            {
                dgvEmp.DataSource = new AdvancedList<EmployeeVO>(result.Data);
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }

        }

        private void frmEmployee_FormClosing(object sender, FormClosingEventArgs e)
        {
            service.Dispose();
        }
    }
}
