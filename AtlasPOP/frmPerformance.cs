
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
using System.Threading;
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
        int timer_CONNECT;
        int timer_KeepAlive;
        int timer_Read;
        string taskID;
        int qty;
        int time;
        int totQty = 0;
        int totfail = 0;
        int procID;
        ThreadPLCTask m_thread;
        BackgroundWorker worker;
        LoggingUtility m_log;
        popServiceHelper service;
        AtlasPOP main;
        ResMessage<List<EquipDetailsVO>> equip = null;
        EquipList item;
        public OperationVO oper { get; set; }
        List<EquipDetailsVO> EquipList = null;
        public frmPerformance(string task, string IP, string Port, OperationVO oper, int processid, AtlasPOP main)
        {
            InitializeComponent();


            this.oper = oper;
            this.procID = processid;
            hostIP = IP;
            hostPort = int.Parse(Port);
            taskID = task;
            service = new popServiceHelper("");
            this.main = main;

            timer_CONNECT = timer_Connec.Interval = int.Parse(ConfigurationManager.AppSettings["timer_Connect"]);
            timer_KeepAlive = int.Parse(ConfigurationManager.AppSettings["timer_KeepAlive"]);
            timer_Read = int.Parse(ConfigurationManager.AppSettings["timer_Read"]);

            m_log = new LoggingUtility(taskID, Level.Debug, 30);

            worker = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_progressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }
        private void frmPerformance_Load(object sender, EventArgs e)
        {
            txtOp.Text = oper.OpID;
            txtItem.Text = oper.ItemName;
            txtOrder.Text = oper.OrderID;
            txtProc.Text = oper.ProcessName;
            txtQty.Text = oper.PlanQty.ToString();

            drawEquip();
            if (worker.IsBusy != true) //스레드 중복 실행 방지
            {
                m_log.WriteInfo("PLC프로그램 시작");
                m_thread = new ThreadPLCTask(m_log, hostIP, hostPort, timer_CONNECT, timer_KeepAlive, timer_Read);
                m_thread.ReadDataReceive += M_thread_ReadDataReceive;
                m_thread.ThreadStart();
                worker.RunWorkerAsync();//스레드 시작
                timer_Connec.Start();
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                m_log.WriteInfo("쓰레드 취소 종료요청");
            }
            else
            {
                m_log.WriteInfo("쓰레드 종료");
            }
        }

        private void worker_progressChanged(object sender, ProgressChangedEventArgs e)
        {
            pgState.Maximum = oper.PlanQty;
            pgState.Value = e.ProgressPercentage;
            label2.Text = e.ProgressPercentage.ToString();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int count = 0;

            while (count <= oper.PlanQty)
            {
                int ss = oper.PlanQty / EquipList.Count;
                if (count >= ss)
                {
                    opFinish(this,e);
                }


                if (worker.CancellationPending == true) //쓰레드 취소 요청시
                {
                    e.Cancel = true;
                    break;
                }
                m_log.WriteInfo(count.ToString());
                worker.ReportProgress(count);
                count++;
                Thread.Sleep(1000);
            }
            
        }

        private void opFinish(object sender, DoWorkEventArgs e)
        {
            //몰라
            item.DrawState("작업종료");
        }

        private void M_thread_ReadDataReceive(object sender, ReadDataEventArgs e)
        {
            txtReadPLC.Invoke(new Action<string>((str) => txtReadPLC.Text = str), e.ReadData);


            string[] datas = e.ReadData.Split('|');
            if (datas.Length != 3) return;
            qty = int.Parse(datas[0]);
            totQty += int.Parse(datas[1]);


            this.Invoke((MethodInvoker)(() => txtTotQty.Text = totQty.ToString("#,##0")));

            time = qty / EquipList.Count;

            if (qty >= 0 && qty <= 10)
                totfail = 0;
            else if (qty >= 11 && qty <= 20)
                totfail = 1;
            else if (qty >= 21 && qty <= 30)
                totfail = 2;
            else if (qty >= 31 && qty <= 50)
                totfail = 3;
            else
                totfail = 4;

            this.Invoke((MethodInvoker)(() => txtFail.Text = totfail.ToString()));

            if (Convert.ToInt32(datas[0]) <= (Convert.ToInt32(txtTotQty.Text) + totfail))
            {
                DialogResult result = MessageBox.Show("작업이 끝났습니다", "작업 종료", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    worker.CancelAsync();
                    timer_Connec.Stop();
                    main.Finish(totQty, totfail, hostPort.ToString());
                }
                this.Close();
            }
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

        public void drawEquip()
        {
            int procID = oper.ProcessID;
            string OperID = oper.OpID;

            equip = service.GetAsync<List<EquipDetailsVO>>("api/pop/GetEquip");
            EquipList = equip.Data.FindAll((p) => p.ProcessID == procID);

            if (equip.Data != null)
            {
                int iRow = (int)Math.Ceiling(EquipList.Count / 1.0);

                int idx = 0;
                for (int c = 0; c < iRow; c++)
                {
                    if (idx >= EquipList.Count) break;
                    item = new EquipList(EquipList[c], OperID);
                    item.Name = $"process";
                    item.Location = new Point(224 * c + 5, 3);
                    item.Size = new Size(214, 154);


                    panel2.Controls.Add(item);
                    idx++;
                }
            }
            else
            {
                MessageBox.Show("서비스 호출 중 오류가 발생했습니다. 다시 시도하여 주십시오.");
            }
        }

        private void timer_Connec_Tick(object sender, EventArgs e)
        {
            pgState.PerformStep();
        }
    }
}
