using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DataAccess
{
    public class Logic
    {
        public static void CreateDB(string conn, string dirpath)
        {
            string[] param = GetConnParams(conn);
            CreateDB(param[0], param[1], param[2], param[3], dirpath);
        }
        public static void TransData(string conn, string dirpath)
        {
            string[] param = GetConnParams(conn);
            TransData(param[0], param[1], param[2], param[3], dirpath);
        }
        public static void BackUp(string connstr, string dirpath)
        {
            string databasexml = BackUpStrut(connstr);
            if (!Directory.Exists(dirpath))
                Directory.CreateDirectory(dirpath);
            using (StreamWriter stream = new StreamWriter(dirpath + GetConnParams(connstr)[3] + ".database.xml", false, Encoding.UTF8))
            {
                stream.WriteLine(databasexml);
            }
            DataBase database = Serializer.XmlDeserialize<DataBase>(databasexml);
            BackUpData(database, connstr, dirpath);
        }

        static void CreateInExistDB(string conn, string dirpath)
        {
            string[] param = GetConnParams(conn);
            CreateInExistDB(param[0], param[1], param[2], param[3], dirpath);
        }

        static string BackUpStrut(string connstr)
        {
            // 构造数据库结构对象。
            DataBase database = new DataBase(string.Format("server={0};uid={1};pwd={2};database={3}", GetConnParams(connstr)));
            // 实例化数据库结构对象。
            database.Init(GetConnParams(connstr));
            return Serializer.XmlSerialize<DataBase>(database);
        }

        public static string[] VaryDataBase(string svr, string uid, string pwd, string dbname)
        {
            DataBase database = new DataBase(string.Format("server={0};uid={1};pwd={2};database={3}", svr, uid, pwd, dbname));
            database.Init(svr, uid, pwd, dbname);
            DataBase db = Logic.LoadDataBase(null);
            List<string> difflist = new List<string>();
            if (database.Tables.Count != db.Tables.Count)
                difflist.Add("表数目不同");
            foreach (Tab tab in database.Tables)
            {
                foreach (Tab tab2 in db.Tables)
                {
                    if (tab.TableName == tab2.TableName && tab.Definition != tab2.Definition)
                        difflist.Add(string.Format("表{0}结构不同", tab.TableName));
                }
            }
            return difflist.ToArray();
        }

        static void CreateDB(string svr, string uid, string pwd, string dbname, string dirpath)
        {
            DataBase db = Logic.LoadDataBase(dirpath);
            SqlExecuter Executer = new SqlExecuter(string.Format("server={0};uid={1};pwd={2};database={3}", svr, uid, pwd, "master"));
            DataRow dr = Executer.QueryRow("select * From master.dbo.sysdatabases where name={0}", dbname);
            if (dr != null)
            {
                CreateInExistDB(svr, uid, pwd, dbname, dirpath);
                return;
            }
            Executer.NonQuery(string.Format("create database [{0}]", dbname));
            Executer.NonQuery(string.Format("use {0} ", dbname));
            foreach (Tab tab in db.Tables)
            {
                Executer.NonQuery(tab.Definition);
            }
            foreach (Proc proc in db.Procs)
            {
                Executer.NonQuery(proc.Definition);
            }
            foreach (Func fun in db.Funcs)
            {
                Executer.NonQuery(fun.Definition);
            }
            foreach (Trigger tri in db.Triggers)
            {
                Executer.NonQuery(tri.Definition);
            }
            List<View> list = new List<View>(db.Views);
            CreateViews(0, list, Executer);
            foreach (ForeignKey fk in db.ForeignKeys)
            {
                Executer.NonQuery(fk.Definition);
            }
        }

        static void CreateInExistDB(string svr, string uid, string pwd, string dbname, string dirpath)
        {
            DataBase db = Logic.LoadDataBase(dirpath);
            SqlExecuter Executer = new SqlExecuter(string.Format("server={0};uid={1};pwd={2};database={3}", svr, uid, pwd, "master"));
            Executer.NonQuery(string.Format("use {0} ", dbname));
            foreach (Tab tab in db.Tables)
            {
                Executer.NonQuery(tab.Definition);
            }
            foreach (Proc proc in db.Procs)
            {
                Executer.NonQuery(proc.Definition);
            }
            foreach (Func fun in db.Funcs)
            {
                Executer.NonQuery(fun.Definition);
            }
            foreach (Trigger tri in db.Triggers)
            {
                Executer.NonQuery(tri.Definition);
            }
            List<View> list = new List<View>(db.Views);
            CreateViews(0, list, Executer);
            foreach (ForeignKey fk in db.ForeignKeys)
            {
                Executer.NonQuery(fk.Definition);
            }
        }

        static List<string> GetOrder(DataBase db)
        {
            List<string> list = new List<string>();
            DirectGraph graph = new DirectGraph();
            foreach (ForeignKey fk in db.ForeignKeys)
            {
                graph.AddE(fk.PK_Tab_Name, fk.PK_Tab_Name);
            }
            string[] tablenames = DirectGraph.GetTopoListNames(graph, null);
            list.AddRange(tablenames);

            foreach (Tab tab in db.Tables)
            {
                if (!list.Contains(tab.TableName))
                {
                    list.Add(tab.TableName);
                }
            }
            return list;
        }

        static void BackUpData(DataBase database, string connstr, string dirpath)
        {
            Data data = new Data();
            // 创建数据库访问对象。
            SqlExecuter Executer = new SqlExecuter(string.Format("server={0};uid={1};pwd={2};database={3}", GetConnParams(connstr)));
            // 根据关联查表。
            var q = from c in database.Tables select c.TableName;
            foreach (string tablename in q.ToArray())
            {
                DataTable dt = GetByName(tablename, database);
                string sql = string.Format("select * from {0}", tablename);
                foreach (DataRow d in Executer.QueryTable(sql).Rows)
                {
                    DataRow dr = dt.NewRow();
                    foreach (DataColumn col in dt.Columns)
                    {
                        dr[col.ColumnName] = d[col.ColumnName];
                    }
                    dt.Rows.Add(dr);
                }
                data.Save(tablename, dt, dirpath);
            }
        }

        static void CreateViews(int i, List<View> vlist, SqlExecuter Executer)
        {
            if (vlist.Count == 0) return;
            try
            {
                Executer.NonQuery(vlist[i].Definition);
                vlist.Remove(vlist[i]);
                if (vlist.Count > 0)
                {
                    CreateViews(0, vlist, Executer);
                }
            }
            catch
            {
                i = i + 1;
                if (i >= vlist.Count) i = 0;
                CreateViews(i, vlist, Executer);
            }
        }

        static object ConvertT(object obj, SqlDbType sqlDbType)
        {
            switch (sqlDbType)
            {
                case SqlDbType.BigInt:
                    return obj;
                case SqlDbType.Binary:
                case SqlDbType.Image:
                case SqlDbType.Timestamp:
                case SqlDbType.VarBinary:
                    return obj;
                case SqlDbType.Bit:
                    return obj;
                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.VarChar:
                    return obj;
                case SqlDbType.DateTime:
                case SqlDbType.SmallDateTime:
                case SqlDbType.Date:
                case SqlDbType.DateTime2:
                    return Convert.ToDateTime(obj);
                case SqlDbType.Decimal:
                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    return obj;
                case SqlDbType.Float:
                    return obj;
                case SqlDbType.Int:
                    return obj;
                case SqlDbType.Real:
                    return typeof(float);
                case SqlDbType.UniqueIdentifier:
                    return obj;
                case SqlDbType.SmallInt:
                    return obj;
                case SqlDbType.TinyInt:
                    return obj;
                case SqlDbType.Xml:
                    return obj;
                case SqlDbType.Time:
                    return obj;
                case SqlDbType.DateTimeOffset:
                    return obj;
            }
            return obj;
        }

        internal static Type GetClosestRuntimeType(SqlDbType sqlDbType)
        {
            switch (sqlDbType)
            {
                case SqlDbType.BigInt:
                    return typeof(long);
                case SqlDbType.Binary:
                case SqlDbType.Image:
                case SqlDbType.Timestamp:
                case SqlDbType.VarBinary:
                    return typeof(System.Byte[]);
                case SqlDbType.Bit:
                    return typeof(bool);
                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.VarChar:
                    return typeof(string);
                case SqlDbType.DateTime:
                case SqlDbType.SmallDateTime:
                case SqlDbType.Date:
                case SqlDbType.DateTime2:
                    return typeof(DateTime);
                case SqlDbType.Decimal:
                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    return typeof(decimal);
                case SqlDbType.Float:
                    return typeof(double);
                case SqlDbType.Int:
                    return typeof(int);
                case SqlDbType.Real:
                    return typeof(float);
                case SqlDbType.UniqueIdentifier:
                    return typeof(Guid);
                case SqlDbType.SmallInt:
                    return typeof(short);
                case SqlDbType.TinyInt:
                    return typeof(byte);
                case SqlDbType.Xml:
                    return typeof(XElement);
                case SqlDbType.Time:
                    return typeof(TimeSpan);
                case SqlDbType.DateTimeOffset:
                    return typeof(DateTimeOffset);
            }
            return typeof(object);
        }

        internal static SqlDbType Parse(string stype)
        {
            stype = stype.ToUpper(CultureInfo.InvariantCulture).Replace("NOT NULL", "");
            stype = stype.ToUpper(CultureInfo.InvariantCulture).Replace("NULL", "");
            string strA = null;
            int index = stype.IndexOf('(');
            int num2 = stype.IndexOf(' ');
            int length = ((index != -1) && (num2 != -1)) ? Math.Min(num2, index) : ((index != -1) ? index : ((num2 != -1) ? num2 : -1));
            if (length == -1)
            {
                strA = stype;
                length = stype.Length;
            }
            else
            {
                strA = stype.Substring(0, length);
            }
            int startIndex = length;
            if ((startIndex < stype.Length) && (stype[startIndex] == '('))
            {
                startIndex++;
                length = stype.IndexOf(',', startIndex);
                if (length > 0)
                {
                    startIndex = length + 1;
                    length = stype.IndexOf(')', startIndex);
                }
                else
                {
                    length = stype.IndexOf(')', startIndex);
                }
                startIndex = length++;
            }
            if (string.Compare(strA, "rowversion", StringComparison.OrdinalIgnoreCase) == 0)
            {
                strA = "Timestamp";
            }
            if (string.Compare(strA, "numeric", StringComparison.OrdinalIgnoreCase) == 0)
            {
                strA = "Decimal";
            }
            if (string.Compare(strA, "sql_variant", StringComparison.OrdinalIgnoreCase) == 0)
            {
                strA = "Variant";
            }
            if (string.Compare(strA, "filestream", StringComparison.OrdinalIgnoreCase) == 0)
            {
                strA = "Binary";
            }
            SqlDbType type = (SqlDbType)Enum.Parse(typeof(SqlDbType), strA, true);
            switch (type)
            {
                case SqlDbType.Binary:
                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NVarChar:
                    return type;
                case SqlDbType.Bit:
                case SqlDbType.DateTime:
                case SqlDbType.Image:
                case SqlDbType.Int:
                case SqlDbType.Money:
                case SqlDbType.NText:
                    return type;
                case SqlDbType.Decimal:
                case SqlDbType.Float:
                case SqlDbType.Real:
                    return type;
                case SqlDbType.Timestamp:
                case SqlDbType.TinyInt:
                    return type;
                case SqlDbType.VarBinary:
                case SqlDbType.VarChar:
                    return type;
            }
            return type;
        }

        public static string GetConnStr(string server, string uid, string pwd, string database)
        {
            return string.Format("server={0};uid={1};pwd={2};database={3}", server, uid, pwd, database);
        }

        public static string[] GetConnParams(string connstring)
        {
            string[] paras = connstring.Split(';');
            string server = null;
            string uid = null;
            string pwd = null;
            string database = null;
            foreach (string p in paras)
            {
                if (p.StartsWith("server=")) server = p.Replace("server=", "");
                if (p.StartsWith("pwd=")) pwd = p.Replace("pwd=", "");
                if (p.StartsWith("uid=")) uid = p.Replace("uid=", "");
                if (p.StartsWith("database=")) database = p.Replace("database=", "");
            }
            return new string[] { server, uid, pwd, database };
        }

        static DataBase LoadDataBase(string dirpath)
        {
            var q = from c in Directory.GetFiles(dirpath) where c.EndsWith("database.xml") select c;
            string filepath = q.ToArray()[0];
            using (StreamReader sr = new StreamReader(filepath))
            {
                string database = sr.ReadToEnd();
                return Serializer.XmlDeserialize<DataBase>(database);
            }
        }

        static Data LoadData(string dirpath, DataBase database)
        {
            Data data = new Data();
            var q = from c in database.Tables select c.TableName;
            data.Load(dirpath, q.ToArray());
            return data;
        }

        static DataTable GetByName(string tbname, DataBase db)
        {
            foreach (Tab table in db.Tables)
            {
                if (table.TableName == tbname)
                {
                    DataTable dt = new DataTable();
                    foreach (Column col in table.Columns)
                    {
                        DataColumn dcol = new DataColumn();
                        dcol.ColumnName = col.ColName;
                        dcol.DataType = GetClosestRuntimeType(Parse(col.TypeName));
                        dcol.AutoIncrement = col.Is_Identity;
                        dt.Columns.Add(dcol);
                    }
                    return dt;
                }
            }
            return null;
        }

        static void TransData(string svr, string uid, string pwd, string dbname, string dirpath)
        {
            SqlExecuter Executer = new SqlExecuter(string.Format("server={0};uid={1};pwd={2};database={3}", svr, uid, pwd, dbname));
            DataBase db = Logic.LoadDataBase(dirpath);
            List<string> order = GetOrder(db);
            Data data = new Data();
            data.Load(dirpath, order.ToArray());

            foreach (string key in order)
            {
                Column[] tabcols = db.GetCols(key);
                DataTable dt = data.Get(key);
                if (dt == null) continue;
                string para = "";
                string val = "";
                List<DataColumn> cols = new List<DataColumn>();
                int inx = 0;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].AutoIncrement) continue;
                    cols.Add(dt.Columns[i]);
                    para += ("[" + dt.Columns[i].ColumnName + "]" + ",");
                    val += "{" + inx + "},";
                    inx++;
                }
                para = para.Substring(0, para.Length - 1);
                val = val.Substring(0, val.Length - 1);
                var qq = from c in tabcols where c.Is_Identity == true select c;
                foreach (DataRow dr in dt.Rows)
                {
                    List<object> items = new List<object>();
                    foreach (DataColumn col in cols)
                    {
                        object obj = dr[col];
                        foreach (Column c in tabcols)
                        {
                            if (c.ColName == col.ColumnName)
                            {
                                if (!(obj is DBNull))
                                    obj = ConvertT(obj, Parse(c.TypeName));
                            }
                        }
                        items.Add(obj);
                    }
                    Executer.NonQuery("insert into " + key + "(" + para + ") values(" + val + ")", items.ToArray());
                }
            }
        }
    }
}
