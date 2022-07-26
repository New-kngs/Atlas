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
            service = new ServiceHelper("");
            popDataGridUtil.SetInitGridView(dgvList);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "작업지시ID", "OpID", colwidth: 130, DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "작업지시일시", "OpDate", colwidth: 250);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "공정ID", "ProcessID", colwidth: 130, DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "공정명", "ProcessName", colwidth: 190);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "제품ID", "ItemID", colwidth: 130, DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "주문ID", "OrderID", colwidth: 130, DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "계획수량", "PlanQty", colwidth: 150,DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "공정상태", "OpState", colwidth: 120, DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "자재투입\n  여부", "resourceYN", colwidth: 120, DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "담당ID", "EmpID", colwidth: 130, DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "날짜", "Date", visibility:false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "시간", "Time", visibility: false);
            LoadData();
            TimeComboInit();
        }
        public void LoadData()
        {
            
            ResMessage<List<OperationVO>> result = service.GetAsync<List<OperationVO>>("api/pop/AllOperation");
            if (result.Data != null)
            {
                dgvList.DataSource = new AdvancedList<OperationVO>(result.Data);
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }
        public void TimeComboInit()
        {
            dtpTo.Value = DateTime.Now;
            dtpFrom.Value = DateTime.Now.AddDays(-7);

            for (int i = 1; i <= 24; i++)
            {
                cboTimeFrom.Items.Add(i);
                cboTimeTo.Items.Add(i);
            }
            cboTimeFrom.SelectedIndex = cboTimeTo.SelectedIndex = DateTime.Now.Hour -1;
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            string OpID = dgvList.SelectedRows[0].Cells["OpID"].Value.ToString();
            this.Close();
            DataSendEvent(OpID);
            
        }

        private void frmOperation_FormClosing(object sender, FormClosingEventArgs e)
        {
            POPMain main = (POPMain)this.MdiParent;
            main.ChangeValue();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ResMessage<List<OperationVO>> result = service.GetAsync<List<OperationVO>>("api/pop/SearchOper/"+ dtpFrom.Value.ToShortDateString() + "/" +dtpTo.Value.ToShortDateString()+"/"+cboTimeFrom.Text+"/"+cboTimeTo.Text);
            if (result.Data != null)
            {
                dgvList.DataSource = new AdvancedList<OperationVO>(result.Data);
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            dtpTo.Value = DateTime.Now;
            dtpFrom.Value = DateTime.Now.AddDays(-7);
            cboTimeFrom.SelectedIndex = cboTimeTo.SelectedIndex = DateTime.Now.Hour - 1;
            LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

