using AltasMES;
using AltasPOP;
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

            popDataGridUtil.SetInitGridView(dgvList);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "작업지시ID", "OpID", colwidth: 150, DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "작업지시일시", "OpDate", colwidth: 280);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "공정ID", "ProcessID", colwidth: 150, DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "공정명", "ProcessName", colwidth: 200);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "제품ID", "ItemID", colwidth: 150, DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "주문ID", "OrderID", colwidth: 150, DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "계획수량", "PlanQty", colwidth: 120,DataGridViewContentAlignment.MiddleRight);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "공정상태", "OpState", colwidth: 120, DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "담당ID", "EmpID", colwidth: 150, DataGridViewContentAlignment.MiddleCenter);
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
            this.Close();
        }

        private void frmOperation_FormClosing(object sender, FormClosingEventArgs e)
        {
            POPMain main = (POPMain)this.MdiParent;
            main.ChangeValue();
        }
    }
}

