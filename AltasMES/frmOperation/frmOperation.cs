using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AtlasDTO;

namespace AltasMES
{
    public partial class frmOperation : BaseForm
    {
        ServiceHelper service;
        ResMessage<List<PlanVO>> planList = null;
        public frmOperation()
        {
            InitializeComponent();
        }

        private void frmOperation_Load(object sender, EventArgs e)
        {
            service = new ServiceHelper("");
            DataGridUtil.SetInitGridView(dgvList);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "생산계획ID", "PlanID", colwidth: 120, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "주문ID", "OrderID", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "제품ID", "ItemID", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "제품명", "ItemName", colwidth: 165, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "제품유형", "ItemCategory", colwidth: 120, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "요청수량", "PlanQty", colwidth: 120, align: DataGridViewContentAlignment.MiddleCenter);  
            DataGridUtil.AddGridTextBoxColumn(dgvList, "작업지시생성여부", "CreateYN", visibility : false);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "생성날짜", "CreateDate", colwidth: 165, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "생성사용자", "CreateUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "수정일날짜", "ModifyDate", colwidth: 165, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "수정사용자", "ModifyUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);

            LoadData();
        }
        public void LoadData()
        {
            
            planList = service.GetAsync<List<PlanVO>>("api/Plan/GetPlanList");
            planList.Data = planList.Data.FindAll((f) => f.CreateYN != "Y");
            if (planList.ErrCode == 0)
            {
                dgvList.DataSource = planList.Data;
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgvList.CurrentCell == null) return;
            if (!dgvList.CurrentCell.Selected)
            {
                MessageBox.Show("작업지시를 선택해주세요", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OperationVO oper = new OperationVO()
            {
                PlanID = Convert.ToInt32(dgvList.SelectedRows[0].Cells["PlanID"].Value),
                OrderID = (dgvList.SelectedRows[0].Cells["OrderID"].Value== null)? "" : dgvList.SelectedRows[0].Cells["OrderID"].Value.ToString(),
                ItemID = dgvList.SelectedRows[0].Cells["ItemID"].Value.ToString(),
                PlanQty = Convert.ToInt32(dgvList.SelectedRows[0].Cells["PlanQty"].Value),
                CreateUser = ((Main)this.MdiParent).EmpName.ToString()
            };

            frmOperation_Add frm = new frmOperation_Add(oper);
            if(frm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("생성되었습니다.");
                LoadData();
                dgvList.ClearSelection();
            }
        }

        private void frmOperation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (service != null)
            {
                service.Dispose();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvList.CurrentCell == null) return;
            if (!dgvList.CurrentCell.Selected)
            {
                MessageBox.Show("작업지시를 선택해주세요", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int planID = Convert.ToInt32(dgvList.SelectedRows[0].Cells["PlanID"].Value);
            if(DialogResult.Yes == MessageBox.Show($"{planID}를 삭제하시겠습니까?", "삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {

                //ResMessage<List<BOMVO>> del = service.GetAsync<List<BOMVO>>($"api/BOM/DeleteBOM/" + id);
                ResMessage<List<PlanVO>> deletePlan = service.GetAsync<List<PlanVO>>("api/Plan/DeletePlan/" + planID);
                if(deletePlan.ErrCode == 0)
                {
                    MessageBox.Show("삭제가 완료되었습니다.");
                    LoadData();
                    dgvList.ClearSelection();
                }
                else
                {
                    MessageBox.Show("삭제중 오류가 발생하였습니다.");
                    return;
                }
                
            }        
        }

        private void frmOperation_Shown(object sender, EventArgs e)
        {
            dgvList.ClearSelection();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
            planList.Data = planList.Data.FindAll((f) => f.ItemName.Contains(txtItemName.Text)).ToList();
            dgvList.DataSource = planList.Data;
            dgvList.ClearSelection();
        }
    }
}
