using AtlasDTO;
using System;
using System.Windows.Forms;

namespace AltasMES
{
    public partial class frmPlan_Plan : Form
    {
        ServiceHelper srv = null;
        public PlanOptVO plan { get; set; }
        public frmPlan_Plan(PlanOptVO plan)
        {
            InitializeComponent();
            this.plan = plan;
        }

        private void frmPlan_Plan_Load(object sender, EventArgs e)
        {
            txtOrderID.Text = plan.OrderID;
            txtProduct.Text = plan.ProductName;
            txtProductQty.Text = plan.ProductQty.ToString();
            //txtSemi1.Text = plan.Semi1ID;
            txtSemi1.Text = plan.Semi1Name;
            txtSemi1Qty.Text = plan.Semi1Qty.ToString(); ;
            //plan.Semi2ID;
            txtSemi2.Text = plan.Semi2Name;
            txtSemi2Qty.Text = plan.Semi2Qty.ToString();
            //plan.Material1ID;
            txtMaterial1.Text = plan.Material1Name;
            txtMaterial1Qty.Text = plan.Material1Qty.ToString();
            //plan.Material2ID;
            txtMaterial2.Text = plan.Material2Name;
            txtMaterial2Qty.Text = plan.Material2Qty.ToString();
            //plan.Material3ID;
            txtMaterial3.Text = plan.Material3Name;
            txtMaterial3Qty.Text = plan.Material3Qty.ToString();
            //plan.Material4ID;
            txtMaterial4.Text = plan.Material4Name;
            txtMaterial4Qty.Text = plan.Material4Qty.ToString();
            //plan.Material5ID;
            txtMaterial5.Text = plan.Material5Name;
            txtMaterial5Qty.Text = plan.Material5Qty.ToString();
            //plan.Material6ID;
            txtMaterial6.Text = plan.Material6Name;
            txtMaterial6Qty.Text = plan.Material6Qty.ToString();
            //plan.Material7ID;
            txtMaterial7.Text = plan.Material7Name;
            txtMaterial7Qty.Text = plan.Material7Qty.ToString();
            //plan.Material8ID;
            txtMaterial8.Text = plan.Material8Name;
            txtMaterial8Qty.Text = plan.Material8Qty.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}
