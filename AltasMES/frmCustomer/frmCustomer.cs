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


        private void frmCustomer_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "거래처";
            groupBox2.Text = "검색조건";
            groupBox3.Text = "거래처 현황";

            service = new ServiceHelper("");
            allList = service.GetAsync<List<CustomerVO>>("api/Customer/GetCustomerlist").Data;


            DataGridUtil.SetInitGridView(dgvCus);

            
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "거래처ID", "CustomerID", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "거래처명", "CustomerName", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "비밀번호", "CustomerPwd",visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "구분", "Category");
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "연락처", "Phone", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "이메일", "Email", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "주소", "Address", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "거래처담당자", "EmpName", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "거래처담당자ID", "EmpID",visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "생성날짜", "CreateDate", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "생성사용자", "CreateUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "수정날짜", "ModifyDate", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "수정사용자", "ModifyUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvCus, "사용여부", "StateYN", visibility: false);

            dgvCus.DataSource = new AdvancedList<CustomerVO>(allList);


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
    }
}
