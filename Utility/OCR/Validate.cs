using System;
using System.IO;
using System.Web;
using System.Drawing;
using Utility.OCR;
using Gif.Components;

namespace Utility
{


    //GIF验证码类
    public class Validate
    {
        //设置最少4位验证码
        private byte TrueValidateCodeCount = 2;
        public byte ValidateCodeCount
        {
            get
            {
                return TrueValidateCodeCount;
            }
            set
            {
                //验证码至少为3位
                if (value > 4)
                    TrueValidateCodeCount = value;
            }
        }
        protected string ValidateCode = "";
        //是否消除锯齿
        public bool FontTextRenderingHint = false;
        //验证码字体
        public string ValidateCodeFont = "Arial";
        //验证码型号(像素)
        public float ValidateCodeSize = 13;
        public int ImageHeight = 23;
        //定义验证码中所有的字符
        public string AllChar = "1,2,3,4,5,6,7,8,9,0,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z";

        //获得随机四位数
        public void CreateValidate()
        {
            ValidateCode = "";
            //将验证码中所有的字符保存在一个字符串数组中
            string[] CharArray = AllChar.Split(',');
            int Temp = -1;
            //生成一个随机对象
            Random RandCode = new Random();
            //根据验证码的位数循环
            for (int i = 0; i < ValidateCodeCount; i++)
            {
                //主要是防止生成相同的验证码
                if (Temp != -1)
                {
                    //加入时间的刻度
                    RandCode = new Random(i * Temp * ((int)DateTime.Now.Ticks));
                }
                int t = RandCode.Next(35);
                if (Temp == t)
                {
                    //相等的话重新生成
                    CreateValidate();
                }
                Temp = t;
                ValidateCode += CharArray[Temp];
            }
            //错误检测,去除超过指定位数的验证码
            if (ValidateCode.Length > TrueValidateCodeCount)
                ValidateCode = ValidateCode.Remove(TrueValidateCodeCount);
        }
        //生成一帧的BMP图象
        private void CreateImageBmp(out Bitmap ImageFrame, string ValidateCode)
        {
            // CreateValidate();
            //获得验证码字符
            char[] CodeCharArray = ValidateCode.ToCharArray(0, 1);
            //图像的宽度-与验证码的长度成一定比例
            int ImageWidth = (int)(TrueValidateCodeCount * ValidateCodeSize * 1.3 + 4);
            //创建一个长20，宽iwidth的图像对象
            ImageFrame = new Bitmap(ImageWidth, ImageHeight);
            //创建一个新绘图对象
            Graphics ImageGraphics = Graphics.FromImage(ImageFrame);
            //清除背景色，并填充背景色
            //Note:Color.Transparent为透明
            ImageGraphics.Clear(Color.White);
            //绘图用的字体和字号
            Font CodeFont = new Font(ValidateCodeFont, ValidateCodeSize, FontStyle.Bold);
            //绘图用的刷子大小
            Brush ImageBrush = new SolidBrush(Color.Red);
            //字体高度计算
            int FontHeight = (int)Math.Max(ImageHeight - ValidateCodeSize - 3, 2);
            //开始随机安排字符的位置，并画到图像里

            //生成随机点，决定字符串的开始输出范围
            int[] FontCoordinate = new int[2];
            //FontCoordinate[0] = (int)(i * ValidateCodeSize + RandomFactory.Next(1)) + 3;
            FontCoordinate[1] = RandomFactory.Next(FontHeight);
            Point FontDrawPoint = new Point(FontCoordinate[0], FontCoordinate[1]);
            //消除锯齿操作
            if (FontTextRenderingHint)
                ImageGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
            else
                ImageGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            //格式化刷子属性-用指定的刷子、颜色等在指定的范围内画图
            ImageGraphics.DrawString(ValidateCode, CodeFont, ImageBrush, FontDrawPoint);
            ImageGraphics.Dispose();
        }
        //处理生成的BMP
        private void DisposeImageBmp(ref Bitmap ImageFrame)
        {
            //创建绘图对象
            Graphics ImageGraphics = Graphics.FromImage(ImageFrame);
            //创建铅笔对象
            Pen ImagePen = new Pen(Color.Red, 1);
            //创建随机对象
            Random rand = new Random();
            //创建随机点
            Point[] RandPoint = new Point[2];
            //随机画线
            for (int i = 0; i < 15; i++)
            {
                RandPoint[0] = new Point(rand.Next(ImageFrame.Width), rand.Next(ImageFrame.Height));
                RandPoint[1] = new Point(rand.Next(ImageFrame.Width), rand.Next(ImageFrame.Height));
                ImageGraphics.DrawLine(ImagePen, RandPoint[0], RandPoint[1]);
            }
            ImageGraphics.Dispose();
        }
        //创建GIF动画
        public void CreateImageGif(string path)
        {
            Bitmap ImageFrame;
            AnimatedGifEncoder GifPic = new AnimatedGifEncoder();
            MemoryStream BmpMemory = new MemoryStream();
            GifPic.Start(path);
            //确保视觉残留
            GifPic.SetDelay(1000);
            //-1:no repeat,0:always repeat
            GifPic.SetRepeat(0);
            int x = RandomFactory.Next(20);
            int y = RandomFactory.Next(20);
            //创建一帧的图像
            CreateImageBmp(out ImageFrame, x.ToString());
            //生成随机线条
            //输出绘图,将图像保存到指定的流
            ImageFrame.Save(BmpMemory, System.Drawing.Imaging.ImageFormat.Png);
            ImageFrame.Save(1 + ".jpg");
            GifPic.AddFrame(Image.FromFile(1 + ".jpg"));

            //创建一帧的图像
            CreateImageBmp(out ImageFrame, "+");
            //输出绘图,将图像保存到指定的流
            ImageFrame.Save(BmpMemory, System.Drawing.Imaging.ImageFormat.Png);
            ImageFrame.Save(2 + ".jpg");
            GifPic.AddFrame(Image.FromFile(2 + ".jpg"));

            //创建一帧的图像
            CreateImageBmp(out ImageFrame, y.ToString());
            ImageFrame.Save(BmpMemory, System.Drawing.Imaging.ImageFormat.Png);
            ImageFrame.Save(3 + ".jpg");
            GifPic.AddFrame(Image.FromFile(3 + ".jpg"));

            //创建一帧的图像
            CreateImageBmp(out ImageFrame, "=");
            ImageFrame.Save(BmpMemory, System.Drawing.Imaging.ImageFormat.Png);
            ImageFrame.Save(4 + ".jpg");
            GifPic.AddFrame(Image.FromFile(4 + ".jpg"));


            //创建一帧的图像
            CreateImageBmp(out ImageFrame, "?");
            ImageFrame.Save(BmpMemory, System.Drawing.Imaging.ImageFormat.Png);
            ImageFrame.Save(5 + ".jpg");
            GifPic.AddFrame(Image.FromFile(5 + ".jpg"));

            GifPic.Finish();
            //e.SetDelay(500);
            ////-1:no repeat,0:always repeat
            //e.SetRepeat(0);
            //for (int i = 0, count = imageFilePaths.Length; i < count; i++)
            //{
            //    e.AddFrame(Image.FromFile(imageFilePaths[i]));
            //}
            //e.Finish();
            ///* extract Gif */
            //string outputPath = "c:\\";
            //GifDecoder gifDecoder = new GifDecoder();
            //gifDecoder.Read("c:\\test.gif");
            //for (int i = 0, count = gifDecoder.GetFrameCount(); i < count; i++)
            //{
            //    Image frame = gifDecoder.GetFrame(i); // frame i
            //    frame.Save(outputPath + Guid.NewGuid().ToString()
            //                          + ".png", ImageFormat.Png);
            //}

        }
    }
}
