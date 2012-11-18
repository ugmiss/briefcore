using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;
using AceCodeMain.Object;
using System.IO;

namespace AceCodeMain
{
    /// <summary>
    /// 生成逻辑。
    /// </summary>
    public class Gen
    {
        
        public const string ss = "ss";
        static string dir = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 串处理。
        /// </summary>
        /// <param name="code"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GenProcess(string code, TableClass table)
        {
            code = code.Replace("[[class]]", table.ClassnameBig);
            code = code.Replace("[(class)]", table.ClassnameSmall);
            code = code.Replace("[[classtxt]]", table.Classtext);
            code = code.Replace("[[pkey]]", table.PkeySmall);
            code = code.Replace("[[pkeybig]]", FirstBig(table.PkeySmall));
            code = code.Replace("[[propcount]]", table.ProplistBig.Count.ToString());
            while (code.IndexOf("[#") != -1)
            {
                string tempstring = "";
                int x = code.IndexOf("[#");
                int y = code.IndexOf("#]");
                string tempfor = code.Substring(x + 2, y - x - 2);
                for (int i = 0; i < table.ProplistBig.Count; i++)
                {
                    string temp = tempfor;
                    temp = temp.Replace("[[prop]]", table.ProplistBig[i]);
                    if (table.ProplistSmall.Count > 0)
                    {
                        temp = temp.Replace("([prop])", table.Proplist[i]);
                        temp = temp.Replace("[(prop)]", table.ProplistSmall[i]);
                        temp = temp.Replace("[[proptype]]", table.ProplistType[i]);
                        temp = temp.Replace("[[proptxt]]", table.Proplisttext[i]);
                        temp = temp.Replace("[[propconvert]]", table.ProplistConvert[i]);
                        temp = temp.Replace("@propN@", i.ToString());
                    }
                    tempstring += temp;
                }
                code = code.Remove(x, y - x + 2);
                code = code.Insert(x, tempstring);
            }

            while (code.IndexOf("[*") != -1)
            {
                string tempstring = "";
                int x = code.IndexOf("[*");
                int y = code.IndexOf("*]");
                string tempfor = code.Substring(x + 2, y - x - 2);
                for (int i = 0; i < table.ProplistBig.Count; i++)
                {
                    string temp = tempfor;
                    temp = temp.Replace("[[prop]]", table.ProplistBig[i]);
                    if (table.ProplistSmall.Count > 0)
                    {
                        temp = temp.Replace("[(prop)]", table.ProplistSmall[i]);
                        temp = temp.Replace("[[proptype]]", table.ProplistType[i]);
                        temp = temp.Replace("[[proptxt]]", table.Proplisttext[i]);
                        temp = temp.Replace("[[propconvert]]", table.ProplistConvert[i]);
                        temp = temp.Replace("@propN@", i.ToString());
                    }
                    tempstring += temp;
                }
                code = code.Remove(x, y - x + 2);
                code = code.Insert(x, tempstring.Substring(0, tempstring.Length - 1));
            }
            return code;
        }
        /// <summary>
        /// 对象层
        /// </summary>
        /// <param name="table"></param>
        public static void GenObject(TableClass table)
        {
            StreamReader reader = new StreamReader(dir + "ModelPad/BusinessObject.pad");
            string code = reader.ReadToEnd();
            code = GenProcess(code, table);
            StreamWriter writer = new StreamWriter(dir + "Project/BusinessObject/" + table.ClassnameBig + ".cs", false, Encoding.Default);
            writer.Write(code);
            writer.Close();
        }

        /// <summary>
        /// 对象层
        /// </summary>
        /// <param name="table"></param>
        public static string GenConst(TableClass table)
        {
            StreamReader reader = new StreamReader(dir + "ModelPad/ConstObject.pad");
            string code = reader.ReadToEnd();
            code = GenProcess(code, table);
            return code;
            //StreamWriter writer = new StreamWriter(dir + "Project/BusinessObject/" + table.ClassnameBig + ".cs", false, Encoding.Default);
            //writer.Write(code);
            //writer.Close();
        }

