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
    public partial class frmOrder_Detail : Form
    {
        public OrderVO order { get; set; }
        ServiceHelper srv = null;
        List<OrderDetailVO> orderDetail = null;
        public frmOrder_Detail(OrderVO order)
        {
            srv = new ServiceHelper("");

            InitializeComponent();

            this.order = order;
            txtOrderID.Text = order.OrderID;
            txtName.Text = order.CustomerName;
            //txtState.Text = order.OrderShip;
            txtCreateDate.Text = order.CreateDate;
            txtEndDate.Text = order.OrderEndDate;
        }

        private void frmOrder_Detail_Load(object sender, EventArgs e)
        {

            DataGridUtil.SetInitGridView(dgvOrderDetail);            
            DataGridUtil.AddGridTextBoxColumn(dgvOrderDetail, "제품ID", "ItemID", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvOrderDetail, "제품명", "ItemName", colwidth: 160, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvOrderDetail, "규격", "ItemSize", colwidth: 70, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvOrderDetail, "수량", "Qty", colwidth: 70, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvOrderDetail, "단가", "ItemPrice", colwidth: 100, align: DataGridViewContentAlignment.MiddleRight);
            dgvOrderDetail.Columns["ItemPrice"].DefaultCellStyle.Format = "###,##0";
            LoadData();

            int sum = 0;
            int price = 0;
            int qty = 0;
            for (int i = 0; i < dgvOrderDetail.Rows.Count; i++)
            {
                sum += qty = Convert.ToInt32(dgvOrderDetail.Rows[i].Cells[3].Value); // 수량
                price += qty * Convert.ToInt32(dgvOrderDetail.Rows[i].Cells[4].Value); // 단가
            }
            txtCount.Text = dgvOrderDetail.Rows.Count.ToString();
            txtPrice.Text = price.ToString("#,##0") + " 원";

        }

        public void LoadData()
        {
            string orderID = this.order.OrderID;

            ResMessage<List<OrderDetailVO>> result = srv.GetAsync<List<OrderDetailVO>>("api/Order/GetAllOrderDetail");
            // orderList = srv.GetAsync<List<OrderVO>>("api/Order/GetAllOrder").Data;

            orderDetail = result.Data.FindAll((p) => p.OrderID == orderID).ToList();

            if (result.Data != null)
            {
                dgvOrderDetail.DataSource = new AdvancedList<OrderDetailVO>(orderDetail);
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void frmOrder_Detail_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (srv != null)
            {
                srv.Dispose();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            List<RptOrderVO> rptOrder = srv.GetAsync<List<RptOrderVO>>("api/Order/GetRptOrder").Data;
            List<RptOrderVO> resultRpt = rptOrder.FindAll(p => p.OrderID.Equals(txtOrderID.Text));


            RptOrderVO vo = new RptOrderVO()
            {
                totPrice = txtPrice.Text,
                Qty = null
            };

            resultRpt.Add(vo);

            DataTable dt = CommonUtil.LinqQueryToDataTable(resultRpt); //DataTable

            rptOrderList rpt = new rptOrderList
            {
                DataSource = dt
            };
            _ = new ReportPreviewForm(rpt);
        }
    }
}
