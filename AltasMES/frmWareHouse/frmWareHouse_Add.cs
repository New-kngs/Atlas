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
    public partial class frmWareHouse_Add : Form
    {
        ServiceHelper service = null;
        public WareHouseVO wareHouse { get; set; }
        public frmWareHouse_Add(WareHouseVO ware)
        {
            InitializeComponent();
            this.wareHouse = ware;
        }

        private void frmWareHouse_Add_Load(object sender, EventArgs e)
        {
            service = new ServiceHelper("");
            txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            cboPdt.Items.AddRange(new string[] { "선택", "완제품", "반제품", "자재" });
            cboPdt.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {         
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("창고명을 입력해주세요");
                return;
            }
            
            ResMessage<List<WareHouseVO>> volist = service.GetAsync<List<WareHouseVO>>("api/WareHouse/AllWareHouse");

            string WHName = txtName.Text;
            List<WareHouseVO> resultVO = volist.Data.FindAll((r) => r.WHName == WHName);
            if (resultVO.Count > 0)
            {
                MessageBox.Show("존재하는 창고명 입니다.");
                txtName.Clear();
                return;
            }
            
            if (cboPdt.SelectedIndex == 0)
            {
                MessageBox.Show("제품유형을 선택하여주시기 바랍니다.");
                return;
            }
            service = new ServiceHelper("api/WareHouse");
            WareHouseVO wareHouse = new WareHouseVO
            {
                WHName = txtName.Text,
                ItemCategory = cboPdt.Text,
                CreateDate = txtDate.Text,
                CreateUser = this.wareHouse.CreateUser
            };

            ResMessage<List<WareHouseVO>> result = service.PostAsync<WareHouseVO, List<WareHouseVO>>("SaveWareHouse", wareHouse);

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
