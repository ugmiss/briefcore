using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Utility.OCR
{
    public class ImageOCR
    {
        /// <summary>
        /// 图片二值化。
        /// </summary>
        /// <param name="bmp"></param>
        public static void Binarizate(Bitmap bmp)
        {
            int grayvalue = GrayValue(bmp);
            int x = bmp.Width;
            int y = bmp.Height;
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (bmp.GetPixel(i, j).R >= grayvalue)
                    {
                        bmp.SetPixel(i, j, Color.FromArgb(0xff, 0xff, 0xff));
                    }
                    else
                    {
                        bmp.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    }
                }
            }
        }
        /// <summary>
        /// 计算图片灰度值。
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        static int GrayValue(Bitmap img)
        {
            int i;
            int k;
            double csum;
            int thresholdValue = 1;
            int[] ihist = new int[0x100];
            for (i = 0; i < 0x100; i++)
            {
                ihist[i] = 0;
            }
            int gmin = 0xff;
            int gmax = 0;
            for (i = 1; i < (img.Width - 1); i++)
            {
                for (int j = 1; j < (img.Height - 1); j++)
                {
                    int cn = img.GetPixel(i, j).R;
                    ihist[cn]++;
                    if (cn > gmax)
                    {
                        gmax = cn;
                    }
                    if (cn < gmin)
                    {
                        gmin = cn;
                    }
                }
            }
            double sum = csum = 0.0;
            int n = 0;
            for (k = 0; k <= 0xff; k++)
            {
                sum += k * ihist[k];
                n += ihist[k];
            }
            if (n == 0)
            {
                return 60;
            }
            double fmax = -1.0;
            int n1 = 0;
            for (k = 0; k < 0xff; k++)
            {
                n1 += ihist[k];
                if (n1 != 0)
                {
                    int n2 = n - n1;
                    if (n2 == 0)
                    {
                        return thresholdValue;
                    }
                    csum += k * ihist[k];
                    double m1 = csum / ((double)n1);
                    double m2 = (sum - csum) / ((double)n2);
                    double sb = ((n1 * n2) * (m1 - m2)) * (m1 - m2);
                    if (sb > fmax)
                    {
                        fmax = sb;
                        thresholdValue = k;
                    }
                }
            }
            return thresholdValue;
        }

    }
}
