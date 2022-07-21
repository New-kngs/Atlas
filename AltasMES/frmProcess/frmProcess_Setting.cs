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
    public partial class frmProcess_Setting : Form
    {
        public ProcessVO process { get; set; }
        ServiceHelper service = null;
        List<EquipDetailsVO> processList = null;
        EquipDetailsVO newEquip;
        ResMessage<List<ComboItemVO>> result;
        ResMessage<List<ProcessVO>> allList;
        public frmProcess_Setting(ProcessVO process)
        {
            InitializeComponent();

            this.process = process;
            txtProcess.Text = process.ProcessName;
        }
        private void fmrProcess_Setting_Load(object sender, EventArgs e)
        {
            //설비목록 가져오기
            service = new ServiceHelper("api/Process");
            allList = service.GetAsync<List<ProcessVO>>("AllProcess");
            result = service.GetAsync<List<ComboItemVO>>("GetEquipName");
            if (result != null)
            {
                //dgvProcess.DataSource = new AdvancedList<ProcessVO>(result.Data);
                CommonUtil.ComboBinding(cboEquip, result.Data, "설비", blankText: "선택");
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }

            DataGridUtil.SetInitGridView(dgvList);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "공정ID", "ProcessID", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter, visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "설비ID", "EquipID", colwidth: 200, align: DataGridViewContentAlignment.MiddleCenter, visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "생성사용자", "CreateUser", colwidth: 150, align: DataGridViewContentAlignment.MiddleCenter, visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvList, "설비명", "EquipName", colwidth: 250, align: DataGridViewContentAlignment.MiddleCenter);




        }
        public void LoadData()
        {
            //필요할려나..?
        }

        private void frmProcess_Setting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (service != null)
                service.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (processList == null)
            {
                processList = new List<EquipDetailsVO>();
            }
            if (cboEquip.SelectedIndex == 0)
            {
                MessageBox.Show("추가할 설비가 없습니다. \n설비를 선택해 주세요");
                return;
            }

            int code = Convert.ToInt32(result.Data.Find((c) => c.CodeName.Equals(cboEquip.Text)).Code);
            int idx = processList.FindIndex((p) => p.EquipID == code);
            if (idx >= 0)
            {
                MessageBox.Show("이미 설비가 있습니다.");
                return;
            }
            else
            {
                newEquip = new EquipDetailsVO()
                {
                    EquipID = Convert.ToInt32(result.Data.Find((c) => c.CodeName.Equals(cboEquip.Text)).Code),
                    ProcessID = allList.Data.Find((p) => p.ProcessName.Equals(txtProcess.Text)).ProcessID,
                    CreateUser = process.CreateUser,
                    EquipName = cboEquip.Text
                };



                processList.Add(newEquip);
                cboEquip.SelectedIndex = 0;

            }


            dgvList.DataSource = null;
            dgvList.DataSource = processList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvList.SelectedRows.Count < 1)
            {
                MessageBox.Show("삭제할 설비를 선택해 주세요");
                return;
            }
            if (processList == null)
            {
                processList = new List<EquipDetailsVO>();
            }

            string ptCode = dgvList.SelectedRows[0].Cells["EquipID"].Value.ToString();

            EquipDetailsVO itemList = processList.Find((p) => p.EquipID.ToString() == ptCode);
            processList.Remove(itemList);

            dgvList.DataSource = null;
            dgvList.DataSource = processList;
            dgvList.ClearSelection();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            service = new ServiceHelper("api/Process");
          
                
                ResMessage<List<EquipDetailsVO>> result = service.PostAsync<List<EquipDetailsVO>, List<EquipDetailsVO>>("SaveProcessEquip", processList);

                if (result.ErrCode == 0)
                {
                    MessageBox.Show("성공적으로 등록되었습니다.");
                    this.DialogResult = DialogResult.OK;
                }
                else
                    MessageBox.Show(result.ErrMsg);

            }
    }
}
