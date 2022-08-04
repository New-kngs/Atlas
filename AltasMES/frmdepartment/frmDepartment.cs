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


            dgvdept.DefaultCellStyle.Font = new Font("Tahoma", 12, FontStyle.Regular);

            DataLoad();


        }

        private void frmDepartment_FormClosing(object sender, FormClosingEventArgs e)
        {
            service.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {


            if (dgvdept[0, dgvdept.Rows.Count - 1].Value.ToString() == "")
            {
                MessageBox.Show("부서명 입력은 필수 입니다.");
                return;
            }

            dt.Rows.Add((dgvdept.Rows.Count + 1), "", "", "", "", "");

            dgvdept[1, dgvdept.Rows.Count - 1].ReadOnly = false;
            dgvdept[2, dgvdept.Rows.Count - 1].ReadOnly = false;
            dgvdept.CurrentCell = dgvdept[1, dgvdept.Rows.Count - 1];
            dgvdept.BeginEdit(true);
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
            service = new ServiceHelper("");
            allList = service.GetAsync<List<DepartmentVO>>("api/Department/all").Data;

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


            if (!dgvdept.CurrentCell.Selected)
            {
                MessageBox.Show("수정하실 부서를 선택해주세요.", "수정", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }

            int rowIdx = dgvdept.CurrentRow.Index;

            dgvdept[1, rowIdx].ReadOnly = false;
            dgvdept.CurrentCell = dgvdept[1, rowIdx];
            dgvdept.BeginEdit(true);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (!dgvdept.CurrentCell.Selected)
            {
                MessageBox.Show("삭제하실 행을 선택해주세요.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
            if (MessageBox.Show("선택한 행을 삭제하시겠습니까?", "삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dgvdept.Rows.RemoveAt(dgvdept.CurrentRow.Index);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dgvdept.EndEdit();
             
            for (int i = 0; i < dgvdept.Rows.Count; i++)
            {

                if (dgvdept.Rows[i].Cells[1].Value.ToString() == "")
                {
                    MessageBox.Show("부서명은 필수 입력 입니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    dgvdept.CurrentCell = dgvdept[1, i];
                    dgvdept.BeginEdit(true);
                    return;
                }

                if (dgvdept.Rows[i].Cells[2].Value.ToString() == "")
                {
                    MessageBox.Show("부서영문명은 필수 입력 입니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    dgvdept.CurrentCell = dgvdept[2, i];
                    dgvdept.BeginEdit(true);
                    return;
                }

            }

            List<DepartmentVO> list = new List<DepartmentVO>();

            //추가(등록)
            DataRow[] insRows = dt.Select(null, null, DataViewRowState.Added);
            foreach (DataRow dr in insRows)
            {
                DepartmentVO VO = new DepartmentVO
                {

                    DeptName = dr["DeptName"].ToString(),
                    DeptN = dr["DeptN"].ToString(),
                    CreateUser = ((Main)this.MdiParent).EmpName.ToString(),
                    DBType = "INS"

                };

                list.Add(VO);
            }




            //수정
            DataRow[] upsRows = dt.Select(null, null, DataViewRowState.ModifiedCurrent);
            foreach (DataRow dr in upsRows)
            {

                DepartmentVO VO = new DepartmentVO
                {
                    DeptID = Convert.ToInt32(dr["DeptID"]),
                    DeptName = dr["DeptName"].ToString(),
                    DeptN = dr["DeptN"].ToString(),
                    ModifyUser = ((Main)this.MdiParent).EmpName.ToString(),
                    DBType = "UPS"
                };

                list.Add(VO);
            }




            //삭제
            DataRow[] delsRows = dt.Select(null, null, DataViewRowState.Deleted);
            foreach (DataRow dr in delsRows)
            {

                DepartmentVO VO = new DepartmentVO
                {
                    DeptID = Convert.ToInt32(dr[0, DataRowVersion.Original]),
                    DBType = "DEL"
                };

                list.Add(VO);
            }

            if (list.Count < 1)
            {
                MessageBox.Show("변경된 사항이 없습니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ResMessage<List<DepartmentVO>> result = service.PostAsync<List<DepartmentVO>, List<DepartmentVO>>("api/Department/UpdateDepart", list);

            if (result.ErrCode == 0)
            {
                MessageBox.Show("저장이 완료 되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataLoad();

            }
            else
                MessageBox.Show("오류가 발생하였습니다. 다시 시도 하여 주십시오");
        }
    }
}
