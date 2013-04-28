using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System
{
    /// <summary>
    /// 字符串
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 字符串非空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool NotNullOrEmpty(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }
        /// <summary>
        /// base64加密
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string EnBase64(this string s)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(s));
        }
        /// <summary>
        /// base64解密
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string DeBase64(this string s)
        {
            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(s));
        }
        /// <summary>
        /// 字符串为空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
        /// <summary>
        /// 拆串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string[] Splitty(this string s)
        {
            return s.Split(new char[] { ',', '|', ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }
        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FirstLower(this string s)
        {
            if (s.IsNullOrEmpty()) return s;
            return s[0].ToString().ToLower() + s.Substring(1);
        }
        /// <summary>
        /// 首字母大写。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FirstUpper(this string s)
        {
            if (s.IsNullOrEmpty()) return s;
            return s[0].ToString().ToUpper() + s.Substring(1);
        }
        /// <summary>
        /// 正则匹配。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsMatch(this string s, string pattern)
        {
            if (s == null) return false;
            else return Regex.IsMatch(s, pattern);
        }
        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
        /// <summary>
        /// 正则匹配串。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string Match(this string s, string pattern)
        {
            if (s == null) return "";
            return Regex.Match(s, pattern).Value;
        }
        /// <summary>
        /// 前缀缩进
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="prefix"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static string TrimPrefix(this string sourceString, string prefix, bool ignoreCase)
        {
            prefix = prefix ?? string.Empty;
            if (!sourceString.StartsWith(prefix, ignoreCase, CultureInfo.CurrentCulture))
            {
                return sourceString;
            }
            return sourceString.Remove(0, prefix.Length);
        }
        /// <summary>
        /// 后缀缩进。
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="suffix"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static string TrimSuffix(this string sourceString, string suffix, bool ignoreCase)
        {
            suffix = suffix ?? string.Empty;
            if (!sourceString.EndsWith(suffix, ignoreCase, CultureInfo.CurrentCulture))
            {
                return sourceString;
            }
            return sourceString.Substring(0, sourceString.Length - suffix.Length);
        }
        /// <summary>
        /// 字符串空间长度。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int Length(this string input)
        {
            if (input.IsNullOrEmpty())
                return 0;
            int n = 0;
            foreach (char c in input)
            {
                if ((int)c > 256)
                    n += 2;
                else
                    n++;
            }
            return n;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Interception(this string input, int length)
        {
            return input.Interception(0, length);
        }
        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="path"></param>
        public static void WriteToFile(this string s, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                sw.Write(s);
            }
        }
        /// <summary>
        /// 写入UTF8格式文件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="path"></param>
        public static void WriteToFile8(this string s, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                sw.Write(s);
            }
        }
        /// <summary>
        /// 按固定编码写文件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        public static void WriteToFile(this string s, string path, Encoding encoding)
        {
            using (StreamWriter sw = new StreamWriter(path, false, encoding))
            {
                sw.Write(s);
            }
        }
        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadFromFile(this string s, string path)
        {
            if (File.Exists(path))
                using (StreamReader sr = new StreamReader(path))
                {
                    StringBuilder sb = new StringBuilder();
                    do
                    {
                        sb.AppendLine(sr.ReadLine());
                    }
                    while (!sr.EndOfStream);
                    return sb.ToString();
                }
            return string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Interception(this string input, int startIndex, int length)
        {
            if (input.IsNullOrEmpty()) return string.Empty;
            int len = input.Length;
            if (startIndex >= len) return string.Empty;

            bool flag = false;
            int i = startIndex, j = 0;
            for (; i < len; i++)
            {
                if (j >= length)
                {
                    flag = true;
                    break;
                }

                if ((int)input[i] > 256)
                    j += 2;
                else
                    j++;
            }
            string returnValue = input.Substring(startIndex, i - startIndex);
            if (flag)
                returnValue += "...";
            return returnValue;
        }
        /// <summary>
        /// 程序域根目录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string AppPath(this string input)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, input);
        }
        /// <summary>
        /// 命令行打印
        /// </summary>
        /// <param name="s"></param>
        public static void Line(this object s)
        {
            if (s == null)
                return;
            else
                Console.WriteLine(s.ToString());
        }
        /// <summary>
        /// MD5
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string MD5(this string input)
        {
            string returnValue = string.Empty;
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] resultBytes = md5.ComputeHash(inputBytes);
            foreach (byte b in resultBytes)
                returnValue += b.ToString("X").PadLeft(2, '0');
            md5.Clear();
            return returnValue;
        }
        /// <summary>
        /// 文件名截取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FileName(this string input)
        {
            int a = input.LastIndexOf('\\');
            int b = input.LastIndexOf('/');
            return input.Substring((a > b ? a : b) + 1);
        }
        /// <summary>
        /// 文件目录截取
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FilePath(this string input)
        {
            int a = input.LastIndexOf('\\');
            int b = input.LastIndexOf('/');
            return input.Substring(0, (a > b ? a : b) + 1);
        }
    }
    public static class TypeExtensions
    {
        /// <summary>
        /// 类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="convertibleValue"></param>
        /// <returns></returns>
        public static T ParseTo<T>(this IConvertible convertibleValue)
        {
            if (null == convertibleValue)
            {
                return default(T);
            }
            if (!typeof(T).IsGenericType)
            {
                try
                {
                    return (T)Convert.ChangeType(convertibleValue, typeof(T));
                }
                catch
                {
                    return default(T);
                }
            }
            else
            {
                Type genericTypeDefinition = typeof(T).GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(Nullable<>))
                {
                    return (T)Convert.ChangeType(convertibleValue, Nullable.GetUnderlyingType(typeof(T)));
                }
            }
            throw new InvalidCastException(string.Format("Invalid cast from type \"{0}\" to type \"{1}\".", convertibleValue.GetType().FullName, typeof(T).FullName));
        }
        public static int GetTypeSize<T>(this T t)
        {
            return typeof(T).IsValueType ? Marshal.SizeOf(default(T)) : IntPtr.Size;
        }
    }
    public static class DataExtensions
    {
        public static T GetModelByDataRow<T>(this DataRow data) where T : new()
        {
            T t = new T();
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo p in properties)
            {
                object[] objs = p.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (null != objs && objs.Length != 0)
                {
                    foreach (object obj in objs)
                    {
                        if ((obj is DescriptionAttribute) && (obj as DescriptionAttribute).Description.IndexOf("DataField") >= 0)
                        {
                            object value = data[p.Name] == DBNull.Value ? null : data[p.Name];
                            if (p.PropertyType.Name == "Byte[]")
                            {
                                byte[] value2 = data.Field<byte[]>(p.Name);
                                p.FastSetValue(t, value2);
                                continue;
                            }
                            p.FastSetValue(t, value);
                            break;
                        }
                    }
                }
            }
            return t;
        }
    }
    public static class EnumExtensions
    {
        public static Dictionary<string, int> EnumMap(this Type enumType)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();
            enumType.GetFields().ToList().ForEach(p =>
            {
                if (p.FieldType.IsEnum)
                {
                    object[] arr = p.GetCustomAttributes(typeof(DescriptionAttribute), true);
                    map.Add(arr.Length > 0 ? ((DescriptionAttribute)arr[0]).Description : p.Name, (int)enumType.InvokeMember(p.Name, BindingFlags.GetField, null, null, null));
                }
            });
            return map;
        }
        public static List<EnumObject> EnumList(this Type enumType)
        {
            List<EnumObject> list = new List<EnumObject>();
            Dictionary<string, int> map = new Dictionary<string, int>();
            enumType.GetFields().ToList().ForEach(p =>
            {
                if (p.FieldType.IsEnum)
                {
                    object[] arr = p.GetCustomAttributes(typeof(DescriptionAttribute), true);
                    EnumObject eo = new EnumObject();
                    eo.Text = arr.Length > 0 ? ((DescriptionAttribute)arr[0]).Description : p.Name;
                    eo.Value = (int)enumType.InvokeMember(p.Name, BindingFlags.GetField, null, null, null);
                    list.Add(eo);
                }
            });
            return list;
        }
        public static EnumObject GetEnumObject(this Type enumType, int Value)
        {
            EnumObject result = null;
            Dictionary<string, int> map = new Dictionary<string, int>();
            enumType.GetFields().ToList().ForEach(p =>
            {
                if (p.FieldType.IsEnum)
                {
                    object[] arr = p.GetCustomAttributes(typeof(DescriptionAttribute), true);
                    EnumObject eo = new EnumObject();
                    eo.Text = arr.Length > 0 ? ((DescriptionAttribute)arr[0]).Description : p.Name;
                    eo.Value = (int)enumType.InvokeMember(p.Name, BindingFlags.GetField, null, null, null);
                    if (eo.Value == Value)
                        result = eo;
                }
            });
            return result;
        }
    }
    public static class ListExtensions
    {
        public static Dictionary<TKey, TValue> TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key) == false) dict.Add(key, value);
            return dict;
        }
        public static Dictionary<TKey, TValue> AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            dict[key] = value;
            return dict;
        }
        public static bool In<T>(this T t, params T[] c)
        {
            return c.Any(i => i.Equals(t));
        }
        public static bool NotNull(this object obj)
        {
            if (obj is DBNull || obj == null)
                return false;
            else
                return true;
        }
        public static void AddTo(this IEnumerable data, List<object> tag)
        {
            foreach (var obj in data)
            {
                tag.Add(obj);
            }
        }
    }
    public static class LinqExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }
    }
    public static class XmlExtensions
    {
        /// <summary>
        /// xml序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string XmlSerialize<T>(this T obj)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            string xmlString = string.Empty;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                xmlSerializer.Serialize(ms, obj, ns);
                xmlString = Encoding.UTF8.GetString(ms.ToArray());
            }
            return xmlString;
        }
        /// <summary>
        /// xml反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static T XmlDeserialize<T>(this string xmlString)
        {
            T t = default(T);
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (Stream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
            {
                using (XmlReader xmlReader = XmlReader.Create(xmlStream))
                {
                    Object obj = xmlSerializer.Deserialize(xmlReader);
                    t = (T)obj;
                }
            }
            return t;
        }
        /// <summary>
        /// xml反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public static object XmlDeserialize(this string xmlString, Type typ)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer xmlSerializer = new XmlSerializer(typ);
            XmlReaderSettings settings = new XmlReaderSettings();
            try
            {
                using (Stream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
                {
                    using (XmlReader xmlReader = XmlReader.Create(xmlStream))
                    {
                        return xmlSerializer.Deserialize(xmlReader);
                    }
                }
            }
            catch
            {
                using (Stream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString.Replace("&#xB;", " "))))
                {
                    using (XmlReader xmlReader = XmlReader.Create(xmlStream))
                    {
                        return xmlSerializer.Deserialize(xmlReader);
                    }
                }
            }
        }
    }
    public static class ImageExtensions
    {
        /// <summary>
        /// 图片转byte[]
        /// </summary>
        /// <param name="imageIn"></param>
        /// <returns></returns>
        public static byte[] ImageToByteArray(this System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }
        /// <summary>
        ///byte[]转图片
        /// </summary>
        /// <param name="imageIn"></param>
        /// <returns></returns>
        public static Image ByteArrayToImage(this byte[] byteArrayIn)
        {
            if (byteArrayIn == null)
                return null;
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
    public class EnumObject
    {
        public int Value { get; set; }
        public string Text { get; set; }
        public override string ToString()
        {
            return Text;
        }
    }
    //public static class JsonExtensions
    //{
    //    public static string JsonSerialize<T>(this T obj)
    //    {
    //        return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
    //    }
    //    public static T JsonDeerialize<T>(this string obj)
    //    {
    //        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(obj);
    //    }
    //}
}
