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

    
    public partial class frmProcess : BaseForm
    {
        ServiceHelper srv = null;
        public frmProcess()
        {
            InitializeComponent();
        }

        private void frmProcess_Load(object sender, EventArgs e)
        {
            DataGridUtil.SetInitGridView(dgvProcess);
            DataGridUtil.AddGridTextBoxColumn(dgvProcess, "공정ID", "ProcessID", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvProcess, "공정명", "ProcessName", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvProcess, "불량확인여부", "FailCheck", colwidth: 300, align: DataGridViewContentAlignment.MiddleCenter);
            DataGridUtil.AddGridTextBoxColumn(dgvProcess, "생성날짜", "CreateDate");
            DataGridUtil.AddGridTextBoxColumn(dgvProcess, "생성사용자", "CreateUser", colwidth: 300, align: DataGridViewContentAlignment.MiddleLeft);
            DataGridUtil.AddGridTextBoxColumn(dgvProcess, "변경날짜", "ModifyDate");
            DataGridUtil.AddGridTextBoxColumn(dgvProcess, "변경사용자", "ModifyUser", colwidth: 200, align: DataGridViewContentAlignment.MiddleLeft);

            LoadData();
        }
        public void LoadData()
        {
            srv = new ServiceHelper("api/Process");
            ResMessage<List<ProcessVO>> result = srv.GetAsync<List<ProcessVO>>("AllProcess");
            if (result != null)
            {
                dgvProcess.DataSource = new AdvancedList<ProcessVO>(result.Data);
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmProcess_Add frm = new frmProcess_Add();
            if(frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            ProcessVO process = new ProcessVO()
            {
                ProcessName = (dgvProcess.SelectedRows[0].Cells["ProcessName"].Value).ToString(),
                FailCheck = (dgvProcess.SelectedRows[0].Cells["FailCheck"].Value).ToString()
            };
            frmPorcess_Modify frm = new frmPorcess_Modify(process);
            frm.ShowDialog();
        }
        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            frmProcess_Delete frm = new frmProcess_Delete();
            frm.ShowDialog();
        }

        private void frmProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            srv.Dispose();
        }
    }
}
