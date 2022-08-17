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
            DataGridUtil.AddGridTextBoxColumn(dgvList, "생산계획ID", "PlanID");
            DataGridUtil.AddGridTextBoxColumn(dgvList, "제품ID", "ItemID");
            DataGridUtil.AddGridTextBoxColumn(dgvList, "요청수량", "PlanQty");
            DataGridUtil.AddGridTextBoxColumn(dgvList, "주문ID", "OrderID");
            DataGridUtil.AddGridTextBoxColumn(dgvList, "생산날짜", "CreateDate");
            DataGridUtil.AddGridTextBoxColumn(dgvList, "생산사용자", "CreateUser");
            DataGridUtil.AddGridTextBoxColumn(dgvList, "수정일날짜", "ModifyDate");
            DataGridUtil.AddGridTextBoxColumn(dgvList, "수정사용자", "ModifyUser");

            LoadData();
        }
        public void LoadData()
        {
            planList = service.GetAsync<List<PlanVO>>("api/Plan/GetPlanList");

            if (planList.ErrCode == 0)
            {
                dgvList.DataSource = planList.Data;
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }
    }
}
