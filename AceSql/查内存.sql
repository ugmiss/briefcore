SELECT TOP 50  
        qs.total_physical_reads ,  
        qs.execution_count ,  
        qs.total_physical_reads / qs.execution_count AS [Avg IO] ,  
        SUBSTRING(qt.text, qs.statement_start_offset / 2,  
                  ( CASE WHEN qs.statement_end_offset = -1  
                         THEN LEN(CONVERT(NVARCHAR(MAX), qt.text)) * 2  
                         ELSE qs.statement_end_offset  
                    END - qs.statement_start_offset ) / 2) AS query_text ,  
        qt.dbid ,  
        dbname = DB_NAME(qt.dbid) ,  
        qt.objectid ,  
        qs.sql_handle ,  
        qs.plan_handle  
FROM    sys.dm_exec_query_stats qs  
        CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) AS qt  
ORDER BY qs.total_physical_reads DESC   

SELECT  total_physical_memory_kb / 1024 AS [物理内存(MB)] ,  
        available_physical_memory_kb / 1024 AS [可用物理内存(MB)] ,  
        system_cache_kb / 1024 AS [系统缓存内存总量(MB)] ,  
        ( kernel_paged_pool_kb + kernel_nonpaged_pool_kb ) / 1024 AS [内核池内存总量(MB)] ,  
        total_page_file_kb / 1024 AS [操作系统报告的提交限制的大小(MB)] ,  
        available_page_file_kb / 1024 AS [未使用的页文件的总量(MB)] ,  
        system_memory_state_desc AS [内存状态说明]  
FROM    sys.dm_os_sys_memory 

SELECT TOP 50  
        qs.total_logical_reads ,  
        qs.execution_count ,  
        qs.total_logical_reads / qs.execution_count AS [Avg IO] ,  
        SUBSTRING(qt.text, qs.statement_start_offset / 2,  
                  ( CASE WHEN qs.statement_end_offset = -1  
                         THEN LEN(CONVERT(NVARCHAR(MAX), qt.text)) * 2  
                         ELSE qs.statement_end_offset  
                    END - qs.statement_start_offset ) / 2) AS query_text ,  
        qt.dbid ,  
        dbname = DB_NAME(qt.dbid) ,  
        qt.objectid ,  
        qs.sql_handle ,  
        qs.plan_handle  
FROM    sys.dm_exec_query_stats qs  
        CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) AS qt  
ORDER BY qs.total_logical_reads DESC  


