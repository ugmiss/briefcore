using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Collections.Specialized;

namespace DataAccess
{
    public class ScriptManager
    {
        public static string GetTableScript(string svr, string uid, string pwd, string databaseName, string tableName)
        {
            Server server = new Server(new ServerConnection(svr, uid, pwd));
            var srcDb = server.Databases[databaseName];
            Table table = srcDb.Tables[tableName.Replace("[", "").Replace("]", "")];
            if (table == null) return "";
            Scripter a = new Scripter(server);
            a.Options.Add(ScriptOption.ContinueScriptingOnError);//容错
            a.Options.Add(ScriptOption.ConvertUserDefinedDataTypesToBaseType);//类型转换
            a.Options.Add(ScriptOption.IncludeIfNotExists);//包含ifnotexists
            a.Options.Add(ScriptOption.DriAll);//添加所有
            a.Options.Add(ScriptOption.DriDefaults);//添加所有
            a.Options.Add(ScriptOption.DriAllConstraints);//添加约束
            a.Options.Add(ScriptOption.DriChecks);//添加检查约束
            a.Options.Add(ScriptOption.DriClustered);//添加聚集索引
            a.Options.Add(ScriptOption.DriIncludeSystemNames);//添加系统名
            a.Options.Add(ScriptOption.DriIndexes);//添加添加索引
            a.Options.Add(ScriptOption.DriNonClustered);//添加非聚集索引
            a.Options.Add(ScriptOption.DriPrimaryKey);//添加主键
            a.Options.Add(ScriptOption.DriUniqueKeys);//添加唯一
            a.Options.Add(ScriptOption.ExtendedProperties);//扩展属性
            UrnCollection collection = new UrnCollection();
            collection.Add(table.Urn);
            string str = "";
            foreach (string s in a.Script(collection))
            {
                str += s + "\r\n";
            }
            return str;
        }

        public static string GetViewScript(string svr, string uid, string pwd, string databaseName, string tableName)
        {
            Server server = new Server(new ServerConnection(svr, uid, pwd));
            var srcDb = server.Databases[databaseName];
            Microsoft.SqlServer.Management.Smo.View table = srcDb.Views[tableName];
            Scripter a = new Scripter(server);
            UrnCollection collection = new UrnCollection();
            collection.Add(table.Urn);
            string str = "";
            foreach (string s in a.Script(collection))
            {
                str += s + "\r\n";
            }
            return str.Substring(str.IndexOf("CREATE", System.StringComparison.OrdinalIgnoreCase));
        }
    }
}





