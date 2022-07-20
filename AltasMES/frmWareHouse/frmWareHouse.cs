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
    public partial class frmWarehouse : BaseForm
    {
        ServiceHelper service = null;
        public frmWarehouse()
        {
            InitializeComponent();
        }

        private void frmWarehouse_Load(object sender, EventArgs e)
        {
            DataGridUtil.SetInitGridView(dgvWH);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "창고ID", "WHID", colwidth: 200 ,align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "창고이름", "WHName", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "제품유형", "ItemCategory", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "생성날짜", "CreateDate", colwidth: 300, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "생성사원", "CreateUser", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "변경날짜", "ModifyDate", colwidth: 300, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "변경사원", "ModifyUser", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "사용여부", "StateYN", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);

            //ItemID, ItemName, CurrentQty, WHID, ItemCategory, ItemSize
            DataGridUtil.SetInitGridView(dgvPDT);
            DataGridUtil.AddGridTextBoxColumn(dgvPDT, "창고ID", "WHID", colwidth: 300, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPDT, "제품ID", "ItemID", colwidth: 250, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPDT, "제품이름", "ItemName", colwidth: 300, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPDT, "제품유형", "ItemCategory", colwidth: 250, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPDT, "제품사이즈", "ItemSize", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPDT, "현재재고", "CurrentQty", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);

            DataLoad();
        }
        private void DataLoad()
        {
            service = new ServiceHelper("api/WareHouse");
            ResMessage<List<WareHouseVO>> result = service.GetAsync<List<WareHouseVO>>("AllWareHouse");
            if (result != null)
            {
                /*dgvWH.DataSource = new AdvancedList<WareHouseVO>(result.Data);*/
                dgvWH.DataSource = result.Data;
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmWareHouse_Add frm = new frmWareHouse_Add();
            frm.ShowDialog();
        }

        private void frmWarehouse_FormClosing(object sender, FormClosingEventArgs e)
        {
            service.Dispose();
        }
                

        private void dgvWH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string whid = dgvWH[0, e.RowIndex].Value.ToString();

            ResMessage<List<ItemVO>> resResult = service.GetAsync<List<ItemVO>>($"WareHouseInfo/{whid}");

            dgvPDT.DataSource = resResult.Data;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = resResult.Data;
        }
    }
}
