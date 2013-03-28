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
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string str = textBox1.Text;
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(str, out qrCode);
            GraphicsRenderer renderer = new GraphicsRenderer(
                new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            string str2 = Guid.NewGuid().ToString().Replace("-", "");
            using (FileStream stream = new FileStream(@"D:\" + str2 + ".bmp", FileMode.OpenOrCreate))
            {
                renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);
            }
            this.pictureBox1.Image = Image.FromFile(@"D:\" + str2 + ".bmp");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            
            
        }
    }
}
