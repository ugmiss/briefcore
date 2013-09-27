using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScanImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //009Aff .08
            Deal("560.bmp", "560bak.bmp");
            Deal("470.bmp", "470bak.bmp");
            pictureBox1.Image = Image.FromFile("560bak.bmp");
        }

        public void Deal(string file, string filetag)
        {
            Bitmap bm = (Bitmap)Bitmap.FromFile(file);
            int height = bm.Height;
            int width = bm.Width;
            int x, y; //x,y是循环次数，result是记录处理后的像素值
            for (x = 0; x < width; x++)
            {
                for (y = 0; y < height; y++)
                {
                    if (TestPix(bm, x, y))
                    {
                        if (TestPix8(bm, x, y))
                            bm.SetPixel(x, y, Color.Black);
                        else
                            bm.SetPixel(x, y, Color.White);
                    }
                    else
                    {
                        bm.SetPixel(x, y, Color.White);
                    }
                }
            }
            bm.Save(filetag);
        }
        bool TestPix8(Bitmap bm, int x, int y)
        {
            if (TestPix(bm, x + 1, y)) return true;
            if (TestPix(bm, x + 1, y + 1)) return true;
            if (TestPix(bm, x + 1, y - 1)) return true;
            if (TestPix(bm, x, y + 1)) return true;
            if (TestPix(bm, x, y - 1)) return true;
            if (TestPix(bm, x - 1, y + 1)) return true;
            if (TestPix(bm, x - 1, y)) return true;
            if (TestPix(bm, x - 1, y - 1)) return true;
            return false;
        }


        bool TestPix(Bitmap bm, int x, int y)
        {
            Color pixel = bm.GetPixel(x, y);//获取当前坐标的像素值
            if (pixel.R == 0x0 && pixel.G == 0x9a && pixel.B == 0xff)
            {
                return true;
            }
            else if (pixel.R == 0x09 && pixel.G == 0xae && pixel.B == 0xf4)
            {
                return true;
            }
            //else if (pixel.R == 0x29 && pixel.G == 0xae && pixel.B == 0xd6)
            //{
            //    return true;
            //}
            else
            {
                return false;
            }
        }
    }
    public class Heart
    {
        public int x { get; set; }
        public int y { get; set; }
    }
}
