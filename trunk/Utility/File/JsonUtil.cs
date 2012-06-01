using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;

namespace Utility
{
    public class JsonUtil
    {
        static JavaScriptSerializer serializer = new JavaScriptSerializer();
        /// <summary>
        /// 生成json类。
        /// </summary>
        /// <param name="url"></param>
        public static void GenerateClass(string url)
        {
            string res = HttpHelper.HttpGet(url,null);
            object[] arr = GetArray(res);
            Dictionary<string, object> classarr = arr[0] as Dictionary<string, object>;
            string _namepace = "Utility";

            StreamWriter streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/Class.cs", false, Encoding.Default);
            streamWriter.WriteLine("using System;");
            streamWriter.WriteLine("using System.Data;");
            streamWriter.WriteLine("using System.Collections.Generic;");
            streamWriter.WriteLine();
            streamWriter.WriteLine("namespace " + _namepace);
            streamWriter.WriteLine("{");
            streamWriter.WriteLine("    public class Class");
            streamWriter.WriteLine("    {");
            foreach (string key in classarr.Keys)
            {
                streamWriter.WriteLine("        private string _" + key + ";");
                streamWriter.WriteLine("        public string " + key[0].ToString().ToUpper() + key.Substring(1));
                streamWriter.WriteLine("        {");
                streamWriter.WriteLine("            get { return  _" + key + "; }");
                streamWriter.WriteLine("            set { _" + key + " = value; }");
                streamWriter.WriteLine("        }");
            }
            streamWriter.WriteLine("         public Class(Dictionary<string, object> map)");
            streamWriter.WriteLine("         {");
            foreach (string key in classarr.Keys)
            {
                streamWriter.WriteLine("                " + key[0].ToString().ToUpper() + key.Substring(1) + " = map[\"" + key + "\"].ToString();");
            }
            streamWriter.WriteLine("         }");
            streamWriter.WriteLine("    }");
            streamWriter.WriteLine("}");
            streamWriter.Flush();
            streamWriter.Close();
        }
        /// <summary>
        /// 转json串成对象数组。
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static object[] GetArray(string json)
        {
            return (object[])(serializer.DeserializeObject(json));
        }
        /// <summary>
        /// 转成网页形式。
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static string ConvertUriCode(string x)
        {
            Byte[] textByte = System.Text.Encoding.GetEncoding("utf-8").GetBytes(x);
            StringBuilder Text = new StringBuilder();
            for (int j = 0; j < textByte.Length; j++)
            {
                char textChar = Convert.ToChar(int.Parse(textByte[j].ToString()));
                Text.Append(System.Uri.HexEscape(textChar));
            }
            return Text.ToString();
        }
    }
}
