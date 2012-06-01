using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Collections;

namespace Utility
{
    public class ImageHelp
    {
        /**/
        /// <summary>
        /// 获取图片中的各帧
        /// </summary>
        /// <param name="pPath">图片路径</param>
        /// <param name="pSavePath">保存路径</param>
        public void GetFrames(string pPath, string pSavedPath)
        {
            Image gif = Image.FromFile(pPath);
            FrameDimension fd = new FrameDimension(gif.FrameDimensionsList[0]);

            //获取帧数(gif图片可能包含多帧，其它格式图片一般仅一帧)
            int count = gif.GetFrameCount(fd);

            //以Jpeg格式保存各帧
            for (int i = 0; i < count; i++)
            {
                gif.SelectActiveFrame(fd, i);
                gif.Save(pSavedPath + "\\frame_" + i + ".jpg", ImageFormat.Jpeg);
            }
        }

        /**/
        /**/
        /**/
        /// <summary>
        /// 获取图片缩略图
        /// </summary>
        /// <param name="pPath">图片路径</param>
        /// <param name="pSavePath">保存路径</param>
        /// <param name="pWidth">缩略图宽度</param>
        /// <param name="pHeight">缩略图高度</param>
        /// <param name="pFormat">保存格式，通常可以是jpeg</param>
        public static void GetSmaller(string pPath, string nPath, int pWidth, int pHeight)
        {
            string fileSaveUrl = nPath;

            using (FileStream fs = new FileStream(pPath, FileMode.Open))
            {

                MakeSmallImg(fs, fileSaveUrl, pWidth, pHeight);
            }

        }


        //按模版比例生成缩略图（以流的方式获取源文件）  
        //生成缩略图函数  
        //顺序参数：源图文件流、缩略图存放地址、模版宽、模版高  
        //注：缩略图大小控制在模版区域内  
        public static void MakeSmallImg(System.IO.Stream fromFileStream, string fileSaveUrl, System.Double templateWidth, System.Double templateHeight)
        {
            //从文件取得图片对象，并使用流中嵌入的颜色管理信息  
            System.Drawing.Image myImage = System.Drawing.Image.FromStream(fromFileStream, true);

            //缩略图宽、高  
            System.Double newWidth = myImage.Width, newHeight = myImage.Height;
            //宽大于模版的横图  
            if (myImage.Width > myImage.Height || myImage.Width == myImage.Height)
            {
                if (myImage.Width > templateWidth)
                {
                    //宽按模版，高按比例缩放  
                    newWidth = templateWidth;
                    newHeight = myImage.Height * (newWidth / myImage.Width);
                }
            }
            //高大于模版的竖图  
            else
            {
                if (myImage.Height > templateHeight)
                {
                    //高按模版，宽按比例缩放  
                    newHeight = templateHeight;
                    newWidth = myImage.Width * (newHeight / myImage.Height);
                }
            }

            //取得图片大小  
            System.Drawing.Size mySize = new Size((int)newWidth, (int)newHeight);
            //新建一个bmp图片  
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(mySize.Width, mySize.Height);
            //新建一个画板  
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法  
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度  
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空一下画布  
            g.Clear(Color.White);
            //在指定位置画图  
            g.DrawImage(myImage, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
            new System.Drawing.Rectangle(0, 0, myImage.Width, myImage.Height),
            System.Drawing.GraphicsUnit.Pixel);

            /**/
            /////文字水印  
            //System.Drawing.Graphics G = System.Drawing.Graphics.FromImage(bitmap);
            //System.Drawing.Font f = new Font("Lucida Grande", 6);
            //System.Drawing.Brush b = new SolidBrush(Color.Gray);
            //G.DrawString("hujao.com", f, b, 0, 0);
            //G.Dispose();

            /**/
            ///图片水印  
            //System.Drawing.Image   copyImage   =   System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath("pic/1.gif"));  
            //Graphics   a   =   Graphics.FromImage(bitmap);  
            //a.DrawImage(copyImage,   new   Rectangle(bitmap.Width-copyImage.Width,bitmap.Height-copyImage.Height,copyImage.Width,   copyImage.Height),0,0,   copyImage.Width,   copyImage.Height,   GraphicsUnit.Pixel);  

            //copyImage.Dispose();  
            //a.Dispose();  
            //copyImage.Dispose();  

            //保存缩略图  
            if (File.Exists(fileSaveUrl))
            {
                File.SetAttributes(fileSaveUrl, FileAttributes.Normal);
                File.Delete(fileSaveUrl);
            }

            bitmap.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);

            g.Dispose();
            myImage.Dispose();
            bitmap.Dispose();
        }



