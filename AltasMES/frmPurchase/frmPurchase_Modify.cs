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
    public partial class frmPurchase_Modify : Form
    {
        public PurchaseVO purchase { get; set; }
        ServiceHelper srv = null;
        List<ItemVO> purList = null;
        List<PurchaseDetailsVO> purchaseList = null;
        List<PurchaseDetailsVO> resultPurList = null;

        string instate = string.Empty;
        string CreateDate = string.Empty;


        public frmPurchase_Modify(PurchaseVO purchase)
        {
            InitializeComponent();

            srv = new ServiceHelper("");
            this.purchase = purchase;
            txtCusName.Text = purchase.CustomerName;
            txtPurID.Text = purchase.PurchaseID;            
            instate = purchase.InState;
            CreateDate = purchase.CreateDate;

        }

        private void frmPurchase_Modify_Load(object sender, EventArgs e)
        {
            srv = new ServiceHelper("");

            // 자재 리스트
            DataGridUtil.SetInitGridView(dgvItem);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "자재ID", "ItemID", colwidth: 90, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "자재명", "ItemName", colwidth: 160, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "규격", "ItemSize", colwidth: 70, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "재고수량", "CurrentQty", colwidth: 100, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "단가", "ItemPrice", colwidth: 100, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "거래처명", "CustomerName", colwidth: 120, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvItem, "거래처ID", "CustomerID", visibility: false);
            dgvItem.Columns["ItemPrice"].DefaultCellStyle.Format = "###,##0";
            dgvItem.ClearSelection();
            DataGridViewButtonColumn btn1 = new DataGridViewButtonColumn();
            btn1.HeaderText = "자재추가";
            btn1.Text = "추가";
            btn1.Width = 80;
            btn1.DefaultCellStyle.Padding = new Padding(5, 1, 5, 1);
            btn1.UseColumnTextForButtonValue = true;
            dgvItem.Columns.Add(btn1);         
                       
            // 발주 리스트
            DataGridUtil.SetInitGridView(dgvPurItem);
            DataGridUtil.AddGridTextBoxColumn(dgvPurItem, "자재ID", "ItemID", colwidth: 90, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPurItem, "자재명", "ItemName", colwidth: 330, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvPurItem, "규격", "ItemSize", colwidth: 70, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPurItem, "수량", "Qty", colwidth: 70, Readonly: false, align: DataGridViewContentAlignment.MiddleRight);
            DataGridUtil.AddGridTextBoxColumn(dgvPurItem, "단가", "ItemPrice", visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvPurItem, "거래처명", "CustomerName", visibility: false);
            //DataGridUtil.AddGridTextBoxColumn(dgvPurItem, "거래처ID", "CustomerID", visibility: false);

            DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
            btn2.HeaderText = "자재삭제";
            btn2.Text = "삭제";
            btn2.Width = 80;
            btn2.DefaultCellStyle.Padding = new Padding(5, 1, 5, 1);
            btn2.UseColumnTextForButtonValue = true;
            dgvPurItem.Columns.Add(btn2);            

            LoadData();
            LoadPurPrice();

            txtCount.Text = dgvPurItem.Rows.Count.ToString();

            if (instate.Equals("Y"))
            {
                btnAdd.Visible = btnComplete.Visible = false;
                btn1.Visible = btn2.Visible = false;
                btnClose.Location = new Point(362, 645);
            }          


        }

        private void LoadData()
        {
            purList = srv.GetAsync<List<ItemVO>>("api/Item/PurChaseItem").Data;
            List<ItemVO> purItemList = purList.FindAll(p => p.CustomerName.Equals(txtCusName.Text));

            purchaseList = srv.GetAsync<List<PurchaseDetailsVO>>("api/Purchase/GetAllPurchaseDetail").Data;
            resultPurList = purchaseList.FindAll(p => p.PurchaseID.Equals(txtPurID.Text));

            if (resultPurList == null || purItemList == null)
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }

            dgvPurItem.DataSource = dgvItem.DataSource = null;
            dgvItem.DataSource = new AdvancedList<ItemVO>(purItemList);
            dgvPurItem.DataSource = resultPurList; //new List<PurchaseDetailsVO>(resultPurList);
        }

        private void dgvItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 7)
            {
                string itemId = dgvItem["ItemID", e.RowIndex].Value.ToString();
                string itemName = dgvItem["ItemName", e.RowIndex].Value.ToString();
                string itemSize = dgvItem["ItemSize", e.RowIndex].Value.ToString();
                string cusName = dgvItem["CustomerName", e.RowIndex].Value.ToString();
                int price = Convert.ToInt32(dgvItem["ItemPrice", e.RowIndex].Value);
                string inState = this.purchase.InState;

                foreach (DataGridViewRow item in dgvPurItem.Rows)
                {
                    string purItemId = item.Cells[0].Value.ToString();                    

                    if (itemId.Equals(purItemId))
                    {
                        MessageBox.Show($"이미 추가된 자재입니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }               
                }
                // null
                resultPurList.Add( new PurchaseDetailsVO { ItemID = itemId, ItemName = itemName, ItemSize = itemSize, CustomerName = cusName, PurTotPrice = price, Qty = 0, }); // price);
                dgvItem.ClearSelection();
                
                txtCusName.Text = cusName;
                dgvPurItem.DataSource = null;
                dgvPurItem.DataSource = resultPurList;                 

                int sum = 0;
                for (int i = 0; i < dgvPurItem.Rows.Count; i++)
                {
                    sum += Convert.ToInt32(dgvPurItem.Rows[i].Cells[3].Value);
                }
                txtCount.Text = dgvPurItem.Rows.Count.ToString();
                dgvPurItem.ClearSelection();
            }
        }

        private void dgvPurItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 6)
            {                
                List<PurchaseDetailsVO> purNewList = dgvPurItem.DataSource as List<PurchaseDetailsVO>;
                purNewList.RemoveAt(e.RowIndex);
                dgvPurItem.DataSource = null;
                dgvPurItem.DataSource = purNewList;
                dgvPurItem.ClearSelection();

                int sum = 0;
                int price = 0;
                int qty = 0;
                for (int i = 0; i < dgvPurItem.Rows.Count; i++)
                {
                    sum += qty = Convert.ToInt32(dgvPurItem.Rows[i].Cells[3].Value); // 수량
                    price += qty * Convert.ToInt32(dgvPurItem.Rows[i].Cells[4].Value); // 단가
                }
                txtCount.Text = dgvPurItem.Rows.Count.ToString();
                txtPrice.Text = price.ToString("#,##0") + " 원";

                if (dgvPurItem.Rows.Count < 1)
                {
                    txtCusName.Text = string.Empty;
                }
            }
        }
        private void dgvPurItem_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int sum = 0;
            int price = 0;
            int qty = 0;

            for (int i = 0; i < dgvPurItem.Rows.Count; i++)
            {
                if (int.TryParse(dgvPurItem.Rows[i].Cells[3].Value.ToString(), out qty))
                {
                    sum += qty;
                    price += qty * Convert.ToInt32(dgvPurItem.Rows[i].Cells[4].Value);
                }
                else
                {
                    MessageBox.Show("수량은 숫자를 입력해 주세요");
                    dgvPurItem.Rows[i].Cells[3].Value = 0;
                    return;
                }

            }
            txtCount.Text = dgvPurItem.Rows.Count.ToString();
            txtPrice.Text = price.ToString("#,##0") + " 원";
        }               

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgvPurItem.Rows.Count < 1)
            {
                MessageBox.Show("등록된 발주 목록이 없습니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }          

            List<PurchaseDetailsVO> purDetailList = new List<PurchaseDetailsVO>();
            for (int i = 0; i < dgvPurItem.Rows.Count; i++)
            {
                if (Convert.ToInt32(dgvPurItem.Rows[i].Cells[3].Value) == 0)
                {
                    MessageBox.Show("발주 수량을 확인해 주세요", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (Convert.ToInt32(dgvPurItem.Rows[i].Cells[3].Value) > 300)
                {
                    MessageBox.Show("한번에 발주가 가능한 자재 수량은 300개 입니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgvPurItem.CurrentCell = dgvPurItem.Rows[i].Cells[3];
                    return;
                }
                PurchaseDetailsVO item = new PurchaseDetailsVO();
                item.ItemID = dgvPurItem.Rows[i].Cells[0].Value.ToString();
                item.Qty = Convert.ToInt32(dgvPurItem.Rows[i].Cells[3].Value.ToString());

                purDetailList.Add(item);
            }
            PurchaseVO purList = new PurchaseVO()
            {
                ModifyUser = this.purchase.ModifyUser,
                CustomerID = dgvItem.Rows[0].Cells[6].Value.ToString(),
                PurchaseID = this.purchase.PurchaseID
            };
            ResMessage result = srv.SavePurchase("api/Purchase/UpdatePurchase", purList, purDetailList);


            if (result.ErrCode == 0)
            {
                MessageBox.Show("수정이 완료되었습니다.", "발주 수정", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("오류가 발생하였습니다. 다시 시도 하여 주십시오.");
        }       

        private void btnComplete_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show($"발주ID : {txtPurID.Text}를 입고 완료 처리 하시겠습니까?", "입고 완료", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                PurchaseVO cPurId = new PurchaseVO()
                {
                    PurchaseID = txtPurID.Text
                };
                ResMessage<List<PurchaseVO>> result = srv.PostAsync<PurchaseVO, List<PurchaseVO>>("api/Purchase/UpdatePurStateItemQty", cPurId);

                if (result.ErrCode == 0)
                {
                    MessageBox.Show("입고 완료 처리 되었습니다.", "입고 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //LoadData();                    
                    dgvItem.ClearSelection();
                    this.Close();
                }
                else
                    MessageBox.Show(result.ErrMsg);
            }
        }

        private void LoadPurPrice()
        {
            int num = 0;
            for (int i = 0; i < dgvPurItem.Rows.Count; i++)
            {
                string id = dgvPurItem.Rows[i].Cells["ItemID"].Value.ToString();
                int qtc = Convert.ToInt32(dgvPurItem.Rows[i].Cells["Qty"].Value);
                purList.FindAll((f) => f.ItemID.Equals(id)).ForEach((f) => num += f.ItemPrice * qtc);
            }
            txtPrice.Text = num.ToString("#,##0") + " 원";
        }

        private void frmPurchase_Modify_Shown(object sender, EventArgs e)
        {
            dgvItem.ClearSelection();
            dgvPurItem.ClearSelection();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {           
            List<RptPurchaseVO> rptPurchase = srv.GetAsync<List<RptPurchaseVO>>("api/Purchase/GetRptPurchase").Data;
            List<RptPurchaseVO> resultRpt = rptPurchase.FindAll(p => p.PurchaseID.Equals(txtPurID.Text));

           
            RptPurchaseVO vo = new RptPurchaseVO()
            {
                totPrice = txtPrice.Text,
                Qty = null
            };

            resultRpt.Add(vo);

            DataTable dt = CommonUtil.LinqQueryToDataTable(resultRpt); //DataTable

            rptPurchaseList rpt = new rptPurchaseList
            {
                DataSource = dt
            };
            _ = new ReportPreviewForm(rpt);

        }
    }
}
