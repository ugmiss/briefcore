--查询外键约束
select FK_Name ,Parent_Tab_Name ,delete_referential_action,update_referential_action,[FK_Col]=stuff
(
    (select ','+[Parent_Col_Name]
		from 
		(
        select    FK.name as FK_Name,Parent_Tab.Name as Parent_Tab_Name,Parent_Col.Name as Parent_Col_Name,
                Referenced_Tab.Name as Referenced_Tab_Name,Referenced_Col.Name as Referenced_Col_Name,FK.delete_referential_action,FK.update_referential_action
        from sys.foreign_keys FK
        inner join sys.foreign_key_columns Col on FK.Object_ID = Col.constraint_object_id
        inner join sys.objects Parent_Tab ON  Col.parent_object_id = Parent_Tab.Object_ID and Parent_Tab.TYPE = 'U'
        inner join sys.columns Parent_Col on Parent_Tab.Object_ID = Parent_Col.object_id 
                                            and  Col.parent_column_id = Parent_Col.column_id
        inner join sys.objects Referenced_Tab ON  Col.referenced_object_id = Referenced_Tab.Object_ID and Referenced_Tab.TYPE = 'U'
        inner join sys.columns Referenced_Col on Referenced_Tab.Object_ID = Referenced_Col.object_id 
                                            and  Col.referenced_column_id = Referenced_Col.column_id
		) t 
		where FK_Name=tb.FK_Name  and Parent_Tab_Name = tb.Parent_Tab_Name and Referenced_Tab_Name = tb.Referenced_Tab_Name   for xml path('')
	), 1, 1, ''
),
    Referenced_Tab_Name,
    [PK_Col]=stuff
    (
        (
		     select ','+[Referenced_Col_Name] from 
             (
                select    FK.name as FK_Name,Parent_Tab.Name as Parent_Tab_Name,Parent_Col.Name as Parent_Col_Name,
                Referenced_Tab.Name as Referenced_Tab_Name,Referenced_Col.Name as Referenced_Col_Name,delete_referential_action,update_referential_action
				from sys.foreign_keys FK
				inner join sys.foreign_key_columns Col on FK.Object_ID = Col.constraint_object_id
				inner join sys.objects Parent_Tab ON  Col.parent_object_id = Parent_Tab.Object_ID and Parent_Tab.TYPE = 'U'
				inner join sys.columns Parent_Col on Parent_Tab.Object_ID = Parent_Col.object_id 
													and  Col.parent_column_id = Parent_Col.column_id
				inner join sys.objects Referenced_Tab ON  Col.referenced_object_id = Referenced_Tab.Object_ID and Referenced_Tab.TYPE = 'U'
				inner join sys.columns Referenced_Col on Referenced_Tab.Object_ID = Referenced_Col.object_id 
													and  Col.referenced_column_id = Referenced_Col.column_id
		     )t
             where FK_Name=tb.FK_Name  and Parent_Tab_Name = tb.Parent_Tab_Name and Referenced_Tab_Name = tb.Referenced_Tab_Name   for xml path('')
        )
        , 1, 1, ''
    )
    --as [外键列]
    
from (
    select    FK.name as FK_Name,Parent_Tab.Name as Parent_Tab_Name,Parent_Col.Name as Parent_Col_Name,
            Referenced_Tab.Name as Referenced_Tab_Name,Referenced_Col.Name as Referenced_Col_Name,delete_referential_action,update_referential_action
    from sys.foreign_keys FK
    inner join sys.foreign_key_columns Col on FK.Object_ID = Col.constraint_object_id
    inner join sys.objects Parent_Tab ON  Col.parent_object_id = Parent_Tab.Object_ID and Parent_Tab.TYPE = 'U'
    inner join sys.columns Parent_Col on Parent_Tab.Object_ID = Parent_Col.object_id 
                                        and  Col.parent_column_id = Parent_Col.column_id
    inner join sys.objects Referenced_Tab ON  Col.referenced_object_id = Referenced_Tab.Object_ID and Referenced_Tab.TYPE = 'U'
    inner join sys.columns Referenced_Col on Referenced_Tab.Object_ID = Referenced_Col.object_id 
                                        and  Col.referenced_column_id = Referenced_Col.column_id
)tb
group by FK_Name,Parent_Tab_Name,Referenced_Tab_Name,delete_referential_action,update_referential_action



