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
    public partial class frmEquipment : BaseForm
    {
        ServiceHelper service = null;
        public frmEquipment()
        {
            InitializeComponent();
        }

        private void frmEquitment_Load(object sender, EventArgs e)
        {
            DataGridUtil.SetInitGridView(dgvEquipment);
            DataGridUtil.AddGridTextBoxColumn(dgvEquipment, "설비ID", "EquipID", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvEquipment, "설비명", "EquipName", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvEquipment, "설비유형", "EquipCategory", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvEquipment, "생성날짜", "CreateDate", colwidth: 200);
            DataGridUtil.AddGridTextBoxColumn(dgvEquipment, "생성사용자", "CreateUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvEquipment, "변경날짜", "ModifyDate", colwidth: 200);
            DataGridUtil.AddGridTextBoxColumn(dgvEquipment, "변경사용자", "ModifyUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvEquipment, "사용여부", "StateYN", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);

            LoadData();
        }
        public void LoadData()
        {
            service = new ServiceHelper("api/Equipment");
            ResMessage<List<EquipmentVO>> result = service.GetAsync<List<EquipmentVO>>("AllEquipment");
            if (result.Data != null)
            {
                dgvEquipment.DataSource = new AdvancedList<EquipmentVO>(result.Data);
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmEquipment_Add frm = new frmEquipment_Add();
            frm.ShowDialog();
        }
    }
}
