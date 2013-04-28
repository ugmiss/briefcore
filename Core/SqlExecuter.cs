using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;

namespace System
{
    public class SqlExecuter : IDisposable
    {
        // 连接串
        string ConnectionString { get; set; }
        // 库名称
        public string DataBaseName
        {
            get
            {
                var Conn = new SqlConnection(ConnectionString);
                Conn.Open();
                return Conn.Database;
            }
        }
        // 测试数据库连接,"second"超时秒数
        public bool TestConnection(int second)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(ConnectionString + ";Connection Timeout=" + second))
                {
                    Connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        // 事务对象
        SqlTransaction Transaction { get; set; }
        // 构造
        public SqlExecuter(string connectionString)
        {
            //try
            //{
            //    SqlDependency.Stop(connectionString);
            //    SqlDependency.Start(connectionString);
            //}
            //catch { }
            ConnectionString = connectionString;
        }
        // 解析命令字符串
        string ParseCommandString(string commandText, params object[] parameterArray)
        {
            // 参数列表
            List<string> parameterList = new List<string>();
            foreach (object obj in parameterArray)
            {
                // 数字类型
                if (obj is short || obj is ushort || obj is int || obj is uint || obj is long || obj is ulong || obj is float || obj is double || obj is decimal || obj is Byte)
                {
                    parameterList.Add(obj.ToString());
                    continue;
                }
                // 字符串类型
                if (obj is string)
                {
                    parameterList.Add(string.Format("'{0}'", (obj as string).Replace("'", "''")));
                    continue;
                }
                // Guid类型
                if (obj is Guid)
                {
                    parameterList.Add(string.Format("'{0}'", obj.ToString()));
                    continue;
                }
                // 时间类型
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
                // 二进制类型
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
                // 布尔类型
                if (obj is bool)
                {
                    parameterList.Add(Convert.ToBoolean(obj) ? "1" : "0");
                    continue;
                }
                // 空类型
                if (obj == null || obj is DBNull)
                {
                    parameterList.Add("Null");
                    continue;
                }
                throw new NotSupportedException();
            }
            return string.Format(commandText, parameterList.ToArray());
        }
        // 表查询
        public DataTable QueryTableWithCache(string commandText, SqlDependency dependency, params object[] parameterArray)
        {
            string sql = ParseCommandString(commandText, parameterArray);
            SqlConnection Connection = null;
            if (Transaction != null)
            {
                Connection = Transaction.Connection;
            }
            else
            {
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();
            }
            try
            {
                DataTable dataTable = new DataTable();
                using (SqlCommand cmd = new SqlCommand(sql, Connection))
                {
                    dependency.AddCommandDependency(cmd);
                    cmd.Transaction = Transaction;
                    dataTable.Load(cmd.ExecuteReader());
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                Exception newEx = new Exception(ex.Message + "(sql:" + commandText + ")", ex);
                Logger.Error(newEx);
                throw newEx;
            }
            finally
            {
                if (Transaction == null)
                {
                    Connection.Dispose();
                }
            }
        }
        // 表查询
        public DataTable QueryDataTable(string commandText, params object[] parameterArray)
        {
            string sql = ParseCommandString(commandText, parameterArray);
            SqlConnection Connection = null;
            if (Transaction != null)
            {
                Connection = Transaction.Connection;
            }
            else
            {
                Connection = new SqlConnection(ConnectionString);

                Connection.Open();
            }
            try
            {
                DataTable dataTable = new DataTable();
                using (SqlCommand cmd = new SqlCommand(sql, Connection))
                {
                    cmd.Transaction = Transaction;
                    dataTable.Load(cmd.ExecuteReader());
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                Exception newEx = new Exception(ex.Message + "(sql:" + commandText + ")", ex);
                Logger.Error(newEx);
                throw newEx;
            }
            finally
            {
                if (Transaction == null)
                {
                    Connection.Dispose();
                }
            }
        }
        // DataReader方式
        public SqlDataReader QueryDataReader(string commandText, params object[] parameterArray)
        {
            string sql = ParseCommandString(commandText, parameterArray);
            SqlConnection Connection = null;
            if (Transaction != null)
            {
                Connection = Transaction.Connection;
            }
            else
            {
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();
            }
            try
            {
                DataTable dataTable = new DataTable();
                using (SqlCommand cmd = new SqlCommand(sql, Connection))
                {
                    cmd.Transaction = Transaction;
                    var reader = cmd.ExecuteReader();
                    return reader;
                }
            }
            catch (Exception ex)
            {
                Exception newEx = new Exception(ex.Message + "(sql:" + commandText + ")", ex);
                Logger.Error(newEx);
                throw newEx;
            }
        }
        // 行查询
        public DataRow QueryDataRow(string commandText, params object[] parameterArray)
        {
            DataTable dataTable = QueryDataTable(commandText, parameterArray);
            if (dataTable.Rows.Count > 0)
                return dataTable.Rows[0];
            else
                return null;
        }
        // 执行非查询类型的语句
        public int ExecuteNonQuery(string commandText, params object[] parameterArray)
        {
            SqlConnection Connection = null;
            if (Transaction != null)
            {
                Connection = Transaction.Connection;
            }
            else
            {
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();
            }
            string sql = ParseCommandString(commandText, parameterArray);
            //Logger.Info(sql);
            try
            {
                using (SqlCommand Command = new SqlCommand(sql, Connection))
                {
                    Command.Transaction = Transaction;
                    return Command.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                Exception newEx = new Exception(ex.Message + "(sql:" + commandText + ")", ex);
                Logger.Error(newEx);
                throw newEx;
            }
            finally
            {
                if (Transaction == null)
                    Connection.Dispose();
            }
        }
        // 需要返回标识列的值时
        public int ExecuteNonQuery(out int IdentityID, string commandText, params object[] parameterArray)
        {
            IdentityID = -1;
            SqlConnection Connection = null;
            if (Transaction != null)
            {
                Connection = Transaction.Connection;
            }
            else
            {
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();
            }
            string sql = ParseCommandString(commandText, parameterArray);
            try
            {
                using (SqlCommand Command = new SqlCommand(sql, Connection))
                {
                    Command.Transaction = Transaction;
                    object obj = Command.ExecuteScalar();
                    if (obj != null)
                    {
                        IdentityID = Convert.ToInt32(obj);
                        return IdentityID;
                    }
                }
            }
            catch (Exception ex)
            {
                Exception newEx = new Exception(ex.Message + "(sql:" + commandText + ")", ex);
                Logger.Error(newEx);
                throw newEx;
            }
            finally
            {
                if (Transaction == null)
                    Connection.Dispose();
            }
            return 0;
        }
        // 执行非查询类型的语句，不抛出异常
        public void NonQueryWithoutException(string commandText, params object[] parameterArray)
        {
            SqlConnection Connection = null;
            if (Transaction != null)
            {
                Connection = Transaction.Connection;
            }
            else
            {
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();
            }
            string sql = ParseCommandString(commandText, parameterArray);
            try
            {
                using (SqlCommand Command = new SqlCommand(sql, Connection))
                {
                    if (Transaction != null)
                    {
                        Command.Transaction = Transaction;
                    }
                    Command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Exception newEx = new Exception(ex.Message + "(sql:" + commandText + ")", ex);
                Logger.Error(newEx);
                throw newEx;
            }
            finally
            {
                if (Transaction == null)
                    Connection.Dispose();
            }
        }
        // 执行非查询类型的语句（SqlParameter方式）
        public void ExecuteNonQuery(string commandText, Dictionary<string, object> parameterMap)
        {
            SqlConnection Connection = null;
            if (Transaction != null)
            {
                Connection = Transaction.Connection;
            }
            else
            {
                Connection = new SqlConnection(ConnectionString);
                Connection.Open();
            }
            try
            {
                SqlCommand Command = new SqlCommand(commandText, Connection);
                foreach (string key in parameterMap.Keys)
                {
                    Command.Parameters.Add(new SqlParameter(key, parameterMap[key]));
                }
                Command.Transaction = Transaction;
                Command.ExecuteNonQuery();
                Command.Dispose();
            }
            catch (Exception ex)
            {
                Exception newEx = new Exception(ex.Message + "(sql:" + commandText + ")", ex);
                Logger.Error(newEx);
                throw newEx;
            }
            finally
            {
                if (Transaction == null)
                    Connection.Dispose();
            }
        }
        // 开始事务
        public void BeginTransaction()
        {
            if (Transaction != null)
            {
                try
                {
                    Transaction.Commit();
                }
                finally
                {
                    Transaction.Dispose();
                }
            }
            SqlConnection Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            Transaction = Connection.BeginTransaction(IsolationLevel.ReadCommitted);
        }
        // 提交事务
        public void CommitTransaction()
        {
            if (Transaction != null)
            {
                try
                {
                    Transaction.Commit();
                }
                finally
                {
                    Transaction.Dispose();
                    Transaction = null;
                }
            }
        }
        // 回滚事务
        public void RollbackTransaction()
        {
            if (Transaction != null)
            {
                try
                {
                    Transaction.Rollback();
                }
                finally
                {
                    Transaction.Dispose();
                    Transaction = null;
                }
            }
        }
        // 析构
        public void Dispose()
        {
            this.ConnectionString = "";
        }
    }
}