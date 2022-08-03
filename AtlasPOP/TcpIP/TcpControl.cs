using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AtlasPOP
{

    public class TcpControl
    {
        TcpClient client;
        NetworkStream dataStream;

        public TcpClient Client { get { return client; } }
        public NetworkStream DataStream { get { return dataStream; } }

        public TcpControl(string host, int port)
        {
            client = new TcpClient(host, port);
            dataStream = client.GetStream();
        }

        public bool CheckConnection()
        {
            bool bStatus = false;
            try
            {
                if (client != null && client.Client != null && client.Client.Connected)
                {
                    if (client.Client.Available == 0 && client.Client.Poll(2000, SelectMode.SelectRead))
                        bStatus = false;
                    else
                        bStatus = true;
                }
                return bStatus;
            }
            catch
            {
                return false;
            }
        }

        public bool Send(byte[] data)
        {
            try
            {
                dataStream.Write(data, 0, data.Length);
                dataStream.Flush();
                return true;
            }
            catch (Exception err)
            {
                Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} : {err.Message}");
                return false;
            }
        }
    }
}
