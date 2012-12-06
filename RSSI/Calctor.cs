using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RSSI
{
    public class Calctor
    {
        public static double GetY(double x1, double x2, double x3, double y1, double y2, double y3, double d1, double d2, double d3)
        {
           
            double fenzi = (x3 - x1) * (d1 * d1 - d2 * d2 - y1 * y1 + y2 * y2 - x1 * x1 + x2 * x2)
                - (x2 - x1) * (d1 * d1 - d3 * d3 + y3 * y3 - y1 * y1 + x3 * x3 - x1 * x1);
            double fenmu = (x2 - x1) * (2 * y1 - 2 * y3) - (x3 - x1) * (2 * y1 - 2 * y2);
            return fenzi / fenmu;
        }



    }
}
