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
    public partial class frmResource : Form
    {
        ServiceHelper service = null;
        public string itemID { get; set; }
        public frmResource()
        {
            InitializeComponent();
        }
        public frmResource(string itemID)
        {
            InitializeComponent();
            this.itemID = itemID;
        }

        private void frmResource_Load(object sender, EventArgs e)
        {
            DataGridUtil.SetInitGridView(dgvList);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "BOMID", "BOMID", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter, visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "제품ID", "ItemID", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter, visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "부모ID", "ParentID", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter,visibility:false);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "제품ID", "ChildID", colwidth: 100);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "제품명", "ItemName", colwidth: 150);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "필요갯수", "UnitQty", colwidth: 80, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "계획수량", "PlanQty", colwidth: 80, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "총 수량", "Qty", colwidth: 80, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "생성일시", "CreateDate", colwidth: 200, visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "생성사용자", "CreateUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter, visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "변경일지", "ModifyDate", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter, visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "변경사용자", "ModifyUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter, visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "사용유무", "StateYN", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter, visibility: false);



            LoadData();
        }
        public void LoadData()
        {
            service = new ServiceHelper("api/pop");
            ResMessage<List<BOMVO>> resource = service.GetAsync<List<BOMVO>>("GetResourceBOM");
            if (resource.Data != null)
            {
                resource.Data = resource.Data.FindAll((r) => r.ItemID == itemID);
                dgvList.DataSource = resource.Data;
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }
    }
}
