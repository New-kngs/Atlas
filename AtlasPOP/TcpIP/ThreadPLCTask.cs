using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AtlasPOP
{
    public delegate void ReadDataEventHandler(object sender, ReadDataEventArgs e);
    public class ThreadPLCTask
    {
        public event ReadDataEventHandler ReadDataReceive;

        ManualResetEvent manualEvent = new ManualResetEvent(false);
        Stopwatch m_aliveTimer = new Stopwatch();


        const string STR_ALIVE = "HeartBeat";
        Thread m_thread;
        Thread pg_thread;
        TcpControl client;
        LoggingUtility m_log;
        string hostIP;
        int hostPort;
        int timer_CONNECT, timer_KeepAlive, timer_Read;

        public bool ConnectStatus { get; set; }

        public ThreadPLCTask(LoggingUtility m_log, string hostIP, int hostPort, int timer_CONNECT, int timer_KeepAlive, int timer_Read)
        {
            this.m_log = m_log;
            this.hostIP = hostIP;
            this.hostPort = hostPort;
            this.timer_CONNECT = timer_CONNECT;
            this.timer_KeepAlive = timer_KeepAlive;
            this.timer_Read = timer_Read;

            m_aliveTimer.Stop();
        }

        public void ThreadStart()
        {
            //Reset() 차단기를 내린다.
            manualEvent.Reset();

            m_thread = new Thread(ExecuteThreadJob);
            m_thread.Start();
        }
        public void ThreadStop()
        {
            if (client == null || client.Client == null) return;

            lock (m_thread)
            {
                client.Client.Close();
                //Set() 차단기를 올린다.
                manualEvent.Set();
            }
        }

        

        private void ExecuteThreadJob()
        {
            while (!manualEvent.WaitOne(timer_Read))
            {
                try
                {
                    lock (m_thread)
                    {
                        WorkingSchedule();
                    }
                }
                catch (Exception err)
                {
                    m_log.WriteError("쓰레드 실행 중 오류 : " + err.Message);
                }
            }
        }

        private void WorkingSchedule()
        {
            if (!ConnectStatus)
            {
                //연결
                client = new TcpControl(hostIP, hostPort);
                if (client.Client.Connected)
                {
                    ConnectStatus = true;
                    m_aliveTimer.Restart();

                    m_log.WriteInfo("서버 접속");
                }
            }
            else
            {
                //keep alive 체크
                if (!m_aliveTimer.IsRunning || m_aliveTimer.Elapsed.TotalMilliseconds > timer_KeepAlive)
                {
                    if (!m_aliveTimer.IsRunning)
                        m_aliveTimer.Restart();

                    m_log.WriteInfo("재접속을 위한 연결종료");
                    ConnectStatus = false;
                    client.Client.Close();

                    client = new TcpControl(hostIP, hostPort);
                    if (client.Client.Connected)
                    {
                        ConnectStatus = true;
                        m_aliveTimer.Restart();

                        m_log.WriteInfo("서버 재접속 성공");
                    }
                }

                //데이터수신
                //50|20|1 , HeartBeat
                OnReceive();
            }
        }

        private void OnReceive()
        {
            if (client.Client.Available > 0)
            {
                //초기 데이터를 받아들이는 임시배열
                byte[] rcvTemp = new byte[client.Client.Available];

                //전송제어문자를 제외하고, 데이터만 있는 배열 => 인코딩대상
                byte[] rcvValue = new byte[client.Client.Available];

                client.DataStream.Read(rcvTemp, 0, rcvTemp.Length);

                bool bFind = false;
                int vIdx = 0;
                for (int i = 0; i < rcvTemp.Length; i++)
                {
                    if (rcvTemp[i] == 0x2)
                    {
                        for (int k = i + 1; k < rcvTemp.Length; k++)
                        {
                            if (rcvTemp[k] == 0x3)
                            {
                                bFind = true;
                                break;
                            }

                            rcvValue[vIdx] = rcvTemp[k];
                            vIdx++;
                        }
                        if (bFind)
                            break;
                    }
                }

                if (!bFind) return;

                //바이트배열의 비어있는 널문자를 공백(space)로 바꿔서 배열을 채워두고, 문자열로 변환해서 공백제거
                for (int i = 0; i < rcvValue.Length; i++)
                {
                    if (rcvValue[i] == 0x0) //널
                        rcvValue[i] = 0x20;
                }

                string readData = Encoding.UTF8.GetString(rcvValue).Replace(" ", "").Trim();
                m_log.WriteInfo("데이터 수신 : " + readData);

                //HeartBeat 인 경우는 stopwatch를 재시작하고 빠져나간다.
                if (readData.Contains(STR_ALIVE))
                {
                    m_aliveTimer.Restart();
                    return;
                }

                if (ReadDataReceive != null)
                {
                    ReadDataEventArgs args = new ReadDataEventArgs();
                    args.ReadData = readData;

                    ReadDataReceive(this, args);
                }

                // 50|20|1
                string[] datas = readData.Split('|');
                if (datas.Length != 3) return;

 

                //DB Insert

                }
            }
        }
    

    public class ReadDataEventArgs : EventArgs
    {
        public string ReadData { get; set; }
    }
}
