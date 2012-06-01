using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data;
using System.Reflection;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Common;
using System.IO;

namespace System
{
    public class Logic : SqlExecuter
    {
        #region 属性和构造

        public string ConnnectString { get; set; }
        public string ConnstringKey { get; set; }
        // 构造方法。
        public Logic(string connstr)
            : base(connstr)
        {
            ConnnectString = connstr;
        }
        #endregion
        // 查询所有。
        public List<T> GetAll<T>() where T : new()
        {
            List<T> list = new List<T>();
            List<string> fields = GetFieldNames<T>();
            string tablename = GetTableName<T>();
            string sql = string.Format("select {0} from {1}", string.Join(",", fields.ToArray()), tablename);
            SqlDataReader reader = QueryDataReader(sql);
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();
            while (reader.Read())
            {
                T t = new T();//Activator.CreateInstance<T>();
                for (int i = 0; i < properties.Length; i++)
                {
                    object o = reader[properties[i].Name];
                    if (o is DBNull)
                    {
                        continue;
                    }
                    else if (o is Guid)
                    {
                        properties[i].FastSetValue(t, o.ToString());
                    }
                    else
                    {
                        properties[i].FastSetValue(t, o);
                    }
                }
                list.Add(t);
            }
            reader.Dispose();
            return list;

            //foreach (DataRow dr in QueryDataTable(sql).Rows)
            //{
            //    list.Add(dr.GetModelByDataRow<T>());
            //}
            //return list;
        }
        // 分页查询。
        public List<T> GetAll<T>(string whereStr, int maxcount, int pagenumber, params string[] rowfields) where T : new()
        {
            List<T> list = new List<T>();
            List<string> fields = GetFieldNames<T>();
            string tablename = GetTableName<T>();
            List<string> pks = GetPrimaryKeyNames<T>();
            string rowfield = null;
            if (rowfields.Length == 0 || rowfields[0].IsEmpty() || !fields.Contains(rowfields[0].Split(' ')[0]))
            {
                if (pks.Count > 0)
                    rowfield = pks[0];
                else
                    rowfield = fields[0];
            }
            else
            {
                rowfield = rowfields[0];
            }
            int start = (pagenumber - 1) * maxcount;
            int end = pagenumber * maxcount + 1;
            string sql = string.Format("select {0} from (select row_number()over(order by {1})rownumber,{0} from {2}) a where (rownumber>{3} and rownumber<{4})", string.Join(",", fields.ToArray()), rowfield, tablename, start, end);
            if (!string.IsNullOrEmpty(whereStr))
            {
                sql += " and " + whereStr;
            }
            foreach (DataRow dr in QueryDataTable(sql).Rows)
            {
                list.Add(dr.GetModelByDataRow<T>());
            }
            return list;
        }
        // 条件查询。
        public List<T> GetAll<T>(Expression<Func<T, bool>> func) where T : new()
        {
            List<T> list = new List<T>();
            string where = string.Empty;
            if (func.Body is BinaryExpression)
            {
                BinaryExpression be = ((BinaryExpression)func.Body);
                where = "where " + BinarExpressionProvider(be.Left, be.Right, be.NodeType);
            }
            string sql = "select {0} from {1} {2}".FormatWith(string.Join(",", GetFieldNames<T>().ToArray()), GetTableName<T>(), where);

            foreach (DataRow dr in QueryDataTable(sql).Rows)
            {
                list.Add(dr.GetModelByDataRow<T>());
            }
            return list;
        }
        // 单例条件查询。
        public T GetSingle<T>(Expression<Func<T, bool>> func) where T : new()
        {
            string where = string.Empty;
            if (func.Body is BinaryExpression)
            {
                BinaryExpression be = ((BinaryExpression)func.Body);
                where = "where " + BinarExpressionProvider(be.Left, be.Right, be.NodeType);
            }
            string sql = "select top 1 {0} from {1} {2}".FormatWith(string.Join(",", GetFieldNames<T>().ToArray()), GetTableName<T>(), where);

            DataRow dr = QueryDataRow(sql);
            if (dr != null)
                return dr.GetModelByDataRow<T>();
            else
                return default(T);
        }
        // 按主键查询。
        public T GetByPrimaryKey<T>(T model) where T : new()
        {
            string tablename = GetTableName<T>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            List<string> pks = GetPrimaryKeyNames<T>();
            List<string> sets = new List<string>();
            List<string> wheres = new List<string>();
            int x = 0;
            List<object> paramList = new List<object>();
            foreach (PropertyInfo p in properties)
            {
                //是主键的
                if (pks.Contains(p.Name))
                {
                    object paramValue = p.FastGetValue(model);
                    paramList.Add(paramValue);
                    wheres.Add(p.Name + " = {" + x + "}");
                    x++;
                }
            }
            if (wheres.Count == 0)
            {
                throw new Exception("没有设置主键。");
            }
            string sql = "select {0} from {1} where {2}".FormatWith(string.Join(",", GetFieldNames<T>().ToArray()), GetTableName<T>(), string.Join(" and ", wheres.ToArray()));
            DataRow dr = this.QueryDataRow(this.ParseCommandString(sql, paramList.ToArray()));
            if (dr != null)
            {
                return dr.GetModelByDataRow<T>();
            }
            else
            {
                return default(T);
            }
        }
        // 添加单例。
        public void Add<T>(T model)
        {
            if (!IsTable(typeof(T)))
            {
                throw new Exception("只能对表进行该操作");
            }

            List<string> fields = GetFieldNames<T>();
            List<string> values = new List<string>();
            int x = 0;
            List<object> paramList = new List<object>();
            foreach (PropertyInfo p in typeof(T).GetProperties())
            {
                object paramValue = p.FastGetValue(model);
                paramList.Add(paramValue);
                values.Add("{" + x + "}");
                x++;
            }
            string sql = string.Format("insert into {0}({1}) values({2})", GetTableName<T>(), string.Join(",", fields.ToArray()), string.Join(",", values.ToArray()));
            this.ExecuteNonQuery(ParseCommandString(sql, paramList.ToArray()));
        }
        // 删除单例。
        public void Delete<T>(T model)
        {
            if (!IsTable(typeof(T)))
            {
                throw new Exception("只能对表进行该操作");
            }
            List<string> pks = GetPrimaryKeyNames<T>();
            List<string> wheres = new List<string>();
            int x = 0;
            List<object> paramList = new List<object>();
            foreach (PropertyInfo p in typeof(T).GetProperties())
            {
                object paramValue = p.FastGetValue(model);
                if (pks.Contains(p.Name))
                {
                    //是主键的
                    paramList.Add(paramValue);
                    wheres.Add(p.Name + " = {" + x + "}");
                    x++;
                }
                //paramList.Add(paramValue);
            }
            if (wheres.Count == 0)
            {
                throw new Exception("没有设置主键。");
            }
            string sql = string.Format("delete from {0} where {1}", GetTableName<T>(), string.Join(" and ", wheres.ToArray()));
            ExecuteNonQuery(ParseCommandString(sql, paramList.ToArray()));
        }
        // 删除批量。
        public void Delete<T>(Expression<Func<T, bool>> func)
        {
            if (!IsTable(typeof(T)))
            {
                throw new Exception("只能对表进行该操作");
            }
            string where = string.Empty;
            if (func.Body is BinaryExpression)
            {
                BinaryExpression be = ((BinaryExpression)func.Body);
                where = BinarExpressionProvider(be.Left, be.Right, be.NodeType);
            }
            string sql = string.Format("delete from {0} where {1}", GetTableName<T>(), where);
            ExecuteNonQuery(ParseCommandString(sql));
        }
        // 更新单例。
        public void Modify<T>(T model)
        {
            if (!IsTable(typeof(T)))
            {
                throw new Exception("只能对表进行该操作");
            }
            List<string> pks = GetPrimaryKeyNames<T>();
            List<string> sets = new List<string>();
            List<string> wheres = new List<string>();
            int x = 0;
            List<object> paramList = new List<object>();
            foreach (PropertyInfo p in typeof(T).GetProperties())
            {
                object paramValue = p.FastGetValue(model);
                paramList.Add(paramValue);
                sets.Add(p.Name + " = {" + x + "}");
                x++;
            }
            foreach (PropertyInfo p in typeof(T).GetProperties())
            {
                object paramValue = p.FastGetValue(model);
                if (pks.Contains(p.Name))
                {
                    //是主键的
                    paramList.Add(paramValue);
                    wheres.Add(p.Name + " = {" + x + "}");
                    x++;
                }
            }
            if (wheres.Count == 0)
            {
                throw new Exception("没有设置主键。");
            }
            string sql = string.Format("update {0} set {1} where {2}", GetTableName<T>(), string.Join(",", sets.ToArray()), string.Join(" and ", wheres.ToArray()));
            ExecuteNonQuery(ParseCommandString(sql, paramList.ToArray()));
        }
        // 更新批量。
        public void Modify<T>(Expression<Func<T, bool>> func, Expression<Action<T>> act)
        {
            if (!IsTable(typeof(T)))
            {
                throw new Exception("只能对表进行该操作");
            }
            string tablename = GetTableName<T>();
            string where = string.Empty;
            string set = string.Empty;
            if (func.Body is BinaryExpression)
            {
                BinaryExpression be = ((BinaryExpression)func.Body);
                where = "where " + BinarExpressionProvider(be.Left, be.Right, be.NodeType);
            }
            if (act.Body is MemberInitExpression)
            {
                MemberInitExpression mie = ((MemberInitExpression)act.Body);
                List<string> fieldsets = new List<string>();
                foreach (MemberAssignment ma in mie.Bindings)
                {
                    fieldsets.Add("{0} = {1}".FormatWith(ma.Member.Name, ExpressionRouter(ma.Expression)));
                }
                set = string.Join(",", fieldsets.ToArray());
            }
            else
            {
                throw new Exception("Action<T> act 参数格式应写成 u => new UserInfo { UserName = \"abc\", UserPass = \"123\" } 形式。");
            }
            string sql = string.Format("update {0} set {1} {2}", tablename, set, where);
            ExecuteNonQuery(ParseCommandString(sql));
        }
        // 仅添加不存在的记录。
        public void AddOnly<T>(T t) where T : new()
        {
            object obj = GetByPrimaryKey<T>(t);
            if (null == obj)
                Add<T>(t);
        }
        // 不存在的添加，已存在的做更新。
        public void AddOrModify<T>(T t) where T : new()
        {
            object obj = GetByPrimaryKey<T>(t);
            if (null == obj)
                Add<T>(t);
            else
                Modify<T>(t);
        }
        // 查询所有。
        public object GetAll(string tablename)
        {
            Type t = GetTypeByTableName(tablename);
            object tempobj = GetObject(t);
            Type returntype = typeof(List<>).MakeGenericType(tempobj.GetType());
            object list = Activator.CreateInstance(returntype);
            MethodInfo mi = returntype.GetMethod("Add");
            List<string> fields = GetFieldNames(t);
            string sql = string.Format("select {0} from {1}", string.Join(",", fields.ToArray()), "dbo.[" + tablename + "]");
            foreach (DataRow dr in this.QueryDataTable(sql).Rows)
            {
                object obj = GetObject(t);
                PropertyInfo[] properties = obj.GetType().GetProperties();
                foreach (PropertyInfo p in properties)
                {
                    object value = dr[p.Name] == DBNull.Value ? null : dr[p.Name];
                    if (value is Guid)
                        value = value.ToString();
                    p.FastSetValue(obj, value);
                }
                mi.FastInvoke(list, obj);
            }
            return list;
        }
        // 查询所有。
        public object GetAll(Type t)
        {
            object tempobj = GetObject(t);
            string tablename = t.Name;
            Type returntype = typeof(List<>).MakeGenericType(tempobj.GetType());
            object list = Activator.CreateInstance(returntype);
            MethodInfo mi = returntype.GetMethod("Add");

