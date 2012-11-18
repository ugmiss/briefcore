using System;
using System.Collections.Generic;
using System.Text;

namespace MJClient
{
    public static class MJCache
    {
        public static List<MJ> Wall;

        public static List<MJ> H1 ;
        public static List<MJ> F1 ;
        public static List<MJ> H2 ;
        public static List<MJ> F2 ;
        public static List<MJ> H3 ;
        public static List<MJ> F3 ;
        public static List<MJ> H4 ;
        public static List<MJ> F4 ;
        static MJCache()
        {
            ClearForNewGame();
        }

        public static void ClearForNewGame()
        {
            Wall = new List<MJ>();
            H1 = new List<MJ>();
            F1 = new List<MJ>();
            H2 = new List<MJ>();
            F2 = new List<MJ>();
            H3 = new List<MJ>();
            F3 = new List<MJ>();
            H4 = new List<MJ>();
            F4 = new List<MJ>();
        }

    }
}
