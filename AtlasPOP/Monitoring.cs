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
    public partial class Monitoring : UserControl
    {
        public Monitoring(EquipDetailsVO equip)
        {
            InitializeComponent();
            
        }

        private void Monitoring_Load(object sender, EventArgs e)
        {

            this.BackColor = Color.Black;
            this.Location = new Point(10, 10);
            this.Size = new Size(100, 100);

        }
    }
}
