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
    public partial class frmBOM_Add : Form
    {
        ServiceHelper service = null;

        public BOMVO bom { get; set; }
        public frmBOM_Add(BOMVO bom)
        {
            InitializeComponent();
            this.bom = bom;
        }

        private void frmBOM_Add_Load(object sender, EventArgs e)
        {
            //BOMID, B.ItemID, ParentID, ChildID, UnitQty, B.CreateDate, B.CreateUser, B.ModifyDate, B.ModifyUser, ItemName, ItemCategory, ItemSize
            label2.Visible = cboPdt.Visible = false;

            cboCategory.Items.AddRange(new string[] { "전체", "완제품", "반제품" });
            cboCategory.SelectedIndex = 0;

            DataGridUtil.SetInitGridView(dgvUnreg);
            DataGridUtil.AddGridTextBoxColumn(dgvUnreg, "제품ID", "ItemID", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvUnreg, "제품이름", "ItemName", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);   

            DataGridUtil.SetInitGridView(dgvParts);
            DataGridUtil.AddGridTextBoxColumn(dgvParts, "제품ID", "ItemID", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvParts, "제품이름", "ItemName", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridViewButtonColumn btnA = new DataGridViewButtonColumn();
            btnA.HeaderText = "추가";
            btnA.Text = "추가";
            btnA.Width = 70;
            btnA.DefaultCellStyle.Padding = new Padding(5, 1, 5, 1);
            btnA.UseColumnTextForButtonValue = true;

            dgvParts.Columns.Add(btnA);

            DataGridUtil.SetInitGridView(dgvNew);
            DataGridUtil.AddGridTextBoxColumn(dgvNew, "제품ID", "ItemID", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvNew, "제품이름", "ItemName", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvNew, "수량", "UnitQty", colwidth: 70, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridViewButtonColumn btnB = new DataGridViewButtonColumn();
            btnB.HeaderText = "삭제";
            btnB.Text = "삭제";
            btnB.Width = 70;
            btnB.DefaultCellStyle.Padding = new Padding(5, 1, 5, 1);
            btnB.UseColumnTextForButtonValue = true;

            dgvNew.Columns.Add(btnB);

            service = new ServiceHelper("");
        }
    }
}
