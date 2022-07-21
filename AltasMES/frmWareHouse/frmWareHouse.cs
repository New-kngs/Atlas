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
    public partial class frmWarehouse : BaseForm
    {
        ServiceHelper service = null;
        public frmWarehouse()
        {
            InitializeComponent();
        }

        private void frmWarehouse_Load(object sender, EventArgs e)
        {
            DataGridUtil.SetInitGridView(dgvWH);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "창고ID", "WHID", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "창고이름", "WHName", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "제품유형", "ItemCategory", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "생성날짜", "CreateDate", colwidth: 270, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "생성사원", "CreateUser", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "변경날짜", "ModifyDate", colwidth: 270, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "변경사원", "ModifyUser", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "사용여부", "StateYN", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);

            //ItemID, ItemName, CurrentQty, WHID, ItemCategory, ItemSize
            DataGridUtil.SetInitGridView(dgvPDT);
            DataGridUtil.AddGridTextBoxColumn(dgvPDT, "창고ID", "WHID", colwidth: 300, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPDT, "제품ID", "ItemID", colwidth: 250, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPDT, "제품이름", "ItemName", colwidth: 300, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPDT, "제품유형", "ItemCategory", colwidth: 250, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPDT, "제품사이즈", "ItemSize", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPDT, "현재재고", "CurrentQty", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);

            cboWH.Items.AddRange(new string[] { "선택", "완제품", "반제품", "자재" });
            cboWH.SelectedIndex = 0;

            DataLoad();
        }
        private void DataLoad()
        {
            cboWH.SelectedIndex = 0;

            service = new ServiceHelper("api/WareHouse");
            ResMessage<List<WareHouseVO>> result = service.GetAsync<List<WareHouseVO>>("AllWareHouse");
            if (result != null)
            {
                dgvWH.DataSource = new AdvancedList<WareHouseVO>(result.Data);
                //dgvWH.DataSource = result.Data;
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            WareHouseVO wareHouse = new WareHouseVO()
            {
                CreateUser = ((Main)this.MdiParent).EmpName.ToString()
            };

            frmWareHouse_Add frm = new frmWareHouse_Add(wareHouse);
            //frm.wareHouse = wareHouse;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DataLoad();
            }
        }

        private void frmWarehouse_FormClosing(object sender, FormClosingEventArgs e)
        {
            service.Dispose();
        }


        private void dgvWH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string whid = dgvWH[0, e.RowIndex].Value.ToString();

            ResMessage<List<ItemVO>> resResult = service.GetAsync<List<ItemVO>>($"WareHouseInfo/{whid}");

            dgvPDT.DataSource = resResult.Data;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = resResult.Data;
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            WareHouseVO wareHouse = new WareHouseVO()
            {
                WHID = dgvWH.SelectedRows[0].Cells["WHID"].Value.ToString(),
                WHName = (dgvWH.SelectedRows[0].Cells["WHName"].Value).ToString(),
                StateYN = (dgvWH.SelectedRows[0].Cells["StateYN"].Value).ToString(),
                ModifyUser = ((Main)this.MdiParent).EmpName.ToString()
            };

            frmWareHouse_Delete frm = new frmWareHouse_Delete(wareHouse);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DataLoad();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            WareHouseVO wareHouse = new WareHouseVO()
            {
                WHID = dgvWH.SelectedRows[0].Cells["WHID"].Value.ToString(),
                WHName = (dgvWH.SelectedRows[0].Cells["WHName"].Value).ToString(),
                StateYN = (dgvWH.SelectedRows[0].Cells["StateYN"].Value).ToString(),
                ModifyUser = ((Main)this.MdiParent).EmpName.ToString()
            };

            if ((dgvWH.SelectedRows[0].Cells["StateYN"].Value).ToString() == "N")
            {
                frmWareHouse_Using frmusing = new frmWareHouse_Using(wareHouse);
                if (frmusing.ShowDialog() == DialogResult.OK)
                {
                    DataLoad();
                }
            }
            else
            {
                //frmWareHouse_Modify frm = new frmWareHouse_Modify(wareHouse);
                //if (frm.ShowDialog() == DialogResult.OK)
                //{
                //    DataLoad();
                //}
                MessageBox.Show("이미 사용중인 창고입니다.");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            service = new ServiceHelper("api/WareHouse");
            ResMessage<List<WareHouseVO>> volist = service.GetAsync<List<WareHouseVO>>("AllWareHouse");

            string category = cboWH.Text;
            List<WareHouseVO> resultVO = volist.Data.FindAll((r) => r.ItemCategory == category);

            if (cboWH.SelectedIndex == 0)
                dgvWH.DataSource = volist.Data;
            else
                dgvWH.DataSource = resultVO;
        }
    }
}
