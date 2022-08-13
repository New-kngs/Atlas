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
    public partial class frmShip_Modify : Form
    {

        ShipVO Ship = null;
        ServiceHelper service = null;
        List<OrderDetailVO> allList = null;
      
        public frmShip_Modify(ShipVO ship)
        {
            Ship = ship;
            InitializeComponent();
        }

        private void frmShip_Modify_Load(object sender, EventArgs e)
        {
           

            DataGridUtil.SetInitGridView(dgvOrderDetail);
            DataGridUtil.AddGridTextBoxColumn(dgvOrderDetail, "제품ID", "ItemID", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvOrderDetail, "제품명", "ItemName", colwidth: 250, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvOrderDetail, "수량", "Qty", colwidth: 100, align: DataGridViewContentAlignment.MiddleRight);


            service = new ServiceHelper("");
            allList = service.GetAsync<List<OrderDetailVO>>("api/Order/GetAllOrderDetail").Data.FindAll(n=>n.OrderID.Equals(Ship.OrderID)).ToList();
            Ship.Address = service.GetAsync<List<CustomerVO>>("api/Customer/GetCustomerlist").Data.Find(n=>n.CustomerName.Equals(Ship.CustomerName)).Address;

            txtOrderID.Text = Ship.OrderID;
            txtName.Text = Ship.CustomerName;
            txtCreateDate.Text = Ship.CreateDate;
            txtEndDate.Text = Ship.EndDate;
            txtAddr.Text = Ship.Address;

            dgvOrderDetail.DataSource = allList;


        }

        private void frmShip_Modify_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(service != null)
            {
                service.Dispose();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            List<ShipVO> vo = new List<ShipVO>();
            vo.Add(Ship);

            DataTable dt = CommonUtil.LinqQueryToDataTable(vo); //DataTable

            XtraReport1 rpt = new XtraReport1();
            rpt.DataSource = dt;
            _ = new ReportPreviewForm(rpt);


        }

        private void frmShip_Modify_Shown(object sender, EventArgs e)
        {
            dgvOrderDetail.ClearSelection();
        }
    }
}
