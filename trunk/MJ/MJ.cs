using System;
using System.Collections.Generic;
using System.Text;

namespace MJClient
{
    public class MJ
    {
        //"万，索，筒36，风16，箭12，花8"
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
                    case "万": return mcount;
                    case "条": return 9 + mcount;
                    case "饼": return 18 + mcount;
                    case "风": return 27 + mcount;
                    case "箭": return 31 + mcount;
                }
                return 35;
            }
            set
            {
                mweight = value;
                if (mweight < 10)
                {
                    mcount = mweight;
                    mclass = "万";
                }
                else if (mweight < 19)
                {
                    mcount = mweight - 9;
                    mclass = "条";
                }
                else if (mweight < 28)
                {
                    mcount = mweight - 18;
                    mclass = "饼";
                }
                else if (mweight < 32)
                {
                    mcount = mweight - 27;
                    mclass = "风";
                }
                else if (mweight < 35)
                {
                    mcount = mweight - 31;
                    mclass = "箭";
                }
                else 
                {
                    mcount = mweight - 34;
                    mclass = "花";
                }

            }
        }
        public string Mpic
        {
            get
            {
                switch (mclass)
                {
                    case "万": return "_" + mcount + "w";
                    case "条": return "_" + mcount + "t";
                    case "饼": return "_" + mcount + "b";
                    case "风":
                        {
                            if (mcount == 1) return "df";
                            else if (mcount == 2) return "nf";
                            else if (mcount == 3) return "xf";
                            else if (mcount == 4) return "bf";
                            break;
                        }
                    case "箭":
                        {
                            if (mcount == 1) return "z";
                            else if (mcount == 2) return "f";
                            else if (mcount == 3) return "b";
                            break;
                        }
                }
                if (mclass == "花")
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
        /// 麻将名
        /// </summary>
        public string Mname
        {
            get
            {
                if (mclass == "万" || mclass == "条" || mclass == "饼")
                {
                    switch (mcount)
                    {
                        case 1: return "一" + mclass;
                        case 2: return "二" + mclass;
                        case 3: return "三" + mclass;
                        case 4: return "四" + mclass;
                        case 5: return "五" + mclass;
                        case 6: return "六" + mclass;
                        case 7: return "七" + mclass;
                        case 8: return "八" + mclass;
                        case 9: return "九" + mclass;
                    }
                }
                if (mclass == "风")
                {
                    switch (mcount)
                    {
                        case 1: return "东";
                        case 2: return "南";
                        case 3: return "西";
                        case 4: return "北";
                    }
                }
                if (mclass == "箭")
                {
                    switch (mcount)
                    {
                        case 1: return "中";
                        case 2: return "发";
                        case 3: return "白";
                    }
                }
                if (mclass == "花")
                {
                    switch (mcount)
                    {
                        case 1: return "渔";
                        case 2: return "樵";
                        case 3: return "耕";
                        case 4: return "读";
                        case 5: return "梅";
                        case 6: return "兰";
                        case 7: return "竹";
                        case 8: return "菊";
                    }
                }
                return mname;

            }
            set { mname = value; }
        }
    }
}
