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
    public partial class frmOperation_Add : PopUpBase
    {
        ServiceHelper service = null;
        public OperationVO oper { get; set; }
        ResMessage<List<ProcessVO>> process;
        public frmOperation_Add(OperationVO oper)
        {
            InitializeComponent();

            service = new ServiceHelper("");

            ResMessage<List<ItemVO>> item = service.GetAsync<List<ItemVO>>("api/Item/AllItem");

            this.oper = oper;
            txtOrder.Text = oper.OrderID;
            txtItem.Text = item.Data.Find((f) => f.ItemID.Equals(oper.ItemID)).ItemName;
            txtQty.Text = oper.PlanQty.ToString();
        }

        private void frmOperation_Add_Load(object sender, EventArgs e)
        {
            process = service.GetAsync<List<ProcessVO>>("api/Process/AllProcess");
            CommonUtil.ComboBinding(cboProcess, process.Data, "ProcessName", "ProcessName", blankText: "선택");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(cboProcess.Text.Equals("선택"))
            {
                MessageBox.Show("공정을 선택해주세요", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OperationVO operVO = new OperationVO()
            {
                PlanID = oper.PlanID,
                ItemID = oper.ItemID,
                OrderID = oper.OrderID,
                ProcessID = process.Data.Find((f) => f.ProcessName.Equals(cboProcess.Text)).ProcessID,
                PlanQty = Convert.ToInt32(txtQty.Text),
                EmpID = oper.CreateUser,
                CreateUser = oper.CreateUser
            };


            ResMessage<List<OperationVO>> result = service.PostAsync<OperationVO, List<OperationVO>>("api/Plan/SaveOperation", operVO);
            if(result.ErrCode == 0)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("문제가 발생하였습니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
