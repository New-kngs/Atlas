using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;

namespace AltasMES
{
    public partial class frmPortSetting : Form
    {


        SerialPort _port;
        bool IsOpen = false;


        public frmPortSetting()
        {
            InitializeComponent();
        }

        private void frmPortSetting_Load(object sender, EventArgs e)
        {


            cboComPort.DataSource = SerialPort.GetPortNames();

            //저장된 설정값을 읽어서 설정화면의 초기값으로 셋팅
            cboComPort.SelectedIndex = cboComPort.Items.IndexOf(Properties.Settings.Default.PortName);
            cboBaudRate.SelectedIndex = cboBaudRate.Items.IndexOf(Properties.Settings.Default.BaudRate);
            cboDataSize.SelectedIndex = cboDataSize.Items.IndexOf(Properties.Settings.Default.DataSize);
            cboParity.SelectedIndex = cboParity.Items.IndexOf(Properties.Settings.Default.Parity);
            cboHandShake.SelectedIndex = cboHandShake.Items.IndexOf(Properties.Settings.Default.Handshake);

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!IsOpen)  //연결버튼을 클릭한 경우
            {
                if (_port == null)
                {
                    _port = new SerialPort();
                    _port.DataReceived += _port_DataReceived;
                    //_port.ErrorReceived
                }

                _port.PortName = cboComPort.SelectedItem.ToString();
                _port.BaudRate = Convert.ToInt32(cboBaudRate.SelectedItem);
                _port.DataBits = Convert.ToInt32(cboDataSize.SelectedItem);
                _port.Parity = (Parity)cboParity.SelectedIndex;
                _port.Handshake = (Handshake)cboHandShake.SelectedIndex;

                try
                {
                    _port.Open();
                    btnConnect.Text = "연결끊기";
                    textBox1.AppendText("연결 됨");

                    IsOpen = true;
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
        }

        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string msg = _port.ReadExisting();
            this.Invoke(new EventHandler(delegate { textBox1.AppendText(msg); }));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PortName = cboComPort.Text;
            Properties.Settings.Default.BaudRate = cboBaudRate.Text;
            Properties.Settings.Default.DataSize = cboDataSize.Text;
            Properties.Settings.Default.Parity = cboParity.Text;
            Properties.Settings.Default.Handshake = cboHandShake.Text;

            Properties.Settings.Default.Save();

            MessageBox.Show("시리얼포트 설정이 저장되었습니다.");
            this.Close();

        }

        private void frmPortSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_port != null)
            {
                if (IsOpen || _port.IsOpen)
                    _port.Close();
            }
        }
    }

}
