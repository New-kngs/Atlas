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
    public partial class frmEmplog : BaseForm
    {
        ServiceHelper service = null;
        List<EmplogVO> allList = null;
        List<DepartmentVO> DeptList = null;
        List<EmplogVO> clist = null;

        public frmEmplog()
        {
            InitializeComponent();
        }

        private void frmEmplog_Load(object sender, EventArgs e)
        {
            btnAdd.Visible = btnModify.Visible = btnDelete.Visible = false;
            lblTitle.Text = "사용이력";
            groupBox2.Text = "검색조건";
            groupBox3.Text = "이력현황";
            lblSearch.Text = "사용자명";
            this.Text = "사용이력";


            service = new ServiceHelper("");
 
            allList = service.GetAsync<List<EmplogVO>>("api/Emplog/GetEmplog").Data;
            DeptList = service.GetAsync<List<DepartmentVO>>("api/Department/all").Data;

         
            DataGridUtil.SetInitGridView(dgvEmp);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "사용자ID", "EmpID", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "사용자명", "EmpName", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "부서명", "DeptName", colwidth: 150, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "이력", "LogText", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvEmp, "날짜", "LogDate", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);


            CommonUtil.ComboBinding(cboCategory, DeptList , "DeptName", "DeptN", blankText: "전체");
            cboCategory.SelectedIndex = 0;
         

            dgvEmp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

           
        }

        private void frmEmplog_Shown(object sender, EventArgs e)
        {
            dgvEmp.ClearSelection();
        }

        private void dgvEmp_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvEmp.ClearSelection();
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategory.SelectedIndex == 0)
            {
                dgvEmp.DataSource = null;
                dgvEmp.DataSource = new AdvancedList<EmplogVO>(allList);
            }

            else
            {
                if (allList != null)
                {
                    

                    clist = allList.FindAll(n => n.DeptName.Equals(cboCategory.Text));
                    dgvEmp.DataSource = null;
                    dgvEmp.DataSource = new AdvancedList<EmplogVO>(clist);

                }

            }
            dgvEmp.ClearSelection();
        }

        private void frmEmplog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(service != null)
                service.Dispose();


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSerach.Text))
            {
                cboCategory_SelectedIndexChanged(this, e);
            }


            else
            {
                if (cboCategory.SelectedIndex == 0)
                {
                   
                    clist = allList.FindAll(n => n.EmpName.Contains(txtSerach.Text));

                    dgvEmp.DataSource = null;
                    dgvEmp.DataSource = new AdvancedList<EmplogVO>(clist);
                }

                else
                {
                    List<EmplogVO> list;
                    list = clist.FindAll(n => n.EmpName.Contains(txtSerach.Text));

                    dgvEmp.DataSource = null;
                    dgvEmp.DataSource = new AdvancedList<EmplogVO>(list);
                }

            }

            dgvEmp.ClearSelection();
        }

        private void txtSerach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnSearch_Click(this, e);
        }
    }
}
