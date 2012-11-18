--查询所有触发器
select triggers.name as TriggerName,tables.name as TableName,triggers.is_disabled as Is_Disabled,
triggers.is_instead_of_trigger AS Is_Instead_Of_Trigger,
case when triggers.is_instead_of_trigger = 1 then 'INSTEAD OF'
     when triggers.is_instead_of_trigger = 0 then 'AFTER'
     else null
end as Detail
from sys.triggers triggers
inner join sys.tables tables on triggers.parent_id = tables.object_id
where triggers.type ='TR'
order by triggers.create_date

