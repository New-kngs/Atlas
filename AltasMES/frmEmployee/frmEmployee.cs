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
        List<EmployeeVO> allList = null;
        List<EmployeeVO> cList = null;
        

        public frmEmployee()
        {
            InitializeComponent();
        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {

            service = new ServiceHelper("api/Employee/");
            ResMessage<List<EmployeeVO>> result = service.GetAsync<List<EmployeeVO>>("AllEmployee");
            if (result != null)
            {
                allList = result.Data;
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
                return;
            }



            DataGridUtil.SetInitGridView(dgvEmp);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "사용자아이디", "EmpID");
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "사용자이름", "EmpName");
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "부서명", "DeptName");
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "비밀번호", "EmpPwd", visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "연락처", "EmpPhone");
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "이메일", "EmpEmail");
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "생성날짜", "CreateDate");
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "생성사용자", "CreateUser");
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "수정날짜", "ModifyDate");
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "수정사용자", "ModifyUser");
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "사용여부", "StateYN");

            EmployeeFormInit();


        }

        private void EmployeeFormInit()
        {
            cboCategory.Items.AddRange(new string[] { "전체", "관리자","임원", "영업", "생산" });

            cboCategory.SelectedIndex = 0;

            lblTitle.Text = "사용자";
            groupBox2.Text = "검색조건";
            groupBox3.Text = "사용자 현황";

        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboCategory.SelectedIndex == 0)
            {
                dgvEmp.DataSource = null;
                dgvEmp.DataSource = new AdvancedList<EmployeeVO>(allList);
            }

            else
            {
                if(allList != null)
                {
                    cList = allList.FindAll(n => n.DeptName.Equals(cboCategory.Text));
                    dgvEmp.DataSource = null;
                    dgvEmp.DataSource = new AdvancedList<EmployeeVO>(cList);

                }

            }

            dgvEmp.ClearSelection();

        }

        private void frmEmployee_Shown(object sender, EventArgs e)
        {
            dgvEmp.ClearSelection();
        }

        private void frmEmployee_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (service != null)
            {
                service.Dispose();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            List<EmployeeVO> list;

            if (string.IsNullOrWhiteSpace(txtSerach.Text))
            {
                cboCategory_SelectedIndexChanged(this, e);
            }

           
            else
            {
                if(cboCategory.SelectedIndex == 0)
                {

                    cList = allList.FindAll(n => n.EmpName.Contains(txtSerach.Text));

                    dgvEmp.DataSource = null;
                    dgvEmp.DataSource = new AdvancedList<EmployeeVO>(cList);
                }

                else
                {
                    list =  cList.FindAll(n => n.EmpName.Contains(txtSerach.Text));

                    dgvEmp.DataSource = null;
                    dgvEmp.DataSource = new AdvancedList<EmployeeVO>(list);

                }

            }


            
        }
    }
}
