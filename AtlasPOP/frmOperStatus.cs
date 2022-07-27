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
    public partial class frmOperStatus : Form
    {
        ResMessage<List<ItemVO>> itemList;
        public string itemID { get; set; }
        public string OperID { get; set; }
        ServiceHelper service;
        public frmOperStatus(string itemID,string OperID)
        {
            InitializeComponent();
            this.itemID = itemID;
            this.OperID = OperID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {

        }

        private void frmOperStatus_Load(object sender, EventArgs e)
        {
            service = new ServiceHelper("");
            itemList = service.GetAsync<List<ItemVO>>("api/Item/AllItem");
            ResMessage<List<OperationVO>> operList = service.GetAsync<List<OperationVO>>("api/pop/AllOperation");
            ResMessage<List<OrderVO>> oderList = service.GetAsync<List<OrderVO>>("api/pop/GetCustomer");
            ResMessage<List<CustomerVO>> customerList = service.GetAsync<List<CustomerVO>>("api/pop/GetCustomerName");

            string OrderID = operList.Data.Find((n) => n.OpID.Equals(OperID)).OrderID;
            string CustomerID = oderList.Data.Find((n) => n.OrderID.Equals(OrderID)).CustomerID;

            txtItemName.Text = itemList.Data.Find((n) => n.ItemID.Equals(itemID)).ItemName;
            txtItemID.Text = OperID;
            txtProcessName.Text = operList.Data.Find((n) => n.OpID.Equals(OperID)).ProcessName;
            txtClient.Text = customerList.Data.Find((n) => n.CustomerID.Equals(CustomerID)).CustomerName;
            txtPlanQty.Text = operList.Data.Find((n) => n.OpID.Equals(OperID)).PlanQty.ToString();
            txtCategory.Text = itemList.Data.Find((n) => n.ItemID.Equals(itemID)).ItemCategory;

            lblStatus.Text = operList.Data.Find((n) => n.OpID.Equals(OperID)).OpState;
            lblPlanQty.Text = operList.Data.Find((n) => n.OpID.Equals(OperID)).PlanQty.ToString();
            if (lblStatus.Text.Equals("작업종료"))
            {
                lblComplete.Text = "28";
                lblFail.Text = "2";
            }
            else
            {
                lblComplete.Text = lblStatus.Text;
                lblFail.Text = lblStatus.Text;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!lblStatus.Text.Equals("작업종료"))
            {
                MessageBox.Show("작업종료가 되지 않았습니다.");
                return;
            }
            ItemVO item = new ItemVO()
            {
                ItemID = itemID,
                CurrentQty = itemList.Data.Find((c) => c.ItemID.Equals(itemID)).CurrentQty,
                CompleteQty = Convert.ToInt32(lblComplete.Text)
            };
            ResMessage<List<ItemVO>> operList = service.PostAsync <ItemVO, List<ItemVO>>("api/pop/PutInItem", item);

            if (operList.ErrCode == 0)
            {
                MessageBox.Show("입고완료되었습니다.");
            }
            else
            {
                MessageBox.Show("입고 중 문제가 발생하였습니다.");
                return;
            }
        }
    }
}
