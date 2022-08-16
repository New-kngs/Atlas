using AtlasDTO;
using DevExpress.Data.Mask.Internal;
using DevExpress.PivotGrid.OLAP;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AltasMES
{
    public partial class frmPlan_Ship : Form
    {
        ServiceHelper srv = null;

        public PlanVO plan { get; set; }
        public frmPlan_Ship(PlanVO plan)
        {
            InitializeComponent();
            this.plan = plan;   
        }              

        private void frmPlan_Ship_Load(object sender, EventArgs e)
        {
            txtOrderID.Text = plan.OrderID;
            txtProduct.Text = plan.ItemName;
            txtQty.Text = plan.LOTIQty.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            srv = new ServiceHelper("api/Plan");
            
            PlanVO list = new PlanVO
            {
                ItemID = plan.ItemID,
                LOTIQty = plan.LOTIQty,
                CreateUser = plan.CreateUser,
                OrderID = plan.OrderID,
            };
            
            ResMessage<List<PlanVO>> result = srv.PostAsync<PlanVO, List<PlanVO>>("SavePlanShip", list);

            if (result.ErrCode == 0)
            {
                MessageBox.Show("성공적으로 등록되었습니다.");
                this.DialogResult = DialogResult.OK;                
            }
            else
                MessageBox.Show(result.ErrMsg);
        }
    }
}
