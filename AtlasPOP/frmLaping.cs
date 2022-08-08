﻿using AtlasDTO;
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
    public partial class frmLaping : Form
    {
        popServiceHelper service = null;
        public OperationVO oper { get; set; }

        public frmLaping(OperationVO oper)
        {
            InitializeComponent();
            this.oper = oper;
        }
        private void frmLaping_Load(object sender, EventArgs e)
        {
            service = new popServiceHelper("");

            popDataGridUtil.SetInitGridView(dgvList);
            popDataGridUtil.AddGridTextBoxColumn(dgvList, "작업지시ID", "OpID", visibility: false);
            //popDataGridUtil.AddGridTextBoxColumn(dgvList, "작업지시일시", visibility: false);
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



            ResMessage<List<OperationVO>> lapingList = service.GetAsync<List<OperationVO>>("api/pop/GetLapingList");
            if(lapingList.ErrCode != 0)
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
            LOTVO lot = new LOTVO()
            {
                ItemID = dgvList.SelectedRows[0].Cells["ItemID"].Value.ToString(),
                OrderID = dgvList.SelectedRows[0].Cells["OrderID"].Value.ToString(),
                CreateUser = dgvList.SelectedRows[0].Cells["CreateUser"].Value.ToString(),
                LOTIQty = Convert.ToInt32(dgvList.SelectedRows[0].Cells["PlanQty"].Value),
            };
            ResMessage<List<LOTVO>> createLOTID = service.PostAsync<LOTVO, List<LOTVO>>("api/pop/CreateLOT", lot);
            if(createLOTID.ErrCode == 0)
            {
                MessageBox.Show("LOT가 생성되었습니다.");
            }
            else
            {
                MessageBox.Show("생성 중 오류가 발생하였습니다.");
            }
            
        }

        
    }
}
