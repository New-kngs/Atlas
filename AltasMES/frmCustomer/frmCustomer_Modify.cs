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
    public partial class frmCustomer_Modify : Form
    {
        List<ComboItemVO> domainlist = null;
        List<EmployeeVO> saleslist = null;
        ServiceHelper service = null;
        public CustomerVO VO { get; set; }
        public frmCustomer_Modify(CustomerVO vo)
        {
            InitializeComponent();
            this.VO = vo;
        }

        private void frmCustomer_Modify_Load(object sender, EventArgs e)
        {

            service = new ServiceHelper("");
            domainlist = service.GetAsync<List<ComboItemVO>>("api/Employee/DomainCategory").Data;
            saleslist = service.GetAsync<List<EmployeeVO>>("/api/Employee/GetSalesEmplist").Data;


            CommonUtil.ComboBinding(cboEmpName, saleslist, "EmpName", "EmpID", blankText: "선택");
            CommonUtil.ComboBinding(cboDomain, domainlist, "Domain", blankText: "선택");

            cboCategory.Items.AddRange(new string[] { "선택", "입고", "출고" });
            cboCategory.SelectedIndex = 0;

            txtZipcode.Enabled = false;

            txtName.Text = VO.CustomerName;
            txtID.Text = VO.CustomerID;
            txtPwd.Text = VO.CustomerPwd;
            txtAddr.Text = VO.Address;
            cboCategory.Text = VO.Category;
            txtDomain.Text = VO.Email.Substring(VO.Email.IndexOf("@") + 1);
            txtEmail.Text = VO.Email.Substring(0, VO.Email.IndexOf("@"));
            cboEmpName.Text = VO.EmpName;
            mtxtphone.Text = VO.Phone;

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
                if (cboDomain.Text == "선택")
                    txtDomain.Text = "";
                else
                    txtDomain.Text = cboDomain.Text;

                txtDomain.Enabled = false;
            }
        }

        private void btnAddr_Click(object sender, EventArgs e)
        {
            ZipcodePopup popup = new ZipcodePopup();
            if (popup.ShowDialog() == DialogResult.OK)
            {
                txtZipcode.Text = popup.Address1;
                txtAddr.Text = popup.Address2;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
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
                MessageBox.Show("거래처 구분을 선택해주세요.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDomain.Text))
            {
                MessageBox.Show("도메인을 확인해주세요.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboEmpName.Text == "선택")
            {
                MessageBox.Show("담당자를 선택해주세요.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAddr.Text))
            {
                MessageBox.Show("주소를 확인해주세요.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CustomerVO cus = new CustomerVO()
            {
                CustomerID = txtID.Text,
                CustomerName = txtName.Text,
                CustomerPwd = txtPwd.Text,
                Category = cboCategory.Text,
                ModifyUser = VO.ModifyUser,
                Address = txtAddr.Text,
                Phone = mtxtphone.Text,
                Email = txtEmail.Text + "@" + txtDomain.Text,
                EmpID = saleslist.Find(n => n.EmpName.Equals(cboEmpName.Text)).EmpID.ToString(),

            };

            ResMessage<List<CustomerVO>> result = service.PostAsync<CustomerVO, List<CustomerVO>>("api/Customer/UpdateCustomer", cus);

            if (result.ErrCode == 0)
            {
                MessageBox.Show("수정이 완료되었습니다.", "거래처 수정", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show(result.ErrMsg);
        }
    }
}
