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
    public partial class frmBOM : BaseForm
    {
        ServiceHelper service = null;

        public frmBOM()
        {
            InitializeComponent();
        }

        private void frmBOM_Load(object sender, EventArgs e)
        {
            //ItemID, ItemName, CurrentQty, WHID, ItemCategory, ItemSize
            DataGridUtil.SetInitGridView(dgvPdt);
            DataGridUtil.AddGridTextBoxColumn(dgvPdt, "제품ID", "ItemID", colwidth: 205, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPdt, "제품이름", "ItemName", colwidth: 265, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPdt, "제품유형", "ItemCategory", colwidth: 205, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPdt, "제품사이즈", "ItemSize", colwidth: 165, align: DataGridViewContentAlignment.MiddleCenter);

            DataGridUtil.SetInitGridView(dgvA);
            DataGridUtil.AddGridTextBoxColumn(dgvA, "제품ID", "ItemID", colwidth: 205, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvA, "제품이름", "ItemName", colwidth: 265, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvA, "제품유형", "ItemCategory", colwidth: 205, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvA, "제품사이즈", "ItemSize", colwidth: 165, align: DataGridViewContentAlignment.MiddleCenter);

            DataGridUtil.SetInitGridView(dgvD);
            DataGridUtil.AddGridTextBoxColumn(dgvD, "제품ID", "ItemID", colwidth: 205, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvD, "제품이름", "ItemName", colwidth: 265, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvD, "제품유형", "ItemCategory", colwidth: 205, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvD, "제품사이즈", "ItemSize", colwidth: 165, align: DataGridViewContentAlignment.MiddleCenter);

            cboPdt.Items.AddRange(new string[] { "선택", "완제품", "반제품", "자재" });
            cboPdt.SelectedIndex = 0;

            DataLoad();
        }
        private void DataLoad()
        {
            cboPdt.SelectedIndex = 0;

            service = new ServiceHelper("api/Item");
            ResMessage<List<ItemVO>> result = service.GetAsync<List<ItemVO>>("AllItem");
            if (result != null)
            {
                dgvPdt.DataSource = new AdvancedList<ItemVO>(result.Data);
                //dgvPdt.DataSource = result.Data;
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            service = new ServiceHelper("api/Item");
            ResMessage<List<ItemVO>> volist = service.GetAsync<List<ItemVO>>("AllItem");

            string category = cboPdt.Text;
            List<ItemVO> resultVO = volist.Data.FindAll((r) => r.ItemCategory == category);

            if (cboPdt.SelectedIndex == 0)
            {
                //dgvPdt.DataSource = volist.Data;
                DataLoad();
            }
            else
            {
                //dgvPdt.DataSource = resultVO;                
                dgvPdt.DataSource = new AdvancedList<ItemVO>(resultVO);
            }

        }

        private void dgvPdt_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmBOM_FormClosing(object sender, FormClosingEventArgs e)
        {
            service.Dispose();
        }
    }
}
