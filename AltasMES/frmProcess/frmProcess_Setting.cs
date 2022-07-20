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
        List<ProcessVO> processList = null;
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
            ResMessage<List<ComboItemVO>> result = service.GetAsync<List<ComboItemVO>>("GetEquipName");
            if (result != null)
            {
                //dgvProcess.DataSource = new AdvancedList<ProcessVO>(result.Data);
                CommonUtil.ComboBinding(cboEquip, result.Data, "설비", blankText: "선택");
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }

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
            processList = new List<ProcessVO>()
            {

            };
        }
    }
}
