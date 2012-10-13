using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Utility
{
    public class UnCodebase
    {
        public Bitmap bmpobj;
        public UnCodebase(Bitmap pic)
        {
            //       if (pic.PixelFormat == PixelFormat.Format8bppIndexed)
            bmpobj = new Bitmap(pic);    //ת��ΪFormat32bppRgb
        }

        /// <summary>
        /// ����RGB������Ҷ�ֵ
        /// </summary>
        /// <param name="posClr">Colorֵ</param>
        /// <returns>�Ҷ�ֵ������</returns>
        private int GetGrayNumColor(System.Drawing.Color posClr)
        {
            return (posClr.R * 19595 + posClr.G * 38469 + posClr.B * 7472) >> 16;
        }

        /// <summary>
        /// �Ҷ�ת��,��㷽ʽ
        /// </summary>
        public void GrayByPixels()
        {
            for (int i = 0; i < bmpobj.Height; i++)
            {
                for (int j = 0; j < bmpobj.Width; j++)
                {
                    int tmpValue = GetGrayNumColor(bmpobj.GetPixel(j, i));
                    bmpobj.SetPixel(j, i, Color.FromArgb(tmpValue, tmpValue, tmpValue));
                }
            }
        }

        /// <summary>
        /// ȥͼ�α߿�
        /// </summary>
        /// <param name="borderWidth"></param>
        public void ClearPicBorder(int borderWidth)
        {
            for (int i = 0; i < bmpobj.Height; i++)
            {
                for (int j = 0; j < bmpobj.Width; j++)
                {
                    if (i < borderWidth || j < borderWidth || j > bmpobj.Width - 1 - borderWidth || i > bmpobj.Height - 1 - borderWidth)
                        bmpobj.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                }
            }
        }

        /// <summary>
        /// �Ҷ�ת��,���з�ʽ
        /// </summary>
        public void GrayByLine()
        {
            Rectangle rec = new Rectangle(0, 0, bmpobj.Width, bmpobj.Height);
            BitmapData bmpData = bmpobj.LockBits(rec, ImageLockMode.ReadWrite, bmpobj.PixelFormat);// PixelFormat.Format32bppPArgb);
            //    bmpData.PixelFormat = PixelFormat.Format24bppRgb;
            IntPtr scan0 = bmpData.Scan0;
            int len = bmpobj.Width * bmpobj.Height;
            int[] pixels = new int[len];
            Marshal.Copy(scan0, pixels, 0, len);

            //��ͼƬ���д���
            int GrayValue = 0;
            for (int i = 0; i < len; i++)
            {
                GrayValue = GetGrayNumColor(Color.FromArgb(pixels[i]));
                pixels[i] = (byte)(Color.FromArgb(GrayValue, GrayValue, GrayValue)).ToArgb();      //Colorתbyte
            }

            bmpobj.UnlockBits(bmpData);

            ////���
            //GCHandle gch = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            //bmpOutput = new Bitmap(bmpobj.Width, bmpobj.Height, bmpData.Stride, bmpData.PixelFormat, gch.AddrOfPinnedObject());
            //gch.Free();
        }

        /// <summary>
        /// �õ���Чͼ�β�����Ϊ��ƽ���ָ�Ĵ�С
        /// </summary>
        /// <param name="dgGrayValue">�Ҷȱ����ֽ�ֵ</param>
        /// <param name="CharsCount">��Ч�ַ���</param>
        /// <returns></returns>
        public void GetPicValidByValue(int dgGrayValue, int CharsCount)
        {
            int posx1 = bmpobj.Width; int posy1 = bmpobj.Height;
            int posx2 = 0; int posy2 = 0;
            for (int i = 0; i < bmpobj.Height; i++)      //����Ч��
            {
                for (int j = 0; j < bmpobj.Width; j++)
                {
                    int pixelValue = bmpobj.GetPixel(j, i).R;
                    if (pixelValue < dgGrayValue)     //���ݻҶ�ֵ
                    {
                        if (posx1 > j) posx1 = j;
                        if (posy1 > i) posy1 = i;

                        if (posx2 < j) posx2 = j;
                        if (posy2 < i) posy2 = i;
                    };
                };
            };
            // ȷ��������
            int Span = CharsCount - (posx2 - posx1 + 1) % CharsCount;   //�������Ĳ����
            if (Span < CharsCount)
            {
                int leftSpan = Span / 2;    //���䵽��ߵĿ��� ����spanΪ����,���ұ߱���ߴ�1
                if (posx1 > leftSpan)
                    posx1 = posx1 - leftSpan;
                if (posx2 + Span - leftSpan < bmpobj.Width)
                    posx2 = posx2 + Span - leftSpan;
            }
            //������ͼ
            Rectangle cloneRect = new Rectangle(posx1, posy1, posx2 - posx1 + 1, posy2 - posy1 + 1);
            bmpobj = bmpobj.Clone(cloneRect, bmpobj.PixelFormat);
        }
        
        /// <summary>
        /// �õ���Чͼ��,ͼ��Ϊ�����
        /// </summary>
        /// <param name="dgGrayValue">�Ҷȱ����ֽ�ֵ</param>
        /// <param name="CharsCount">��Ч�ַ���</param>
        /// <returns></returns>
        public void GetPicValidByValue(int dgGrayValue)
        {
            int posx1 = bmpobj.Width; int posy1 = bmpobj.Height;
            int posx2 = 0; int posy2 = 0;
            for (int i = 0; i < bmpobj.Height; i++)      //����Ч��
            {
                for (int j = 0; j < bmpobj.Width; j++)
                {
                    int pixelValue = bmpobj.GetPixel(j, i).R;
                    if (pixelValue < dgGrayValue)     //���ݻҶ�ֵ
                    {
                        if (posx1 > j) posx1 = j;
                        if (posy1 > i) posy1 = i;

                        if (posx2 < j) posx2 = j;
                        if (posy2 < i) posy2 = i;
                    };
                };
            };
            //������ͼ
            Rectangle cloneRect = new Rectangle(posx1, posy1, posx2 - posx1 + 1, posy2 - posy1 + 1);
            bmpobj = bmpobj.Clone(cloneRect, bmpobj.PixelFormat);
        }

        /// <summary>
        /// �õ���Чͼ��,ͼ�������洫��
        /// </summary>
        /// <param name="dgGrayValue">�Ҷȱ����ֽ�ֵ</param>
        /// <param name="CharsCount">��Ч�ַ���</param>
        /// <returns></returns>
        public Bitmap GetPicValidByValue(Bitmap singlepic, int dgGrayValue)
        {
            int posx1 = singlepic.Width; int posy1 = singlepic.Height;
            int posx2 = 0; int posy2 = 0;
            for (int i = 0; i < singlepic.Height; i++)      //����Ч��
            {
                for (int j = 0; j < singlepic.Width; j++)
                {
                    int pixelValue = singlepic.GetPixel(j, i).R;
                    if (pixelValue < dgGrayValue)     //���ݻҶ�ֵ
                    {
                        if (posx1 > j) posx1 = j;
                        if (posy1 > i) posy1 = i;

                        if (posx2 < j) posx2 = j;
                        if (posy2 < i) posy2 = i;
                    };
                };
            };
            //������ͼ
            Rectangle cloneRect = new Rectangle(posx1, posy1, posx2 - posx1 + 1, posy2 - posy1 + 1);
            return singlepic.Clone(cloneRect, singlepic.PixelFormat);
        }
        
        /// <summary>
        /// ƽ���ָ�ͼƬ
        /// </summary>
        /// <param name="RowNum">ˮƽ�Ϸָ���</param>
        /// <param name="ColNum">��ֱ�Ϸָ���</param>
        /// <returns>�ָ�õ�ͼƬ����</returns>
        public Bitmap [] GetSplitPics(int RowNum,int ColNum)
        {
            if (RowNum == 0 || ColNum == 0)
                return null;
            int singW = bmpobj.Width / RowNum;
            int singH = bmpobj.Height / ColNum;
            Bitmap [] PicArray=new Bitmap[RowNum*ColNum];

            Rectangle cloneRect;
            for (int i = 0; i < ColNum; i++)      //����Ч��
            {
                for (int j = 0; j < RowNum; j++)
                {
                    cloneRect = new Rectangle(j*singW, i*singH, singW , singH);
                    PicArray[i*RowNum+j]=bmpobj.Clone(cloneRect, bmpobj.PixelFormat);//����С��ͼ
                }
            }
            return PicArray;
        }

        /// <summary>
        /// ���ػҶ�ͼƬ�ĵ��������ִ���1��ʾ�ҵ㣬0��ʾ����
        /// </summary>
        /// <param name="singlepic">�Ҷ�ͼ</param>
        /// <param name="dgGrayValue">��ǰ����ɫ����</param>
        /// <returns></returns>
        public string GetSingleBmpCode(Bitmap singlepic, int dgGrayValue)
        {
            Color piexl;
            string code = "";
            for (int posy = 0; posy < singlepic.Height; posy++)
                for (int posx = 0; posx < singlepic.Width; posx++)
                {
                    piexl = singlepic.GetPixel(posx, posy);
                    if (piexl.R < dgGrayValue)    // Color.Black )
                        code = code + "1";
                    else
                        code = code + "0";
                }
            return code;
        }


    }

}
