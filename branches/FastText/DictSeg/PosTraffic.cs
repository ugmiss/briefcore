using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KTDictSeg
{
    public class PosTraffic
    {
        Hashtable m_PosBinTbl = new Hashtable();
        ArrayList m_PosBinList = new ArrayList();

        CPOS m_POS;

        public CPOS POS
        {
            get
            {
                return m_POS;
            }

            set
            {
                m_POS = value;
            }
        }

        private void Hit(T_POSBin posBin)
        {
            T_POSBin bin = (T_POSBin)m_PosBinTbl[posBin.HashCode];
            if (bin == null)
            {
                bin = new T_POSBin(posBin.m_Pos1, posBin.m_Pos2);
                bin.m_Count = 1;
                m_PosBinTbl[bin.HashCode] = bin;
                m_PosBinList.Add(bin);
            }
            else
            {
                bin.m_Count++;
            }
        }

        public ArrayList GetPosBinGroup()
        {
            m_PosBinList.Sort();
            return m_PosBinList;
        }

        public void Traffic(ArrayList words)
        {
            for (int i = 0; i < words.Count-1; i++)
            {
                bool isReg;
                T_INNER_POS[] curPos = m_POS.GetPos((String)words[i], out isReg);
                T_INNER_POS[] nextPos = m_POS.GetPos((String)words[i + 1], out isReg);


                //ArrayList curList = m_POS.GetPosList(curPos);

                if (curPos.Length != 1)
                {
                    continue;
                }

                T_INNER_POS pos1 = curPos[0];

                if (pos1 == T_INNER_POS.POS_UNK)
                {
                    continue;
                }


                //ArrayList nextList = m_POS.GetPosList(nextPos);

                if (nextPos.Length != 1)
                {
                    continue;
                }

                T_INNER_POS pos2 = (T_INNER_POS)nextPos[0];

                if (pos2 == T_INNER_POS.POS_UNK)
                {
                    continue;
                }

                T_POSBin bin = new T_POSBin(pos1, pos2);

                Hit(bin);
            }
        }

    }
}