        /**/
        /**/
        /**/
        /// <summary>
        /// 获取图片指定部分
        /// </summary>
        /// <param name="pPath">图片路径</param>
        /// <param name="pSavePath">保存路径</param>
        /// <param name="pPartStartPointX">目标图片开始绘制处的坐标X值(通常为)</param>
        /// <param name="pPartStartPointY">目标图片开始绘制处的坐标Y值(通常为)</param>
        /// <param name="pPartWidth">目标图片的宽度</param>
        /// <param name="pPartHeight">目标图片的高度</param>
        /// <param name="pOrigStartPointX">原始图片开始截取处的坐标X值</param>
        /// <param name="pOrigStartPointY">原始图片开始截取处的坐标Y值</param>
        /// <param name="pFormat">保存格式，通常可以是jpeg</param>
        public void GetPart(string pPath, string pSavedPath, int pPartStartPointX, int pPartStartPointY, int pPartWidth, int pPartHeight, int pOrigStartPointX, int pOrigStartPointY)
        {
            string normalJpgPath = pSavedPath + "\\normal.jpg";

            using (Image originalImg = Image.FromFile(pPath))
            {
                Bitmap partImg = new Bitmap(pPartWidth, pPartHeight);
                Graphics graphics = Graphics.FromImage(partImg);
                Rectangle destRect = new Rectangle(new Point(pPartStartPointX, pPartStartPointY), new Size(pPartWidth, pPartHeight));//目标位置
                Rectangle origRect = new Rectangle(new Point(pOrigStartPointX, pOrigStartPointY), new Size(pPartWidth, pPartHeight));//原图位置（默认从原图中截取的图片大小等于目标图片的大小）


                /**/
                ///文字水印  
                System.Drawing.Graphics G = System.Drawing.Graphics.FromImage(partImg);
                System.Drawing.Font f = new Font("Lucida Grande", 6);
                System.Drawing.Brush b = new SolidBrush(Color.Gray);
                G.Clear(Color.White);
                graphics.DrawImage(originalImg, destRect, origRect, GraphicsUnit.Pixel);
                G.DrawString("Ftodo.com", f, b, 0, 0);
                G.Dispose();

                originalImg.Dispose();
                if (File.Exists(normalJpgPath))
                {
                    File.SetAttributes(normalJpgPath, FileAttributes.Normal);
                    File.Delete(normalJpgPath);
                }
                partImg.Save(normalJpgPath, ImageFormat.Jpeg);
            }
        }
        /**/
        /**/
        /**/
        /// <summary>
        /// 获取按比例缩放的图片指定部分
        /// </summary>
        /// <param name="pPath">图片路径</param>
        /// <param name="pSavePath">保存路径</param>
        /// <param name="pPartStartPointX">目标图片开始绘制处的坐标X值(通常为)</param>
        /// <param name="pPartStartPointY">目标图片开始绘制处的坐标Y值(通常为)</param>
        /// <param name="pPartWidth">目标图片的宽度</param>
        /// <param name="pPartHeight">目标图片的高度</param>
        /// <param name="pOrigStartPointX">原始图片开始截取处的坐标X值</param>
        /// <param name="pOrigStartPointY">原始图片开始截取处的坐标Y值</param>
        /// <param name="imageWidth">缩放后的宽度</param>
        /// <param name="imageHeight">缩放后的高度</param>
        public void GetPart(string pPath, string pSavedPath, int pPartStartPointX, int pPartStartPointY, int pPartWidth, int pPartHeight, int pOrigStartPointX, int pOrigStartPointY, int imageWidth, int imageHeight)
        {
            string normalJpgPath = pSavedPath + "\\normal.jpg";
            using (Image originalImg = Image.FromFile(pPath))
            {
                if (originalImg.Width == imageWidth && originalImg.Height == imageHeight)
                {
                    GetPart(pPath, pSavedPath, pPartStartPointX, pPartStartPointY, pPartWidth, pPartHeight, pOrigStartPointX, pOrigStartPointY);
                    return;
                }

                Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                Image zoomImg = originalImg.GetThumbnailImage(imageWidth, imageHeight, callback, IntPtr.Zero);//缩放
                Bitmap partImg = new Bitmap(pPartWidth, pPartHeight);

                Graphics graphics = Graphics.FromImage(partImg);
                Rectangle destRect = new Rectangle(new Point(pPartStartPointX, pPartStartPointY), new Size(pPartWidth, pPartHeight));//目标位置
                Rectangle origRect = new Rectangle(new Point(pOrigStartPointX, pOrigStartPointY), new Size(pPartWidth, pPartHeight));//原图位置（默认从原图中截取的图片大小等于目标图片的大小）

                /**/
                ///文字水印  
                System.Drawing.Graphics G = System.Drawing.Graphics.FromImage(partImg);
                System.Drawing.Font f = new Font("Lucida Grande", 6);
                System.Drawing.Brush b = new SolidBrush(Color.Gray);
                G.Clear(Color.White);

                graphics.DrawImage(zoomImg, destRect, origRect, GraphicsUnit.Pixel);
                G.DrawString("Ftodo.com", f, b, 0, 0);
                G.Dispose();

                originalImg.Dispose();
                if (File.Exists(normalJpgPath))
                {
                    File.SetAttributes(normalJpgPath, FileAttributes.Normal);
                    File.Delete(normalJpgPath);
                }
                partImg.Save(normalJpgPath, ImageFormat.Jpeg);
            }
        }

