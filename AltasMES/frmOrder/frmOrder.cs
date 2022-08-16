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
        List<OrderVO> orderList = null;  // 기간내의 주문목록       

        string selId = string.Empty;
        string[] inputColumn = { "주문ID", "거래처명", "완료여부", "주문요청일", "생성사용자", "주문완료일", "출하담당자" };

        public frmOrder()
        {
            InitializeComponent();
        }

        private void frmOrder_Load(object sender, EventArgs e)
        {
            srv = new ServiceHelper("");

            DataGridUtil.SetInitGridView(dgvOrder);
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "주문ID", "OrderID", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "거래처명", "CustomerName", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "완료여부", "OrderShip", colwidth: 110, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "주문요청일", "CreateDate", colwidth: 170, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "생성사용자", "CreateUser", colwidth: 120, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "주문완료일", "OrderEndDate", colwidth: 170, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvOrder, "출하담당자", "ModifyUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            

            dtpTo.Value = DateTime.Now;
            dtpFrom.Value = DateTime.Now.AddDays(-7);
            
            cboStateYN.Items.AddRange(new string[] { "전체", "Y", "N" });
            cboStateYN.SelectedIndex = 0;

            LoadData();
        }

        public void LoadData()
        {            
            orderList = srv.GetAsync<List<OrderVO>>("api/Order/GetSearchOrder/" + dtpFrom.Value.ToShortDateString() + "/" + dtpTo.Value.AddDays(1).ToShortDateString()).Data; //"yyyy-MM-dd HH:mm:ss"

            if (orderList != null)
            {
                List<OrderVO> list = null;
                if (cboStateYN.SelectedIndex > 0)
                {
                    if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                    {
                        list = orderList.FindAll(p => p.CustomerName.Contains(txtSearch.Text.Trim()) && p.OrderShip.Equals(cboStateYN.Text));
                    }
                    else
                    {
                        list = orderList.FindAll(p => p.OrderShip.Equals(cboStateYN.Text));
                    }
                }
                else 
                {
                    if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                    {
                        list = orderList.FindAll(p => p.CustomerName.Contains(txtSearch.Text.Trim()));
                    }
                    else
                    {                        
                        list = orderList;
                    }
                }
                dgvOrder.DataSource = null;
                dgvOrder.DataSource = new AdvancedList<OrderVO>(list);
            }
            else
            {
                //MessageBox.Show("검색 내용이 없습니다.");
                //dtpFrom.Value = DateTime.Now.AddDays(-7);
                dgvOrder.DataSource = null;
                dgvOrder.ClearSelection();
                return;
            }
            dgvOrder.ClearSelection();
        }                

        private void dgvOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            selId = (dgvOrder[0, e.RowIndex].Value).ToString();
        }        

        private void btnAdd_Click(object sender, EventArgs e) // 상세보기 버튼
        {
            if (dgvOrder.CurrentCell == null) return;

            if (!dgvOrder.CurrentCell.Selected || selId == string.Empty)
            {
                MessageBox.Show("주문 항목을 선택해 주세요", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }            

            OrderVO order = srv.GetAsync<OrderVO>($"/api/Order/{selId}").Data;            
            order.ModifyUser = ((Main)this.MdiParent).EmpName.ToString();
            frmOrder_Detail pop = new frmOrder_Detail(order);
            if (pop.ShowDialog() == DialogResult.OK)
            {                
                LoadData();
            }
            selId = string.Empty;
            dgvOrder.ClearSelection();
            txtSearch.Clear();
            cboStateYN.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (dtpFrom.Value > dtpTo.Value)
            {
                MessageBox.Show("검색 기간을 확인해 주세요", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFrom.Value = dtpTo.Value.AddDays(-7);
                btnSearch_Click(this, e);
                //cboStateYN.SelectedIndex = 0;
                return;
            }
            else
            {
                LoadData();
            }
        }

        private void cboStateYN_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }        

        private void frmOrder_Shown(object sender, EventArgs e)
        {
            dgvOrder.ClearSelection();
        }      

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSearch_Click(this, e);
            }
        }

        private void frmOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (srv != null)
            {
                srv.Dispose();
            }
        }

        private void dgvOrder_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvOrder.ClearSelection();
        }

        private void btnExecl_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Execl Files(*.xls)|*.xls";
            dlg.Title = "엑셀파일로 내보내기";

            if (dlg.ShowDialog() == DialogResult.OK)
            {

                ExcelUtil excel = new ExcelUtil();

                if (excel.ExportExcelGridView(dlg.FileName, dgvOrder, inputColumn))
                {
                    MessageBox.Show("엑셀 다운로드 완료", "엑셀 다운로드", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }
    }
}
