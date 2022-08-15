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

        }
    }
}