            List<string> fields = GetFieldNames(t);
            string sql = string.Format("select {0} from {1}", string.Join(",", fields.ToArray()), "dbo.[" + tablename + "]");
            foreach (DataRow dr in this.QueryDataTable(sql).Rows)
            {
                object obj = GetObject(t);
                PropertyInfo[] properties = t.GetProperties();
                foreach (PropertyInfo p in properties)
                {
                    object value = dr[p.Name] == DBNull.Value ? null : dr[p.Name];
                    if (value is Guid)
                        value = value.ToString();
                    p.FastSetValue(obj, value);
                }
                mi.FastInvoke(list, obj);
            }
            return list;
        }
        // 查询所有。
        public object GetAllByTypeName(string typename)
        {
            string[] arr = typename.Split(",".ToCharArray());
            Assembly asmb = Assembly.LoadFrom(arr[1] + ".dll");
            Type supType = asmb.GetType(arr[0]);
            return GetAll(supType);
        }
        // 按主键查询。
        public object GetByPrimaryKey(object model)
        {
            Type t = model.GetType();
            string tablename = t.Name;
            List<string> fieldlist = new List<string>();
            List<string> wheres = new List<string>();
            List<object> paramlist = new List<object>();
            PropertyInfo[] properties = t.GetProperties();
            int x = 0;
            List<string> pklist = new List<string>();
            foreach (PropertyInfo p in properties)
            {
                fieldlist.Add("[" + p.Name + "]");
                object[] objs = p.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (null != objs && objs.Length != 0)
                {
                    foreach (object obj in objs)
                    {
                        if ((obj is DescriptionAttribute) && (obj as DescriptionAttribute).Description == "IsPrimaryKey")
                        {
                            wheres.Add("[" + p.Name + "]=" + "{" + x + "}");
                            object value = p.FastGetValue(model);
                            paramlist.Add(value);
                            x++;
                            break;
                        }
                    }
                }
            }
            string sql = string.Format("select {0} from {1} where {2}", string.Join(",", fieldlist.ToArray()), tablename, string.Join(" and ", wheres.ToArray()));
            DataRow dr = QueryDataRow(ParseCommandString(sql, paramlist.ToArray()));
            if (dr != null)
            {
                object obj = GetObject(t);
                foreach (PropertyInfo p in properties)
                {
                    object value = dr[p.Name] == DBNull.Value ? null : dr[p.Name];
                    if (value is Guid)
                        value = value.ToString();
                    p.FastSetValue(obj, value);
                }
                return obj;
            }
            else
                return null;
        }
        // 添加单例。
        public void Add(object model)
        {
            Type t = model.GetType();
            if (!IsTable(t))
            {
                throw new Exception("只能对表进行该操作");
            }
            string tablename = t.Name;

            List<string> fieldlist = new List<string>();
            List<string> values = new List<string>();
            List<object> paramlist = new List<object>();
            PropertyInfo[] properties = t.GetProperties();
            int x = 0;
            foreach (PropertyInfo p in properties)
            {
                fieldlist.Add("[" + p.Name + "]");
                object value = p.FastGetValue(model);
                paramlist.Add(value);
                values.Add("{" + x + "}");
                x++;
            }
            string sql = string.Format("insert into {0}({1}) values({2})", tablename, string.Join(",", fieldlist.ToArray()), string.Join(",", values.ToArray()));
            ExecuteNonQuery(ParseCommandString(sql, paramlist.ToArray()));
        }
        // 删除单例。
        public void Delete(object model)
        {
            Type t = model.GetType();
            if (!IsTable(t))
            {
                throw new Exception("只能对表进行该操作");
            }
            string tablename = t.Name;

            List<string> wheres = new List<string>();
            List<object> paramlist = new List<object>();
            PropertyInfo[] properties = t.GetProperties();
            int x = 0;
            List<string> pklist = new List<string>();
            foreach (PropertyInfo p in properties)
            {
                object[] objs = p.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (null != objs && objs.Length != 0)
                {
                    foreach (object obj in objs)
                    {
                        if ((obj is DescriptionAttribute) && (obj as DescriptionAttribute).Description == "IsPrimaryKey")
                        {
                            object value = p.FastGetValue(model);
                            paramlist.Add(value);
                            wheres.Add("[" + p.Name + "]=" + "{" + x + "}");
                            x++;
                            break;
                        }
                    }
                }

            }
            string sql = string.Format("delete from {0} where ({1})", tablename, string.Join(" and ", wheres.ToArray()));
            ExecuteNonQuery(ParseCommandString(sql, paramlist.ToArray()));
        }
        // 更新单例。
        public void Modify(object model)
        {
            Type t = model.GetType();
            if (!IsTable(t))
            {
                throw new Exception("只能对表进行该操作");
            }
            string tablename = t.Name;
            List<string> sets = new List<string>();
            List<string> wheres = new List<string>();
            List<object> paramlist = new List<object>();
            PropertyInfo[] properties = t.GetProperties();
            int x = 0;
            List<string> pklist = new List<string>();
            foreach (PropertyInfo p in properties)
            {
                sets.Add("[" + p.Name + "]={" + x + "}");
                object value = p.FastGetValue(model);
                paramlist.Add(value);
                object[] objs = p.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (null != objs && objs.Length != 0)
                {
                    foreach (object obj in objs)
                    {
                        if ((obj is DescriptionAttribute) && (obj as DescriptionAttribute).Description == "IsPrimaryKey")
                        {
                            wheres.Add("[" + p.Name + "]=" + "{" + x + "}");
                            break;
                        }
                    }
                }
                x++;
            }
            string sql = string.Format("update {0} set {1} where ({2})", tablename, string.Join(",", sets.ToArray()), string.Join(" and ", wheres.ToArray()));
            ExecuteNonQuery(ParseCommandString(sql, paramlist.ToArray()));
        }
        // 仅添加不存在的记录。
        public void AddOnly(object model)
        {
            Type t = model.GetType();
            string tablename = t.Name;
            if (GetByPrimaryKey(model) == null)
                Add(model);
        }
        // 不存在的添加，已存在的做更新。
        public void AddOrModify(object model)
        {
            Type t = model.GetType();
            string tablename = t.Name;
            if (GetByPrimaryKey(model) == null)
                Add(model);
            else
                Modify(model);
        }
        // 按条件查找单个实例。
        public object GetSingle(string tablename, string where)
        {
            if (where.IsEmpty())
                throw new Exception("where语句不能为空。");
            if (tablename.IsEmpty())
                throw new Exception("tablename不能为空。");
            Type t = GetObjectByTypeName(tablename).GetType();
            List<string> fieldlist = new List<string>();
            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo p in properties)
            {
                fieldlist.Add("[" + p.Name + "]");
            }
            string sql = string.Format("select {0} from {1} where {2}", string.Join(",", fieldlist.ToArray()), tablename, where);
            DataRow dr = QueryDataRow(sql);
            if (dr != null)
            {
                object obj = GetObjectByTypeName(tablename);
                foreach (PropertyInfo p in properties)
                {
                    object value = dr[p.Name] == DBNull.Value ? null : dr[p.Name];
                    if (value is Guid)
                        value = value.ToString();
                    p.FastSetValue(obj, value);
                }
                return obj;
            }
            else
                return GetObject(t);
        }
        // 按条件查找所有。
        public object GetAll(string tablename, string where)
        {
            if (where.IsEmpty())
                throw new Exception("where语句不能为空。");
            if (tablename.IsEmpty())
                throw new Exception("tablename不能为空。");
            Type t = GetTypeByTableName(tablename);
            Type resultType = typeof(List<>).MakeGenericType(t);
            object list = Activator.CreateInstance(resultType);
            MethodInfo mi = resultType.GetMethod("Add");

