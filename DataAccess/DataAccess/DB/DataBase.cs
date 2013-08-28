using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using DataAccess;

namespace DataAccess
{
    [Serializable]
    public class DataBase
    {
        public string ConnectionString { get; set; }
        public string ServerIP { get; set; }
        public string UID { get; set; }
        public string PWD { get; set; }
        public string DataBaseName { get; set; }
        public List<Tab> Tables;
        public List<ForeignKey> ForeignKeys;
        public List<View> Views;
        public List<Func> Funcs;
        public List<Proc> Procs;
        public List<Index> Indexs;
        public List<Trigger> Triggers;

        public DataBase() { }

        public DataBase(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
            Tables = new List<Tab>();
            ForeignKeys = new List<ForeignKey>();
            Views = new List<View>();
            Funcs = new List<Func>();
            Procs = new List<Proc>();
            Triggers = new List<Trigger>();
            Indexs = new List<Index>();
        }

        public void Init(params string[] conns)
        {
            ServerIP = conns[0];
            UID = conns[1];
            PWD = conns[2];
            DataBaseName = conns[3];
            GetTables();
            GetProcs();
            GetFuncs();
            GetForeignKeys();
            GetTriggers();
            GetViews();
            GetIndexs();
        }

        public void Init(string svr, string uid, string pwd, string dbname)
        {
            DataBaseName = dbname;
            ServerIP = svr;
            UID = uid;
            PWD = pwd;
            GetTables();
            GetProcs();
            GetFuncs();
            GetForeignKeys();
            GetTriggers();
            GetViews();
            GetIndexs();
        }

        public void GetTables()
        {
            SqlExecuter Executer = new SqlExecuter(ConnectionString);
            DataTable dttables = Executer.QueryTable(AppResource.tables);
            DataTable dtcols = Executer.QueryTable(AppResource.cols);

            foreach (DataRow dr in dttables.Rows)
            {
                Tab table = new Tab();
                table.TableName = dr["TABLE_NAME"].ToString();
                foreach (DataRow dr2 in dtcols.Rows)
                {
                    if (dr2["tablename"].ToString() == table.TableName)
                    {
                        Column col = new Column();
                        col.TableName = dr2["TableName"].ToString();
                        col.ColName = dr2["ColName"].ToString();
                        col.Is_Primary_Key = (dr2["Is_Primary_Key"] is DBNull) ? false : Convert.ToBoolean(dr2["Is_Primary_Key"]);
                        col.Length = Convert.ToInt32(dr2["Length"]);
                        col.TypeName = dr2["TypeName"].ToString();
                        col.DescValue = dr2["DescValue"].ToString();
                        col.Is_Identity = Convert.ToBoolean(dr2["Is_Identity"]);
                        table.Columns.Add(col);
                    }
                }
                table.Definition = ScriptManager.GetTableScript(ServerIP, UID, PWD, DataBaseName, table.TableName);
                Tables.Add(table);
            }
        }

        public static List<Tab> GetTablesList(string connstr)
        {
            List<Tab> list = new List<Tab>();
            SqlExecuter Executer = new SqlExecuter(connstr);
            DataTable dttables = Executer.QueryTable(AppResource.tables);
            DataTable dtcols = Executer.QueryTable(AppResource.cols);
            foreach (DataRow dr in dttables.Rows)
            {
                Tab table = new Tab();
                table.TableName = dr["TABLE_NAME"].ToString();
                foreach (DataRow dr2 in dtcols.Rows)
                {
                    if (dr2["tablename"].ToString() == table.TableName)
                    {
                        Column col = new Column();
                        col.TableName = dr2["TableName"].ToString();
                        col.ColName = dr2["ColName"].ToString();
                        col.Is_Primary_Key = (dr2["Is_Primary_Key"] is DBNull) ? false : Convert.ToBoolean(dr2["Is_Primary_Key"]);
                        col.Length = Convert.ToInt32(dr2["Length"]);
                        col.TypeName = dr2["TypeName"].ToString();
                        col.DescValue = dr2["DescValue"].ToString();
                        col.Is_Identity = Convert.ToBoolean(dr2["Is_Identity"]);
                        table.Columns.Add(col);
                    }
                }
                list.Add(table);
            }
            return list;
        }

        public void GetViews()
        {
            SqlExecuter Executer = new SqlExecuter(ConnectionString);
            DataTable dtviews = Executer.QueryTable(AppResource.views);
            DataTable dtviewcols = Executer.QueryTable(AppResource.viewcols);
            foreach (DataRow dr in dtviews.Rows)
            {
                View view = new View();
                view.ViewName = dr["TABLE_NAME"].ToString();
                try
                {
                    Executer.QueryTable("select 1 from " + view.ViewName);
                }
                catch
                {
                    continue;
                }
                foreach (DataRow dr2 in dtviewcols.Rows)
                {
                    if (dr2["ViewName"].ToString() == view.ViewName)
                    {
                        Column col = new Column();
                        col.TableName = dr2["ViewName"].ToString();
                        col.ColName = dr2["ColName"].ToString();
                        col.Length = Convert.ToInt32(dr2["Length"]);
                        col.TypeName = dr2["TypeName"].ToString();
                        view.Columns.Add(col);
                    }
                }
                view.Definition = ScriptManager.GetViewScript(ServerIP, UID, PWD, DataBaseName, view.ViewName);
                Views.Add(view);
            }
        }

