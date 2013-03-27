using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geometria
{
    public class CircleToCircle
    {
        /// <summary>
        /// 两圆根轴与圆心连线的交点。
        /// </summary>
        /// <param name="x1">圆心A的x坐标。</param>
        /// <param name="y1">圆心A的y坐标。</param>
        /// <param name="x2">圆心B的x坐标。</param>
        /// <param name="y2">圆心B的y坐标。</param>
        /// <param name="d1">圆A半径。</param>
        /// <param name="d2">圆B半径。</param>
        /// <returns></returns>
        public static double GetBowStringMidX(double x1, double y1, double x2, double y2, double d1, double d2)
        {   //取两圆根轴与圆心连线焦点
            double fenzi = (y2 - y1) * (y2 * x1 - y1 * x2) - 0.5 * (d1 * d1 - d2 * d2 - x1 * x1 + x2 * x2 - y1 * y1 + y2 * y2) * (x1 - x2);
            double fenmu = (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);
            return fenzi / fenmu;
        }
        /// <summary>
        /// 三圆根心。
        /// </summary>
        /// <param name="x1">A圆心x坐标。</param>
        /// <param name="x2">A圆心y坐标。</param>
        /// <param name="x3">B圆心x坐标。</param>
        /// <param name="y1">B圆心y坐标。</param>
        /// <param name="y2">C圆心x坐标。</param>
        /// <param name="y3">C圆心y坐标。</param>
        /// <param name="d1">A圆半径。</param>
        /// <param name="d2">B圆半径。</param>
        /// <param name="d3">C圆半径。</param>
        /// <returns></returns>
        public static double GetTriCircleRootHeartY(double x1, double x2, double x3, double y1, double y2, double y3, double d1, double d2, double d3)
        {
            // 平面定位3点坐标和到3点的距离 
            // 求x，y 两圆方程想减得到的直线叫做圆的根轴，也叫等幂轴，根轴上不在圆内的任意一点引两圆的切线 切线长相等
            // 圆心不共线的三圆两两想减得到的三条根轴交于一点，叫做根心
            double fenzi = (x3 - x1) * (d1 * d1 - d2 * d2 - y1 * y1 + y2 * y2 - x1 * x1 + x2 * x2)
                - (x2 - x1) * (d1 * d1 - d3 * d3 + y3 * y3 - y1 * y1 + x3 * x3 - x1 * x1);
            double fenmu = (x2 - x1) * (2 * y1 - 2 * y3) - (x3 - x1) * (2 * y1 - 2 * y2);
            return fenzi / fenmu;
        }
    }
}
