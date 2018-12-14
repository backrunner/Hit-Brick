using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hit_Brick_Server
{
    public class Room
    {
        public long id;
        public string name;
        public string level;
        public int capacity;

        //0: 等待中
        //1: 游戏中
        public short status;

        public List<string> players = new List<string>();

        public Room(string name)
        {
            capacity = 4;
            this.name = name;
        }

        public Room(string name, int capacity)
        {
            this.name = name;
            this.capacity = capacity;
        }
    }
}
