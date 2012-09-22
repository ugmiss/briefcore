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
using FastReflect;

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
            foreach (DataRow dr in QueryDataTable(sql).Rows)
            {
                list.Add(dr.GetModelByDataRow<T>());
            }
            return list;
        }
        // 分页查询。
        public List<T> GetAll<T>(string whereStr, int maxcount, int pagenumber, params string[] rowfields) where T : new()
        {
            List<T> list = new List<T>();
            List<string> fields = GetFieldNames<T>();
            string tablename = GetTableName<T>();
            List<string> pks = GetPrimaryKeyNames<T>();
            string rowfield = null;
            if (rowfields.Length == 0 || rowfields[0].IsNullOrEmpty() || !fields.Contains(rowfields[0].Split(' ')[0]))
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
        // 添加单例
        public void Add<T>(T model)
        {
            if (!IsTable(typeof(T)))
            {
                throw new Exception("只能对表进行该操作");
            }

            List<string> fields = GetFieldNamesWithOutIdentity<T>();
            List<string> values = new List<string>();
            int x = 0;
            List<object> paramList = new List<object>();
            foreach (PropertyInfo p in typeof(T).GetProperties())
            {
                if (GetIdentityNames<T>().Contains(p.Name))
                    continue;
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
                if (GetIdentityNames<T>().Contains(p.Name))
                    continue;
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
        // 缓存字段名集合。
        static Dictionary<Type, List<string>> FieldNameListMap = new Dictionary<Type, List<string>>();
        // 缓存主键名集合。
        static Dictionary<Type, List<string>> PrimaryKeyNameListMap = new Dictionary<Type, List<string>>();
        // 缓存标识列集合。
        static Dictionary<Type, List<string>> IdentityNameListMap = new Dictionary<Type, List<string>>();
        // 非标识列集合。
        static Dictionary<Type, List<string>> FieldNameListWithOutIdentityMap = new Dictionary<Type, List<string>>();


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
        static List<string> GetFieldNamesWithOutIdentity<T>()
        {
            List<string> fields = null;
            if (FieldNameListWithOutIdentityMap.TryGetValue(typeof(T), out fields))
            {
                return fields;
            }
            else
            {
                fields = new List<string>();
                PropertyInfo[] properties = typeof(T).GetProperties();
                foreach (PropertyInfo p in properties)
                {
                    if (GetIdentityNames<T>().Contains(p.Name))
                        continue;
                    fields.Add("{0}".FormatWith(p.Name));
                }
                FieldNameListWithOutIdentityMap.TryAdd(typeof(T), fields);
            }
            return fields;
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
                            if ((obj is DescriptionAttribute) && (obj as DescriptionAttribute).Description.IndexOf("IsPrimaryKey") >= 0)
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
        // 获取标识列名集合。
        static List<string> GetIdentityNames<T>()
        {
            List<string> fields = null;
            if (IdentityNameListMap.TryGetValue(typeof(T), out fields))
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
                            if ((obj is DescriptionAttribute) && (obj as DescriptionAttribute).Description.IndexOf("IsIdentity") >= 0)
                            {
                                fields.Add(p.Name);
                                break;
                            }
                        }
                    }
                }
                IdentityNameListMap.TryAdd(typeof(T), fields);
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
