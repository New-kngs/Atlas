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
            DataGridUtil.AddGridTextBoxColumn(dgvList, "작업지시ID", "OpID", colwidth: 200);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "작업지시일시", "OpDate", colwidth: 200);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "공정ID", "ProcessID", colwidth: 200);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "공정명", "ProcessName", colwidth: 200);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "제품ID", "ItemID", colwidth: 150);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "주문ID", "OrderID", colwidth: 200);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "계획수량", "PlanQty", colwidth: 150);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "공정상태", "OpState", colwidth: 200);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "담당ID", "EmpID", colwidth: 150);
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

