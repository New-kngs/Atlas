using AltasMES;
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
    public delegate void DataGetEventHandler(String data);
    public partial class frmOperation : Form
    {

        public DataGetEventHandler DataSendEvent;
        ServiceHelper service = null;
        public frmOperation()
        {
            InitializeComponent();
        }

        private void frmOperation_Load(object sender, EventArgs e)
        {

            DataGridUtil.SetInitGridView(dgvList);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "공정ID", "OpID", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "공정명", "OpDate", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "불량확인여부", "ItemID", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "생성날짜", "OrderID", colwidth: 200);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "생성사용자", "PlanQty", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "변경날짜", "OpState", colwidth: 200);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "변경사용자", "EmpID", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            LoadData();
        }
        public void LoadData()
        {
            service = new ServiceHelper("api/pop");
            ResMessage<List<OperationVO>> result = service.GetAsync<List<OperationVO>>("AllOperation");
            if (result.Data != null)
            {
                dgvList.DataSource = new AdvancedList<OperationVO>(result.Data);
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {           
            string OpID = dgvList.SelectedRows[0].Cells["OpID"].Value.ToString();


            DataSendEvent(OpID);
            this.DialogResult = DialogResult.OK;
        }
    }
}

