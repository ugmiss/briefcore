using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geometria
{
    public class PointToLine
    {
        /// <summary>
        /// 三点共线，一点在终点方向到起点的距离,求此点坐标。
        /// </summary>
        /// <param name="x1">起点x坐标。</param>
        /// <param name="y1">起点y坐标。</param>
        /// <param name="x2">终点x坐标。</param>
        /// <param name="y2">终点y坐标。</param>
        /// <param name="distance">距离。</param>
        /// <returns></returns>
        public static double GetXByDistance(double x1, double y1, double x2, double y2, double distance)
        {
            // 解法：直线段比例等于坐标差值比例 d/D=（x-x1）/（x2-x1） 
            if (x1 == x2 && y1 == y2)
            {
                return x1;
            }
            return x1 + distance * (x2 - x1) / PointToPoint.GetDistance(x1, y1, x2, y2);
        }

        /// <summary>
        /// 三点共线，已知两点和一个坐标X求Y。
        /// </summary>
        /// <param name="x1">A点x坐标。</param>
        /// <param name="y1">A点y坐标。</param>
        /// <param name="x2">B点x坐标。</param>
        /// <param name="y2">B点y坐标。</param>
        /// <param name="x">C点x坐标。</param>
        /// <returns>C点y坐标。</returns>
        public static double GetYByX(double x1, double y1, double x2, double y2, double x)
        {   //已知两点加一点的X求Y
            if (x1 == x2) return y1;
            double y = ((y1 - y2) * x + (y2 * x1 - y1 * x2)) / (x1 - x2);
            return y;
        }

        /// <summary>
        /// 线段投影坐标，投影在线段的延长线上返回空。
        /// </summary>
        /// <param name="x1">线段A端点x坐标。</param>
        /// <param name="y1">线段A端点y坐标。</param>
        /// <param name="x2">线段B端点x坐标。</param>
        /// <param name="y2">线段B端点y坐标。</param>
        /// <param name="x0">投影源点x坐标。</param>
        /// <param name="y0">投影源点y坐标。</param>
        /// <returns>投影点。</returns>
        public static Point? GetProjectionPoint(double x1, double y1, double x2, double y2, double x0, double y0)
        {   // 点到线段的投影,落在区间外的返回空值
            Point p = new Point();
            //点到直线投影的X坐标
            p.X = GetProjectionX(x1, y1, x2, y2, x0, y0);
            //点到直线投影的Y坐标
            p.Y = GetYByX(x1, y1, x2, y2, p.X);
            //点到A端点距离
            double d1 = PointToPoint.GetDistance(p.X, p.Y, x1, y1);
            //点到B端点距离
            double d2 = PointToPoint.GetDistance(p.X, p.Y, x2, y2);
            //AB端点的距离
            double d = PointToPoint.GetDistance(x1, y1, x2, y2);
            if (d1 + d2 - d > 0)
            {   //两个距离大于AB基站距离认为投影没落在AB的线段上。
                return null;
            }
            return p;
        }
        /// <summary>
        /// 求直线的投影x坐标。
        /// </summary>
        /// <param name="x1">线段A端点x坐标。</param>
        /// <param name="y1">线段A端点y坐标。</param>
        /// <param name="x2">线段B端点x坐标。</param>
        /// <param name="y2">线段B端点y坐标。</param>
        /// <param name="x0">投影源点x坐标。</param>
        /// <param name="y0">投影源点y坐标。</param>
        /// <returns>投影x坐标</returns>
        public static double GetProjectionX(double x1, double y1, double x2, double y2, double x0, double y0)
        {   // 求点到直线的投影的X坐标
            double fenzi = (y0 - y1) * (x1 - x2) * (y1 - y2) + x0 * (x1 - x2) * (x1 - x2) + x1 * (y1 - y2) * (y1 - y2);
            double fenmu = (y1 - y2) * (y1 - y2) + (x1 - x2) * (x1 - x2);
            return fenzi / fenmu;
        }
    }
    public struct Point
    {
        /// <summary>
        /// X坐标
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Y坐标
        /// </summary>
        public double Y { get; set; }
    }
}
