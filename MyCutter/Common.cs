using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MyCutter
{
    public class Common
    {
        /// <summary>
        /// 选文件.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string SelectFile(string type)
        {

            OpenFileDialog dialog = new OpenFileDialog();

            switch (type)
            {
                case "pic":
                    dialog.DefaultExt = "*.jpg|(jpg),*.png|(png)";
                    break;
                case "txt":
                    dialog.DefaultExt = "*.txt|(txt)";
                    break;
                case "video":
                    dialog.DefaultExt = "*.mp4|(mp4)";
                    break;
                case "audio":
                    dialog.DefaultExt = "*.wav|(wav)";
                    break;
                case "all":
                    dialog.DefaultExt = "*.*|(所有文件)";
                    break;
            }
            dialog.ShowDialog();
            if (!dialog.FileName.IsEmpty())
            {
                return dialog.FileName;
            }
            return "";
        }
        /// <summary>
        /// 取文件短名
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static string GetShortFileName(string FileName)
        {
            string[] arr = FileName.Split("\\/".ToArray());
            return arr[arr.Length - 1];
        }
        /// <summary>
        /// 添加资源。
        /// </summary>
        /// <param name="srcfile"></param>
        /// <param name="tagFile"></param>
        /// <returns></returns>
        public static string AddResouce(string bookname, string srcfile, string tagFile)
        {
            if (File.Exists(tagFile))
            {
                return GetShortFileName(GetResouce(bookname, GetShortFileName(tagFile)));
            }
            while (File.Exists(tagFile))
                tagFile = tagFile.Insert(tagFile.LastIndexOf("."), "(1)");
            //File.Create(tagFile);
            File.Copy(srcfile, tagFile, true);
            return GetShortFileName(GetResouce(bookname, GetShortFileName(tagFile)));
        }
        public static string GetResouce(string bookname, string filename)
        {
            return AppDomain.CurrentDomain.BaseDirectory + bookname + "\\" + filename;
        }

    }
}
