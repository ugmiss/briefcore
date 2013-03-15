using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utility;
using System.IO;
using NAudio.Wave;
using System.Threading;

namespace Test
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string html = HttpHelper.HttpGet("");
            string html2 = HttpHelper.NoHTML(html);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "*.gif|*.gif|*.jpg|*.jpg|*.png|*png|*.bmp|*.bmp";

            file.ShowDialog();
            if (!string.IsNullOrEmpty(file.FileName))
            {
                try
                {
                    FileStream fs = new FileStream(file.FileName, FileMode.Open, FileAccess.Read);
                    int byteLength = (int)fs.Length;
                    byte[] fileBytes = new byte[byteLength];
                    fs.Read(fileBytes, 0, byteLength);
                    //文件流关闭,文件解除锁定
                    fs.Close();
                    this.pictureBox1.Image = Image.FromStream(new MemoryStream(fileBytes));
                }
                catch (OutOfMemoryException ex)
                {
                    MessageBox.Show("图片太大或非法的图片格式。");
                    return;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "*.jpg|*.jpg|*.png|*png|*.bmp|*.bmp|*.gif|*.gif";
            file.ShowDialog();
            if (!string.IsNullOrEmpty(file.FileName))
            {
                try
                {
                    this.pictureBox1.Image = Image.FromFile(file.FileName); ;
                }
                catch (OutOfMemoryException ex)
                {
                    MessageBox.Show("图片太大或非法的图片格式。");
                    return;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "*.mp3|*.mp3|*.wav|*wav";
            file.ShowDialog();
            if (!string.IsNullOrEmpty(file.FileName))
            {
                NAudio.Codecs.Band b = new NAudio.Codecs.Band();
                NAudio.Codecs.G722Codec c = new NAudio.Codecs.G722Codec();
                NAudio.Codecs.G722CodecState s = new NAudio.Codecs.G722CodecState(64000, NAudio.Codecs.G722Flags.SampleRate8000);

                using (var ms = File.OpenRead(file.FileName))
                using (var rdr = new Mp3FileReader(ms))
                using (var wavStream = WaveFormatConversionStream.CreatePcmStream(rdr))
                using (var baStream = new BlockAlignReductionStream(wavStream))
                using (var waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                {
                    waveOut.Init(baStream);
                    waveOut.Play();
                    while (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(100);
                    }
                }
            }
        }

        private void repositoryItemTextEdit1_MouseHover(object sender, EventArgs e)
        {

        }
        double x1 = 0;
        double y1 = 0;

        double x2 = 0;
        double y2 = 0;

        double d1 = 0;
        double d2 = 0;
        double x = 0;
        double y = 0;
        private void button5_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < 10; i++)
            //{
                x1 = RandomFactory.NextDouble() * 400;
                y1 = RandomFactory.NextDouble() * 400;

                x2 =  RandomFactory.NextDouble() * 400;
                y2 =  RandomFactory.NextDouble() * 400;

                d1 =   RandomFactory.NextDouble() *50;
                d2 =   RandomFactory.NextDouble() *50;

                x = GetBowStringMidX(x1, y1, x2, y2, d1, d2);
                double xx = GetBowStringMidX(x2, y2, x1, y1, d2, d1);
                y = GetBowStringMidY(x1, y1, x2, y2, x);
                panel1.Refresh();
            //    MessageBox.Show(string.Format("({0},{1})-{2} ({3},{4})-{5} x={6}", x1, y1, d1, x2, y2, d2, x));
            //}
        }

        public double GetBowStringMidY(double x1, double y1, double x2, double y2, double x)
        {
            if (x1 == x2) return y1;
            double y = ((y1 - y2) * x + (y2 * x1 - y1 * x2)) / (x1 - x2);
            return y;
        }
        //取两圆根轴与圆心连线焦点
        public double GetBowStringMidX(double x1, double y1, double x2, double y2, double d1, double d2)
        {
            double fenzi = (y2 - y1) * (y2 * x1 - y1 * x2) - 0.5 * (d1 * d1 - d2 * d2 - x1 * x1 + x2 * x2 - y1 * y1 + y2 * y2) * (x1 - x2);
            double fenmu = (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);
            return fenzi / fenmu;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.Black);
            Pen p = new Pen(Color.Black);
            g.FillEllipse(Brushes.Blue, (float)(x1 - d1), (float)(y1 - d1), (float)(2.0 * d1), (float)(2.0 * d1));
            g.FillEllipse(Brushes.Blue, (float)(x2 - d2), (float)(y2 - d2), (float)(2.0 * d2), (float)(2.0 * d2));
            g.FillEllipse(Brushes.Red, (float)(x - 2), (float)(y - 2), (float)(2.0 * 2), (float)(2.0 * 2));

        }
    }
}
