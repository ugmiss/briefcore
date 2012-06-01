using System;
using System.Text;
using System.Security.Cryptography;

namespace Utility
{
    /// <summary>
    /// MD5�����ࡣ
    /// </summary>
    public class MD5
    {
        /// <summary>
        /// MD5�ı�������
        /// </summary>
        private static System.Security.Cryptography.MD5 m_MD5Provider;
        /// <summary>
        /// ��̬���캯������ʼ��������������
        /// </summary>
        static MD5()
        {
            m_MD5Provider = new MD5CryptoServiceProvider();
        }
        /// <summary>
        /// MD5���롣
        /// </summary>
        /// <param name="data">�������ַ������ݡ�</param>
        /// <returns>��������</returns>
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
        /// MD5���롣
        /// </summary>
        /// <param name="data">���������ݡ�</param>
        /// <returns>��������(16�ֽ�)</returns>
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
