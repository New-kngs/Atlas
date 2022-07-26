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

namespace AtlasPOP
{
    public delegate void DataGetEventHandler(OperationVO data);
    public partial class frmOperation : Form
    {
        public DataGetEventHandler DataSendEvent;
        popServiceHelper service = null;
        ResMessage<List<OperationVO>> searchList = null;
        public bool IsState { get; set; }

        public frmOperation()
        {
            InitializeComponent();
        }
        private void frmOperation_Load(object sender, EventArgs e)
        {
            service = new popServiceHelper("");
            popDataGridUtil.SetInitGridView(dgvList);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "작업지시ID", "OpID", colwidth: 120, DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "작업지시일시", "OpDate", colwidth: 180, DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "공정ID", "ProcessID",visibility : false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "공정명", "ProcessName", colwidth: 120);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "제품ID", "ItemID",visibility : false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "시작", "BeginDate", visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "종료", "EndDate", visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "제품명", "ItemName", colwidth: 130, DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "주문ID", "OrderID", colwidth: 130, DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "지시수량", "PlanQty", colwidth: 100,DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "공정상태", "OpState", colwidth: 100, DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "자재투입여부", "resourceYN", colwidth: 140, DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "창고입고여부", "PutInYN", colwidth: 140, DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "포장여부", "LapingYN", visibility : false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "담당ID", "EmpID", visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "포트", "port",visibility: false);
            dgvList.ClearSelection();
            dgvList.MultiSelect = false;

            string[] combo = {"전체", "작업대기", "작업중", "입고대기", "작업종료" };
            cboState.Items.AddRange(combo);
            cboState.SelectedIndex = 0;
            dtpTo.Value = DateTime.Now;
            dtpFrom.Value = DateTime.Now.AddDays(-7);

            LoadData();   
        }
        public void LoadData()
        {
            searchList = service.GetAsync<List<OperationVO>>("api/pop/SearchOper/" + dtpFrom.Value.ToShortDateString() + "/" + dtpTo.Value.ToShortDateString());
            if (searchList.Data != null)
            {
                dgvList.DataSource = null;
                dgvList.DataSource = new popAdvancedList<OperationVO>(searchList.Data);
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
            dgvList.ClearSelection();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            switch (cboState.Text)
            {
                case "전체":
                    LoadData(); break;
                default:
                    dgvList.DataSource = null;
                    dgvList.DataSource = searchList.Data.FindAll((f) => f.OpState.Equals(cboState.Text)); break;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            dtpTo.Value = DateTime.Now;
            dtpFrom.Value = DateTime.Now.AddDays(-7);
            LoadData();
            cboState.SelectedIndex = 0;   
        }
        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        public void loadDetails()
        {
            OperationVO oper = new OperationVO()
            {
                OpID = dgvList.SelectedRows[0].Cells["OpID"].Value.ToString(),
                OpDate = dgvList.SelectedRows[0].Cells["OpDate"].Value.ToString(),
                OrderID = dgvList.SelectedRows[0].Cells["OrderID"].Value.ToString(),
                ItemID = dgvList.SelectedRows[0].Cells["ItemID"].Value.ToString(),
                ItemName = dgvList.SelectedRows[0].Cells["ItemName"].Value.ToString().Trim(),
                BeginDate = (dgvList.SelectedRows[0].Cells["BeginDate"].Value == null) ? "" : dgvList.SelectedRows[0].Cells["BeginDate"].Value.ToString(),
                EndDate = (dgvList.SelectedRows[0].Cells["EndDate"].Value == null) ? "" : dgvList.SelectedRows[0].Cells["EndDate"].Value.ToString(),
                EmpID = dgvList.SelectedRows[0].Cells["EmpID"].Value.ToString(),
                ProcessID = Convert.ToInt32(dgvList.SelectedRows[0].Cells["ProcessID"].Value),
                ProcessName = dgvList.SelectedRows[0].Cells["ProcessName"].Value.ToString(),
                PlanQty = Convert.ToInt32(dgvList.SelectedRows[0].Cells["PlanQty"].Value),
                OpState = dgvList.SelectedRows[0].Cells["OpState"].Value.ToString(),
                resourceYN = dgvList.SelectedRows[0].Cells["resourceYN"].Value.ToString(),
                PutInYN = dgvList.SelectedRows[0].Cells["PutInYN"].Value.ToString(),
                port = dgvList.SelectedRows[0].Cells["port"].Value.ToString(),
            };
            DataSendEvent(oper);

            AtlasPOP main = (AtlasPOP)this.MdiParent;
            main.ChangeValue();
        }

        private void frmOperation_Shown(object sender, EventArgs e)
        {
            dgvList.ClearSelection();
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            loadDetails();
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string finish = dgvList.Rows[e.RowIndex].Cells["LapingYN"].Value.ToString();

            if (finish != "Y")
            {
                e.CellStyle.ForeColor = Color.Blue;
            }      
        }
    }
}

