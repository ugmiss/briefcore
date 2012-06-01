using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caching;
using System.Data.SqlClient;

namespace Caching
{
    public class DataTableExpiration : ICacheItemExpiration
    {
        // 开启CLR通知。
        // use master
        // exec sp_configure 'clr enabled',1
        // reconfigure
        // 开启数据库sql broker。
        // alter database chatter set enable_broker
        // SqlDependency.Stop(connectionString);
        // SqlDependency.Start(connectionString);
        // 不能用*,不能用top，不能用函数，包括聚合函数，不能用子查询，包括where后的子查询，不能用外连接，自连接，
        // 不能用临时表，不能用变量，不能用视图，不能垮库，而且表名之前必须加类似dbo这样的前缀
        // 依赖变化时。
        // private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        // {
        //    SqlDependency dependency = sender as SqlDependency;
        //    dependency.OnChange -= Dependency_OnChange;
        // }
        SqlDependency _denpendency;
        public DataTableExpiration(SqlDependency denpendency)
        {
            _denpendency = denpendency;
        }
        public void Notify() { }

        public bool HasExpired()
        {
            return _denpendency.HasChanges;
        }
        public void Initialize(CacheItem cacheItem)
        {
        }
    }
}
