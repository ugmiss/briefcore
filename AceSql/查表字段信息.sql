select         STUFF(Col.Name,1,1,UPPER(SUBSTRING(Col.Name,1,1))) [propname],
isnull(identity_columns.is_identity ,0) is_identity, isnull(pk.is_primary_key,0) pk,Col.is_nullable is_nullable,Type.name typename,
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
                 WHEN Type.name = 'datetime' THEN 'DateTime'
                 WHEN Type.name = 'smalldatetime' THEN 'DateTime'
                 WHEN Type.name = 'bit' THEN 'bool'
                 WHEN Type.name = 'sql_variant' THEN 'object'
                 ELSE Type.name
                 END) [type] 
                 from sys.objects Tab inner join sys.columns Col on Tab.object_id =Col.object_id
                 inner join sys.types Type on Col.system_type_id = Type.system_type_id
                 left join sys.identity_columns identity_columns on  Tab.object_id = identity_columns.object_id and Col.column_id = identity_columns.column_id
                 left join(
                 select index_columns.object_id,index_columns.column_id,indexes.is_primary_key 
                 from sys.indexes  indexes inner join sys.index_columns index_columns 
                 on indexes.object_id = index_columns.object_id and indexes.index_id = index_columns.index_id
                 where indexes.is_primary_key = 1
               ) PK on Tab.object_id = PK.object_id AND Col.column_id = PK.column_id
               where Type.Name <> 'sysname' and (Tab.type = 'U' or Tab.type='V')  and Tab.Name<>'sysdiagrams'