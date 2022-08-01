
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

namespace AtlasPOP
{
    public partial class frmPerformance : Form
    {
        popServiceHelper service = null;
        public frmPerformance()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

        private void frmPerformance_Load(object sender, EventArgs e)
        {
            service = new popServiceHelper("");
            ResMessage<List<OperationVO>> result = service.GetAsync<List<OperationVO>>("api/pop/AllOperation");

            List<OperationVO> operList = result.Data.FindAll((p) => p.OpState.Equals("작업중"));

            if (result.Data != null)
            {
                
                int iRow = (int)Math.Ceiling(operList.Count / 3.0);

                int idx = 0;
                for (int r = 0; r < iRow; r++)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        if (idx >= operList.Count) break;

                        Monitoring item = new Monitoring(operList[c]);
                        item.Name = $"process";
                        item.Location = new Point(344 * c + 5, 217 * r + 5);
                        item.Size = new Size(344, 217);
                        //item.MovieInfo = currentList[idx];

                        panel1.Controls.Add(item);
                        idx++;
                    }
                }


            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }
    }
}
