using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geometria
{
    public class LineToLine
    {
        // 求两条直线的交点X
        public static double GetX(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            if (x1 == x2)
                return x1;
            if (x3 == x4)
                return x3;
            double fenzi = (x3 * y4 - x4 * y3) / (x3 - x4) - (x1 * y2 - x2 * y1) / (x1 - x2);
            double fenmu = (y1 - y2) / (x1 - x2) - (y3 - y4) / (x3 - x4);
            if (fenmu == 0)
                throw new Exception("两直线平行无交点");
            return fenzi / fenmu;
        }
        // 求两条直线的交点Y
        public static double GetY(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
        {
            if (y1 == y2)
                return y1;
            if (y3 == y4)
                return y3;
            double fenzi = (y3 * x4 - y4 * x3) / (y3 - y4) - (y1 * x2 - y2 * x1) / (y1 - y2);
            double fenmu = (x1 - x2) / (y1 - y2) - (x3 - x4) / (y3 - y4);
            if (fenmu == 0)
                throw new Exception("两直线平行无交点");
            return fenzi / fenmu;
        }
    }
}
