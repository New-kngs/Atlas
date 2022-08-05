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

        }

        private void btnAddr_Click(object sender, EventArgs e)
        {
            ZipcodePopup popup = new ZipcodePopup();
            if (popup.ShowDialog() == DialogResult.OK)
            {
                txtAddr.Text = popup.Address2;
            }
        }
    }
}
