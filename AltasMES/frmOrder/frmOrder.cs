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
    public partial class frmOrder : BaseForm
    {
        ServiceHelper srv = null;
        List<OrderVO> orderList;
        public frmOrder()
        {
            InitializeComponent();
        }

        private void frmOrder_Load(object sender, EventArgs e)
        {
            srv = new ServiceHelper("");
            orderList = srv.GetAsync<List<OrderVO>>("api/Order/GetAllOrder").Data;

            DataGridUtil.SetInitGridView(dgvOrder);
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "주문ID", "OrderID", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "거래처명", "CustomerName", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "출하여부", "OrderShip", colwidth: 110, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "주문완료일", "OrderEndDate", colwidth: 160, align: DataGridViewContentAlignment.MiddleCenter);            
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "생성사용자", "CreateUser", colwidth: 120, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "생성날짜", "CreateDate", colwidth: 160, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "변경사용자", "ModifyUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "변경날짜", "ModifyDate", colwidth: 160, align: DataGridViewContentAlignment.MiddleCenter);

            LoadData();
        }

        public void LoadData()
        {
            if (orderList != null)
            {
                dgvOrder.DataSource = null;
                dgvOrder.DataSource = new AdvancedList<OrderVO>(orderList);
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
                return;
            }
        }
    }
}
