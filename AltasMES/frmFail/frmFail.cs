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
    public partial class frmFail : BaseForm
    {
        ServiceHelper service = null;

        ResMessage<List<FailVO>> result = null;
        ResMessage<List<ComboItemVO>> failCode = null;
        public frmFail()
        {
            InitializeComponent();
        }


        string[] inputColumn = { "불량ID", "작업지시ID", "제품명", "유형", "불량명", "불량갯수", "생성날짜", "생성사용자", "변경날짜", "변경사용자" };

        private void frmFail_Load(object sender, EventArgs e)
        {
            service = new ServiceHelper("");

            

            DataGridUtil.SetInitGridView(dgvList);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "불량ID", "FailID", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "작업지시ID", "OpID", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "제품ID", "ItemID",visibility :false);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "제품명", "ItemName", colwidth: 150, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "유형", "ItemCategory", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "불량코드", "FailCode", visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "불량명", "FailName", colwidth: 150, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "불량갯수", "FailQty", colwidth: 150, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "생성날짜", "CreateDate", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "생성사용자", "CreateUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "변경날짜", "ModifyDate", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "변경사용자", "ModifyUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);

            failCode = service.GetAsync<List<ComboItemVO>>("api/Fail/GetFailCode");
            CommonUtil.ComboBinding(cboFail, failCode.Data, "불량코드", blankText: "선택");
            cboFail.SelectedIndex = 0;

            dtpTo.Value = DateTime.Now;
            dtpFrom.Value = DateTime.Now.AddDays(-7);

            LoadData();
        }

        public void LoadData()
        {
            result = service.GetAsync<List<FailVO>>("api/Fail/GetFailSearchList/" + dtpFrom.Value.ToShortDateString() + "/" + dtpTo.Value.ToShortDateString());
            if (result.Data != null)
            {
                dgvList.DataSource = new AdvancedList<FailVO>(result.Data);
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
            dgvList.ClearSelection();
        }

        private void cboFail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cboFail.SelectedIndex != 0)
            {
                LoadData();
                result.Data = result.Data.FindAll((f) => f.FailName.Equals(cboFail.Text.Trim())).ToList();
                dgvList.DataSource = new AdvancedList<FailVO>(result.Data);
                dgvList.ClearSelection();
            }
            else
            {
                LoadData();
                dgvList.ClearSelection();
            }
            
        }

        private void frmFail_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (service != null)
            {
                service.Dispose();
            }
        }

        private void frmFail_Shown(object sender, EventArgs e)
        {
            dgvList.ClearSelection();
        }

        private void btnExecl_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Execl Files(*.xls)|*.xls";
            dlg.Title = "엑셀파일로 내보내기";

            if (dlg.ShowDialog() == DialogResult.OK)
            {

                ExcelUtil excel = new ExcelUtil();

                if (excel.ExportExcelGridView(dlg.FileName, dgvList, inputColumn))
                {
                    MessageBox.Show("엑셀 다운로드 완료", "엑셀 다운로드", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }
    }
}
