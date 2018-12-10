using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class UdpState
{
    public UdpClient Client;
    public IPEndPoint EndPoint;

    public UdpState(UdpClient client)
    {
        Client = client;
    }
}

