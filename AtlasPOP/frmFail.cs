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
    public partial class frmFail : Form
    {
        public string OperID { get; set; }
        ServiceHelper service;
        public frmFail(int FailQty, string OperID)
        {
            InitializeComponent();
            txtFailTOT.Text = FailQty.ToString();
            this.OperID = OperID;
        }

        private void frmResource_Load(object sender, EventArgs e)
        {
            service = new ServiceHelper("");
            ResMessage<List<ComboItemVO>> comboList = service.GetAsync<List<ComboItemVO>>("api/pop/GetFailCode");
            CommonUtil.ComboBinding(cboFailList, comboList.Data, "불량코드", blankText: "선택");

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
    }
}
