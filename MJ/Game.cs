using System;
using System.Collections.Generic;
using System.Text;

namespace MJClient
{
    public class Game
    {
        /// <summary>
        /// ��Ϸid��
        /// </summary>
        int gid;

        public int Gid
        {
            get { return gid; }
            set { gid = value; }
        }
        //���ֵ���
        List<Player> playerlist;

        public List<Player> Playerlist
        {
            get { return playerlist; }
            set { playerlist = value; }
        }
        //���ֵ�ׯ
        int zhuang;

        public int Zhuang
        {
            get { return zhuang; }
            set { zhuang = value; }
        }
        //һ��
        int first;

        public int First
        {
            get { return first; }
            set { first = value; }
        }
        //����
        int second;

        public int Second
        {
            get { return second; }
            set { second = value; }
        }
        //Ȧ
        int quan;

        public int Quan
        {
            get { return quan; }
            set { quan = value; }
        }
    }
}
