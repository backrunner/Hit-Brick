using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Hit_Brick_Server
{
    public class UdpState
    {
        public UdpClient client;
        public IPEndPoint remoteEndPoint;
        public const int bufferSize = 4096;
        public byte[] buffer = new byte[bufferSize];

        public UdpState(UdpClient client)
        {
            this.client = client;
        }
    }
}
