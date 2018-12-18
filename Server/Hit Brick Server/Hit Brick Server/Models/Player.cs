using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hit_Brick_Server
{
    public class Player
    {
        public string GUID;
        public string Name;
        public IPEndPoint EndPoint;

        public Player(string guid,string name)
        {
            GUID = guid;
            Name = name;
            EndPoint = null;
        }

        public Player(string guid, string name, IPEndPoint endPoint)
        {
            GUID = guid;
            Name = name;
            EndPoint = endPoint;
        }
    }
}
