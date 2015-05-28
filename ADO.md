1 表过滤
```
DataTable dt；
dt.DefaultView.RowFilter= "id> '4 ' "; 
DataTable TempTable=dt.Clone(); 
for(int i=0;i <dt.DefaultView.Count;i++) 
{ 
DataRow row=TempTable.NewRow(); 
for(int j=0;j <TempTable.Columns.Count;j++) 
{ 
row[j]=dt.DefaultView[i][j]; 
} 
TempTable.Rows.Add(row); 
} 
dataGrid1.DataSource=TempTable;
```

2 如何清除 SQL Server 登录时记录的信息SQL Server 客户端登陆信息记录在
`%AppData%\Microsoft\Microsoft SQL Server\100\Tools\Shell\SqlStudio.bin （SQL Server 2008）`
`AppData%\Microsoft\Microsoft SQL Server\90\Tools\Shell\mru.dat（SQL Server 2005）`
文件中，直接删除后重新启动客户端就可以了。
`%AppData%` 是环境变量，可通过在命令行中运行 `ECHO %APPDATA%`查看。

3 sql查询库名及物理文件
```
select db.name,sf.name,sf.filename,sf.size from sys.sysaltfiles sf 
inner join sys.databases db on sf.dbid=db.database_id where
 db.database_id>4 and db.name <>'distribution' order by db.name
```

4 SQL SERVER 2008 阻止保存要求重新创建表的更改
提示： 不允许保存更改。您所做的更改要求删除并重新创建以下表。您对无法重新创建的标进行了更改或者启用了“阻止保存要求重新创建表的更改”选项。
> 如果要去掉此提示，打开SQL 2008 在最上面 工具-〉选项-〉左侧有个 设计器-〉表设计器和数据库设计器 -> 阻止保存要求重新创建表的更改(右侧) 把钩去重新启动2008即可。

5C#类型与sql类型
|bool|System.Boolean|1  bit|(bit)   0 到 1   |
|:---|:-------------|:-----|:-----------------|
|byte|System.Byte   |8  bit |(tinyint)  0 到 255|
|char|System.Char   |8  bit|1 byte            |
|short|System.Int16  |16 bit  |2 byte  (smallint)  -2<sup>15</sup> (-32,768) 到 2<sup>15</sup> - 1 (32,767)|
|int |System.Int32  |32 bit|4 byte (int) -2<sup>31</sup> (-2,147,483,648) 到 2<sup>31</sup> - 1 (2,147,483,647)|
|float|System.Single |32 bit|4 byte (real) 1bit 符号位 8bit 指数位 23bit 尾数位 -2<sup>128</sup>~+2<sup>128</sup> 同 -3.40E+38~+3.40E+38|
|long|System.Int64  | 64 bit | 8 byte  (bigint)  -2<sup>63</sup>(-9223372036854775808) 到 2<sup>63</sup>-1(9223372036854775807)|
|System.DateTime |System.DateTime | 64 bit | 8 byte (smalldatetime,datetime)    (datetime)  1753年1月1日 到 9999年12月31日 的日期和时间数据,精确到百分之三秒(或 3.33 毫秒) (smalldatetime)  1900年1月1日 到 2079年 6月 6日 的日期和时间数据,精确到分钟|
|double|System.Double |64 bit|8 byte  (float) -1.79E+308 到 1.79E+308 的浮点精度数字|
|decimal|System.Decimal|96 bit|12byte  (decimal,momey,numeric,smallmoney)                                      1bit 符号位 11bit 指数位 52bit 尾数位 -2<sup>1024</sup>~+2<sup>1024</sup> 同 -1.79E+308~+1.79E+308 (decimal,numeric)  -10<sup>38+1</sup> 到 10<sup>38-1</sup>  (money) -2<sup>63</sup> (-922,337,203,685,477.5808) 与 2<sup>63</sup> - 1 (+922,337,203,685,477.5807) (smallmoney) -214,748.3648 与 +214,748.3647 之间,精确到货币单位的千分之十|
| System.Guid |       128bit | 16byte  |(uniqueidentifier)|
|byte[.md](.md)  | System.Byte[.md](.md)|N/A   |(binary,image,timestamp,varbinary)(binary)固定长度的二进制数据,其最大长度为 8,000 个字节(varbinary)可变长度的二进制数据,其最大长度为 8,000 个字节(image)可变长度的二进制数据,其最大长度为2<sup>31</sup> - 1 (2,147,483,647)个字节(timestamp)数据库范围的唯一数字,每次更新行时也进行更新|
|string  | System.String|N/A   |(char,nchar,text,ntext,varchar,nvarchar,xml)(char) 固定长度的非 Unicode 字符数据,1字符1字节,长度不足时自动补空(nchar)1字符占2字节,数据长度不足自动补空(varchar)可变长度的非 Unicode 数据,最长为 8,000 个字符(nvarchar)可变长度 Unicode 数据,其最大长度为 4,000 字符 (nvarchar(max))可变长度 Unicode 数据, 在 SQL 2005+ 以后用来取代ntext(text)可变长度的非 Unicode 数据,最大长度为 2<sup>31</sup> - 1 (2,147,483,647) 个字符 1字符占1字节,最大2GB(ntext)可变长度 Unicode 数据,其最大长度为 2<sup>30 - 1</sup> (1,073,741,823) 个字符|
|object|System.Object |N/A   |(sql\_variant)    |

6 SqlBulkCopy
```
conn.Open();
SqlBulkCopyColumnMapping mapping1 = new SqlBulkCopyColumnMapping("DataPointID", "DataPointID");
SqlBulkCopyColumnMapping mapping2 = new SqlBulkCopyColumnMapping("TimeStamp", "TimeStamp");
SqlBulkCopyColumnMapping mapping3 = new SqlBulkCopyColumnMapping("DataValue", "DataValue");
SqlBulkCopyColumnMapping mapping4 = new SqlBulkCopyColumnMapping("Status", "Status");
SqlBulkCopy bulkCopy = new SqlBulkCopy(conn);
bulkCopy.BatchSize = 10000;
bulkCopy.ColumnMappings.Add(mapping1);
bulkCopy.ColumnMappings.Add(mapping2);
bulkCopy.ColumnMappings.Add(mapping3);
bulkCopy.ColumnMappings.Add(mapping4);
bulkCopy.DestinationTableName = "dbo.DP_GeneralData";
bulkCopy.WriteToServer(dt);

```