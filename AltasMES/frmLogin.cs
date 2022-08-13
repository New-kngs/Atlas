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
    public partial class frmLogin : Form
    {

        ServiceHelper service = null;
        List<EmployeeVO> list = null;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

            service = new ServiceHelper("");
            list = service.GetAsync<List<EmployeeVO>>("api/Employee/AllEmployee").Data;
           

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("ID를 입력해주세요", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(string.IsNullOrWhiteSpace(txtPwd.Text))
            {
                MessageBox.Show("비밀번호를 입력해주세요", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }


            if(list.Find(n => n.EmpID.Equals(txtID.Text)) == null )
            {
                MessageBox.Show("ID를 확인하여 주세요", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (list.Find(n => n.EmpID.Equals(txtID.Text)) != null)
            {
                if (list.Find(n => n.EmpID.Equals(txtID.Text)).EmpPwd.Equals(txtPwd.Text))
                {

                    ((Main)this.Owner).EmpName = list.Find(m => m.EmpID.Equals(txtID.Text)).EmpName;
                    ((Main)this.Owner).EmpID = txtID.Text;
                    ((Main)this.Owner).DeptName = list.Find(m => m.EmpID.Equals(txtID.Text)).DeptName;
                    this.DialogResult = DialogResult.OK;
                    this.Close();

                }
                else
                {
                    MessageBox.Show("비밀번호를 확인하여 주세요", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnLogin_Click(this, e);
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtPwd.Focus();
        }
    }
}
