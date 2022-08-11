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
    public partial class frmCustomer : BaseForm
    {
        public frmCustomer()
        {
            InitializeComponent();
        }


        ServiceHelper service = null;
        List<CustomerVO> allList = null;
        List<CustomerVO> cList = null;


        private void frmCustomer_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "거래처";
            groupBox2.Text = "검색조건";
            groupBox3.Text = "거래처 현황";

            service = new ServiceHelper("");
            allList = service.GetAsync<List<CustomerVO>>("api/Customer/GetCustomerlist").Data;


            DataGridUtil.SetInitGridView(dgvCus);

            dgvCus.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "거래처ID", "CustomerID",  align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "거래처명", "CustomerName", align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "비밀번호", "CustomerPwd",visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "거래처구분", "Category", align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "연락처", "Phone",  align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "이메일", "Email",  align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "주소", "Address",  align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "거래처담당자", "EmpName", align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "거래처담당자ID", "EmpID",visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "생성날짜", "CreateDate", align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "생성사용자", "CreateUser",  align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "수정날짜", "ModifyDate", align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "수정사용자", "ModifyUser",  align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "사용여부", "StateYN", visibility: false);


            cboCategory.SelectedIndex = 0;

        }

        private void frmCustomer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (service != null)
            {
                service.Dispose();
            }
        }

        private void frmCustomer_Shown(object sender, EventArgs e)
        {
            dgvCus.ClearSelection();
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (cboCategory.SelectedIndex == 0)
            {

                if (allList != null)
                {
                    dgvCus.DataSource = null;
                    dgvCus.DataSource = new AdvancedList<CustomerVO>(allList);
                }
            }

            else 
            {
                if(allList != null)
                {
                    cList = allList.FindAll(n => n.Category.Equals(cboCategory.Text));
                    dgvCus.DataSource = null;
                    dgvCus.DataSource = new AdvancedList<CustomerVO>(cList);
                }
            
            }


            dgvCus.ClearSelection();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            List<CustomerVO> list;

            if (string.IsNullOrWhiteSpace(txtSerach.Text))
            {
                cboCategory_SelectedIndexChanged(this, e);
            }


            else
            {
                if (cboCategory.SelectedIndex == 0)
                {

                    cList = allList.FindAll(n => n.CustomerName.Contains(txtSerach.Text));

                    dgvCus.DataSource = null;
                    dgvCus.DataSource = new AdvancedList<CustomerVO>(cList);
                }

                else
                {
                    list = cList.FindAll(n => n.CustomerName.Contains(txtSerach.Text));

                    dgvCus.DataSource = null;
                    dgvCus.DataSource = new AdvancedList<CustomerVO>(list);

                }

            }

            dgvCus.ClearSelection();
        }

        private void txtSerach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSearch_Click(this, e);
            }
        }

        private void dgvCus_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvCus.ClearSelection();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmCustomer_Add frm = new frmCustomer_Add(((Main)this.MdiParent).EmpName.ToString());
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DataRebinding();
            }
        }

        private void DataRebinding()
        {
            allList = service.GetAsync<List<CustomerVO>>("api/Customer/GetCustomerlist").Data;
            dgvCus.DataSource = null;
            dgvCus.DataSource = new AdvancedList<CustomerVO>(allList);
            dgvCus.ClearSelection();
            cboCategory.Text = "전체";
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
      
            if (dgvCus.CurrentCell == null)
            {
                MessageBox.Show("수정하실 거래처를 선택해주세요.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CustomerVO VO = new CustomerVO()
            {
                CustomerID = dgvCus.SelectedRows[0].Cells["CustomerID"].Value.ToString(),
                CustomerName = dgvCus.SelectedRows[0].Cells["CustomerName"].Value.ToString(),
                CustomerPwd = dgvCus.SelectedRows[0].Cells["CustomerPwd"].Value.ToString(),
                Category = dgvCus.SelectedRows[0].Cells["Category"].Value.ToString(),
                Phone = dgvCus.SelectedRows[0].Cells["Phone"].Value.ToString(),
                Email = dgvCus.SelectedRows[0].Cells["Email"].Value.ToString(),
                Address = dgvCus.SelectedRows[0].Cells["Address"].Value.ToString(),
                EmpName = dgvCus.SelectedRows[0].Cells["EmpName"].Value.ToString(),
                ModifyUser = ((Main)this.MdiParent).EmpName.ToString(),
            };

            frmCustomer_Modify frm = new frmCustomer_Modify(VO);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                DataRebinding();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCus.CurrentCell == null)
            {
                MessageBox.Show("삭제하실 거래처를 선택해주세요.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"거래처 : {dgvCus.SelectedRows[0].Cells["CustomerName"].Value} 대해 삭제 하시겠습니까?", "거래처 삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                CustomerVO VO = new CustomerVO()
                {
                    CustomerID = dgvCus.SelectedRows[0].Cells["CustomerID"].Value.ToString(),
                };
               

                ResMessage<List<CustomerVO>> result = service.PostAsync<CustomerVO, List<CustomerVO>>("api/Customer/DeleteCustomer", VO);

                if (result.ErrCode == 0)
                {
                    MessageBox.Show("삭제가 완료되었습니다.", "거래처 삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataRebinding();

                }
                else
                    MessageBox.Show(result.ErrMsg);
            }
        }
    }
}