        public void GetForeignKeys()
        {
            SqlExecuter Executer = new SqlExecuter(ConnectionString);
            DataTable dt = Executer.QueryTable(AppResource.fks);
            foreach (DataRow dr in dt.Rows)
            {
                ForeignKey fk = new ForeignKey();
                fk.FK_Col = dr["FK_Col"].ToString();
                fk.FK_Name = dr["FK_Name"].ToString();
                fk.FK_Tab_Name = dr["Parent_Tab_Name"].ToString();
                fk.PK_Col = dr["PK_Col"].ToString();
                fk.PK_Tab_Name = dr["Referenced_Tab_Name"].ToString();
                fk.Update_Referential_Action = Convert.ToBoolean(dr["Update_Referential_Action"]);
                fk.Delete_Referential_Action = Convert.ToBoolean(dr["Delete_Referential_Action"]);

                fk.Definition = string.Format("alter table {0} add constraint {1} foreign key ({2}) references {3}({4}) ",
                    fk.FK_Tab_Name,
                    fk.FK_Name,
                    fk.FK_Col,
                    fk.PK_Tab_Name,
                    fk.PK_Col
                    );
                fk.Definition += (fk.Update_Referential_Action ? " on update cascade" : "") + (fk.Delete_Referential_Action ? " on delete cascade " : "");
                ForeignKeys.Add(fk);
            }
        }

        public void GetProcs()
        {
            SqlExecuter Executer = new SqlExecuter(ConnectionString);
            DataTable dt = Executer.QueryTable(AppResource.procs);
            foreach (DataRow dr in dt.Rows)
            {
                Proc proc = new Proc();
                proc.ProcName = dr["ProcName"].ToString();
                DataTable dt2 = Executer.QueryTable("exec sp_helptext " + proc.ProcName);
                foreach (DataRow drow in dt2.Rows)
                {
                    proc.Definition += drow[0].ToString();
                }
                Procs.Add(proc);
            }
        }

        public void GetTriggers()
        {
            SqlExecuter Executer = new SqlExecuter(ConnectionString);
            DataTable dt = Executer.QueryTable(AppResource.triggers);
            foreach (DataRow dr in dt.Rows)
            {
                Trigger tri = new Trigger();
                tri.TriggerName = dr["TriggerName"].ToString();
                DataTable dt2 = Executer.QueryTable("exec sp_helptext " + tri.TriggerName);
                foreach (DataRow drow in dt2.Rows)
                {
                    tri.Definition += drow[0].ToString();
                }
                Triggers.Add(tri);
            }
        }

        public void GetIndexs()
        {
            SqlExecuter Executer = new SqlExecuter(ConnectionString);
            DataTable dt = Executer.QueryTable(AppResource.indexs);
            foreach (DataRow dr in dt.Rows)
            {
                Index inx = new Index();
                inx.Tab_Name = dr["Tab_Name"].ToString();
                inx.Co_Names = dr["Co_Names"].ToString();
                inx.Index_Name = dr["Index_Name"].ToString();
                inx.Is_Disabled = Convert.ToBoolean(dr["Is_Disabled"]);
                inx.Is_Primary_Key = Convert.ToBoolean(dr["Is_Primary_Key"]);
                inx.Is_Unique = Convert.ToBoolean(dr["Is_Unique"]);
                inx.ClusterStr = dr["ClusterStr"].ToString();
                if (!inx.Is_Primary_Key)
                {
                    string unique = inx.Is_Unique ? "unique" : "";
                    inx.Definition = "create " + unique + " " + inx.ClusterStr + " index " + inx.Index_Name + " on " + inx.Tab_Name + "(" + inx.Co_Names + ")";
                    Indexs.Add(inx);
                }
            }
        }

        public void GetFuncs()
        {
            SqlExecuter Executer = new SqlExecuter(ConnectionString);
            DataTable dt = Executer.QueryTable(AppResource.funcs);
            foreach (DataRow dr in dt.Rows)
            {
                Func func = new Func();
                func.FuncName = dr["FuncName"].ToString();
                DataTable dt2 = Executer.QueryTable("exec sp_helptext " + func.FuncName);
                foreach (DataRow drow in dt2.Rows)
                {
                    func.Definition += drow[0].ToString();
                }
                Funcs.Add(func);
            }
        }

        public Column[] GetPK(string tabname)
        {
            List<Column> PK = new List<Column>();
            foreach (Tab tab in this.Tables)
            {
                if (tab.TableName == tabname)
                {
                    foreach (Column col in tab.Columns)
                    {
                        if (col.Is_Primary_Key)
                        {
                            PK.Add(col);
                        }
                    }
                }
            }
            return PK.ToArray();
        }

        public Column[] GetCols(string tabname)
        {
            List<Column> cols = new List<Column>();
            foreach (Tab tab in this.Tables)
            {
                if (tab.TableName == tabname)
                {
                    foreach (Column col in tab.Columns)
                    {

                        cols.Add(col);
                    }
                }
            }
            return cols.ToArray();
        }
    }
}
