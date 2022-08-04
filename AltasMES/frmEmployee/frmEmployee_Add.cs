using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AtlasDTO;

namespace AltasMES
{
    public partial class frmEmployee_Add : Form
    {


        ServiceHelper service = null;
        List<DepartmentVO> DeptList = null;
        List<ComboItemVO> CboList = null;
        string createUser = string.Empty;

        public frmEmployee_Add(string CreateUser)
        {
            createUser = CreateUser;
            InitializeComponent();
        }


        
        private void frmEmployee_Add_Load(object sender, EventArgs e)
        {

            service = new ServiceHelper("");

            DeptList = service.GetAsync<List<DepartmentVO>>("api/Department/all").Data;
            CboList = service.GetAsync<List<ComboItemVO>>("api/Employee/DomainCategory").Data;

            CommonUtil.ComboBinding(cboCategory, DeptList, "DeptName", "DeptN", blankText: "선택");
            CommonUtil.ComboBinding(cboDomain, CboList, "Domain", blankText: "선택");

            txtDomain.Enabled = false;

        }

        private void cboDomain_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboDomain.Text == "직접입력")
            {
                txtDomain.Text = "";
                txtDomain.Enabled = true;
            }

            else
            {
                if(cboDomain.Text == "선택")
                    txtDomain.Text = ""; 
                else
                    txtDomain.Text = cboDomain.Text;

                txtDomain.Enabled = false;
            }

        }

       

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("이름을 입력해주세요.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("ID를 입력해주세요.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPwd.Text))
            {
                MessageBox.Show("비밀번호를 입력해주세요.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("이메일을 입력해주세요.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cboCategory.Text == "선택")
            {
                MessageBox.Show("부서를 선택해주세요.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDomain.Text))
            {
                MessageBox.Show("도메인을 확인해주세요.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            EmployeeVO emp = new EmployeeVO()
            {
                EmpID = txtID.Text,
                EmpName = txtName.Text,
                EmpPhone = mtxtphone.Text,
                EmpPwd = txtPwd.Text,
                EmpEmail = txtEmail.Text + "@" + txtDomain.Text,
                DeptName = DeptList.Find(n => n.DeptName.Equals(cboCategory.Text)).DeptID.ToString(),
                CreateUser = createUser,

            };

            ResMessage<List<EmployeeVO>> result = service.PostAsync<EmployeeVO, List<EmployeeVO>>("api/Employee/SaveEmployee", emp);

            if (result.ErrCode == 0)
            {
                MessageBox.Show("등록이 완료되었습니다.", "사용자 등록", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("오류가 발생하였습니다. 다시 시도 하여 주십시오.");

        }
        private void frmEmployee_Add_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (service != null)
            {
                service.Dispose();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
