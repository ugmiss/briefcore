using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    /// <summary>
    /// 圆心不共线三圆的跟心求值
    /// </summary>
    public class RadicalCenter
    {
        /// <summary>
        ///  取跟心Y坐标（x y为圆心坐标 d为半径）
        /// </summary>
        public static double GetY(double x1, double x2, double x3, double y1, double y2, double y3, double d1, double d2, double d3)
        {
            double fenzi = (x3 - x1) * (d1 * d1 - d2 * d2 - y1 * y1 + y2 * y2 - x1 * x1 + x2 * x2)
                - (x2 - x1) * (d1 * d1 - d3 * d3 + y3 * y3 - y1 * y1 + x3 * x3 - x1 * x1);
            double fenmu = (x2 - x1) * (2 * y1 - 2 * y3) - (x3 - x1) * (2 * y1 - 2 * y2);
            return fenzi / fenmu;
        }
        /// <summary>
        ///  取跟心X坐标（x y为圆心坐标 d为半径）
        /// </summary>
        public static double GetX(double y1, double y2, double y3, double x1, double x2, double x3, double d1, double d2, double d3)
        {
            double fenzi = (x3 - x1) * (d1 * d1 - d2 * d2 - y1 * y1 + y2 * y2 - x1 * x1 + x2 * x2)
                - (x2 - x1) * (d1 * d1 - d3 * d3 + y3 * y3 - y1 * y1 + x3 * x3 - x1 * x1);
            double fenmu = (x2 - x1) * (2 * y1 - 2 * y3) - (x3 - x1) * (2 * y1 - 2 * y2);
            return fenzi / fenmu;
        }
    }
}
