
using AtlasDTO;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlasPOP
{
    public partial class frmPerformance : Form
    {
        bool bExit = false;

        bool logVisible = false;
        string hostIP;
        int hostPort;
        TcpControl client;
        string connStr;
        int workID;
        int timer_CONNECT;
        int timer_KeepAlive;
        int timer_Read;
        string taskID;

        int totQty = 0;

        ThreadPLCTask m_thread;
        LoggingUtility m_log;
        popServiceHelper service = null;
        
        public bool TaskExit { get { return bExit; } set { bExit = value; } }

        public frmPerformance(string task, string IP, string Port)
        {
            InitializeComponent();

            hostIP = IP;
            hostPort = int.Parse(Port);
            taskID = task;
            //workID = int.Parse(taskID.Replace("PLC_", ""));

            timer_CONNECT = timer_Connec.Interval = int.Parse(ConfigurationManager.AppSettings["timer_Connect"]);
            timer_KeepAlive = int.Parse(ConfigurationManager.AppSettings["timer_KeepAlive"]);
            timer_Read = int.Parse(ConfigurationManager.AppSettings["timer_Read"]);

            m_log = new LoggingUtility(taskID, Level.Debug, 30);
        }


        private void frmPerformance_Load(object sender, EventArgs e)
        {
            m_log.WriteInfo("PLC프로그램 시작");

            m_thread = new ThreadPLCTask( m_log, workID, hostIP, hostPort, timer_CONNECT, timer_KeepAlive, timer_Read);

            m_thread.ReadDataReceive += M_thread_ReadDataReceive;
            m_thread.ThreadStart();

            timer_Connec.Start();
        }

        private void M_thread_ReadDataReceive(object sender, ReadDataEventArgs e)
        {
            txtReadPLC.Invoke(new Action<string>((str) => txtReadPLC.Text = str), e.ReadData);

            if (logVisible)
            {
                if (listBox1.Items.Count > 50)
                {
                    listBox1.Items.Clear();
                }

                listBox1.Invoke(new Action<string>((str) => listBox1.Items.Add($"[{DateTime.Now.ToString("HH:mm:ss,fff")}]:{str}")), e.ReadData);

                this.Invoke((MethodInvoker)(() =>
                listBox1.SelectedIndex = listBox1.Items.Count - 1));

            }

            string[] datas = e.ReadData.Split('|');
            if (datas.Length != 3) return;

            totQty += int.Parse(datas[1]);
            this.Invoke((MethodInvoker)(() => txtTotQty.Text = totQty.ToString("#,##0")));
        }

        private void frmPerformance_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!bExit)
            {
                this.Hide();
                e.Cancel = true;
            }
            else
            {
                m_log.RemoveRepository(taskID);
                m_thread.ThreadStop();
            }
        }

        private void timer_Connect_Tick(object sender, EventArgs e)
        {
            try
            {
                if (m_thread.ConnectStatus)
                    lblState.BackColor = Color.Green;
                else
                    lblState.BackColor = Color.Red;
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
            }
        }

        private void txtTotQty_TextChanged(object sender, EventArgs e)
        {
            string[] datas = txtReadPLC.Text.Split('|');
            if (Convert.ToInt32(datas[0]) == Convert.ToInt32(txtTotQty.Text))
            {
                m_log.RemoveRepository(taskID);
                m_thread.ThreadStop();

                
            }
        }
    }
}
