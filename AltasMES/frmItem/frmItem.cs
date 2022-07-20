using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AtlasDTO;

namespace AltasMES
{
    public partial class frmItem : BaseForm
    {
        ServiceHelper service = null;
        public frmItem()
        {
            InitializeComponent();
        }

        private void frmItem_Load(object sender, EventArgs e)
        {
            //ItmeImage, ItemExplain

            DataGridUtil.SetInitGridView(dgvItem);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "제품ID", "ItemID", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "제품명", "ItemName", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "거래처ID", "CustomerID", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "재고수량", "CurrentQty", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "창고ID", "WHID", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "제품유형", "ItemCategory", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "제품규격", "ItemSize", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "생성날짜", "CreateDate", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "생성사용자", "CreateUser", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "변경날짜", "ModifyDate", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
<<<<<<< HEAD
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "삭제여부", "StateYN", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
=======
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "사용여부", "StateYN", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
>>>>>>> 45f44d8f68ad9a56a0bce54a3175602e662619a0

            service = new ServiceHelper("api/Item");
            ResMessage<List<ItemVO>> result = service.GetAsync<List<ItemVO>>("AllItem");
            if (result != null)
            {
                dgvItem.DataSource = new AdvancedList<ItemVO>(result.Data);
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void frmItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            service.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmItem_Add pop = new frmItem_Add();
            if (pop.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
           
        }

        private void btnModify_Click(object sender, EventArgs e)
        {

        }
    }
}
