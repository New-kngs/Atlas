﻿using AtlasDTO;
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
    public partial class frmCustomer_Add : Form
    {

        List<ComboItemVO> domainlist = null;
        List<EmployeeVO> saleslist = null;
        ServiceHelper service = null;
        string createUser = string.Empty;


        public frmCustomer_Add(string CreateUser)
        {

            createUser = CreateUser;
            InitializeComponent();
        }

        private void frmCustomer_Add_Load(object sender, EventArgs e)
        {

            service = new ServiceHelper("");
            domainlist =  service.GetAsync<List<ComboItemVO>>("api/Employee/DomainCategory").Data;
            saleslist = service.GetAsync<List<EmployeeVO>>("/api/Employee/GetSalesEmplist").Data;


            CommonUtil.ComboBinding(cboEmpName, saleslist, "EmpName", "EmpID", blankText: "선택");
            CommonUtil.ComboBinding(cboDomain, domainlist, "Domain", blankText: "선택");

            cboCategory.Items.AddRange(new string[] { "선택", "입고", "출고"});
            cboCategory.SelectedIndex = 0;

            txtZipcode.Enabled = false;

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("거래처명을 입력해주세요.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            CustomerVO VO = new CustomerVO()
            {
                CustomerID = txtID.Text,
                CustomerName = txtName.Text,
                CustomerPwd = txtPwd.Text,
                Category = cboCategory.Text,
                CreateUser = createUser,
                Address = txtAddr.Text,
                Phone = mtxtphone.Text,
                Email = txtEmail.Text + "@" + txtDomain.Text,
                EmpID = saleslist.Find(n=> n.EmpName.Equals(cboEmpName.Text)).EmpID.ToString(),

                        
            };

            ResMessage<List<CustomerVO>> result = service.PostAsync<CustomerVO, List<CustomerVO>>("api/Customer/SaveCustomer", VO);


            if (result.ErrCode == 0)
            {
                MessageBox.Show("등록이 완료되었습니다.", "거래처 등록", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("오류가 발생하였습니다. 다시 시도 하여 주십시오.");
        }

        private void frmCustomer_Add_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (service != null)
            {
                service.Dispose();
            }
        }
    }
}
