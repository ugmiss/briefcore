using System;
using System.Collections.Generic;
using System.Text;

namespace MJClient
{
    public class Game
    {
        /// <summary>
        /// 游戏id。
        /// </summary>
        int gid;

        public int Gid
        {
            get { return gid; }
            set { gid = value; }
        }
        //本局的人
        List<Player> playerlist;

        public List<Player> Playerlist
        {
            get { return playerlist; }
            set { playerlist = value; }
        }
        //本局的庄
        int zhuang;

        public int Zhuang
        {
            get { return zhuang; }
            set { zhuang = value; }
        }
        //一泼
        int first;

        public int First
        {
            get { return first; }
            set { first = value; }
        }
        //二泼
        int second;

        public int Second
        {
            get { return second; }
            set { second = value; }
        }
        //圈
        int quan;

        public int Quan
        {
            get { return quan; }
            set { quan = value; }
        }
    }
}