        /**/
        /// <summary>
        /// 获得图像高宽信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ImageInformation GetImageInfo(string path)
        {
            using (Image image = Image.FromFile(path))
            {
                return new ImageInformation { Width = image.Width, Height = image.Height };
            }
        }
        public bool ThumbnailCallback()
        {
            return false;
        }

    }
    public struct ImageInformation
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }



}



namespace Imag_writer
{
    /**/
    /// <summary>
    /// 水印的类型
    /// </summary>
    public enum WaterMarkType
    {
        /**/
        /// <summary>
        /// 文字水印
        /// </summary>
        TextMark,
        /**/
        /// <summary>
        /// 图片水印
        /// </summary>
        //ImageMark // 暂时只能添加文字水印
    };

    /**/
    /// <summary>
    /// 水印的位置
    /// </summary>
    public enum WaterMarkPosition
    {
        /**/
        /// <summary>
        /// 左上角
        /// </summary>
        WMP_Left_Top,
        /**/
        /// <summary>
        /// 左下角
        /// </summary>
        WMP_Left_Bottom,
        /**/
        /// <summary>
        /// 右上角
        /// </summary>
        WMP_Right_Top,
        /**/
        /// <summary>
        /// 右下角
        /// </summary>
        WMP_Right_Bottom
    };

    /**/
    /// <summary>
    /// 处理图片的类（包括加水印，生成缩略图）
    /// </summary>
    public class ImageWaterMark
    {
        public ImageWaterMark()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region 给图片加水印
        /**/
        /// <summary>
        /// 添加水印(分图片水印与文字水印两种)
        /// </summary>
        /// <param name="oldpath">原图片绝对地址</param>
        /// <param name="newpath">新图片放置的绝对地址</param>
        /// <param name="wmtType">要添加的水印的类型</param>
        /// <param name="sWaterMarkContent">水印内容，若添加文字水印，此即为要添加的文字；
        /// 若要添加图片水印，此为图片的路径</param>
        public void addWaterMark(string oldpath, string newpath, WaterMarkType wmtType, string sWaterMarkContent)
        {
            try
            {
                Image image = Image.FromFile(oldpath);

                Bitmap b = new Bitmap(image.Width, image.Height,
                 PixelFormat.Format24bppRgb);

                Graphics g = Graphics.FromImage(b);
                g.Clear(Color.White);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.High;

                g.DrawImage(image, 0, 0, image.Width, image.Height);

                switch (wmtType)
                {
                    /**/
                    /*case WaterMarkType.ImageMark:
   //图片水印
   this.addWatermarkImage(g, 
    Page.Server.MapPath(Watermarkimgpath), 
    WatermarkPosition,image.Width,image.Height);
   break;*/
                    case WaterMarkType.TextMark:
                        //文字水印
                        this.addWatermarkText(g, sWaterMarkContent, "WM_BOTTOM_RIGHT",
                         image.Width, image.Height);
                        break;
                }
                b.Save(newpath);
                b.Dispose();
                image.Dispose();
            }
            catch(Exception ex)
            {
                if (File.Exists(oldpath))
                {
                    File.Delete(oldpath);
                }
                throw ex;
            }
            finally
            {
                if (File.Exists(oldpath))
                {
                    //File.Delete(oldpath);
                }
            }
        }

