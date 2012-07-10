using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpdateService
{
    /// <summary>
    /// down 的摘要说明
    /// </summary>
    public class down : IHttpHandler
    {
        const long ChunkSize = 1024;
        public void ProcessRequest(HttpContext context)
        {
            string filename = context.Request["f"];
            context.Response.Clear();
            System.IO.FileStream iStream = System.IO.File.OpenRead("".AppPath()+ "/update/"+filename);
            long dataLengthToRead = iStream.Length;//获得下载文件的总大小
            context.Response.ContentType = "application/octet-stream";
            context.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8));
            byte[] buffer = new byte[ChunkSize];
            if (dataLengthToRead == 0)
            {
                int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小
                context.Response.OutputStream.Write(buffer, 0, lengthRead);
                context.Response.Flush();
            }
            while (dataLengthToRead > 0 && context.Response.IsClientConnected)
            {
                int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小
                context.Response.OutputStream.Write(buffer, 0, lengthRead);
                context.Response.Flush();
                dataLengthToRead = dataLengthToRead - lengthRead;
            }
            context.Response.Close();
            context.Response.End();
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