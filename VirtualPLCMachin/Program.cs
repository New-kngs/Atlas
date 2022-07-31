using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Timers;

namespace VirtualPLCMachin
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3) return;

            Service srv = new Service(args[0], args[1], args[2]);
            srv.OnStart();

            Console.ReadLine();
        }
    }

    public class Service
    {
        TcpListener listener;
        NetworkStream ns;
        TcpClient tc;
        Timer timer1;
        string taskID, ip, port;
        public Service(string taskID, string ip, string port)
        {
            this.taskID = taskID;
            this.ip = ip;
            this.port = port;
        }
        public void OnStart()
        {
            Console.WriteLine($"{taskID} => 서비스 시작");
            if (listener == null)
            {
                listener = new TcpListener(IPAddress.Parse(ip), int.Parse(port));
            }

            AsyncServer();
        }
        private async void AsyncServer()
        {
            listener.Start();
            while (true)
            {
                tc = await listener.AcceptTcpClientAsync().ConfigureAwait(false);
                ns = tc.GetStream();

                timer1 = new Timer(3000);
                timer1.Elapsed += Timer1_Elapsed;
                timer1.AutoReset = true;
                timer1.Enabled = true;
            }
        }

        private void Timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            Random rnd = new Random();

            string msg = $"{rnd.Next(1, 77)}|{rnd.Next(10, 15)}|{rnd.Next(0, 2)}";
            byte[] buff = Encoding.Default.GetBytes(msg);
            ns.Write(buff, 0, buff.Length);
            Console.WriteLine("데이터 전송 : " + msg);
        }

        public void OnStop()
        {
            timer1.Enabled = false;
            ns.Close();
            tc.Close();
            listener.Stop();
        }

    }
}
