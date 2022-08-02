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
    public partial class frmDepartment : BaseForm
    {
        public frmDepartment()
        {
            InitializeComponent();
        }

        ServiceHelper service = null;
        List<DepartmentVO> allList = null;
        DataTable dt = null;

        private void frmDepartment_Load(object sender, EventArgs e)
        {

            lblTitle.Text = "부서";
            groupBox2.Text = "검색조건";
            groupBox3.Text = "부서현황";

            DataLoad();


            //DataGridUtil.SetInitGridView(dgvdept);
            //DataGridUtil.AddGridTextBoxColumn(dgvdept, "부서ID", "DeptID");
            //DataGridUtil.AddGridTextBoxColumn(dgvdept, "부서명", "DeptName");
            //DataGridUtil.AddGridTextBoxColumn(dgvdept, "생성날짜", "CreateDate");
            //DataGridUtil.AddGridTextBoxColumn(dgvdept, "생성사용자", "CreateUser");
            //DataGridUtil.AddGridTextBoxColumn(dgvdept, "수정날짜", "ModifyDate");
            //DataGridUtil.AddGridTextBoxColumn(dgvdept, "수정사용자", "ModifyUser");


            //dgvdept.DataSource = null;
            //dgvdept.DataSource = new AdvancedList<DepartmentVO>(allList);







        }

        private void frmDepartment_FormClosing(object sender, FormClosingEventArgs e)
        {
            service.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (dgvdept[0, dgvdept.Rows.Count - 1].Value.ToString() == "")
            {
                MessageBox.Show("부서ID 입력은 필수 입니다.");
                return;
            }


            if (dgvdept[0, dgvdept.Rows.Count - 1].Value.ToString() == "")
            {
                MessageBox.Show("부서명 입력은 필수 입니다.");
                return;
            }

            dt.Rows.Add("", "", "", "", "", "");

            dgvdept[0, dgvdept.Rows.Count - 1].ReadOnly = false;
            dgvdept[1, dgvdept.Rows.Count - 1].ReadOnly = false;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<DepartmentVO> list;

            if (string.IsNullOrWhiteSpace(txtSerach.Text.Trim()))
            {
                DataLoad();
            }

            else
            {
                list = allList.FindAll(n => n.DeptName.Contains(txtSerach.Text.Trim()));
                dt = CommonUtil.LinqQueryToDataTable(list); 
                dgvdept.DataSource = dt;
            }


        }

        private void DataLoad()
        {
            service = new ServiceHelper("api/Department/");
            allList = service.GetAsync<List<DepartmentVO>>("all").Data;

            dt = CommonUtil.LinqQueryToDataTable(allList); //DataTable
            dgvdept.DataSource = dt;
          

        }

        private void dgvdept_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.CellStyle.BackColor = Color.Yellow;
        }

        private void frmDepartment_Shown(object sender, EventArgs e)
        {
            dgvdept.ClearSelection();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            int rowIdx = dgvdept.CurrentRow.Index;

            dgvdept[0, rowIdx].ReadOnly = false;
            dgvdept[1, rowIdx].ReadOnly = false;
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            dgvdept.Rows.RemoveAt(dgvdept.CurrentRow.Index);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dgvdept.EndEdit();


            List<DepartmentVO> adlist = new List<DepartmentVO>();

            //추가(등록)
            DataRow[] insRows = dt.Select(null, null, DataViewRowState.Added);
            foreach (DataRow dr in insRows)
            {
                DepartmentVO VO = new DepartmentVO
                {
                    DeptID = dr["DeptID"].ToString(),
                    DeptName = dr["DeptName"].ToString(),
                };

                adlist.Add(VO);
            }


            List<DepartmentVO> uplist = new List<DepartmentVO>();

            //수정
            DataRow[] upsRows = dt.Select(null, null, DataViewRowState.ModifiedCurrent);
            foreach (DataRow dr in upsRows)
            {

                DepartmentVO VO = new DepartmentVO
                {
                    DeptID = dr["DeptID"].ToString(),
                    DeptName = dr["DeptName"].ToString(),
                };

                uplist.Add(VO);
            }


            List<DepartmentVO> delist = new List<DepartmentVO>();

            //삭제
            DataRow[] delsRows = dt.Select(null, null, DataViewRowState.Deleted);
            foreach (DataRow dr in delsRows)
            {

                DepartmentVO VO = new DepartmentVO
                {
                    DeptID = dr[0,DataRowVersion.Original].ToString(),
                };

                delist.Add(VO);
            }
        }
    }
}
