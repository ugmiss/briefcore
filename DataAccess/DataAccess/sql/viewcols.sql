--查询所有视图
select views.Name as ViewName,Col.Name as ColName ,Type.name as TypeName,Col.max_length as Length
        --,Col_Desc.Value as Col_Description
from sys.views views
inner join sys.columns Col on views.object_id  = Col.object_id
inner join sys.types Type on Col.system_type_id = Type.system_type_id
--left join sys.extended_properties Col_Desc 
--    on Col_Desc.major_id=views.object_id and Col_Desc.minor_id=Col.Column_id and Col_Desc.class=1 
order by Create_Date
