using System;
using System.Text;
using System.Security.Cryptography;

namespace Utility
{
    /// <summary>
    /// MD5编码类。
    /// </summary>
    public class MD5
    {
        /// <summary>
        /// MD5的编码器。
        /// </summary>
        private static System.Security.Cryptography.MD5 m_MD5Provider;
        /// <summary>
        /// 静态构造函数，初始化各个编码器。
        /// </summary>
        static MD5()
        {
            m_MD5Provider = new MD5CryptoServiceProvider();
        }
        /// <summary>
        /// MD5编码。
        /// </summary>
        /// <param name="data">待编码字符串数据。</param>
        /// <returns>编码结果。</returns>
        public static string Encode(string data)
        {
            if (data == null || data.Length == 0)
                return null;
            else
            {
                byte[] input = Encoding.Default.GetBytes(data);
                m_MD5Provider.Initialize();
                return Convert.ToBase64String(m_MD5Provider.ComputeHash(input, 0, input.Length));
            }
        }
        /// <summary>
        /// MD5编码。
        /// </summary>
        /// <param name="data">待编码数据。</param>
        /// <returns>编码结果。(16字节)</returns>
        public static byte[] Encode(byte[] data)
        {
            if (data == null || data.Length == 0)
                return null;
            else
            {
                m_MD5Provider.Initialize();
                return m_MD5Provider.ComputeHash(data, 0, data.Length);
            }
        }
    }
}
