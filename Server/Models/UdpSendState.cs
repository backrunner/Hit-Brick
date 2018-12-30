using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Hit_Brick_Server
{
    public class UdpSendState
    {
        public UdpClient client;
        public IPEndPoint remoteEndPoint;
        public string message;

        public UdpSendState(UdpClient client)
        {
            this.client = client;
        }

        public UdpSendState(UdpClient client, string message)
        {
            this.client = client;
            this.message = message;
        }

        public UdpSendState(UdpClient client, IPEndPoint remoteEndPoint)
        {
            this.client = client;
            this.remoteEndPoint = remoteEndPoint;
        }
    }
}