        /**/
        /// <summary>
        ///   加水印文字
        /// </summary>
        /// <param name="picture">imge 对象</param>
        /// <param name="_watermarkText">水印文字内容</param>
        /// <param name="_watermarkPosition">水印位置</param>
        /// <param name="_width">被加水印图片的宽</param>
        /// <param name="_height">被加水印图片的高</param>
        private void addWatermarkText(Graphics picture, string _watermarkText,
         string _watermarkPosition, int _width, int _height)
        {
            // 确定水印文字的字体大小
            int[] sizes = new int[] { 32, 30, 28, 26, 24, 22, 20, 18, 16, 14, 12, 10, 8, 6, 4 };
            Font crFont = null;
            SizeF crSize = new SizeF();
            for (int i = 0; i < sizes.Length; i++)
            {
                crFont = new Font("Arial Black", sizes[i], FontStyle.Bold);
                crSize = picture.MeasureString(_watermarkText, crFont);

                if ((ushort)crSize.Width < (ushort)_width)
                {
                    break;
                }
            }

            // 生成水印图片（将文字写到图片中）
            Bitmap floatBmp = new Bitmap((int)crSize.Width + 3,
                  (int)crSize.Height + 3, PixelFormat.Format32bppArgb);
            Graphics fg = Graphics.FromImage(floatBmp);
            PointF pt = new PointF(0, 0);

            // 画阴影文字
            Brush TransparentBrush0 = new SolidBrush(Color.FromArgb(255, Color.Black));
            Brush TransparentBrush1 = new SolidBrush(Color.FromArgb(255, Color.Black));
            fg.DrawString(_watermarkText, crFont, TransparentBrush0, pt.X, pt.Y + 1);
            fg.DrawString(_watermarkText, crFont, TransparentBrush0, pt.X + 1, pt.Y);

            fg.DrawString(_watermarkText, crFont, TransparentBrush1, pt.X + 1, pt.Y + 1);
            fg.DrawString(_watermarkText, crFont, TransparentBrush1, pt.X, pt.Y + 2);
            fg.DrawString(_watermarkText, crFont, TransparentBrush1, pt.X + 2, pt.Y);

            TransparentBrush0.Dispose();
            TransparentBrush1.Dispose();

            // 画文字
            fg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fg.DrawString(_watermarkText,
             crFont, new SolidBrush(Color.White),
             pt.X, pt.Y, StringFormat.GenericDefault);

            // 保存刚才的操作
            fg.Save();
            fg.Dispose();
            // floatBmp.Save("d:\\WebSite\\DIGITALKM\\ttt.jpg");

            // 将水印图片加到原图中
            this.addWatermarkImage(
             picture,
             new Bitmap(floatBmp),
             "WM_BOTTOM_RIGHT",
             _width,
             _height);
        }

        /**/
        /// <summary>
        ///   加水印图片
        /// </summary>
        /// <param name="picture">imge 对象</param>
        /// <param name="iTheImage">Image对象（以此图片为水印）</param>
        /// <param name="_watermarkPosition">水印位置</param>
        /// <param name="_width">被加水印图片的宽</param>
        /// <param name="_height">被加水印图片的高</param>
        private void addWatermarkImage(Graphics picture, Image iTheImage,
         string _watermarkPosition, int _width, int _height)
        {
            Image watermark = new Bitmap(iTheImage);

            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMap colorMap = new ColorMap();

            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = { colorMap };

            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            float[][] colorMatrixElements = {
             new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
             new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
             new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
             new float[] {0.0f, 0.0f, 0.0f, 0.3f, 0.0f},
             new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
            };

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            int xpos = 0;
            int ypos = 0;
            int WatermarkWidth = 0;
            int WatermarkHeight = 0;
            double bl = 1d;

            //计算水印图片的比率
            //取背景的1/4宽度来比较
            if ((_width > watermark.Width * 4) && (_height > watermark.Height * 4))
            {
                bl = 1;
            }
            else if ((_width > watermark.Width * 4) && (_height < watermark.Height * 4))
            {
                bl = Convert.ToDouble(_height / 4) / Convert.ToDouble(watermark.Height);

            }
            else if ((_width < watermark.Width * 4) && (_height > watermark.Height * 4))
            {
                bl = Convert.ToDouble(_width / 4) / Convert.ToDouble(watermark.Width);
            }
            else
            {
                if ((_width * watermark.Height) > (_height * watermark.Width))
                {
                    bl = Convert.ToDouble(_height / 4) / Convert.ToDouble(watermark.Height);

                }
                else
                {
                    bl = Convert.ToDouble(_width / 4) / Convert.ToDouble(watermark.Width);

                }

            }

            WatermarkWidth = Convert.ToInt32(watermark.Width * bl);
            WatermarkHeight = Convert.ToInt32(watermark.Height * bl);

            switch (_watermarkPosition)
            {
                case "WM_TOP_LEFT":
                    xpos = 10;
                    ypos = 10;
                    break;
                case "WM_TOP_RIGHT":
                    xpos = _width - WatermarkWidth - 10;
                    ypos = 10;
                    break;
                case "WM_BOTTOM_RIGHT":
                    xpos = _width - WatermarkWidth - 10;
                    ypos = _height - WatermarkHeight - 10;
                    break;
                case "WM_BOTTOM_LEFT":
                    xpos = 10;
                    ypos = _height - WatermarkHeight - 10;
                    break;
            }

            picture.DrawImage(
             watermark,
             new Rectangle(xpos, ypos, WatermarkWidth, WatermarkHeight),
             0,
             0,
             watermark.Width,
             watermark.Height,
             GraphicsUnit.Pixel,
             imageAttributes);

            watermark.Dispose();
            imageAttributes.Dispose();
        }

