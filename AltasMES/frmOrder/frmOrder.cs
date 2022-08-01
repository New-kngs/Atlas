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
        List<OrderVO> orderList = null;  // 주문
        //List<CustomerVO> cusList = null; // 출고 거래처 바인딩

        string selId = string.Empty;

        public frmOrder()
        {
            InitializeComponent();
        }

        private void frmOrder_Load(object sender, EventArgs e)
        {
            srv = new ServiceHelper("");

            orderList = srv.GetAsync<List<OrderVO>>("api/Order/GetAllOrder").Data;
            //cusList = srv.GetAsync<List<CustomerVO>>("api/Customer/AllCustomer").Data;

            DataGridUtil.SetInitGridView(dgvOrder);
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "주문ID", "OrderID", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "거래처명", "CustomerName", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "출하여부", "OrderShip", colwidth: 110, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "주문완료일", "OrderEndDate", colwidth: 160, align: DataGridViewContentAlignment.MiddleCenter);            
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "생성사용자", "CreateUser", colwidth: 120, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "주문날짜", "CreateDate", colwidth: 170, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "변경사용자", "ModifyUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "변경날짜", "ModifyDate", colwidth: 170, align: DataGridViewContentAlignment.MiddleCenter);            

            //CommonUtil.ComboBinding<CustomerVO>(cboCustomer, cusList.FindAll(p => p.Category.Equals("출고")), "CustomerName", "CustomerID", blankText: "선택");

            LoadData();

            cboStateYN.Items.AddRange(new string[] { "선택", "Y", "N" });
            cboStateYN.SelectedIndex = 0;

        }

        public void LoadData()
        {
            orderList = srv.GetAsync<List<OrderVO>>("api/Order/GetAllOrder").Data;

            if (orderList == null)
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }

            dgvOrder.DataSource = null;
            dgvOrder.DataSource = new AdvancedList<OrderVO>(orderList);
        }                

        private void dgvOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            selId = (dgvOrder[0, e.RowIndex].Value).ToString();
        }        

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (selId == string.Empty)
            {
                MessageBox.Show("주문 항목을 선택해 주세요");
                return;
            }

            OrderVO order = srv.GetAsync<OrderVO>($"/api/Order/{selId}").Data;            
            order.ModifyUser = ((Main)this.MdiParent).EmpName.ToString();
            frmOrder_Detail pop = new frmOrder_Detail(order);
            if (pop.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text) && cboStateYN.SelectedIndex == 0)
            {
                MessageBox.Show("출하여부를 선택하거나 거래처명을 입력해 주세요");
                LoadData();
                return;
            }

            //if (string.IsNullOrWhiteSpace(txtSearch.Text) && cboStateYN.SelectedIndex == 0)
            //{
            //    List<OrderVO> list = srv.GetAsync<List<OrderVO>>("api/Order/GetSearchOrder/" + dtpFrom.Value.ToShortDateString() + "/" + dtpTo.Value.AddDays(1).ToShortDateString()).Data; //"yyyy-MM-dd HH:mm:ss"

            //    dgvOrder.DataSource = null;
            //    dgvOrder.DataSource = new AdvancedList<OrderVO>(list);                
            //}

            

            if (string.IsNullOrWhiteSpace(txtSearch.Text.Trim()))
            {
                cboStateYN_SelectedIndexChanged(this, e);
            }
            else
            {
                if (cboStateYN.SelectedIndex == 0)
                {
                    List<OrderVO> list = orderList.FindAll(p => p.CustomerName.Contains(txtSearch.Text.Trim()));                    

                    dgvOrder.DataSource = null;
                    dgvOrder.DataSource = new AdvancedList<OrderVO>(list);
                }
                else
                {                    
                    List<OrderVO> searchList = orderList.FindAll(p => p.CustomerName.Contains(txtSearch.Text.Trim()) && p.OrderShip.Equals(cboStateYN.Text));
                    dgvOrder.DataSource = null;
                    dgvOrder.DataSource = new AdvancedList<OrderVO>(searchList);
                }
            }


        }

        private void cboStateYN_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Clear();
            dgvOrder.DataSource = null;
            if (cboStateYN.SelectedIndex == 0)
            {
                dgvOrder.DataSource = new AdvancedList<OrderVO>(orderList);
            }
            else
            {
                List<OrderVO> stateList = orderList.FindAll(p => p.OrderShip.Equals(cboStateYN.Text));
                dgvOrder.DataSource = new AdvancedList<OrderVO>(stateList);
            }
            dgvOrder.ClearSelection();
        }

        //private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //dgvOrder.DataSource = null;

        //    //if (cboCustomer.SelectedIndex == 0)
        //    //{
        //    //    dgvOrder.DataSource = new AdvancedList<OrderVO>(orderList);
        //    //}
        //    //else
        //    //{
        //    //    List<OrderVO> cOrderList = orderList.FindAll(p => p.CustomerName.Equals(cboCustomer.Text));
        //    //    dgvOrder.DataSource = new AdvancedList<OrderVO>(cOrderList);
        //    //}
        //    //dgvOrder.ClearSelection();
        //}

        private void frmOrder_Shown(object sender, EventArgs e)
        {
            dgvOrder.ClearSelection();
        }


        private void frmOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (srv != null)
            {
                srv.Dispose();
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSearch_Click(this, e);
            }
        }
    }
}
