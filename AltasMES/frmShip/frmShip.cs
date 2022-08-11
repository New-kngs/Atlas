using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using AtlasDTO;


namespace AltasMES
{
    public partial class frmShip : BaseForm
    {
        public frmShip()
        {
            InitializeComponent();
        }

        ServiceHelper service = null;
        List<ShipVO> allList = null;

        private void frmShip_Load(object sender, EventArgs e)
        {
            service = new ServiceHelper("");

            allList = service.GetAsync<List<ShipVO>>("api/Ship/GetAllShip").Data;

            dgvShip.DataSource = allList;
        }
    }
}
