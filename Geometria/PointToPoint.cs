using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geometria
{
    public class PointToPoint
    {
        /// <summary>
        /// 计算两点的距离。
        /// </summary>
        /// <param name="x1">A点x坐标。</param>
        /// <param name="y1">A点y坐标。</param>
        /// <param name="x2">B点x坐标。</param>
        /// <param name="y2">B点y坐标。</param>
        /// <returns>A，B两点的距离。</returns>
        public static double GetDistance(double x1, double y1, double x2, double y2)
        {
            double sum = (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2);
            return Math.Sqrt(sum);
        }
    }
}
