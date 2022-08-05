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
    public delegate void DataGetEventHandler(OperationVO data);
    public partial class frmOperation : Form
    {



        public DataGetEventHandler DataSendEvent;
        popServiceHelper service = null;
        ResMessage<List<OperationVO>> operList;
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
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "담당ID", "EmpID", colwidth: 130, DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "포트", "port",visibility: false);
            dgvList.ClearSelection();

            
            LoadData();
            dtpTo.Value = DateTime.Now;
            dtpFrom.Value = DateTime.Now.AddDays(-7);
        }
        public void LoadData()
        {
            operList = service.GetAsync<List<OperationVO>>("api/pop/AllOperation");

            if (operList.Data != null)
            {
                dgvList.DataSource = new AdvancedList<OperationVO>(operList.Data);
                dgvList.ClearSelection();
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
           ResMessage<List<OperationVO>> result = service.GetAsync<List<OperationVO>>("api/pop/SearchOper/"+ dtpFrom.Value.ToShortDateString() +  "/" + dtpTo.Value.ToShortDateString());
            if (result.Data != null)
            {
                dgvList.DataSource = new AdvancedList<OperationVO>(result.Data);
                dgvList.ClearSelection();
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            dtpTo.Value = DateTime.Now;
            dtpFrom.Value = DateTime.Now.AddDays(-7);
            LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            loadDetails();      
            drawEquip();
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
                port = dgvList.SelectedRows[0].Cells["port"].Value.ToString(),
            };
            DataSendEvent(oper);

            AtlasPOP main = (AtlasPOP)this.MdiParent;
            main.ChangeValue();
        }

        public void drawEquip()
        {
            panel2.Controls.Clear();
            int procID = Convert.ToInt32(dgvList.SelectedRows[0].Cells["ProcessID"].Value.ToString());
            string OperID = dgvList.SelectedRows[0].Cells["OpID"].Value.ToString();
            ResMessage<List<EquipDetailsVO>> equip = service.GetAsync<List<EquipDetailsVO>>("api/pop/GetEquip");
            List<EquipDetailsVO> EquipList = equip.Data.FindAll((p) => p.ProcessID == procID);

            if (equip.Data != null)
            {
                int iRow = (int)Math.Ceiling(EquipList.Count / 1.0);

                int idx = 0;
                for (int c = 0; c < iRow; c++)
                {
                    if (idx >= EquipList.Count) break;
                    EquipList item = new EquipList(EquipList[c], OperID);
                    item.Name = $"process";
                    item.Location = new Point(224 * c + 5, 3);
                    item.Size = new Size(214, 154);

                    panel2.Controls.Add(item);
                    idx++;
                }
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}

