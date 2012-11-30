﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace DataAccess
{
    public class SqlExecuter
    {
        /// <summary>
        /// 命令。
        /// </summary>
        private SqlCommand _command;
        /// <summary>
        /// 设置和获取命令。
        /// </summary>
        public SqlCommand Command
        {
            get { return _command; }
            set { _command = value; }
        }
        /// <summary>
        /// 事务。
        /// </summary>
        private SqlTransaction _transaction;
        /// <summary>
        /// 设置和获取事务。
        /// </summary>
        public SqlTransaction Transaction
        {
            get { return _transaction; }
            set { _transaction = value; }
        }
        /// <summary>
        /// 连接对象。
        /// </summary>
        private SqlConnection _connection;
        /// <summary>
        /// 设置和获取链接对象。
        /// </summary>
        public SqlConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }
        /// <summary>
        /// 打开数据库连接。
        /// </summary>
        public void OpenConnection()
        {
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
            else
            {
                Connection.Close();
                Connection.Open();
            }
        }
        /// <summary>
        /// 关闭数据库连接。
        /// </summary>
        public void CloseConnection()
        {
            Connection.Close();
        }
        /// <summary>
        /// 解析命令字符串。
        /// </summary>
        /// <param name="commandText">带参数的命令。</param>
        /// <param name="parameterArray">参数数组。</param>
        /// <returns>最终执行的命令。</returns>
        public string ParseCommandString(string commandText, params object[] parameterArray)
        {
            // 参数列表。
            List<string> parameterList = new List<string>();
            foreach (object obj in parameterArray)
            {
                // 数字类型。
                if (obj is short || obj is ushort || obj is int || obj is uint || obj is long || obj is ulong || obj is float || obj is double || obj is decimal)
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
        /// <summary>
        /// 表查询。
        /// </summary>
        /// <param name="commandText">命令。</param>
        /// <param name="parameterArray">参数数组。</param>
        /// <returns>数据表。</returns>
        public DataTable QueryTable(string commandText, params object[] parameterArray)
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(ParseCommandString(commandText, parameterArray), this.Connection);
            sda.Fill(dataTable);
            dataTable.TableName = "TableName";
            return dataTable;
        }
        /// <summary>
        /// 行查询。
        /// </summary>
        /// <param name="commandText">命令。</param>
        /// <param name="parameterArray">参数数组。</param>
        /// <returns>数据行。</returns>
        public DataRow QueryRow(string commandText, params object[] parameterArray)
        {
            DataTable dataTable = QueryTable(commandText, parameterArray);
            if (dataTable.Rows.Count > 0)
                return dataTable.Rows[0];
            else
                return null;
        }
        /// <summary>
        /// 执行非查询类型的语句。
        /// </summary>
        /// <param name="commandText">命令。</param>
        /// <param name="parameterArray">参数列表。</param>
        public void NonQuery(string commandText, params object[] parameterArray)
        {
            this.Command = new SqlCommand(ParseCommandString(commandText, parameterArray), this.Connection);
            try
            {
                Command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 开始事务。
        /// </summary>
        /// <remarks>开始事务后一定要将事务提交或回滚，暂时不支持事务嵌套，如果嵌套使用事务，则认为前一个事物提交。</remarks>
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
                    Transaction = null;
                }
            }
            Transaction = Connection.BeginTransaction();
        }
        /// <summary>
        /// 提交事务。
        /// </summary>
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
                    Transaction = null;
                }
            }
        }
        /// <summary>
        /// 回滚事务。
        /// </summary>
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
                    Transaction = null;
                }
            }
        }
    }
}