        /**/
        /// <summary>
        ///   加水印图片
        /// </summary>
        /// <param name="picture">imge 对象</param>
        /// <param name="WaterMarkPicPath">水印图片的地址</param>
        /// <param name="_watermarkPosition">水印位置</param>
        /// <param name="_width">被加水印图片的宽</param>
        /// <param name="_height">被加水印图片的高</param>
        private void addWatermarkImage(Graphics picture, string WaterMarkPicPath,
         string _watermarkPosition, int _width, int _height)
        {
            Image watermark = new Bitmap(WaterMarkPicPath);

            this.addWatermarkImage(picture, watermark, _watermarkPosition, _width,
             _height);
        }
        #endregion

        #region 生成缩略图
        /**/
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="image">Image 对象</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="ici">指定格式的编解码参数</param>
        private void SaveImage(Image image, string savePath, ImageCodecInfo ici)
        {
            //设置 原图片 对象的 EncoderParameters 对象
            EncoderParameters parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(
             System.Drawing.Imaging.Encoder.Quality, ((long)90));
            image.Save(savePath, ici, parameters);
            parameters.Dispose();
        }

        /**/
        /// <summary>
        /// 获取图像编码解码器的所有相关信息
        /// </summary>
        /// <param name="mimeType">包含编码解码器的多用途网际邮件扩充协议 (MIME) 类型的字符串</param>
        /// <returns>返回图像编码解码器的所有相关信息</returns>
        private ImageCodecInfo GetCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in CodecInfo)
            {
                if (ici.MimeType == mimeType)
                    return ici;
            }
            return null;
        }

        /**/
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="sourceImagePath">原图片路径(相对路径)</param>
        /// <param name="thumbnailImagePath">生成的缩略图路径,如果为空则保存为原图片路径(相对路径)</param>
        /// <param name="thumbnailImageWidth">缩略图的宽度（高度与按源图片比例自动生成）</param>
        public void ToThumbnailImages(
         string SourceImagePath,
         string ThumbnailImagePath,
         int ThumbnailImageWidth)
        {
            Hashtable htmimes = new Hashtable();
            htmimes[".jpeg"] = "image/jpeg";
            htmimes[".jpg"] = "image/jpeg";
            htmimes[".png"] = "image/png";
            htmimes[".tif"] = "image/tiff";
            htmimes[".tiff"] = "image/tiff";
            htmimes[".bmp"] = "image/bmp";
            htmimes[".gif"] = "image/gif";

            // 取得原图片的后缀
            string sExt = SourceImagePath.Substring(
             SourceImagePath.LastIndexOf(".")).ToLower();

            //从 原图片 创建 Image 对象
            Image image = Image.FromFile(SourceImagePath);
            int num = ((ThumbnailImageWidth / 4) * 3);
            int width = image.Width;
            int height = image.Height;

            //计算图片的比例
            if ((((double)width) / ((double)height)) >= 1.3333333333333333f)
            {
                num = ((height * ThumbnailImageWidth) / width);
            }
            else
            {
                ThumbnailImageWidth = ((width * num) / height);
            }
            if ((ThumbnailImageWidth < 1) || (num < 1))
            {
                return;
            }

            //用指定的大小和格式初始化 Bitmap 类的新实例
            Bitmap bitmap = new Bitmap(ThumbnailImageWidth, num,
             PixelFormat.Format32bppArgb);

            //从指定的 Image 对象创建新 Graphics 对象
            Graphics graphics = Graphics.FromImage(bitmap);

            //清除整个绘图面并以透明背景色填充
            graphics.Clear(Color.Transparent);

            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.High;

            //在指定位置并且按指定大小绘制 原图片 对象
            graphics.DrawImage(image, new Rectangle(0, 0, ThumbnailImageWidth, num));
            image.Dispose();

            try
            {
                //将此 原图片 以指定格式并用指定的编解码参数保存到指定文件 
                SaveImage(bitmap, ThumbnailImagePath,
                 GetCodecInfo((string)htmimes[sExt]));
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}