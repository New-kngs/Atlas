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
        public EquipDetailsVO equip { get; set; }
        public EquipList(EquipDetailsVO equip, string OpID,int idx)
        {
            InitializeComponent();
            service = new popServiceHelper("");
            ResMessage<List<OperationVO>> list = service.GetAsync<List<OperationVO>>("api/pop/AllOperation");
            this.equip = equip;
            lblName.Text = $"{idx+1}번 {equip.EquipName}";
            lblType.Text = $"{equip.EquipCategory}({equip.EquipID})";
            
            
        }
        public void DrawState(string opstate)
        {

            lblState state = new lblState(opstate);
            state.Dock = DockStyle.Fill;
            this.Invoke((MethodInvoker)(() => panel1.Controls.Add(state)));
        }


        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void lblType_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