            List<string> fieldlist = new List<string>();
            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo p in properties)
            {
                fieldlist.Add("[" + p.Name + "]");
            }
            string sql = string.Format("select {0} from {1} where {2}", string.Join(",", fieldlist.ToArray()), tablename, where);
            foreach (DataRow dr in QueryDataTable(sql).Rows)
            {
                object obj = GetObject(t);
                foreach (PropertyInfo p in properties)
                {
                    object value = dr[p.Name] == DBNull.Value ? null : dr[p.Name];
                    if (value is Guid)
                        value = value.ToString();
                    p.FastSetValue(obj, value);
                }
                mi.FastInvoke(list, obj);
            }
            return list;
        }
        // 根据数据库字典，Emit反射生成对象类。
        public Type GetTypeByTableName(SqlExecuter dataaccess, string tablename)
        {
            tablename = tablename.Replace("[", "").Replace("]", "");
            string namesp = "Goodway.CommonModel." + ConnstringKey;
            List<string> fields = new List<string>();
            string sql =
                 @"select 
                 pk.is_primary_key pk,Col.is_nullable is_nullable,
                 (CASE
                 WHEN Type.name = 'uniqueidentifier' THEN 'System.String'
                 WHEN Type.name = 'char' THEN 'System.String'
                 WHEN Type.name = 'nchar' THEN 'System.String'
                 WHEN Type.name = 'varchar' THEN 'System.String'
                 WHEN Type.name = 'nvarchar' THEN 'System.String'
                 WHEN Type.name = 'text' THEN 'System.String'
                 WHEN Type.name = 'ntext' THEN 'System.String'
                 WHEN Type.name = 'xml' THEN 'System.String'
                 WHEN Type.name = 'image' THEN 'System.Byte[]'
                 WHEN Type.name = 'timestamp' THEN 'System.Byte[]'
                 WHEN Type.name = 'binary' THEN 'System.Byte[]'
                 WHEN Type.name = 'varbinary' THEN 'System.Byte[]'
                 WHEN Type.name = 'tinyint' THEN 'System.Byte'
                 WHEN Type.name = 'int' THEN 'System.Int32'
                 WHEN Type.name = 'smallint' THEN 'System.Int16'
                 WHEN Type.name = 'bigint' THEN 'System.Int64'
                 WHEN Type.name = 'float' THEN 'System.Double'
                 WHEN Type.name = 'real' THEN 'System.Single'
                 WHEN Type.name = 'money' THEN 'System.Decimal'
                 WHEN Type.name = 'smallmoney' THEN 'System.Decimal'
                 WHEN Type.name = 'decimal' THEN 'System.Decimal'
                 WHEN Type.name = 'numeric' THEN 'System.Decimal'
                 WHEN Type.name = 'datetime' THEN 'System.DateTime'
                 WHEN Type.name = 'smalldatetime' THEN 'System.DateTime'
                 WHEN Type.name = 'bit' THEN 'System.Boolean'
                 WHEN Type.name = 'sql_variant' THEN 'System.Object'
                 ELSE Type.name
                 END) [type], 
                 STUFF(Col.Name,1,1,UPPER(SUBSTRING(Col.Name,1,1))) [propname] 
                 from sys.objects Tab inner join sys.columns Col on Tab.object_id =Col.object_id
                 inner join sys.types Type on Col.system_type_id = Type.system_type_id
                 left join sys.identity_columns identity_columns on  Tab.object_id = identity_columns.object_id and Col.column_id = identity_columns.column_id
                 left join(
                 select index_columns.object_id,index_columns.column_id,indexes.is_primary_key 
                 from sys.indexes  indexes inner join sys.index_columns index_columns 
                 on indexes.object_id = index_columns.object_id and indexes.index_id = index_columns.index_id
                 where indexes.is_primary_key = 1
                 ) PK on Tab.object_id = PK.object_id AND Col.column_id = PK.column_id
                 where Type.Name <> 'sysname' and (Tab.type = 'U' or Tab.type = 'V')  and Tab.Name<>'sysdiagrams' and Tab.Name='" + tablename + "'";
            AssemblyName name = new AssemblyName();
            name.Name = namesp;
            AppDomain ad = System.Threading.Thread.GetDomain();
            AssemblyBuilder abuilder = ad.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndSave);

            ModuleBuilder mbuilder = abuilder.DefineDynamicModule(namesp, "Business.dll");
            TypeBuilder typeBuilder = mbuilder.DefineType(namesp + "." + tablename, TypeAttributes.Public | TypeAttributes.Class);
            foreach (DataRow dr in dataaccess.QueryDataTable(sql).Rows)
            {
                Type t;
                string privateName = "_" + dr["propname"].ToString();
                string publicName = dr["propname"].ToString();
                bool isnullable = Convert.ToBoolean(dr["is_nullable"]);
                switch (dr["type"].ToString())
                {
                    case "System.String":
                        t = typeof(string);
                        break;
                    case "System.Int32":
                        t = isnullable ? typeof(int?) : typeof(int);
                        break;
                    case "System.Int16":
                        t = isnullable ? typeof(short?) : typeof(short);
                        break;
                    case "System.Int64":
                        t = isnullable ? typeof(long?) : typeof(long);
                        break;
                    case "System.Decimal":
                        t = isnullable ? typeof(decimal?) : typeof(decimal);
                        break;
                    case "System.Single":
                        t = isnullable ? typeof(float?) : typeof(float);
                        break;
                    case "System.DateTime":
                        t = isnullable ? typeof(DateTime?) : typeof(DateTime);
                        break;
                    case "System.Guid"://Guid特殊处理
                        t = typeof(string);
                        break;
                    case "System.Byte[]":
                        t = typeof(byte[]);
                        break;
                    case "System.Byte":
                        t = isnullable ? typeof(byte?) : typeof(byte);
                        break;
                    case "System.Boolean":
                        t = isnullable ? typeof(bool?) : typeof(bool);
                        break;
                    case "System.Object":
                        t = typeof(System.Object);
                        break;
                    default:
                        t = isnullable ? Type.GetType("System." + dr["type"].ToString() + "?") : Type.GetType("System." + dr["type"].ToString());
                        break;
                }
                FieldBuilder fieldBuilder = typeBuilder.DefineField(privateName, t, FieldAttributes.Private);
                PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(publicName, System.Reflection.PropertyAttributes.None, t, null);
                Type[] ctorParams = new Type[] { typeof(string) };
                if (dr["pk"].NotNull())
                {
                    ConstructorInfo classCtorInfo = typeof(DescriptionAttribute).GetConstructor(ctorParams);
                    CustomAttributeBuilder myCABuilder = new CustomAttributeBuilder(
                    classCtorInfo,
                    new object[] { "IsPrimaryKey" });
                    propertyBuilder.SetCustomAttribute(myCABuilder);
                }
                MethodBuilder getPropertyABuilder = typeBuilder.DefineMethod("get", MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, t, Type.EmptyTypes);
                ILGenerator getAIL = getPropertyABuilder.GetILGenerator();
                getAIL.Emit(OpCodes.Ldarg_0);
                getAIL.Emit(OpCodes.Ldfld, fieldBuilder);
                getAIL.Emit(OpCodes.Ret);
                MethodBuilder setPropertyABuilder = typeBuilder.DefineMethod("set", MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, null, new Type[] { t });
                ILGenerator setAIL = setPropertyABuilder.GetILGenerator();
                setAIL.Emit(OpCodes.Ldarg_0);
                setAIL.Emit(OpCodes.Ldarg_1);
                setAIL.Emit(OpCodes.Stfld, fieldBuilder);
                setAIL.Emit(OpCodes.Ret);
                propertyBuilder.SetGetMethod(getPropertyABuilder);
                propertyBuilder.SetSetMethod(setPropertyABuilder);
            }
            Type t1 = typeBuilder.CreateType();
            return GetObject(t1).GetType();
            //return t1;
        }
        // 根据数据库字典，Emit反射生成对象类。
        public Type GetTypeByTableName(string tablename)
        {
            return GetTypeByTableName(this, tablename);
        }
        // 缓存字段名集合。
        static Dictionary<Type, List<string>> FieldNameListMap = new Dictionary<Type, List<string>>();
        // 缓存主键名集合。
        static Dictionary<Type, List<string>> PrimaryKeyNameListMap = new Dictionary<Type, List<string>>();
        // 获取字段名集合。
        static List<string> GetFieldNames(Type t)
        {
            List<string> list = new List<string>();
            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo p in properties)
            {
                list.Add(p.Name);
            }
            return list;
        }
        // 获取字段名集合。
        static List<string> GetFieldNames<T>()
        {
            List<string> fields = null;
            if (FieldNameListMap.TryGetValue(typeof(T), out fields))
            {
                return fields;
            }
            else
            {
                fields = new List<string>();
                PropertyInfo[] properties = typeof(T).GetProperties();
                foreach (PropertyInfo p in properties)
                {
                    fields.Add("{0}".FormatWith(p.Name));
                }
                FieldNameListMap.TryAdd(typeof(T), fields);
            }
            return fields;
        }
        // 获取主键名集合。
        static List<string> GetPrimaryKeyNames<T>()
        {
            List<string> fields = null;
            if (PrimaryKeyNameListMap.TryGetValue(typeof(T), out fields))
            {
                return fields;
            }
            else
            {
                fields = new List<string>();
                PropertyInfo[] properties = typeof(T).GetProperties();
                foreach (PropertyInfo p in properties)
                {
                    object[] objs = p.GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (null != objs && objs.Length != 0)
                    {
                        foreach (object obj in objs)
                        {
                            if ((obj is DescriptionAttribute) && (obj as DescriptionAttribute).Description == "IsPrimaryKey")
                            {
                                fields.Add(p.Name);
                                break;
                            }
                        }
                    }
                }
                PrimaryKeyNameListMap.TryAdd(typeof(T), fields);
            }
            return fields;
        }
        // 获取表名。
        string GetTableName<T>()
        {
            string dbname = "";
            string typename = typeof(T).ToString();
            return dbname + "dbo.[" + typename.Substring(typename.LastIndexOf(".") + 1) + "]";
        }
        // 获取表名。
        //string GetTableName(string tablename)
        //{
        //    string dbname = "";
        //    return dbname + "dbo.[" + tablename.Replace("[", "").Replace("]", "") + "]";
        //}
        // 取查询语句。
        //string GetSelectSql(Type t)
        //{
        //    List<string> fields = GetFieldNames(t);
        //    return string.Format("select {0} from {1}", string.Join(",", fields.ToArray()), "dbo.[" + t.Name + "]");
        //}
        // 解析sql
        string ParseCommandString(string commandText, params object[] parameterArray)
        {
            // 参数列表。
            List<string> parameterList = new List<string>();
            foreach (object obj in parameterArray)
            {
                // 数字类型。
                if (obj is short || obj is ushort || obj is int || obj is uint || obj is long || obj is ulong || obj is float || obj is double || obj is decimal || obj is Byte)
                {
                    parameterList.Add(obj.ToString());
                    continue;
                }
                // 字符串类型。
                if (obj is string)
                {
                    parameterList.Add(string.Format("'{0}'", (obj as string).Replace("'", "''")));
                    continue;
                }
                // Guid类型。
                if (obj is Guid)
                {
                    parameterList.Add(string.Format("'{0}'", obj.ToString()));
                    continue;
                }
                // 时间类型。
                if (obj is DateTime)
                {
                    DateTime datetime = Convert.ToDateTime(obj);
                    if (datetime < SqlDateTime.MinValue.Value)
                        datetime = SqlDateTime.MinValue.Value;
                    else if (datetime > SqlDateTime.MaxValue.Value)
                        datetime = SqlDateTime.MaxValue.Value;
                    parameterList.Add(string.Format("'{0}'", datetime));
                    continue;
                }
                // 二进制类型。
                if (obj is byte[])
                {
                    StringBuilder stringBuilder = new StringBuilder("0x", (obj as byte[]).Length * 2 + 2);
                    foreach (byte @byte in (obj as byte[]))
                    {

                        stringBuilder.AppendFormat("{0:X2}", @byte);
                    }
                    parameterList.Add(stringBuilder.ToString());
                    continue;
                }
                // 布尔类型。
                if (obj is bool)
                {
                    parameterList.Add(Convert.ToBoolean(obj) ? "1" : "0");
                    continue;
                }
                // 空类型。
                if (obj == null || obj is DBNull)
                {
                    parameterList.Add("Null");
                    continue;
                }
                throw new NotSupportedException();
            }
            return string.Format(commandText, parameterList.ToArray());
        }
        public void StartSqlDependency()
        {
            SqlDependency.Start(this.ConnnectString);
        }
        public object GetObject(Type t)
        {
            return GetObjectByTypeName(t.Name);
        }
        public object GetObjectByTypeName(string typename)
        {
            Assembly assem = Assembly.Load("Bussiness.dll");
            return assem.CreateInstance(typename);
        }
        public bool IsTable(object model)
        {
            Type t = model.GetType();
            bool istable = false;
            object[] objs = t.GetCustomAttributes(false);
            if (null != objs && objs.Length != 0)
            {
                foreach (object obj in objs)
                {
                    if ((obj is DescriptionAttribute) && (obj as DescriptionAttribute).Description == "IsTable")
                    {
                        istable = true;
                        break;
                    }
                }
            }
            return istable;
        }
        public bool IsTable(Type t)
        {
            bool istable = false;
            object[] objs = t.GetCustomAttributes(false);
            if (null != objs && objs.Length != 0)
            {
                foreach (object obj in objs)
                {
                    if ((obj is DescriptionAttribute) && (obj as DescriptionAttribute).Description == "IsTable")
                    {
                        istable = true;
                        break;
                    }
                }
            }
            return istable;
        }
        // 解析Lambda表达式。
        static string BinarExpressionProvider(Expression left, Expression right, ExpressionType type)
        {
            string sb = "(";
            string tmpStr = ExpressionRouter(left);
            if (tmpStr == "null")
            {
                Expression temp = left;
                left = right;
                right = temp;
            }
            sb += ExpressionRouter(left);
            sb += ExpressionTypeCast(type);
            tmpStr = ExpressionRouter(right);
            if (tmpStr == "null")
            {
                if (sb.EndsWith(" ="))
                    sb = sb.Substring(0, sb.Length - 2) + " is null";
                else if (sb.EndsWith("<>"))
                    sb = sb.Substring(0, sb.Length - 2) + " is not null";
            }
            else
                sb += tmpStr;
            return sb += ")";
        }
        // 解析Lambda表达式路由。
        static string ExpressionRouter(Expression exp)
        {
            string sb = string.Empty;
            if (exp is BinaryExpression)
            {
                BinaryExpression be = ((BinaryExpression)exp);
                return BinarExpressionProvider(be.Left, be.Right, be.NodeType);
            }
            else if (exp is MemberExpression)
            {
                if (!exp.ToString().StartsWith("value("))
                {
                    MemberExpression me = ((MemberExpression)exp);
                    return me.Member.Name;
                }
                else
                {
                    var result = Expression.Lambda(exp).Compile().DynamicInvoke();
                    return GetExpressionValue(result);
                }
            }
            else if (exp is NewArrayExpression)
            {
                NewArrayExpression ae = ((NewArrayExpression)exp);
                StringBuilder tmpstr = new StringBuilder();
                foreach (Expression ex in ae.Expressions)
                {
                    tmpstr.Append(ExpressionRouter(ex));
                    tmpstr.Append(",");
                }
                return tmpstr.ToString(0, tmpstr.Length - 1);
            }
            else if (exp is MethodCallExpression)
            {
                MethodCallExpression mce = (MethodCallExpression)exp;
                if (mce.Method.Name == "Like")
                    return string.Format("({0} like {1})", ExpressionRouter(mce.Arguments[0]), ExpressionRouter(mce.Arguments[1]));
                else if (mce.Method.Name == "NotLike")
                    return string.Format("({0} Not like {1})", ExpressionRouter(mce.Arguments[0]), ExpressionRouter(mce.Arguments[1]));
                else if (mce.Method.Name == "In")
                    return string.Format("{0} In ({1})", ExpressionRouter(mce.Arguments[0]), ExpressionRouter(mce.Arguments[1]));
                else if (mce.Method.Name == "NotIn")
                    return string.Format("{0} Not In ({1})", ExpressionRouter(mce.Arguments[0]), ExpressionRouter(mce.Arguments[1]));
                else
                {
                    var result = Expression.Lambda(exp).Compile().DynamicInvoke();
                    return GetExpressionValue(result);
                }
            }
            else if (exp is ConstantExpression)
            {
                ConstantExpression ce = ((ConstantExpression)exp);
                return GetExpressionValue(ce.Value);
            }
            else if (exp is UnaryExpression)
            {
                UnaryExpression ue = ((UnaryExpression)exp);
                return ExpressionRouter(ue.Operand);
            }
            else if (exp is NewExpression)
            {
                var result = Expression.Lambda(exp).Compile().DynamicInvoke();
                return GetExpressionValue(result);
            }
            return null;
        }
        // 计算Lambda表达式值。
        static string GetExpressionValue(object result)
        {
            if (result == null)
                return "null";
            if (result is bool)
                return (bool)result ? "1" : "0";
            if (result is Guid)
                return string.Format("'{0}'", result.ToString());
            if (result is string || result is DateTime || result is char || result is Guid)
                return string.Format("'{0}'", result.ToString().Replace("'", "''"));

            if (result is ValueType)
                return result.ToString();
            return null;
        }
        // 解析Lambda表达式连接符。
        static string ExpressionTypeCast(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return " AND ";
                case ExpressionType.Equal:
                    return " =";
                case ExpressionType.GreaterThan:
                    return " >";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.LessThanOrEqual:
                    return "<=";
                case ExpressionType.NotEqual:
                    return "<>";
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return " Or ";
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                    return "+";
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    return "-";
                case ExpressionType.Divide:
                    return "/";
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                    return "*";
                default:
                    return null;
            }
        }
    }
}
