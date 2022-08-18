using AtlasDTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlasPOP
{
    public partial class frmLaping : Form
    {
        popServiceHelper service = null;
        public OperationVO oper { get; set; }
        LOTVO lot;
        public string userName;
        

        public frmLaping(string user)
        {

            userName = user;
            InitializeComponent();
        
        }
        private void frmLaping_Load(object sender, EventArgs e)
        {
            service = new popServiceHelper("");

            popDataGridUtil.SetInitGridView(dgvList);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "작업지시ID", "OpID", visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "공정ID", "ProcessID", visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "공정명", "ProcessName", visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "제품ID", "ItemID");
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "시작", "BeginDate", visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "종료", "EndDate", visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "제품명", "ItemName", colwidth: 130, DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "주문ID", "OrderID", colwidth: 130, DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "지시수량", "PlanQty", visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "공정상태", "OpState", visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "자재투입여부", "resourceYN", visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "창고입고여부", "PutInYN", visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "담당ID", "EmpID", visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "포트", "port", visibility: false);


            btnCreateLOT.Enabled = true;
            btnLaping.Enabled = false;
            btnPutIN.Enabled = false;


            LoadData();
        }
        public void LoadData()
        {
            ResMessage<List<OperationVO>> lapingList = service.GetAsync<List<OperationVO>>("api/pop/GetLapingList");
            if (lapingList.ErrCode != 0)
            {
                MessageBox.Show("데이터 로드 중 오류가 발생하였습니다.");
            }
            else
            {
                dgvList.DataSource = lapingList.Data;
            }
        }

        private void btnCreateLOT_Click(object sender, EventArgs e)
        {
            //등록된 로트인지 확인하는 예외처리 넣기
            if(lot == null)
            {
                MessageBox.Show("선택된 지시서가 없습니다.");
                return;
            }
            
            ResMessage<List<LOTVO>> createLOTID = service.PostAsync<LOTVO, List<LOTVO>>("api/pop/CreateLOT", lot);
            if(createLOTID.ErrCode == 0)
            {
                MessageBox.Show("LOT가 생성되었습니다.");
                btnCreateLOT.Enabled = false;
                btnLaping.Enabled = true;
                btnPutIN.Enabled = false;
            }
            else
            {
                MessageBox.Show("생성 중 오류가 발생하였습니다.");
            }
        }

        private void btnPutIN_Click(object sender, EventArgs e)
        {
            ResMessage<List<ItemVO>> itemList = service.GetAsync<List<ItemVO>>("api/Item/AllItem");
            ItemVO Item = new ItemVO()
            {
                CurrentQty = itemList.Data.Find((f) => f.ItemID == lot.ItemID).CurrentQty - lot.LOTIQty,
                ModifyUser = userName,
                ItemID = lot.ItemID
            };
            
            ResMessage<List<ItemVO>> putIn = service.PostAsync<ItemVO, List<ItemVO>>("api/pop/PutInItem", Item);
            if (putIn.ErrCode == 0)
            {
                MessageBox.Show("출하 창고에 입고되었습니다.");
                LoadData();
                btnCreateLOT.Enabled = true;
                btnLaping.Enabled = false;
                btnPutIN.Enabled = false;
            }
            else
            {
                MessageBox.Show("입고 중 문제가 발생하였습니다.");
                return;
            }
        }

        private void btnLaping_Click(object sender, EventArgs e)
        {
 
            MessageBox.Show("포장이 완료 되었습니다. 창고에 입고시켜주세요");
            btnCreateLOT.Enabled = false;
            btnLaping.Enabled = false;
            btnPutIN.Enabled = true;
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lot = new LOTVO()
            {
                ItemID = dgvList.SelectedRows[0].Cells["ItemID"].Value.ToString(),
                OrderID = dgvList.SelectedRows[0].Cells["OrderID"].Value.ToString(),
                CreateUser = userName,
                LOTIQty = Convert.ToInt32(dgvList.SelectedRows[0].Cells["PlanQty"].Value),
            };
        }

        private void frmLaping_Shown(object sender, EventArgs e)
        {
            dgvList.ClearSelection();
        }
    }
}
