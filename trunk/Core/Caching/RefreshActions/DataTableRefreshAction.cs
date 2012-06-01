using System;
using System.Data;

namespace Caching
{
    public class DataTableRefreshAction : ICacheItemRefreshAction
    {
        SqlExecuter _exec;
        public DataTableRefreshAction(SqlExecuter exec)
        {
            _exec = exec;
        }
        public void Refresh(string removedKey, object expiredValue, EnumRemovedReason removalReason)
        {
            DataTable dt = _exec.QueryDataTable(removedKey);
        }
    }
}
