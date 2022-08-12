using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace AltasMES
{

    public delegate void ReadCompletedHandler(object sender, frmShip_End.ReadEventArgs e);

    public partial class frmShip_End : Form
    {

        public event ReadCompletedHandler ReadCompleted;

        SerialPort _port;
        bool IsOpen = false;

        public frmShip_End()
        {
            InitializeComponent();
        }

        private void frmShip_End_Load(object sender, EventArgs e)
        {
            
            SerialPortConnect();
            ReadCompleted += FrmShip_End_ReadCompleted;
        }

        private void FrmShip_End_ReadCompleted(object sender, ReadEventArgs e)
        {
            textBox1.Text = e.ReadMessage;
        }

        private void SerialPortConnect()
        {
            if (IsOpen) return;

            if (_port == null)
            {
                _port = new SerialPort();
                _port.DataReceived += _port_DataReceived;
            }

            _port.PortName = Properties.Settings.Default.PortName;
            _port.BaudRate = Convert.ToInt32(Properties.Settings.Default.BaudRate);
            _port.DataBits = Convert.ToInt32(Properties.Settings.Default.DataSize);

            Parity par = Parity.None;
            if (Properties.Settings.Default.Parity == "odd")
                par = Parity.Odd;
            else if (Properties.Settings.Default.Parity == "even")
                par = Parity.Even;

            _port.Parity = par;
            _port.Handshake = Handshake.None;

            try
            {
                if (_port.IsOpen)
                {
                   _port.Close();
                }
                _port.Open();
                IsOpen = true;
                MessageBox.Show("연결성공");
            }
            catch (Exception err)
            {
                IsOpen = false;
                MessageBox.Show(err.Message);
            }
        }

        private void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            System.Threading.Thread.Sleep(500);

            string msg = _port.ReadExisting();

            this.Invoke(new EventHandler(delegate
            {
                if (ReadCompleted != null)
                {
                    ReadEventArgs args = new ReadEventArgs();
                    args.ReadMessage = msg;
                    ReadCompleted(this, args);
                }
            }));
        }

        public class ReadEventArgs : EventArgs
        {
            public string ReadMessage { get; set; }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            if (_port.IsOpen)
            {
                _port.Close();
            }

            //2.설정폼을 열고,
            frmPortSetting frm = new frmPortSetting();
            frm.ShowDialog();

            //3.변경된 설정정보로 다시 재연결
            if (!string.IsNullOrEmpty(Properties.Settings.Default.PortName))
            {
                IsOpen = false;
                SerialPortConnect();
            }
        }

        private void frmShip_End_FormClosing(object sender, FormClosingEventArgs e)
        {
            _port.Close();

        }
    }
}
