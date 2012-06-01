using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace Utility
{
    /// <summary>
    /// ѹ����ѹ�ࡣ
    /// </summary>
    public class GZip
    {
        /// <summary>
        /// ѹ����
        /// </summary>
        /// <param name="data">��ѹ�����ݡ�</param>
        /// <returns>����ѹ�����ݡ�</returns>
        public static byte[] Compress(byte[] data)
        {
            MemoryStream ms = null;
            GZipStream compressedzipStream = null;
            try
            {
                ms = new MemoryStream();
                compressedzipStream = new GZipStream(ms, CompressionMode.Compress);
                compressedzipStream.Write(data, 0, data.Length);
                compressedzipStream.Close();
                return ms.ToArray();
            }
            finally
            {
                ms.Close();
            }
        }
        /// <summary>
        /// ��ѹ����
        /// </summary>
        /// <param name="data">����ѹ���ݡ�</param>
        /// <returns>���ؽ�ѹ���ݡ�</returns>
        public static byte[] Decompress(byte[] data)
        {
            MemoryStream ms = null;
            GZipStream zipStream = null;
            MemoryStream outms = null;
            try
            {
                ms = new MemoryStream(data);
                zipStream = new GZipStream(ms, CompressionMode.Decompress);
                byte[] zipBuffer = new byte[1024];
                outms = new MemoryStream();
                while (true)
                {
                    int nSize = zipStream.Read(zipBuffer, 0, zipBuffer.Length);
                    if (nSize > 0)
                    {
                        outms.Write(zipBuffer, 0, nSize);
                    }
                    else
                        break;
                }
                return outms.ToArray();
            }
            finally
            {
                zipStream.Close();
                ms.Close();
                outms.Close();
            }
        }
    }
}
