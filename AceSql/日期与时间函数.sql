--��ǰϵͳ���ڡ�ʱ�� 
SELECT GETDATE()  
--DATEADD  ����ָ�����ڼ���һ��ʱ��Ļ����ϣ������µ� datetime ֵ;���磺�����ڼ���2�� 
SELECT DATEADD(DAY,2,'2004-10-15')  --���أ�2004-10-17 00:00:00.000
--DATEDIFF ���ؿ�����ָ�����ڵ����ں�ʱ��߽�����
SELECT DATEDIFF(DAY,'2004-09-01','2004-09-18')   --���أ�17
--DATEPART ���ش���ָ�����ڵ�ָ�����ڲ��ֵ�������
SELECT DATEPART(MONTH, '2004-10-15')  --���� 10
--DATENAME ���ش���ָ�����ڵ�ָ�����ڲ��ֵ��ַ�����
SELECT DATENAME(WEEKDAY, '2004-10-15')  --���أ�������
--DAY(), month(),year() --������datepart����һ�£�
SELECT ��ǰ����=CONVERT(VARCHAR(10),GETDATE(),120) 
,��ǰʱ��=CONVERT(VARCHAR(8),GETDATE(),114) 
SELECT DATENAME(dw,'2004-10-15') 
SELECT ����ڶ�����=DATENAME(WEEK,'2004-10-15')
       ,�������ܼ�=DATENAME(WEEKDAY,'2004-10-15')
--GETDATE()  ����ϵͳĿǰ��������ʱ�� 
--DATEDIFF (interval,date1,date2) ��interval ָ���ķ�ʽ������date2 ��date1��������֮��Ĳ�ֵ date2-date1 
--DATEADD (interval,number,date) ��intervalָ���ķ�ʽ������number֮������� 
--DatePart (interval,date) ��������date�У�intervalָ����������Ӧ������ֵ 
--DATENAME (interval,date) ��������date�У�intervalָ����������Ӧ���ַ������� 
--���� interval���趨ֵ���£�
--ֵ �� д��Sql Server�� (Access �� ASP) ˵�� 
--Year Yy yyyy �� 1753 ~ 9999 
--Quarter Qq q   �� 1 ~ 4 
--Month Mm m   ��1 ~ 12 
--DAY of year Dy y  һ�������,һ���еĵڼ��� 1-366 
--DAY Dd d   �գ�1-31 
--WeekDAY Dw w һ�ܵ�������һ���еĵڼ��� 1-7 
--Week Wk ww  �ܣ�һ���еĵڼ��� 0 ~ 51 
--Hour Hh h   ʱ0 ~ 23 
--Minute Mi n  ����0 ~ 59 
--Second Ss s �� 0 ~ 59 
--Millisecond Ms - ���� 0 ~ 999 
--һ���µ�һ���
SELECT DATEADD(mm, DATEDIFF(mm,0,GETDATE()), 0)
--���ܵ�����һ
SELECT DATEADD(wk, DATEDIFF(wk,0,GETDATE()), 0)
--һ��ĵ�һ��
SELECT DATEADD(yy, DATEDIFF(yy,0,GETDATE()), 0)
--���ȵĵ�һ��
SELECT DATEADD(qq, DATEDIFF(qq,0,GETDATE()), 0)
--����İ�ҹ
SELECT DATEADD(dd, DATEDIFF(dd,0,GETDATE()), 0)
--�ϸ��µ����һ��
SELECT DATEADD(ms,-3,DATEADD(mm, DATEDIFF(mm,0,GETDATE()), 0))
--ȥ������һ��
SELECT DATEADD(ms,-3,DATEADD(yy, DATEDIFF(yy,0,GETDATE()), 0))
--���µ����һ��
SELECT DATEADD(ms,-3,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1, 0))
--��������һ��
SELECT DATEADD(ms,-3,DATEADD(yy, DATEDIFF(yy,0,GETDATE())+1, 0))
--���µĵ�һ������һ
SELECT DATEADD(wk, DATEDIFF(wk,0,DATEADD(dd,6-datepart(DAY,GETDATE()),GETDATE())), 0) 

 
