using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using AtlasDTO;


namespace AltasMES
{
    public partial class frmShip : BaseForm
    {
        public frmShip()
        {
            InitializeComponent();
        }

        ServiceHelper service = null;
        List<ShipVO> allList = null;
        List<ShipVO> clist = null;

      
        private void frmShip_Load(object sender, EventArgs e)
        {
            service = new ServiceHelper("");

            lblTitle.Text = "출하";
            groupBox2.Text = "검색조건";

            DataGridUtil.SetInitGridView(dgvShip);
            DataGridUtil.AddGridTextBoxColumn(dgvShip, "주문ID", "OrderID", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvShip, "거래처명", "CustomerName", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvShip, "완료여부", "OrderShip", colwidth: 110, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvShip, "영업담당자", "CreateUser", colwidth: 130, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvShip, "주문요청일", "CreateDate", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvShip, "생산완료일", "EndDate", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvShip, "Barcode", "BarcodeID", visibility: false);

            Databinding();
           
            
        }

        private void frmShip_Shown(object sender, EventArgs e)
        {
            dgvShip.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgvShip.CurrentCell == null || !dgvShip.CurrentCell.Selected)
            {
                MessageBox.Show("조회하실 출하 내역을 선택해주세요.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ShipVO VO = new ShipVO()
            {
                OrderID = dgvShip.SelectedRows[0].Cells["OrderID"].Value.ToString(),
                CustomerName = dgvShip.SelectedRows[0].Cells["CustomerName"].Value.ToString(),
                BarCodeID = Convert.ToInt32(dgvShip.SelectedRows[0].Cells["BarcodeID"].Value),
                CreateDate = dgvShip.SelectedRows[0].Cells["CreateDate"].Value.ToString(),
                EndDate = dgvShip.SelectedRows[0].Cells["EndDate"].Value.ToString(),
                
            };

            frmShip_Modify frm = new frmShip_Modify(VO);
            frm.ShowDialog();

        }

        private void frmShip_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(service != null)
            {
                service.Dispose();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            frmShip_End frm = new frmShip_End(((Main)this.MdiParent).EmpName.ToString());
            if(frm.ShowDialog() == DialogResult.OK)
            {
                Databinding();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            if (allList != null)
            {
                if (string.IsNullOrWhiteSpace(txtSerach.Text.Trim()))
                {
                    dgvShip.DataSource = null;
                    clist = allList;
                    dgvShip.DataSource = new AdvancedList<ShipVO>(clist);
                    dgvShip.ClearSelection();
                }
                else
                {
                    clist = allList.FindAll(n => n.CustomerName.Contains(txtSerach.Text.Trim()));
                    dgvShip.DataSource = new AdvancedList<ShipVO>(clist);
                    dgvShip.ClearSelection();

                }
            }
          
        }


        private void txtSerach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnSearch_Click(this, e);
        }

        private void Databinding()
        {
            allList = service.GetAsync<List<ShipVO>>("api/Ship/GetAllShip").Data;

            if (allList != null)
            {
                clist = allList;
                dgvShip.DataSource = new AdvancedList<ShipVO>(clist);
            }
            else
                MessageBox.Show("출하가능한 목록이 없습니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            dgvShip.ClearSelection();
        }

        private void dgvShip_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvShip.ClearSelection();
        }

        private void btnExecl_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Execl Files(*.xls)|*.xls";
            dlg.Title = "엑셀파일로 내보내기";

            if (dlg.ShowDialog() == DialogResult.OK)
            {

                ExcelUtil excel = new ExcelUtil();

            
                string[] columnName = { "주문ID", "거래처명", "완료여부", "영업담당자", "주문요청일", "생산완료일"};

                if (excel.ExportExcelGridView(dlg.FileName,dgvShip, columnName))
                {
                    MessageBox.Show("엑셀 다운로드 완료", "엑셀 다운로드", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }
    }
}
