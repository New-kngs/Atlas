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

        private void frmShip_Load(object sender, EventArgs e)
        {
            service = new ServiceHelper("");
            allList = service.GetAsync<List<ShipVO>>("api/Ship/GetAllShip").Data;

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

            dgvShip.DataSource = new AdvancedList<ShipVO>(allList);
        }

        private void frmShip_Shown(object sender, EventArgs e)
        {
            dgvShip.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!dgvShip.CurrentCell.Selected)
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
            frm.ShowDialog();
        }
    }
}
