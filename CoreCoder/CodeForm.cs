﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace CoreCoder
{
    public partial class CodeForm : Form
    {
        public CodeForm()
        {
            InitializeComponent();
        }
        string namespacestr = "Business";
        SqlExecuter exec;
        private void button1_Click(object sender, EventArgs e)
        {
            exec = new SqlExecuter(this.textBox1.Text);
            if (!textBox2.Text.IsEmpty())
                namespacestr = textBox2.Text;
            try
            {
                if (Directory.Exists(namespacestr.AppPath()))
                    Directory.Delete(namespacestr.AppPath(), true);

                Directory.CreateDirectory(namespacestr.AppPath());
                Thread.Sleep(50);

                string sqltable = "SELECT TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE FROM INFORMATION_SCHEMA.TABLES WHERE Table_name<>'sysdiagrams' and (Table_Type='BASE TABLE' or  Table_Type='View') and ISNULL(OBJECTPROPERTY(OBJECT_ID(TABLE_NAME), 'IsMSShipped'), 0) = 0 ORDER BY TABLE_NAME";
                foreach (DataRow dr in exec.QueryDataTable(sqltable).Rows)
                {
                    bool isview = !(dr["TABLE_TYPE"].ToString() == "BASE TABLE");
                    string tablename = dr["TABLE_NAME"].ToString();
                    Gen(tablename.FirstUpper(), isview);
                }
                MessageBox.Show("生成完毕。");
                Process.Start(AppDomain.CurrentDomain.BaseDirectory);
            }
            catch
            {
                MessageBox.Show("生成失败，重新生成。");
            }
        }
        void Gen(string tablename, bool isview)
        {
            List<string> fields = new List<string>();
            string sql =
                 @"select isnull(identity_columns.is_identity ,0) is_identity,
                 isnull(pk.is_primary_key,0) pk,Col.is_nullable is_nullable,
                 (CASE
                 WHEN Type.name = 'uniqueidentifier' THEN 'string'
                 WHEN Type.name = 'char' THEN 'string'
                 WHEN Type.name = 'nchar' THEN 'string'
                 WHEN Type.name = 'varchar' THEN 'string'
                 WHEN Type.name = 'nvarchar' THEN 'string'
                 WHEN Type.name = 'text' THEN 'string'
                 WHEN Type.name = 'ntext' THEN 'string'
                 WHEN Type.name = 'xml' THEN 'string'
                 WHEN Type.name = 'image' THEN 'byte[]'
                 WHEN Type.name = 'timestamp' THEN 'byte[]'
                 WHEN Type.name = 'binary' THEN 'byte[]'
                 WHEN Type.name = 'varbinary' THEN 'byte[]'
                 WHEN Type.name = 'tinyint' THEN 'byte'
                 WHEN Type.name = 'int' THEN 'int'
                 WHEN Type.name = 'smallint' THEN 'short'
                 WHEN Type.name = 'bigint' THEN 'long'
                 WHEN Type.name = 'float' THEN 'double'
                 WHEN Type.name = 'real' THEN 'float'
                 WHEN Type.name = 'money' THEN 'decimal'
                 WHEN Type.name = 'smallmoney' THEN 'decimal'
                 WHEN Type.name = 'decimal' THEN 'decimal'
                 WHEN Type.name = 'numeric' THEN 'decimal'
                 WHEN Type.name = 'time' THEN 'DateTime'
                 WHEN Type.name = 'datetime' THEN 'DateTime'
                 WHEN Type.name = 'smalldatetime' THEN 'DateTime'
                 WHEN Type.name = 'bit' THEN 'bool'
                 WHEN Type.name = 'sql_variant' THEN 'object'
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
               where Type.Name <> 'sysname' and (Tab.type = 'U' or Tab.type='V')  and Tab.Name<>'sysdiagrams' and Tab.Name='" + tablename + "'";
            string code =
@"using System;
using System.ComponentModel;
using System.Runtime.Serialization;

/* Code Generate Time " + DateTime.Now.ToLocalTime() + @"*/
namespace {0}
{{
    {3}
    public class {1}
    {{
{2}
    }}
            //        StaticDataProvider.cs  
            //        public {1}[] GetAll{1}()
            //        {{
            //            return logic.GetAll<{1}>().ToArray();
            //        }}
            //        public void Add{1}({1} model)
            //        {{
            //            logic.Add<{1}>(model);
            //        }}
            //        public void Delete{1}({1} model)
            //        {{
            //            logic.Delete<{1}>(model);
            //        }}
            //        public void Modify{1}({1} model)
            //        {{
            //            logic.Modify<{1}>(model);
            //        }}

            //        IStaticDataProvider.cs  
            //        {1}[] GetAll{1}();             
            //        void Add{1}({1} model);
            //        void Delete{1}({1} model);
            //        void Modify{1}({1} model);

            //        BusinessManager.cs
            //        public static {1}[] GetAll{1}()
            //        {{
            //            return s_staticDataProvider.GetAll{1}();
            //        }}
            //        public static void Add{1}({1} model)
            //        {{
            //            s_staticDataProvider.Add{1}(model);
            //        }}
            //        public static void Delete{1}({1} model)
            //        {{
            //            s_staticDataProvider.Delete{1}(model);
            //        }}
            //        public static void Modify{1}({1} model)
            //        {{
            //            s_staticDataProvider.Modify{1}(model);
            //        }}

            //        IWCFService.cs
            //        [OperationContract]
            //        {1}[] GetAll{1}();
            //        [OperationContract]
            //        void Add{1}({1} model);
            //        [OperationContract]
            //        void Delete{1}({1} model);
            //        [OperationContract]
            //        void Modify{1}({1} model);

            //        WCFServiceHandler.cs
            //        public {1}[] GetAll{1}()
            //        {{
            //            return BusinessManager.GetAll{1}();
            //        }}
            //        public void Add{1}({1} model)
            //        {{
            //            BusinessManager.Add{1}(model);
            //        }}
            //        public void Delete{1}({1} model)
            //        {{
            //            BusinessManager.Delete{1}(model);
            //        }}
            //        public void Modify{1}({1} model)
            //        {{
            //            BusinessManager.Modify{1}(model);
            //        }}


            //        WCFClient.cs
            //        public {1}[] GetAll{1}()
            //        {{
            //            return client.GetAll{1}();
            //        }}
            //        public void Add{1}({1} model)
            //        {{
            //            client.Add{1}(model);
            //        }}
            //        public void Delete{1}({1} model)
            //        {{
            //            client.Delete{1}(model);
            //        }}
            //        public void Modify{1}({1} model)
            //        {{
            //            client.Modify{1}(model);
            //        }}
}}";

           

            

          


            
            string fieldstr = "";
            foreach (DataRow dr in exec.QueryDataTable(sql).Rows)
            {
                string publicName = dr["propname"].ToString();
                bool pk = Convert.ToBoolean(dr["pk"]);
                bool is_identity = Convert.ToBoolean(dr["is_identity"]);
                bool is_nullable = Convert.ToBoolean(dr["is_nullable"]);
                string typestr = dr["type"].ToString();
                string nullable = is_nullable && typestr != "object" && typestr != "string" && typestr != "byte[]" ? "?" : "";

                List<string> desc = new List<string>();
                if (pk)
                    desc.Add("IsPrimaryKey");
                if (is_identity)
                    desc.Add("IsIdentity");
                if (desc.Count > 0)
                    fieldstr += "        [Description(\"" + string.Join(",", desc) + "\")]\r\n";

                fieldstr += "        [DataMember]\r\n";
                fieldstr += "        public " + typestr + nullable + " " + publicName + " { set; get; }\r\n";
            }
            fieldstr = fieldstr.TrimEnd();
            string istborview = !isview ? "[Description(\"IsTable\")]\r\n" : "[Description(\"IsView\")]\r\n";
            istborview += "    [DataContract]";
            code.FormatWith(namespacestr, tablename, fieldstr, istborview).WriteToFile((namespacestr + "\\" + tablename + ".cs").AppPath());



        }


    }
}
