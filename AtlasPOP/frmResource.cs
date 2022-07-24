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
    public partial class frmResource : Form
    {
        ServiceHelper service = null;
        public string itemID { get; set; }
        public frmResource()
        {
            InitializeComponent();
        }
        public frmResource(string itemID)
        {
            this.itemID = itemID;
        }

        private void frmResource_Load(object sender, EventArgs e)
        {

        }
        public void LoadData()
        {
            service = new ServiceHelper("api/pop");
            ResMessage<List<BOMVO>> resource = service.GetAsync<List<BOMVO>>("GetResourceBOM");
            if (resource.Data != null)
            {
                dgvList.DataSource = resource;
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }
    }
}
