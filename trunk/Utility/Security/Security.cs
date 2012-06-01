using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Utility
{
    /// <summary>
    /// ��ȫ�����ࡣ
    /// </summary>
    public class Security
    {
        /// <summary>
        ///  RC2 ����Կ��
        /// </summary>
        private byte[] m_RC2Key;
        /// <summary>
        ///  RC2 �ı�������
        /// </summary>
        private RC2 m_RC2Provider;
        /// <summary>
        ///  �����������
        /// </summary>
        private static Random m_Random;
        /// <summary>
        /// ���캯����
        /// </summary>
        static Security()
        {
            m_Random = new Random();
        }
        /// <summary>
        /// ���캯����
        /// </summary>
        public Security()
        {
            m_RC2Key = Encoding.UTF8.GetBytes("ace");
            m_RC2Provider = new RC2CryptoServiceProvider();
        }
        /// <summary>
        /// ���캯����
        /// </summary>
        /// <param name="key">Կ�ס�</param>
        public Security(string key)
        {
            m_RC2Key = Encoding.UTF8.GetBytes(key);
            m_RC2Provider = new RC2CryptoServiceProvider();
        }
        /// <summary>
        /// ������ɳ�ʼ��������
        /// </summary>
        /// <returns>��ʼ�����������ַ�����</returns>
        private static byte[] GenerateIV()
        {
            byte[] buffer = new byte[8];
            m_Random.NextBytes(buffer);
            return buffer;
        }
        /// <summary>
        /// RC2 ���롣
        /// </summary>
        /// <param name="clear">�����ַ�����</param>
        /// <returns>�����ַ�����</returns>
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
        /// RC2 ���롣
        /// </summary>
        /// <param name="cipher">�����ַ�����</param>
        /// <returns>�����ַ�����</returns>
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
