using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Hujaoweb
{
    /// <summary>
    /// GetPic 的摘要说明
    /// </summary>
    public class GetPic : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string[] pics = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "images/green/");
            List<string> plist = new List<string>();
            foreach (var f in pics)
            {
                plist.Add(f.FileName());
            }
            context.Response.Write(plist.JsonSerialize());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}