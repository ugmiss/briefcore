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
    }
}
