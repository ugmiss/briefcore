using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Utility
{
    /// <summary>
    /// 安全处理类。
    /// </summary>
    public class Security
    {
        /// <summary>
        ///  RC2 的密钥。
        /// </summary>
        private byte[] m_RC2Key;
        /// <summary>
        ///  RC2 的编码器。
        /// </summary>
        private RC2 m_RC2Provider;
        /// <summary>
        ///  随机发生器。
        /// </summary>
        private static Random m_Random;
        /// <summary>
        /// 构造函数。
        /// </summary>
        static Security()
        {
            m_Random = new Random();
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        public Security()
        {
            m_RC2Key = Encoding.UTF8.GetBytes("ace");
            m_RC2Provider = new RC2CryptoServiceProvider();
        }
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="key">钥匙。</param>
        public Security(string key)
        {
            m_RC2Key = Encoding.UTF8.GetBytes(key);
            m_RC2Provider = new RC2CryptoServiceProvider();
        }
        /// <summary>
        /// 随机生成初始化向量。
        /// </summary>
        /// <returns>初始化向量（四字符）。</returns>
        private static byte[] GenerateIV()
        {
            byte[] buffer = new byte[8];
            m_Random.NextBytes(buffer);
            return buffer;
        }
        /// <summary>
        /// RC2 编码。
        /// </summary>
        /// <param name="clear">明文字符串。</param>
        /// <returns>密文字符串。</returns>
        public string RC2Encode(string clear)
        {
            if (clear == null || clear.Length == 0)
            {
                return null;
            }
            else
            {
                m_RC2Provider.Clear();
                byte[] vector = GenerateIV();
                byte[] input = Encoding.UTF8.GetBytes(clear);
                byte[] output = m_RC2Provider.CreateEncryptor(m_RC2Key, vector).TransformFinalBlock(input, 0, input.Length);
                byte[] result = new byte[vector.Length + output.Length];
                Array.Copy(vector, 0, result, 0, vector.Length);
                Array.Copy(output, 0, result, vector.Length, output.Length);
                return Convert.ToBase64String(result);
            }
        }
        /// <summary>
        /// RC2 解码。
        /// </summary>
        /// <param name="cipher">密文字符串。</param>
        /// <returns>明文字符串。</returns>
        public string RC2Decode(string cipher)
        {
            if (cipher == null || cipher.Length == 0)
            {
                return null;
            }
            else
            {
                m_RC2Provider.Clear();
                byte[] result = Convert.FromBase64String(cipher);
                byte[] vector = new byte[8];
                Array.Copy(result, 0, vector, 0, vector.Length);
                byte[] input = new byte[result.Length - vector.Length];
                Array.Copy(result, vector.Length, input, 0, result.Length - vector.Length);
                byte[] output = m_RC2Provider.CreateDecryptor(m_RC2Key, vector).TransformFinalBlock(input, 0, input.Length);
                return Encoding.UTF8.GetString(output);
            }
        }
    }
}