        /// <summary>
        /// 逻辑层
        /// </summary>
        /// <param name="table"></param>
        public static void GenLogic(TableClass table)
        {
            StreamReader reader = new StreamReader(dir + "ModelPad/BusinessLogic.pad");
            string code = reader.ReadToEnd();
            code = GenProcess(code, table);
            StreamWriter writer = new StreamWriter(dir + "Project/BusinessLogic/" + table.ClassnameBig + "Service.cs", false, Encoding.Default);
            writer.Write(code);
            writer.Close();
        }
        /// <summary>
        /// 逻辑层
        /// </summary>
        /// <param name="table"></param>
        public static void GenUserLogic(TableClass table,string uid,string upass)
        {
            StreamReader reader = new StreamReader(dir + "ModelPad/UserBusinessLogic.pad");
            string code = reader.ReadToEnd();
            code = GenProcess(code, table);
            code = code.Replace("[[uid]]", FirstBig(uid));
            code = code.Replace("[[upass]]", FirstBig(upass));
            code = code.Replace("[(uid)]", FirstSmall(uid));
            code = code.Replace("[(upass)]", FirstSmall(upass));
            StreamWriter writer = new StreamWriter(dir + "Project/BusinessLogic/" + table.ClassnameBig + "Service.cs", false, Encoding.Default);
            writer.Write(code);
            writer.Close();
        }
        
        /// <summary>
        /// 接口层。
        /// </summary>
        /// <param name="table"></param>
        public static void GenInterface(TableClass table)
        {

            StreamReader reader = new StreamReader(dir + "ModelPad/IBusinessLogic.pad");
            string code = reader.ReadToEnd();
            code = GenProcess(code, table);
            StreamWriter writer = new StreamWriter(dir + "Project/IBusinessLogic/I" + table.ClassnameBig + "Service.cs", false, Encoding.Default);
            writer.Write(code);
            writer.Close();
        }
        /// <summary>
        /// 接口层。
        /// </summary>
        /// <param name="table"></param>
        public static void GenUserInterface(TableClass table)
        {

            StreamReader reader = new StreamReader(dir + "ModelPad/IUserBusinessLogic.pad");
            string code = reader.ReadToEnd();
            code = GenProcess(code, table);
            StreamWriter writer = new StreamWriter(dir + "Project/IBusinessLogic/I" + table.ClassnameBig + "Service.cs", false, Encoding.Default);
            writer.Write(code);
            writer.Close();
        }
        /// <summary>
        /// 适配层。
        /// </summary>
        /// <param name="table"></param>
        public static void GenAdapter(TableClass table)
        {
            StreamReader reader = new StreamReader(dir + "ModelPad/Proxy.pad");
            string code = reader.ReadToEnd();
            code = GenProcess(code, table);
            StreamWriter writer = new StreamWriter(dir + "Project/Adapter/Proxy.cs", false, Encoding.Default);
            writer.Write(code);
            writer.Close();
        }

        /// <summary>
        /// 适配层。
        /// </summary>
        /// <param name="table"></param>
        public static void GenFactory(TableClass table)
        {
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            StreamReader reader = new StreamReader(dir + "ModelPad/Factory.pad");
            string code = reader.ReadToEnd();
            code = GenProcess(code, table);
            StreamWriter writer = new StreamWriter(dir + "Project/Factory.cs", false, Encoding.Default);
            writer.Write(code);
            writer.Close();
            reader = new StreamReader(dir + "ModelPad/Environment.cs.pad");
            code = reader.ReadToEnd();
            code = GenProcess(code, table);
            writer = new StreamWriter(dir + "Project/Environment.cs", false, Encoding.Default);
            writer.Write(code);
            writer.Close();
        }
        /// <summary>
        /// 固定文件。
        /// </summary>
        /// <param name="conn"></param>
        public static void GenFix(string conn)
        {
            StreamReader reader;
            StreamWriter writer;
            string code;

            reader = new StreamReader(dir + "ModelPad/Fix/BasicLogic.cs.pad");
            code = reader.ReadToEnd();
            writer = new StreamWriter(dir + "Project/BusinessLogic/BasicLogic.cs", false, Encoding.Default);
            writer.Write(code);
            writer.Close();

            reader = new StreamReader(dir + "ModelPad/Fix/Configuration.cs.pad");
            code = reader.ReadToEnd();
            code = code.Replace("[[conn]]", conn);
            writer = new StreamWriter(dir + "Project/BusinessLogic/Configuration.cs", false, Encoding.Default);
            writer.Write(code);
            writer.Close();

            reader = new StreamReader(dir + "ModelPad/Fix/SqlExecuter.cs.pad");
            code = reader.ReadToEnd();
            writer = new StreamWriter(dir + "Project/DataAccess/SqlExecuter.cs", false, Encoding.Default);
            writer.Write(code);
            writer.Close();
        }
        /// <summary>
        /// 首字大写，
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static string FirstBig(string str)
        {
            if (str == null) return null;
            if (str.Length == 1) return str.ToUpper();
            return str.Substring(0, 1).ToUpper() + str.Substring(1, str.Length - 1);
        }
        /// <summary>
        /// 首字小写，
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static string FirstSmall(string str)
        {
            if (str.Length == 1) return str.ToLower();
            return str.Substring(0, 1).ToLower() + str.Substring(1, str.Length - 1);
        }
    }
}
