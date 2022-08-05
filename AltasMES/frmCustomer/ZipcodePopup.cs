using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AltasMES
{
    public partial class ZipcodePopup : Form
    {


        private string address1, address2, address3;
        public string Address1 { get { return address1; } }
        public string Address2 { get { return address2; } }
        public string Address3 { get { return address3; } }

        public ZipcodePopup()
        {
            InitializeComponent();
        }

      

        private void ZipcodePopup_Load(object sender, EventArgs e)
        {
            DataGridUtil.SetInitGridView(dgvZip);
            DataGridUtil.AddGridTextBoxColumn(dgvZip, "우편번호", "zipNo", colwidth: 80);
            DataGridUtil.AddGridTextBoxColumn(dgvZip, "도로명주소", "roadAddr", colwidth: 200);
            DataGridUtil.AddGridTextBoxColumn(dgvZip, "지번주소", "jibunAddr", colwidth: 200);
            DataGridUtil.AddGridTextBoxColumn(dgvZip, "주소", "roadAddrPart1", colwidth: 10, visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvZip, "주소", "roadAddrPart2", colwidth: 10, visibility: false);
            DataGridUtil.AddGridTextBoxColumn(dgvZip, "주소", "bdNm", colwidth: 10, visibility: false);

        }

        private void dgvZip_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; }
            txtRoadZipcode.Text = txtJibunZipCode.Text = dgvZip["zipNo", e.RowIndex].Value.ToString();

            txtRoadAddr1.Text = dgvZip["roadAddrPart1", e.RowIndex].Value.ToString();
            txtRoadAddr2.Text = $"{dgvZip["roadAddrPart2", e.RowIndex].Value} {dgvZip["bdNm", e.RowIndex].Value}";

            txtJibunAddr1.Text = dgvZip["jibunAddr", e.RowIndex].Value.ToString();
        }

       
        private void btnSerach_Click(object sender, EventArgs e)
        {
            //유효성검사
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("검색하실 주소를 입력하세요.");
                return;
            }

            string apiKey = $"devU01TX0FVVEgyMDIyMDgwNTEyMzkzNjExMjg1NDE=";
            string apiUrl = $"https://www.juso.go.kr/addrlink/addrLinkApi.do?confmKey={apiKey}&currentPage=1&countPerPage=100&keyword={txtSearch.Text}";


            WebClient wc = new WebClient();
            XmlReader reader = new XmlTextReader(wc.OpenRead(apiUrl));

            DataSet ds = new DataSet();
            ds.ReadXml(reader);

            if (ds.Tables.Count > 1)
            {
                dgvZip.DataSource = ds.Tables[1];
            }
            else
            {
                MessageBox.Show(ds.Tables[0].Rows[0]["errorMessage"].ToString());
            }


        }


        private void btnRoad_Click(object sender, EventArgs e)
        {

            if (txtRoadAddr1.Text.Length < 1)
            {
                MessageBox.Show("주소를 검색하여 선택해주세요.");
            }
            else
            {
                address1 = txtRoadZipcode.Text;
                address2 = txtRoadAddr1.Text;
                address3 = txtRoadAddr2.Text;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }


        private void btnJibun_Click(object sender, EventArgs e)
        {
            if (txtJibunAddr1.Text.Length < 1)
            {
                MessageBox.Show("주소를 검색하여 선택해주세요.");
            }
            else
            {
                address1 = txtJibunZipCode.Text;
                address2 = txtJibunAddr1.Text;
                address3 = txtJibunAddr2.Text;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }



    }
}
