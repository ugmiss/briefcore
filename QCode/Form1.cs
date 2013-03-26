using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

namespace QCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = textBox1.Text;
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(str, out qrCode);
            GraphicsRenderer renderer = new GraphicsRenderer(
                new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            using (FileStream stream = new FileStream(@"D:\" + str + ".png", FileMode.OpenOrCreate))
            {
                renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);
                //this.pictureBox1.Image = Image.FromStream(stream);
            }
            //Thread.Sleep(2000);
            //FileStream fs = new FileStream(@"D:\Matrix.png", FileMode.Open, FileAccess.Read);
            //int byteLength = (int)fs.Length;
            //byte[] fileBytes = new byte[byteLength];
            //fs.Read(fileBytes, 0, byteLength);
            ////文件流关闭,文件解除锁定    
            //fs.Close();
            //this.pictureBox1.Image = Image.FromStream(new MemoryStream(fileBytes));
            this.pictureBox1.Image = Image.FromFile(@"D:\" + str + ".png");
        }
    }
}
