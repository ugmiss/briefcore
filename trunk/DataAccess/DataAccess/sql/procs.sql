﻿--查询所有存储过程
select name as ProcName from sys.procedures
--select Pr_Name  as ProcName, Para=stuff((select ','+[Parameter] 
    --from (
        --select Pr.Name as Pr_Name,parameter.name +' ' +Type.Name + ' ('+convert(varchar(32),parameter.max_length)+')' as Parameter
        --from sys.procedures Pr left join 
        --sys.parameters parameter  on Pr.object_id = parameter.object_id
        --inner join sys.types Type on parameter.system_type_id = Type.system_type_id
        --where type = 'P' 
    --) t where Pr_Name=tb.Pr_Name for xml path('')), 1, 1, '')
--from (
    --select Pr.Name as Pr_Name,parameter.name +' ' +Type.Name + ' ('+convert(varchar(32),parameter.max_length)+')' as Parameter
    --from sys.procedures Pr left join 
    --sys.parameters parameter  on Pr.object_id = parameter.object_id
    --inner join sys.types Type on parameter.system_type_id = Type.system_type_id
    --where type = 'P' 
--)tb
--where Pr_Name not like 'sp_%' --and Pr_Name not like 'dt%'
--group by Pr_Name
--order by Pr_Name
