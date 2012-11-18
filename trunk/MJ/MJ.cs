using System;
using System.Collections.Generic;
using System.Text;

namespace MJClient
{
    public class MJ
    {
        //"������Ͳ36����16����12����8"
        public string mclass;
        public int mcount;
        //public int mdiff;
        private string mname;
        string mpic;
        int mweight;
        public int MWeight
        {
            get
            {
                if (mweight > 0) return mweight;
                switch (mclass)
                {
                    case "��": return mcount;
                    case "��": return 9 + mcount;
                    case "��": return 18 + mcount;
                    case "��": return 27 + mcount;
                    case "��": return 31 + mcount;
                }
                return 35;
            }
            set
            {
                mweight = value;
                if (mweight < 10)
                {
                    mcount = mweight;
                    mclass = "��";
                }
                else if (mweight < 19)
                {
                    mcount = mweight - 9;
                    mclass = "��";
                }
                else if (mweight < 28)
                {
                    mcount = mweight - 18;
                    mclass = "��";
                }
                else if (mweight < 32)
                {
                    mcount = mweight - 27;
                    mclass = "��";
                }
                else if (mweight < 35)
                {
                    mcount = mweight - 31;
                    mclass = "��";
                }
                else 
                {
                    mcount = mweight - 34;
                    mclass = "��";
                }

            }
        }
        public string Mpic
        {
            get
            {
                switch (mclass)
                {
                    case "��": return "_" + mcount + "w";
                    case "��": return "_" + mcount + "t";
                    case "��": return "_" + mcount + "b";
                    case "��":
                        {
                            if (mcount == 1) return "df";
                            else if (mcount == 2) return "nf";
                            else if (mcount == 3) return "xf";
                            else if (mcount == 4) return "bf";
                            break;
                        }
                    case "��":
                        {
                            if (mcount == 1) return "z";
                            else if (mcount == 2) return "f";
                            else if (mcount == 3) return "b";
                            break;
                        }
                }
                if (mclass == "��")
                {
                    switch (mcount)
                    {
                        case 1: return "yu";
                        case 2: return "qiao";
                        case 3: return "geng";
                        case 4: return "du";
                        case 5: return "mei";
                        case 6: return "lan";
                        case 7: return "zhu";
                        case 8: return "ju";
                    }
                }
                return mpic;
            }
            set { mpic = value; }
        }
        /// <summary>
        /// �齫��
        /// </summary>
        public string Mname
        {
            get
            {
                if (mclass == "��" || mclass == "��" || mclass == "��")
                {
                    switch (mcount)
                    {
                        case 1: return "һ" + mclass;
                        case 2: return "��" + mclass;
                        case 3: return "��" + mclass;
                        case 4: return "��" + mclass;
                        case 5: return "��" + mclass;
                        case 6: return "��" + mclass;
                        case 7: return "��" + mclass;
                        case 8: return "��" + mclass;
                        case 9: return "��" + mclass;
                    }
                }
                if (mclass == "��")
                {
                    switch (mcount)
                    {
                        case 1: return "��";
                        case 2: return "��";
                        case 3: return "��";
                        case 4: return "��";
                    }
                }
                if (mclass == "��")
                {
                    switch (mcount)
                    {
                        case 1: return "��";
                        case 2: return "��";
                        case 3: return "��";
                    }
                }
                if (mclass == "��")
                {
                    switch (mcount)
                    {
                        case 1: return "��";
                        case 2: return "��";
                        case 3: return "��";
                        case 4: return "��";
                        case 5: return "÷";
                        case 6: return "��";
                        case 7: return "��";
                        case 8: return "��";
                    }
                }
                return mname;

            }
            set { mname = value; }
        }
    }
}
