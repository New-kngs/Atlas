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
    public partial class frmBOM : BaseForm
    {
        ServiceHelper service = null;

        public frmBOM()
        {
            InitializeComponent();
        }

        private void frmBOM_Load(object sender, EventArgs e)
        {
            //ItemID, ItemName, CurrentQty, WHID, ItemCategory, ItemSize
            DataGridUtil.SetInitGridView(dgvPdt);
            DataGridUtil.AddGridTextBoxColumn(dgvPdt, "제품ID", "ItemID", colwidth: 205, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPdt, "제품이름", "ItemName", colwidth: 265, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvPdt, "제품유형", "ItemCategory", colwidth: 205, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvPdt, "제품사이즈", "ItemSize", colwidth: 165, align: DataGridViewContentAlignment.MiddleCenter);

            DataGridUtil.SetInitGridView(dgvA);
            DataGridUtil.AddGridTextBoxColumn(dgvA, "제품ID", "ItemID", colwidth: 205, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvA, "제품이름", "ItemName", colwidth: 265, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvA, "제품유형", "ItemCategory", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvA, "제품사이즈", "ItemSize", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);

            DataGridUtil.SetInitGridView(dgvD);
            DataGridUtil.AddGridTextBoxColumn(dgvD, "제품ID", "ItemID", colwidth: 205, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvD, "제품이름", "ItemName", colwidth: 265, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvD, "제품유형", "ItemCategory", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvD, "제품사이즈", "ItemSize", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);

            service = new ServiceHelper("");

            cboPdt.Items.AddRange(new string[] { "전체보기", "완제품", "반제품", "자재" });
            cboPdt.SelectedIndex = 0;
            //DataLoad();
        }
        private void DataLoad()
        {
            ResMessage<List<ItemVO>> result = service.GetAsync<List<ItemVO>>("api/Item/AllItem");

            if (result != null)
            {
                dgvPdt.DataSource = new AdvancedList<ItemVO>(result.Data);
                //dgvPdt.DataSource = result.Data;
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ResMessage<List<ItemVO>> volist = service.GetAsync<List<ItemVO>>("api/Item/AllItem");

            string category = cboPdt.Text;
            List<ItemVO> resultVO = volist.Data.FindAll((r) => r.ItemCategory == category);

            if (cboPdt.SelectedIndex == 0)
            {
                //dgvPdt.DataSource = volist.Data;
                DataLoad();
            }
            else
            {
                //dgvPdt.DataSource = resultVO;                
                dgvPdt.DataSource = new AdvancedList<ItemVO>(resultVO);
            }

        }
        private void cboPdt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPdt.SelectedIndex == 0)
            {
                dgvPdt.DataSource = null;
                //dgvPdt.DataSource = volist.Data;
                DataLoad();
            }

            else
            {
                ResMessage<List<ItemVO>> volist = service.GetAsync<List<ItemVO>>("api/Item/AllItem");

                string category = cboPdt.Text;
                List<ItemVO> resultVO = volist.Data.FindAll((r) => r.ItemCategory == category);
                //dgvPdt.DataSource = resultVO;                
                dgvPdt.DataSource = new AdvancedList<ItemVO>(resultVO);
                //if (allList != null)
                //{
                //    cList = allList.FindAll(n => n.DeptName.Equals(cboCategory.Text));
                //    dgvEmp.DataSource = null;
                //    dgvEmp.DataSource = new AdvancedList<EmployeeVO>(cList);

                //}

            }

            dgvPdt.ClearSelection();
        }

        private void dgvPdt_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //api/pop/GetResourceBOM
            ResMessage<List<BOMVO>> resource = service.GetAsync<List<BOMVO>>("api/BOM/BOMFoward");

            ResMessage<List<BOMVO>> resResult = service.GetAsync<List<BOMVO>>("api/BOM/AllBOMItem");

            if (e.RowIndex > -1)
            {
                string pdtID = dgvPdt[0, e.RowIndex].Value.ToString();
                string category = dgvPdt["ItemCategory", e.RowIndex].Value.ToString();

                if (category == "완제품")
                {
                    List<BOMVO> listF = resource.Data.FindAll((r) => r.ItemID == pdtID);
                    dgvA.DataSource = null;
                    dgvD.DataSource = null;
                    dgvA.DataSource = listF;
                }
                else
                {
                    List<BOMVO> listF = resResult.Data.FindAll((r) => r.ParentID == pdtID);
                    dgvA.DataSource = null;
                    dgvA.DataSource = listF;

                    List<BOMVO> listR = resResult.Data.FindAll((r) => r.ChildID == pdtID);
                    dgvD.DataSource = null;
                    dgvD.DataSource = listR;
                }      
            }
            else
            {
                return;
            }
        }
        
        private void frmBOM_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (service != null)
            {
                service.Dispose();
            }
        }
    }
}
