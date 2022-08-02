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
    public partial class EquipList : UserControl
    {

        popServiceHelper service = null;
        public EquipList(EquipDetailsVO equip, string OpID)
        {
            InitializeComponent();
            service = new popServiceHelper("");
            ResMessage<List<OperationVO>> list = service.GetAsync<List<OperationVO>>("api/pop/AllOperation");

            lblName.Text = equip.EquipName;
            lblType.Text = equip.EquipCategory;
            lblState.Text = list.Data.Find((n) => n.OpID.Equals(OpID)).OpState;

            if (lblState.Text == "작업중")
                lblState.ForeColor = Color.Green;
            else if (lblState.Text == "작업종료")
                lblState.ForeColor = Color.Red;

            
        }

        private void EquipList_MouseClick(object sender, MouseEventArgs e)
        {


        }

        private void EquipList_Load(object sender, EventArgs e)
        {

        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }
    }
}
