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
    public partial class frmFail : Form
    {
        int maxNum = 0;
        popServiceHelper service;
        OperationVO oper;
        ResMessage<List<ItemVO>> itemList;
        List<FailVO> failList;

        public frmFail(OperationVO oper)
        {
            InitializeComponent();
            
            this.oper = oper;
            txtFailTOT.Text = oper.FailQty.ToString();
            numQty.Maximum = maxNum = Convert.ToInt32(txtFailTOT.Text);
        }

        private void frmResource_Load(object sender, EventArgs e)
        {
            service = new popServiceHelper("");
            popDataGridUtil.clickSetInitGridView(dgvList);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "불량ID", "FailID", visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "제품ID", "ItemID", visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "불량코드", "FailCode", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "불량명", "FailName", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "불량갯수", "FailQty", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "생성사용자", "CreateUser", visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "작업지시ID", "OpID", visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "생성일자", "CreateDate", visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "생성사용자", "CreateUser", visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "수정날짜", "ModifyDate", visibility: false);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "수정사용자", "ModifyUser", visibility: false);

            ResMessage<List<ComboItemVO>> comboList = service.GetAsync<List<ComboItemVO>>("api/pop/GetFailCode");
            itemList = service.GetAsync<List<ItemVO>>("api/Item/AllItem");
            if(itemList.ErrCode != 0)
            {
                MessageBox.Show("에러코드를 받아오지 못하였습니다.");
            }
            popCommonUtil.ComboBinding(cboFailList, comboList.Data, "불량코드", blankText: "선택");
        }
        /// <summary>
        /// 추기버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(failList == null)
            {
                failList = new List<FailVO>();
            }
            if(cboFailList.SelectedIndex == 0)
            {
                MessageBox.Show("불량코드를 선택해주세요");
                return;
            }
            if(numQty.Value == 0)
            {
                MessageBox.Show("불량 갯수를 입력해주세요");
                return;
            }
            if(maxNum == 0)
            {
                MessageBox.Show("등록할 불량 제품이 없습니다");
                return;
            }

            int idx = failList.FindIndex((i) => i.FailCode.Equals(cboFailList.SelectedValue));
            if(idx >= 0)
            {
                failList[idx].FailQty += Convert.ToInt32(numQty.Value);
            }
            else
            {
                FailVO fail = new FailVO()
                {
                    ItemID = oper.ItemID,
                    FailQty = Convert.ToInt32(numQty.Value),
                    FailCode = cboFailList.SelectedValue.ToString(),
                    FailName = cboFailList.Text,
                    OpID = oper.OpID,
                    CreateUser = oper.EmpID
                };
                failList.Add(fail);
            }
            cboFailList.SelectedIndex = 0;
            maxNum -= Convert.ToInt32(numQty.Value);
            txtFailTOT.Text = maxNum.ToString();
            numQty.Value = 0;
            numQty.Maximum = maxNum;
            dgvList.DataSource = null;
            dgvList.DataSource = failList;
            dgvList.ClearSelection();
        }
        /// <summary>
        /// 삭제버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvList.SelectedRows.Count < 1)
            {
                MessageBox.Show("삭제할 불량코드를 선택해 주세요");
                return;
            }
            if (failList == null)
            {
                failList = new List<FailVO>();
            }

            string ptCode = dgvList.SelectedRows[0].Cells["FailCode"].Value.ToString();

            FailVO fail = failList.Find((p) => p.FailCode.ToString() == ptCode);
            
            failList.Remove(fail);

            maxNum += fail.FailQty;
            txtFailTOT.Text = maxNum.ToString();

            dgvList.DataSource = null;
            dgvList.DataSource = failList;
            dgvList.ClearSelection();
        }
        /// <summary>
        /// 닫기버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 저장버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            ResMessage<List<FailVO>> opid = service.GetAsync<List<FailVO>>("api/pop/GetOperID");
            int idx = opid.Data.FindIndex((f) => f.OpID.Contains(oper.OpID));
            if (txtFailTOT.Text != "0")
            {
                MessageBox.Show("불량제품이 남아있습니다. \n 불량을 전부 등록해주세요");
                return;
            }
            
            if(idx >= 0)
            {
                MessageBox.Show("이미 불량등록이 된 작업입니다.");
                return;
            }
            
            ResMessage<List<FailVO>> putFail = service.PostAsync<List<FailVO>, List<FailVO>>("api/pop/InsertFailLog", failList);
            if (putFail.ErrCode == 0)
            {
                MessageBox.Show("등록이 완료되었습니다.");
                dgvList.DataSource = null;
                failList = null;
            }
            else
            {
                MessageBox.Show("등록 중 오류가 발생하였습니다.");
                return;
            }
        }

        private void frmFail_Shown(object sender, EventArgs e)
        {
            dgvList.ClearSelection();
        }
    }
}
