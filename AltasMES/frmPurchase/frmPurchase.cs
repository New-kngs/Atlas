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

namespace AltasMES
{
    public partial class frmPurchase : BaseForm
    {
        ServiceHelper srv = null;
        List<PurchaseVO> purchaseList = null;

        string selId = string.Empty;

        public frmPurchase()
        {
            InitializeComponent();
        }        

        private void frm_Purchase_Load(object sender, EventArgs e)
        {
            srv = new ServiceHelper("");            

            DataGridUtil.SetInitGridView(dgvPurchase);
            DataGridUtil.AddGridTextBoxColumn(dgvPurchase, "발주ID", "PurchaseID", colwidth: 100, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPurchase, "거래처명", "CustomerName", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvPurchase, "입고여부", "InState", colwidth: 110, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPurchase, "입고완료일", "PurchaseEndDate", colwidth: 160, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPurchase, "창고명", "WHName", colwidth: 110, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPurchase, "생성사용자", "CreateUser", colwidth: 120, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPurchase, "발주날짜", "CreateDate", colwidth: 170, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPurchase, "변경사용자", "ModifyUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvPurchase, "변경날짜", "ModifyDate", colwidth: 170, align: DataGridViewContentAlignment.MiddleCenter);


            dtpTo.Value = DateTime.Now;
            dtpFrom.Value = DateTime.Now.AddDays(-7);

            cboStateYN.Items.AddRange(new string[] { "전체", "Y", "N" });
            cboStateYN.SelectedIndex = 0;

            LoadData();
        }

        public void LoadData()
        {         
            purchaseList = srv.GetAsync<List<PurchaseVO>>("api/Purchase/GetSearchPurchase/" + dtpFrom.Value.ToShortDateString() + "/" + dtpTo.Value.AddDays(1).ToShortDateString()).Data; //"yyyy-MM-dd HH:mm:ss"

            if (purchaseList != null)
            {
                List<PurchaseVO> list = null;

                if (cboStateYN.SelectedIndex > 0)
                {
                    if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                    {
                        list = purchaseList.FindAll(p => p.CustomerName.Contains(txtSearch.Text.Trim()) && p.InState.Equals(cboStateYN.Text));
                    }
                    else
                    {
                        list = purchaseList.FindAll(p => p.InState.Equals(cboStateYN.Text));
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                    {
                        list = purchaseList.FindAll(p => p.CustomerName.Contains(txtSearch.Text.Trim()));
                    }
                    else
                    {
                        list = purchaseList;
                    }
                }

                dgvPurchase.DataSource = null;
                dgvPurchase.DataSource = new AdvancedList<PurchaseVO>(list);
            }
            else
            {
                MessageBox.Show("검색 내용이 없습니다.");
                return;
            }
        }


        

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();           
        }

        private void cboStateYN_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void frm_Purchase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSearch_Click(this, e);
            }
        }

        private void frm_Purchase_Shown(object sender, EventArgs e)
        {
            dgvPurchase.ClearSelection();
        }

        private void frm_Purchase_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (srv != null)
            {
                srv.Dispose();
            }
        }       

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSearch_Click(this, e);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            PurchaseVO item = new PurchaseVO()
            {
                CreateUser = ((Main)this.MdiParent).EmpName.ToString()
            };
            frmPurchase_Add pop = new frmPurchase_Add(item);
            if (pop.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

       

        private void dgvPurchase_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            selId = (dgvPurchase[0, e.RowIndex].Value).ToString();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (!dgvPurchase.CurrentCell.Selected || selId == string.Empty)
            {
                MessageBox.Show("발주 항목을 선택해 주세요", "정보", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
