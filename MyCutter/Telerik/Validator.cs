using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.WinControls.UI;
using System.Drawing;
using System.Text.RegularExpressions;

namespace DocBasic
{
    public class Validator
    {
        /// <summary>
        /// 必填校验。
        /// </summary>
        /// <param name="map"></param>
        public static void ForNotNull(Dictionary<string, RadTextBox> map)
        {
            List<string> list = new List<string>();
            List<RadTextBox> ControlList = new List<RadTextBox>();
            List<RadTextBox> ControlList2 = new List<RadTextBox>();

            foreach (string key in map.Keys)
            {
                RadTextBox tb = map[key];
                if (tb.Text == string.Empty)
                {
                    list.Add(string.Format("[{0}]", key));
                    ControlList.Add(tb);
                }
                else
                {
                    ControlList2.Add(tb);
                }
            }
            string errors = string.Join(",", list.ToArray());
            if (list.Count != 0)
            {
                UIException uex = new UIException(errors + "不能为空！", ControlList.ToArray(), ControlList2.ToArray());
                foreach (RadTextBox tb in uex.Controls)
                {
                    tb.RootElement.ClipDrawing = true;
                    tb.BackColor = Color.Red;
                }
                foreach (RadTextBox tb in uex.GoodControls)
                {
                    tb.RootElement.ClipDrawing = true;
                    tb.BackColor = Color.Orange;
                }
                throw uex;
            }
        }
        /// <summary>
        /// 正则校验。
        /// </summary>
        /// <param name="map"></param>
        //public static void ForRegex(Dictionary<string, Pair> map)
        //{
        //    List<string> list = new List<string>();
        //    List<RadTextBox> ControlList = new List<RadTextBox>();
        //    List<RadTextBox> ControlList2 = new List<RadTextBox>();

        //    foreach (string key in map.Keys)
        //    {

        //        RadTextBox tb = map[key].value as RadTextBox;
        //        if (!Regex.IsMatch(tb.Text, (map[key].key as ValidateRule).RuleDefine))
        //        {
        //            list.Add(string.Format("[{0}]", key));
        //            ControlList.Add(tb);
        //        }
        //        else
        //        {
        //            ControlList2.Add(tb);
        //        }
        //    }
        //    string errors = string.Join(",", list.ToArray());
        //    if (list.Count != 0)
        //    {
        //        UIException uex = new UIException(errors + "不匹配正则规则！", ControlList.ToArray(), ControlList2.ToArray());
        //        //foreach (RadTextBox tb in uex.Controls)
        //        //{
        //        //    tb.RootElement.ClipDrawing = true;
        //        //    tb.BackColor = Color.Red;
        //        //}
        //        //foreach (RadTextBox tb in uex.GoodControls)
        //        //{
        //        //    tb.RootElement.ClipDrawing = true;
        //        //    tb.BackColor = Color.Orange;
        //        //}
        //        throw uex;
        //    }

        //}

        /// <summary>
        /// 非全部必填校验。
        /// </summary>
        /// <param name="map"></param>
        public static void ForNotAllNull(Dictionary<string, RadTextBox> map)
        {
            List<string> list = new List<string>();
            List<RadTextBox> ControlList = new List<RadTextBox>();
            List<RadTextBox> ControlList2 = new List<RadTextBox>();

            foreach (string key in map.Keys)
            {
                RadTextBox tb = map[key];
                if (tb.Text == string.Empty)
                {
                    list.Add(string.Format("[{0}]", key));
                    ControlList.Add(tb);
                }
                else
                {
                    ControlList2.Add(tb);
                }
            }

          
            string errors = string.Join(",", list.ToArray());
            if (ControlList.Count == map.Count)
            {
                UIException uex = new UIException(errors + "不能同时为空！", ControlList.ToArray(), ControlList2.ToArray());
                foreach (RadTextBox tb in uex.Controls)
                {
                    tb.RootElement.ClipDrawing = true;
                    tb.BackColor = Color.Red;
                }
                foreach (RadTextBox tb in uex.GoodControls)
                {
                    tb.RootElement.ClipDrawing = true;
                    tb.BackColor = Color.Orange;
                }
                throw uex;
            }
        }

    }
    public class Pair
    {
        public object key { get; set; }
        public object value { get; set; }
    }
    public class UIException : Exception
    {
        public RadTextBox[] Controls;
        public RadTextBox[] GoodControls;
        public UIException(string exstr, RadTextBox[] ctrls, RadTextBox[] goodctrls)
            : base(exstr)
        {
            Controls = ctrls;
            GoodControls = goodctrls;
        }
    }


}
