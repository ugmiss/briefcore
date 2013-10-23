using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utility;
using System.Reflection;
using System.Net;
using System.IO;

namespace RTL
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        string Filename = "";
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Excel 2007 (*.xls)|*.xls|Excel 2010 (*.xlsx)|*.xlsx";
            dialog.ShowDialog();
            if (dialog.FileName.NotNullOrEmpty())
            {
                Filename = dialog.FileName;
                Filename.WriteToFile("p.data");

                textBox1.Text = Filename;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!Filename.NotNullOrEmpty()) return;
            ExcelEdit edit = new ExcelEdit();
            edit.Open(Filename);
            var ws = edit.GetSheet("sheet1");
            int y = 2;
            while (true)
            {
                var range = ws.get_Range("A" + y, Missing.Value);
                String rangevalue = (String)range.Value2;
                if (rangevalue == null)
                    break;
                rangevalue = rangevalue.Trim();
                if (rangevalue.NotNullOrEmpty())
                {
                    Ent_Person person = new Ent_Person();
                    foreach (var p in typeof(Ent_Person).GetProperties())
                    {
                        var r = ws.get_Range(Environment.ExcelDic[p.Name] + y, Missing.Value);
                        String value = Convert.ToString(r.Value2);
                        string v = value;
                        p.FastSetValue(person, v);
                    }
                    string postdata = "__EVENTTARGET=&__EVENTARGUMENT=&__VIEWSTATE=%2FwEPDwUKLTc0NjQ3NTY4OWRk2lHPrQmYMxplDl%2FNhL0lb%2FEeRT4%3D&__EVENTVALIDATION=%2FwEWBAK6i7byCwLAkPSkCgKygfmWCALBsdVZW7JjviZemfj1DUThsNq1sDPv1Zw%3D&textarea=+++++++++++++++++++++++++++%0D%0A+++++++++++++++++++++++++++++++++++++++++++++++++++%E7%89%B9%E5%88%AB%E6%B3%A8%E6%84%8F%E4%BA%8B%E9%A1%B9%0D%0A%0D%0A%E4%B8%80%E3%80%81%E7%99%BB%E9%99%86%0D%0A%E4%BD%BF%E7%94%A8%E7%94%B1%E4%B8%8A%E6%B5%B7%E5%9B%BD%E6%8B%8D%E6%8F%90%E4%BE%9B%E7%9A%84%E3%80%8A%E6%8A%95%E6%A0%87%E6%8B%8D%E5%8D%96%E5%8D%A1%E3%80%8B%E3%80%81%E3%80%8A%E6%8A%95%E6%A0%87%E5%AF%86%E7%A0%81%E6%9D%A1%E3%80%8B%E4%B8%AD%E5%94%AF%E4%B8%80%E7%9A%84%E6%8A%95%E6%A0%87%E5%8F%B7%E3%80%81%E5%AF%86%E7%A0%81%E7%99%BB%E9%99%86%E4%B8%8A%E6%B5%B7%E5%9B%BD%E9%99%85%E5%95%86%E5%93%81%E6%9C%89%E9%99%90%E5%85%AC%E5%8F%B8%E7%BD%91%E7%AB%99%E6%9C%BA%E5%8A%A8%E8%BD%A6%E9%A2%9D%E5%BA%A6%E6%8B%8D%E5%8D%96%E7%BD%91%E7%BB%9C%E6%94%AF%E4%BB%98%E7%B3%BB%E7%BB%9F%E5%AE%9E%E6%96%BD%E6%93%8D%E4%BD%9C%E3%80%82%0D%0A%0D%0A%E4%BA%8C%E3%80%81%E9%93%B6%E8%A1%8C%E5%8D%A1%0D%0A++++%E6%9C%AC%E7%B3%BB%E7%BB%9F%E6%89%80%E6%94%AF%E6%8C%81%E7%9A%84%E7%94%A8%E4%BA%8E%E7%BD%91%E7%BB%9C%E6%94%AF%E4%BB%98%E7%9A%84%E9%93%B6%E8%A1%8C%E5%8D%A1%E6%98%AF%E6%8C%87%E4%B8%8E%E2%80%9C%E9%93%B6%E8%81%94%E2%80%9D%E6%89%80%E8%BF%9E%E9%80%9A%E7%9A%84%E5%9B%BD%E5%86%85%E5%95%86%E4%B8%9A%E9%93%B6%E8%A1%8C%E4%B9%8B%E9%93%B6%E8%A1%8C%E5%8D%A1%E3%80%82%0D%0A%0D%0A%E4%B8%89%E3%80%81%E3%80%8A%E9%A2%9D%E5%BA%A6%E8%AF%81%E6%98%8E%E3%80%8B%E3%80%81%E5%8F%91%E7%A5%A8%E7%9A%84%E9%A2%86%E5%8F%96%0D%0A%0D%0A1%E3%80%81%E9%A2%86%E5%8F%96%E6%97%B6%E9%97%B4%EF%BC%9A%E8%8B%A5%E7%94%A8%E6%88%B7%E5%9C%A8%E6%AF%8F%E6%97%A515%EF%BC%9A30%E5%89%8D%E4%BD%BF%E7%94%A8%E6%9C%AC%E6%9C%8D%E5%8A%A1%E6%94%AF%E4%BB%98%E6%88%90%E5%8A%9F%E7%9A%84%EF%BC%8C%E5%8F%AF%E4%BA%8E%E6%AC%A1%E6%97%A59%EF%BC%9A00%E2%80%9416%EF%BC%9A00%E9%A2%86%E5%8F%96%E3%80%8A%E9%A2%9D%E5%BA%A6%E8%AF%81%E6%98%8E%E3%80%8B%E5%8F%8A%E5%8F%91%E7%A5%A8%28%E8%8A%82%E5%81%87%E6%97%A5%E9%99%A4%E5%A4%96%29%EF%BC%9B%E8%8B%A5%E7%94%A8%E6%88%B7%E5%9C%A8%E6%AF%8F%E6%97%A515%EF%BC%9A30%E5%90%8E%E4%BD%BF%E7%94%A8%E6%9C%AC%E6%9C%8D%E5%8A%A1%E6%94%AF%E4%BB%98%E6%88%90%E5%8A%9F%E7%9A%84%EF%BC%8C%E5%BA%94%E4%BA%8E%E6%94%AF%E4%BB%98%E6%88%90%E5%8A%9F48%E5%B0%8F%E6%97%B6%E5%90%8E%E7%9A%849%EF%BC%9A00%E2%80%9416%EF%BC%9A00%E9%A2%86%E5%8F%96%E3%80%8A%E9%A2%9D%E5%BA%A6%E8%AF%81%E6%98%8E%E3%80%8B%E5%8F%8A%E5%8F%91%E7%A5%A8%28%E8%8A%82%E5%81%87%E6%97%A5%E9%99%A4%E5%A4%96%29%E3%80%82%EF%BC%88%E3%80%8A%E9%A2%9D%E5%BA%A6%E8%AF%81%E6%98%8E%E3%80%8B%E6%9C%89%E6%95%88%E6%9C%9F%E5%85%AD%E4%B8%AA%E6%9C%88%EF%BC%8C%E7%94%A8%E6%88%B7%E5%BA%94%E5%9C%A8%E8%AF%A5%E6%9C%89%E6%95%88%E6%9C%9F%E5%86%85%E5%8F%8A%E6%97%B6%E9%A2%86%E5%8F%96%EF%BC%8C%E5%90%A6%E5%88%99%E8%BF%87%E6%9C%9F%E4%BD%9C%E5%BA%9F%EF%BC%89%0D%0A%0D%0A2%E3%80%81%E9%A2%86%E5%8F%96%E5%9C%B0%E7%82%B9%3A+%E7%A6%8F%E5%B7%9E%E8%B7%AF108%E5%8F%B7%E2%80%95%E2%80%95%E5%9B%BD%E6%8B%8D%E5%A4%A7%E6%A5%BC%0D%0A%0D%0A3%E3%80%81%E9%A2%86%E5%8F%96%E6%97%B6%E6%89%80%E9%9C%80%E6%90%BA%E5%B8%A6%E7%9A%84%E6%89%8B%E7%BB%AD%E6%9D%90%E6%96%99%EF%BC%9A%0D%0A+++%E2%97%8F%E6%8B%8D%E5%8D%96%E6%88%90%E4%BA%A4%E7%94%A8%E6%88%B7%E8%8B%A5%E4%B8%BA%E8%87%AA%E7%84%B6%E4%BA%BA%EF%BC%9A%0D%0A%E9%A1%BB%E7%94%B1%E3%80%8A%E6%8A%95%E6%A0%87%E6%8B%8D%E5%8D%96%E5%8D%A1%E3%80%8B%E5%8F%8A%E3%80%8A%E6%8A%95%E6%A0%87%E5%AF%86%E7%A0%81%E6%9D%A1%E3%80%8B%E4%B8%AD%E7%99%BB%E8%AE%B0%E7%9A%84%E7%94%A8%E6%88%B7%E6%9C%AC%E4%BA%BA%E5%88%B0%E5%9C%BA%E6%8F%90%E4%BA%A4%E7%9C%9F%E5%AE%9E%E6%9C%89%E6%95%88%E7%9A%84%E8%BA%AB%E4%BB%BD%E8%AF%81%E6%98%8E%E5%8E%9F%E4%BB%B6%EF%BC%88%E9%99%84%E5%85%8D%E5%86%A0%E7%85%A7%E7%89%87%E7%9A%84%EF%BC%89%E5%92%8C%E3%80%8A%E6%8A%95%E6%A0%87%E6%8B%8D%E5%8D%96%E5%8D%A1%E3%80%8B%E3%80%81%E3%80%8A%E6%8A%95%E6%A0%87%E5%AF%86%E7%A0%81%E6%9D%A1%E3%80%8B%E5%8E%9F%E4%BB%B6%E3%80%82%0D%0A+++%E2%97%8F%E6%8B%8D%E5%8D%96%E6%88%90%E4%BA%A4%E7%94%A8%E6%88%B7%E8%8B%A5%E4%B8%BA%E6%B3%95%E4%BA%BA%E6%88%96%E5%85%B6%E5%AE%83%E7%BB%84%E7%BB%87%EF%BC%9A%0D%0A%E9%A1%BB%E7%94%B1%E7%BB%8F%E5%90%88%E6%B3%95%E6%8E%88%E6%9D%83%E7%9A%84%E4%BB%A3%E7%90%86%E4%BA%BA%E5%88%B0%E5%9C%BA%E6%8F%90%E4%BA%A4%E3%80%8A%E6%8A%95%E6%A0%87%E6%8B%8D%E5%8D%96%E5%8D%A1%E3%80%8B%E5%8F%8A%E3%80%8A%E6%8A%95%E6%A0%87%E5%AF%86%E7%A0%81%E6%9D%A1%E3%80%8B%E5%8E%9F%E4%BB%B6%E5%92%8C%E8%AF%A5%E7%BB%84%E7%BB%87%E6%9C%89%E6%95%88%E7%9A%84%E3%80%8A%E7%BB%84%E7%BB%87%E6%9C%BA%E6%9E%84%E4%BB%A3%E7%A0%81%E8%AF%81%E3%80%8B%E5%8E%9F%E4%BB%B6%E3%80%82%0D%0A%0D%0A&ctl00%24LoginMain%24tbxBidderNo={0}&ctl00%24LoginMain%24tbxPassword={1}&ctl00%24LoginMain%24btnSubmit=%E5%90%8C%E6%84%8F";
                    var res = GetResponse("https://live.alltobid.com/edupai/LimitBidSys/LimitBidLogin.aspx", "POST",
                        postdata.FormatWith(person.CardNo, person.PassWord));

                    var rangeD = ws.get_Range("D" + y, Missing.Value);
                    if (res.IndexOf("LimitBidPay.aspx") > 0)
                    {

                        rangeD.set_Value(Missing.Value, "中");
                    }
                    else
                    {
                        rangeD.set_Value(Missing.Value, "不中");
                    }

                }
                else
                {
                    break;
                }
                y++;
            }
            edit.SaveAs(Filename);
            edit.Close();
            MessageBox.Show("完成");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Filename = "".ReadFromFile("p.data").Trim();
            textBox1.Text = Filename;
        }
        private static CookieContainer m_Cookie = new CookieContainer();
        public static string GetResponse(string url, string method, string data)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.KeepAlive = true; req.Method = method.ToUpper();
                req.AllowAutoRedirect = true;
                req.CookieContainer = m_Cookie;
                req.ContentType = "application/x-www-form-urlencoded";
                req.UserAgent = "IE7";
                req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                req.Timeout = 50000;
                if (method.ToUpper() == "POST" && data != null)
                {
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    byte[] postBytes = encoding.GetBytes(data); ;
                    req.ContentLength = postBytes.Length;
                    Stream st = req.GetRequestStream();
                    st.Write(postBytes, 0, postBytes.Length); st.Close();
                }
                System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) => { return true; };
                Encoding myEncoding = Encoding.GetEncoding("UTF-8");
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                Stream resst = res.GetResponseStream();
                StreamReader sr = new StreamReader(resst, myEncoding);
                string str = sr.ReadToEnd();
                return str;
            }
            catch (Exception)
            { return string.Empty; }
        }
    }
}
