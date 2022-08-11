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

namespace AtlasPOP
{
    public partial class frmLogin : Form
    {
        popServiceHelper service = null;

        public string UserName { get; set; }
        public string DeptName { get; set; }
        public frmLogin()
        {
            InitializeComponent();
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
#if DEBUG
            txtID.Text = "MA1234";
            txtPWD.Text = "1111";
#endif


            service = new popServiceHelper("");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            ResMessage<List<EmployeeVO>> empList = service.GetAsync<List<EmployeeVO>>("api/Employee/AllEmployee");
            if (empList.ErrCode == 0)
            {
                
            }
            else
            {
                MessageBox.Show("문제가 발생하였습니다. 관리자에게 문의바랍니다.");
            }
            

            try
            {
                int IsID = empList.Data.FindIndex((s) => s.EmpID.Equals(txtID.Text));
                int IsPWD = empList.Data.FindIndex((s) => s.EmpPwd.Equals(txtPWD.Text));
                if (string.IsNullOrWhiteSpace(txtID.Text) || IsID < 0)
                {
                    MessageBox.Show("아이디를 확인해주세요");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtPWD.Text) || IsPWD < 0)
                {
                    MessageBox.Show("비밀번호를 확인해주세요");
                    return;
                }

                if (IsID > 0 && IsPWD > 0)
                {
                    string IsDept = empList.Data.Find((d) => d.EmpID.Equals(txtID.Text)).DeptName;

                    if (!IsDept.Equals("생산부서"))
                    {
                        MessageBox.Show("생산부서만 로그인 가능합니다.");
                        return;
                    }
                    else
                    {
                        ((AtlasPOP)this.Owner).User = empList.Data.Find((f) => f.EmpID.Equals(txtID.Text)).EmpName;
                        ((AtlasPOP)this.Owner).Dept = empList.Data.Find((f) => f.EmpID.Equals(txtID.Text)).DeptName;

                        this.Close();
                        this.DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    MessageBox.Show("로그인에 실패하였습니다.");
                    return;
                }
            }catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
