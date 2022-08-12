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
    public partial class frmProdPlan_Add : Form
    {
        public PlanVO plan { get; set; }
        public frmProdPlan_Add(PlanVO plan)
        {
            InitializeComponent();
            this.plan = plan;

            //textBox1.Text = plan.PlanID;
        }
    }
}
