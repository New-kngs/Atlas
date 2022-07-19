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
    public partial class frmLogin : Form
    {

        ServiceHelper service = null;
        List<EmployeeVO> list = null;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

            service = new ServiceHelper("api/Employee");
            ResMessage<List<EmployeeVO>> result = service.GetAsync<List<EmployeeVO>>("AllEmployee");
            if (result != null)
            {
                list = result.Data;
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }


        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtID.Text))
            {

                MessageBox.Show("ID를 입력해주세요");
                return;
            }

            //if(string.IsNullOrWhiteSpace)
        }

    }
}
