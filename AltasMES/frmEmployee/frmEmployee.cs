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
        List<DepartmentVO> DeptList = null;

        public frmEmployee()
        {
            InitializeComponent();
        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {

            service = new ServiceHelper("");

            allList = service.GetAsync<List<EmployeeVO>>("api/Employee/AllEmployee").Data;
            DeptList = service.GetAsync<List<DepartmentVO>>("api/Department/all").Data;

             
            DataGridUtil.SetInitGridView(dgvEmp);

            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "Eid", "Eid",visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "사용자ID", "EmpID", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "사용자이름", "EmpName", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "부서명", "DeptName", colwidth: 150, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "비밀번호", "EmpPwd", visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "연락처", "EmpPhone", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "이메일", "EmpEmail", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "생성날짜", "CreateDate", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "생성사용자", "CreateUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "수정날짜", "ModifyDate", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "수정사용자", "ModifyUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "사용여부", "StateYN", visibility: false);

            EmployeeFormInit();


        }

        private void EmployeeFormInit()
        {
            CommonUtil.ComboBinding(cboCategory, DeptList, "DeptName", "DeptN", blankText: "전체");


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

        private void btnAdd_Click(object sender, EventArgs e)
        {

            frmEmployee_Add frm = new frmEmployee_Add(((Main)this.MdiParent).EmpName.ToString());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DataRebinding();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (dgvEmp.CurrentCell == null) return;
            if (dgvEmp.CurrentCell == null)
            {
                MessageBox.Show("수정하실 사용자를 선택해주세요.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            EmployeeVO emp = new EmployeeVO()
            {
                Eid = Convert.ToInt32(dgvEmp.SelectedRows[0].Cells["Eid"].Value),
                EmpID = dgvEmp.SelectedRows[0].Cells["EmpID"].Value.ToString(),
                EmpName = dgvEmp.SelectedRows[0].Cells["EmpName"].Value.ToString(),
                DeptName = dgvEmp.SelectedRows[0].Cells["DeptName"].Value.ToString(),
                EmpPwd = dgvEmp.SelectedRows[0].Cells["EmpPwd"].Value.ToString(),
                EmpPhone = dgvEmp.SelectedRows[0].Cells["EmpPhone"].Value.ToString(),
                EmpEmail = dgvEmp.SelectedRows[0].Cells["EmpEmail"].Value.ToString(),
                ModifyUser = ((Main)this.MdiParent).EmpName.ToString(),
            };

            frmEmployee_Modify frm = new frmEmployee_Modify(emp);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DataRebinding();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dgvEmp.CurrentCell == null)
           {
                MessageBox.Show("삭제하실 사용자를 선택해주세요.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
           }
           
            if (MessageBox.Show($"사용자 : {dgvEmp.SelectedRows[0].Cells["EmpName"].Value} 대해 삭제 하시겠습니까?", "사용자 삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                EmployeeVO emp = new EmployeeVO()
                {
                    Eid = Convert.ToInt32(dgvEmp.SelectedRows[0].Cells["Eid"].Value)
                };


                ResMessage<List<EmployeeVO>> result = service.PostAsync<EmployeeVO, List<EmployeeVO>>("api/Employee/DeleteEmployee", emp);

                if (result.ErrCode == 0)
                {
                    MessageBox.Show("삭제가 완료되었습니다.", "사용자 삭제", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    DataRebinding();

                }
                else
                    MessageBox.Show("오류가 발생하였습니다. 다시 시도 하여 주십시오");
            }

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

        private void DataRebinding()
        {
            allList = service.GetAsync<List<EmployeeVO>>("api/Employee/AllEmployee").Data;
            dgvEmp.DataSource = null;
            dgvEmp.DataSource = new AdvancedList<EmployeeVO>(allList);
            dgvEmp.ClearSelection();
            cboCategory.Text = "전체";
        }

        private void dgvEmp_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvEmp.ClearSelection();
        }

        private void txtSerach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSearch_Click(this, e);
            }
        }
    }
}
