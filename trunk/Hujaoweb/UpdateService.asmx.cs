using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Diagnostics;
using System.IO;

namespace UpdateService
{
    /// <summary>
    /// UpdateService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class UpdateService : System.Web.Services.WebService
    {

        [WebMethod]
        public string GetFileVersion()
        {
            string[]files=  Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\update");
            List<UpdateFile> list = new List<UpdateFile>();
            foreach (var f in files)
            {
                UpdateFile u=new UpdateFile();
                FileVersionInfo fv = FileVersionInfo.GetVersionInfo(f);
                u.Name=f.FileName();
                u.Version=fv.FileVersion;
                u.Size = new FileInfo(f).Length;
                list.Add(u);
            }
            return  list.JsonSerialize();
        }
    }

    public class UpdateFile
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public long Size { get; set; }

    }
}
