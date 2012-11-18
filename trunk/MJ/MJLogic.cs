using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections;

namespace MJClient
{
    public class MJLogic
    {
        /// <summary>
        /// 洗牌。
        /// </summary>
        /// <returns></returns>
        public List<MJ> XiPai()
        {
            List<MJ> MJList = new List<MJ>();
            for (int x = 1; x <= 4; x++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    for (int i = 1; i <= 9; i++)
                    {
                        MJ mj=new MJ();
                        mj.mcount=i;
                        switch(j)
                        {
                            case 1: mj.mclass = "万"; break;
                            case 2: mj.mclass = "条"; break;
                            case 3: mj.mclass = "饼"; break;                           
                        }
                        MJList.Add(mj);
                    }
                }
                for (int i = 1; i <= 4; i++)
                {
                    MJ mj = new MJ();
                    mj.mcount = i;
                    mj.mclass = "风";
                    MJList.Add(mj);
                }
                for (int i = 1; i <= 3; i++)
                {
                    MJ mj = new MJ();
                    mj.mcount = i;
                    mj.mclass = "箭";
                    MJList.Add(mj);
                }

            }
            for (int x = 1; x <= 8; x++)
            {
                MJ mj = new MJ();
                mj.mcount = x;
                mj.mclass = "花";
                MJList.Add(mj);
            }

            return Shuttle(Shuttle(Shuttle(MJList)));
        }
        /// <summary>
        /// 乱序。
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<MJ> Shuttle(List<MJ> list)
        {
            int count = list.Count;
            MJ[] MJarray = list.ToArray();
            for (int i = count; i >= 1; i--)
            {
                Random rnd = new Random(System.DateTime.Now.Millisecond);
                Thread.Sleep(7);
                int x=rnd.Next(i);
                MJ temp = MJarray[i-1];
                MJarray[i-1] = MJarray[x];
                MJarray[x] = temp;
            }
            return new List<MJ>(MJarray);
        }
        /// <summary>
        /// 发牌。
        /// </summary>
        public void Fapai()
        {
            MJCache.ClearForNewGame();
            MJCache.Wall =this.XiPai();
            
            for (int i = 1; i <= 14; i++)
            {
                MJCache.H1.Add(MJCache.Wall[0]);
                MJCache.Wall.Remove(MJCache.Wall[0]);
            }
            for (int i = 1; i <= 13; i++)
            {
                MJCache.H2.Add(MJCache.Wall[0]);
                MJCache.Wall.Remove(MJCache.Wall[0]);
            }
            for (int i = 1; i <= 13; i++)
            {
                MJCache.H3.Add(MJCache.Wall[0]);
                MJCache.Wall.Remove(MJCache.Wall[0]);
            }
            for (int i = 1; i <= 13; i++)
            {
                MJCache.H4.Add(MJCache.Wall[0]);
                MJCache.Wall.Remove(MJCache.Wall[0]);
            }
        }
        /// <summary>
        /// 补花。
        /// </summary>
        /// <param name="handlist"></param>
        /// <param name="flowerlist"></param>
        public void Buhua(List<MJ>handlist,List<MJ>flowerlist)
        { 
            for(int i=0;i<handlist.Count;i++)
            {
                if (handlist[i].mclass == "花")
                {
                    flowerlist.Add(handlist[i]);
                    handlist.Remove(handlist[i]);
                    handlist.Add(MJCache.Wall[0]);
                    MJCache.Wall.Remove(MJCache.Wall[0]);
                    Buhua(handlist, flowerlist);
                    return;
                }
            }
        }
        /// <summary>
        /// 排序。
        /// </summary>
        /// <param name="handlist"></param>
        public void Sort(List<MJ> handlist)
        {
            Comparison<MJ> com = new Comparison<MJ>(Compare);
            handlist.Sort(com);
        }
        private int Compare(MJ info1, MJ info2)
        {
            int result;
            CaseInsensitiveComparer ObjectCompare = new CaseInsensitiveComparer();
            result = ObjectCompare.Compare(info1.MWeight, info2.MWeight);
            return result;
        }
        

    }
}
