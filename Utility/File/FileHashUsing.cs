using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Utility
{
    public class FileHashUsing
    {
        /// <summary>
        /// 获得文件的hash代码
        /// </summary>
        /// <param name="strFileName">文件全名</param>
        /// <returns></returns>
        public static string GetFileHash(string strFileName)
        {
            if (strFileName == null || strFileName.Equals(""))
            {
                return string.Empty;
            }

            if (!File.Exists(strFileName))
            {
                return string.Empty;
            }

            HashAlgorithm hasher = new MD5CryptoServiceProvider();
            StringBuilder buffer = new StringBuilder();
            try
            {
                using (FileStream f = new FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.Read, 8192))
                {
                    hasher.ComputeHash(f);
                    byte[] hash = hasher.Hash;
                    foreach (byte bt in hash)
                    {
                        buffer.Append(string.Format("{0:x2}", bt));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return buffer.ToString();
        }
    }
}
