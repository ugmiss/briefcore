using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Utility
{
    /// <summary>
    /// Http对像构造。
    /// </summary>
    public class HttpHelper
    {
        static string cookies;

        public static string Cookies
        {
            get { return HttpHelper.cookies; }
            set { HttpHelper.cookies = value; }
        }
        /// <summary>
        /// Post请求。
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string HttpPost(string url, string postData, Encoding encoding)
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
                Encoding encoding = Encoding.GetEncoding("gb2312");
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
        /// <summary>
        /// Get请求。
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string HttpGet(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //request.Headers.Set("Cookie", "verifysession=" + Cookies);
                //声明一个HttpWebRequest请求
                request.Timeout = 30000;
                //设置连接超时时间
                request.Headers.Set("Pragma", "no-cache");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding("gb2312");
                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                string strResult = streamReader.ReadToEnd();
                return strResult;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// Get请求。
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string HttpGet(string url, Encoding encoding)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Set("Cookie", "verifysession=" + Cookies);
                //声明一个HttpWebRequest请求
                request.Timeout = 30000;
                //设置连接超时时间
                request.Headers.Set("Pragma", "no-cache");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                string strResult = streamReader.ReadToEnd();
                return strResult;
            }
            catch
            {
                return "";
            }
        }

        public static List<string> FindLinks(string html)
        {
            string regex = @"(?<=<a\s+.*?href\s*=\s*[""']?)[^'""\s]+";
            List<string> list = new List<string>();
            Regex reg = new Regex(regex);
            foreach (Match m in reg.Matches(html))
            {
                list.Add(m.Value);
            }
            return list;
        }

        public static List<string> FindImgs(string html)
        {
            string regex = @"<img .+?>";
            List<string> list = new List<string>();
            Regex reg = new Regex(regex);
            foreach (Match m in reg.Matches(html))
            {
                list.Add(m.Value);
            }
            return list;
        }

        public static Image SavePic(string url, string path)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //声明一个HttpWebRequest请求
            request.Timeout = 15000;
            //设置连接超时时间
            request.Headers.Set("Pragma", "no-cache");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            Image img = Image.FromStream(streamReceive);
            string[] arr = url.Split('/');
            string name = arr[arr.Length - 1];

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (File.Exists(path + "\\" + url.Replace("http://", "").Replace("/", "").Replace(":", "")))
            {
                using (StreamWriter sw = new StreamWriter("log.txt"))
                {
                    sw.WriteLine(url);
                }
                return null;
            }
            img.Save(path + "\\" + url.Replace("http://", "").Replace("/", "").Replace(":", ""));

            return img;
        }

        public static string GetEncodeGB2312(string s)
        {
            return System.Web.HttpUtility.UrlEncode(s, Encoding.GetEncoding("gb2312"));
        }
        public static string GetEncodeUTF8(string s)
        {
            return System.Web.HttpUtility.UrlEncode(s, Encoding.GetEncoding("utf-8"));
        }
        /// <summary>
        /// 登陆qq空间。
        /// </summary>
        /// <param name="qq"></param>
        /// <param name="pass"></param>
        /// <param name="vcode"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        private static string Login(string qq, string pass, string vcode, string cookie)
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
            Encoding encoding = Encoding.GetEncoding("gb2312");
            StreamReader streamReader = new StreamReader(streamReceive, encoding);
            string s = streamReader.ReadToEnd();
            string cookies = response.Headers.ToString();
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

        public static string NoHTML(string Htmlstring) //去除HTML标记   
        {
            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            //Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }

    }
}
