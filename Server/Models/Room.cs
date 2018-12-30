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

        public Player[] players = new Player[4];
        public int playerCount = 0;

        public int holder = 0;  //房主对应的序号

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
