--查询用户表对象信息
select  Tab.Name as tablename,
        Col.Name  as colname ,Type.name as typename,Col.max_length as length,
        pk.is_primary_key as is_primary_key,
        Col.is_identity as is_identity,
        identity_columns.seed_value as seed_value,
        identity_columns.increment_value as increment_value,
        Col.is_nullable  AS is_nullable,
        Def.text as defaulttext,
        Col.is_computed as is_computed ,
        computed_columns.definition as definition,
        Col_Desc.Value as DescValue
from sys.objects Tab inner join sys.columns Col on Tab.object_id =Col.object_id
inner join sys.types Type on Col.system_type_id = Type.system_type_id
left join sys.identity_columns identity_columns on  Tab.object_id = identity_columns.object_id and Col.column_id = identity_columns.column_id
left join syscomments Def  on Col.default_object_id = Def.ID
left join(
    select index_columns.object_id,index_columns.column_id,indexes.is_primary_key 
    from sys.indexes  indexes inner join sys.index_columns index_columns 
    on indexes.object_id = index_columns.object_id and indexes.index_id = index_columns.index_id
    where indexes.is_primary_key = 1/*主键*/
) PK on Tab.object_id = PK.object_id AND Col.column_id = PK.column_id
left join sys.computed_columns  computed_columns on Tab.object_id =computed_columns.object_id and Col.column_id = computed_columns.column_id
left join sys.extended_properties Col_Desc on Col_Desc.major_id=Tab.object_id and Col_Desc.minor_id=Col.Column_id and Col_Desc.class=1 
where Type.Name <> 'sysname' and Tab.type = 'U' and Tab.Name<>'sysdiagrams'
order by Tab.Name
