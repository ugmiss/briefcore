using System;
using System.Collections.Generic;
using System.Text;

namespace MJClient
{
    public class Player
    {
        //���id
        string pid;

        public string Pid
        {
            get { return pid; }
            set { pid = value; }
        }
        //��λ
        int seat;

        public int Seat
        {
            get { return seat; }
            set { seat = value; }
        }

    }
}
