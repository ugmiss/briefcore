using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Drawing;

namespace Utility
{
    /// <summary>
    /// Http对像构造。
    /// </summary>
    public class HttpManagement
    {
        static string cookies;

        public static string Cookies
        {
            get { return HttpManagement.cookies; }
            set { HttpManagement.cookies = value; }
        }
        /// <summary>
        /// 登陆qq空间。
        /// </summary>
        /// <param name="qq"></param>
        /// <param name="pass"></param>
        /// <param name="vcode"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static string Login(string qq, string pass, string vcode, string cookie)
        {
            string loginUrl = @"http://ptlogin2.qq.com/login";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(loginUrl);
            request.Method = WebRequestMethods.Http.Post;
            request.KeepAlive = true;
            //必须禁止自动重定向才能获得 Set-Cookie
            request.AllowAutoRedirect = false;
            string postData = "u=" + qq + "&p=" + pass + "&verifycode=" + vcode + "&aid=15000101&u1=http%3A%2F%2Fphp.qzone.qq.com%2Findex.php%3Fmod%3Dportal%26act%3Dlogin&fp=loginerroralert&h=1&ptredirect=1&ptlang=0&from_ui=1&dumy=";
            request.ContentLength = postData.Length;
            //设置 Cookie
            request.Headers.Set("Cookie", "verifysession=" + Cookies);
            Stream requestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(requestStream);
            myStreamWriter.Write(postData, 0, postData.Length);
            myStreamWriter.Close();
            requestStream.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            Encoding encoding = Encoding.GetEncoding("utf-8");
            StreamReader streamReader = new StreamReader(streamReceive, encoding);
            string s = streamReader.ReadToEnd();
            string cookies=response.Headers.ToString();
            string[] setCookies = response.Headers["Set-Cookie"].Split(';');
            response.Close();
            string myCookie = "verifysession=" + cookie;
            foreach (string set in setCookies)
            {
                if (set.Contains("pt2gguin=") || set.Contains("uin=") || set.Contains("skey=") || set.Contains("ptcz="))
                {
                    myCookie += "; " + set.Trim(',');
                }
            }
            Thread.Sleep(3000);
            return myCookie;
        }
        /// <summary>
        /// Post请求。
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string HttpPost(string url, string postData)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = WebRequestMethods.Http.Post;
                request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = true;
                //必须禁止自动重定向才能获得 Set-Cookie
                request.AllowAutoRedirect = false;
                request.ContentLength = postData.Length;
                //设置 Cookie
                request.Headers.Set("Cookie", cookies);
                request.Headers.GetType();
                Stream requestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(requestStream);
                myStreamWriter.Write(postData, 0, postData.Length);
                myStreamWriter.Flush();
                myStreamWriter.Close();
                requestStream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding("utf-8");
                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                string s = streamReader.ReadToEnd();
                response.Close();
                return s;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static string HttpGet(string url)
        {
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Set("Cookie", "verifysession=" + Cookies);
            //声明一个HttpWebRequest请求
            request.Timeout = 30000;
            //设置连接超时时间
            request.Headers.Set("Pragma", "no-cache");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            Encoding encoding = Encoding.GetEncoding("utf-8");
            StreamReader streamReader = new StreamReader(streamReceive, encoding);
            string strResult = streamReader.ReadToEnd();
            return strResult;
        }

        public static Image GetPic(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //声明一个HttpWebRequest请求
            request.Timeout = 30000;
            //设置连接超时时间
            request.Headers.Set("Pragma", "no-cache");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            Image img = Image.FromStream(streamReceive);
            return img;
        }
    }
}
