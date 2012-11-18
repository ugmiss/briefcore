using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Collections;

namespace MJClient
{
    //牌型合计。
    public class TryLogic
    {
        /// <summary>
        /// 全不靠。
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool TryQuanBuKao(List<MJ> list)
        {
            //全不等
            Comparison<MJ> com = new Comparison<MJ>(Compare);
            list.Sort(com);
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (list[i].MWeight == list[i + 1].MWeight) return false;
            }
            //将牌集合。
            List<MJ> tempList = new List<MJ>();
            foreach (MJ mj in list)
            {
                if (mj.MWeight <= 9)
                    tempList.Add(mj);
            }
            if (QuanBuKaoDengCha(tempList)) return false;
            tempList = new List<MJ>();
            foreach (MJ mj in list)
            {
                if (mj.MWeight <= 18 && mj.MWeight >= 10)
                    tempList.Add(mj);
            }
            if (QuanBuKaoDengCha(tempList)) return false;
            tempList = new List<MJ>();
            foreach (MJ mj in list)
            {
                if (mj.MWeight <= 27 && mj.MWeight >= 19)
                    tempList.Add(mj);
            }
            if (QuanBuKaoDengCha(tempList)) return false;
            return true;
        }
        /// <summary>
        /// 真 表示不等差3。
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool QuanBuKaoDengCha(List<MJ> list)
        {
            if (list.Count == 1) return false;
            for (int x = 1; x < list.Count; x++)
            {
                if ((list[x].MWeight - list[0].MWeight) % 3 != 0) return true;
            }
            return false;
        }
        /// <summary>
        /// 是否七星不靠。
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool TryQiXingBuKao(List<MJ> list)
        {
            if (TryQuanBuKao(list))
            {
                List<MJ> tempList = new List<MJ>();
                foreach (MJ mj in list)
                {
                    if (mj.MWeight > 27)
                        tempList.Add(mj);
                }
                if (tempList.Count == 7)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 权比较。
        /// </summary>
        /// <param name="info1"></param>
        /// <param name="info2"></param>
        /// <returns></returns>
        private static int Compare(MJ m1, MJ m2)
        {
            int result;
            CaseInsensitiveComparer ObjectCompare = new CaseInsensitiveComparer();
            result = ObjectCompare.Compare(m1.MWeight, m2.MWeight);
            return result;
        }
        /// <summary>
        /// 十三幺。
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool TryShiSanYao(List<MJ> list)
        {
            //全排序
            Comparison<MJ> com = new Comparison<MJ>(Compare);
            list.Sort(com);
            List<MJ> templist = new List<MJ>(list);
            //删除一个相同元素。
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (templist[i].MWeight == templist[i + 1].MWeight)
                {
                    templist.Remove(templist[i]);
                    break;
                }
            }
            //13个不一样的。
            for (int i = 0; i < templist.Count - 1; i++)
            {
                if (templist[i].MWeight == templist[i + 1].MWeight)
                    return false;
            }
            //1 9共六个。
            int k = 0;
            foreach (MJ mj in templist)
            {
                if (mj.MWeight < 28 && mj.mcount != 1 & mj.mcount != 9) return false;
                if (mj.MWeight < 28) k++;
            }
            if (k != 6) return false;
            return true;
        }
        /// <summary>
        /// 七小对。
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool TryQiXiaoDui(List<MJ> list)
        {
            if (list.Count != 14) return false;
            for (int i = 0; i < list.Count; i++)
            {
                int count = 0;
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[i].MWeight == list[j].MWeight) count++;
                }
                if (count != 2 && count != 4)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 组合龙。
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool TryZuHeLong(List<MJ> list)
        {
            Comparison<MJ> com = new Comparison<MJ>(Compare);
            list.Sort(com);
            List<MJ> templist = new List<MJ>(list);
            //按花色分组。
            List<MJ> templistA = new List<MJ>();
            List<MJ> templistB = new List<MJ>();
            List<MJ> templistC = new List<MJ>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].MWeight <= 9) templistA.Add(list[i]);
                else if (list[i].MWeight <= 18) templistB.Add(list[i]);
                else if (list[i].MWeight <= 27) templistC.Add(list[i]);
            }
            //分别获取3个花色的 等差3的基数
            int a = FindDengCha3(templistA);
            int b = FindDengCha3(templistB);
            int c = FindDengCha3(templistC);
            //继续检查
            if (a > 0 && b > 0 && c > 0 && a != b && b != c && a != c)
            {
                //删除9张组合龙牌。牌如果能胡组合龙，只能有一种组合。
                templist.Remove(templist.Find(delegate(MJ p) { return (p.MWeight == a); }));
                templist.Remove(templist.Find(delegate(MJ p) { return (p.MWeight == a + 3); }));
                templist.Remove(templist.Find(delegate(MJ p) { return (p.MWeight == a + 6); }));
                templist.Remove(templist.Find(delegate(MJ p) { return (p.MWeight == b + 9); }));
                templist.Remove(templist.Find(delegate(MJ p) { return (p.MWeight == b + 12); }));
                templist.Remove(templist.Find(delegate(MJ p) { return (p.MWeight == b + 15); }));
                templist.Remove(templist.Find(delegate(MJ p) { return (p.MWeight == c + 18); }));
                templist.Remove(templist.Find(delegate(MJ p) { return (p.MWeight == c + 21); }));
                templist.Remove(templist.Find(delegate(MJ p) { return (p.MWeight == c + 24); }));
                if (templist.Count < 5)
                    return false;
                //只对于余牌的前3位 进行按牌找顺成牌和横成牌。找3位就可以涵盖所有可能。
                for (int f = 0; f < 3; f++)
                {
                    if (ZuHeLongChengPai(templist, templist[f]))
                        return true;
                }
                return false;
            }
            return false;
        }
        /// <summary>
        /// 组合龙找同花色中 寻找等差3的3张 有返回最小的序基数 1~3 没有返回0（如 2 5 8 返回 2）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int FindDengCha3(List<MJ> list)
        {
            for (int i = 0; i < list.Count && list[i].mcount <= 3; i++)
            {
                MJ y = list.Find(delegate(MJ c) { return (c.mclass == list[i].mclass) && (c.mcount == list[i].mcount + 3); });
                MJ z = list.Find(delegate(MJ c) { return (c.mclass == list[i].mclass) && (c.mcount == list[i].mcount + 6); });
                if (y != null && z != null)
                    return list[i].mcount;
            }
            return 0;
        }
        /// <summary>
        /// 组合龙余牌（5~6张）判断。
        /// </summary>
        /// <param name="list">除去147 258 369的余牌</param>
        /// <returns></returns>
        public static bool ZuHeLongChengPai(List<MJ> list, MJ m)
        {
            //尝试按目标m找顺 找到后判断余牌成将
            List<MJ> temp = new List<MJ>(list);
            for (int i = 0; i < list.Count; i++)
            {
                MJ x = temp.Find(delegate(MJ c) { return (c.mclass == m.mclass) && (c.mcount == m.mcount); });
                MJ y = temp.Find(delegate(MJ c) { return (c.mclass == m.mclass) && (c.mcount == m.mcount + 1); });
                MJ z = temp.Find(delegate(MJ c) { return (c.mclass == m.mclass) && (c.mcount == m.mcount + 2); });
                if (x != null && y != null && z != null)
                {
                    temp.Remove(x);
                    temp.Remove(y);
                    temp.Remove(z);
                    if (temp.Count == 2)
                        if (temp[0].MWeight == temp[1].MWeight)
                            return true;
                }
            }
            //尝试按目标m找横 找到后判断余牌成将
            temp = new List<MJ>(list);
            for (int i = 0; i < list.Count; i++)
            {
                List<MJ> Lie = temp.FindAll(delegate(MJ c) { return (c.mclass == m.mclass) && (c.mcount == m.mcount); });
                if (Lie.Count > 2)
                {
                    temp.RemoveAll(delegate(MJ c) { return (c.mclass == m.mclass) && (c.mcount == m.mcount); });
                    if (temp.Count == 2)
                        if (temp[0].MWeight == temp[1].MWeight)
                            return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 2 3(4) 3(4) 3(4) 3(4) 为基本牌型。
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool TryPuTong(List<MJ> list)
        {
            for (int f = 0; f < 3; f++)
            {
                if (PuTongChengPai(list, list[f]))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 成排找顺和横判断。
        /// </summary>
        /// <param name="list"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static bool PuTongChengPai(List<MJ> list, MJ m)
        {
            if (list.Count < 5) return false;
            //尝试按目标m找顺 找到后判断余牌成将
            List<MJ> temp = new List<MJ>(list);
            for (int i = 0; i < list.Count; i++)
            {
                MJ x = temp.Find(delegate(MJ c) { return (c.mclass == m.mclass) && (c.mcount == m.mcount); });
                MJ y = temp.Find(delegate(MJ c) { return (c.mclass == m.mclass) && (c.mcount == m.mcount + 1); });
                MJ z = temp.Find(delegate(MJ c) { return (c.mclass == m.mclass) && (c.mcount == m.mcount + 2); });
                if (x != null && y != null && z != null)
                {
                    temp.Remove(x);
                    temp.Remove(y);
                    temp.Remove(z);
                    if (temp.Count == 2)
                    {
                        if (temp[0].MWeight == temp[1].MWeight)
                            return true;
                    }
                    else
                    {
                        for (int f = 0; f < 3; f++)
                        {
                            if (PuTongChengPai(temp, temp[f]))
                                return true;
                        }
                    }
                }
            }
            //尝试按目标m找横 找到后判断余牌成将
            temp = new List<MJ>(list);
            for (int i = 0; i < list.Count; i++)
            {
                List<MJ> Lie = temp.FindAll(delegate(MJ c) { return (c.mclass == m.mclass) && (c.mcount == m.mcount); });
                if (Lie.Count > 2)
                {
                    temp.RemoveAll(delegate(MJ c) { return (c.mclass == m.mclass) && (c.mcount == m.mcount); });
                    if (temp.Count == 2)
                    {
                        if (temp[0].MWeight == temp[1].MWeight)
                            return true;
                    }
                    else
                    {
                        for (int f = 0; f < 3; f++)
                        {
                            if (PuTongChengPai(temp, temp[f]))
                                return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
