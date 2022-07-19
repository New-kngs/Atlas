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
        public frmWarehouse()
        {
            InitializeComponent();
        }

        private void frmWarehouse_Load(object sender, EventArgs e)
        {
            DataGridUtil.SetInitGridView(dgvWH);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "창고ID", "WHID", align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "창고이름", "WHName", align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "제품유형", "ItemCategory", align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "생성날짜", "CreateDate", align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "생성사원", "CreateUser", align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "변경날짜", "ModifyDate", align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "변경사원", "ModifyUser", align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvWH, "미사용여부", "DeletedYN", align: DataGridViewContentAlignment.MiddleCenter);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmWareHouse_Add frm = new frmWareHouse_Add();
            frm.ShowDialog();
        }

        
    }
}
