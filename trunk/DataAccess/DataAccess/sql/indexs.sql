--查询所有索引
select    indexs.Tab_Name  as Tab_Name,indexs.Index_Name as Index_Name ,indexs.[Co_Names] as Co_Names,
        Ind_Attribute.is_primary_key as is_primary_key,Ind_Attribute.is_unique AS is_unique,
        Ind_Attribute.is_disabled AS is_disabled
 from (
    select Tab_Name,Index_Name, [Co_Names]=stuff((select ','+[Co_Name] from 
    (    select tab.Name as Tab_Name,ind.Name as Index_Name,Col.Name as Co_Name from sys.indexes ind 
        inner join sys.tables tab on ind.Object_id = tab.object_id and ind.type in (1,2)/*索引的类型：0=堆/1=聚集/2=非聚集/3=XML*/
        inner join sys.index_columns index_columns on tab.object_id = index_columns.object_id and ind.index_id = index_columns.index_id
        inner join sys.columns Col on tab.object_id = Col.object_id and index_columns.column_id = Col.column_id
    ) t where Tab_Name=tb.Tab_Name and Index_Name=tb.Index_Name  for xml path('')), 1, 1, '')
    from (
        select tab.Name as Tab_Name,ind.Name as Index_Name,Col.Name as Co_Name from sys.indexes ind 
        inner join sys.tables tab on ind.Object_id = tab.object_id and ind.type in (1,2)/*索引的类型：0=堆/1=聚集/2=非聚集/3=XML*/
        inner join sys.index_columns index_columns on tab.object_id = index_columns.object_id and ind.index_id = index_columns.index_id
        inner join sys.columns Col on tab.object_id = Col.object_id and index_columns.column_id = Col.column_id
    )tb
    where Tab_Name not like 'sys%'
    group by Tab_Name,Index_Name
) indexs inner join sys.indexes  Ind_Attribute on indexs.Index_Name = Ind_Attribute.name
order by indexs.Tab_Name

