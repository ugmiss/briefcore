/* �ַ����� */
/* �����ַ����ʽ��������ַ���ASCII����ֵ */
select Ascii('a') --a:97,A:65
 
/* ������ASCII����ת��Ϊ�ַ� */
select Char(97)--97:a,65:A
 
/* ���ر��ʽ��ָ���ַ��Ŀ�ʼλ�� */
select Charindex('b','abcdefg',5)
 
/* ���������������ַ����ʽ��SOUNDEXֵ֮�� */
select Difference('bet','bit')--3
 
/* �����ַ����ʽ�����ָ����Ŀ���ַ� */
select Left('abcdefg',3)--abc
 
/* ���ظ����ַ��������ַ��� */
select Len('abcdefg')--7
 
/* ���ؽ���д�ַ�ת��ΪС�ַ����ַ����ʽ */
select Lower('ABCDEFG')--abcdefg
 
/* ����ɾ����ǰ���ո�֮���ַ����ʽ */
select Ltrim('���� abcdefg')--abcdefg
 
/* ���ؾ��и��������������UNICODE�ַ� */
select Nchar(65)--A
 
/* ����ָ�����ʽ��ģʽ��һ�γ��ֵĿ�ʼλ�� */
select Patindex('%_cd%','abcdefg')--2
 
/* ����Ϊ��Ϊ��Ч��SQL SERVER�ָ���ʶ��������˷ָ�����UNICODE�ַ��� */
select Quotename('create table')
 
/* �õ��������ʽ�滻��һ�����ʽ�г��ֵĵڶ������ʽ */
select Replace('abcdefg','cd','xxx')--abxxxefg
 
/* ��ָ�������ظ����ʽ */
select Replicate('abc|',4)--abc|abc|abc|abc|
 
/* �����ַ����ʽ��������ʽ */
select Reverse('abc')--cba
 
/* �����ַ����ʽ�Ҳ�ָ����Ŀ���ַ� */
select Right('abcd',3)--bcd
 
/* ���ؽض�������β��ո�֮����ַ����ʽ */
select Rtrim('abcd������ ')--abcd
 
/* �������ĸ��ַ�����SOUNDEX���� */
select Soundex('abcd')--A120
 
/* �������ظ��ո���ɵ��ַ��� */
select Space(10)--[������������������ ]
 
/* ���ش�Ĭ�ϱ��ת���������ַ��� */
select Str(100)--[������������ 100]
 
/*�� */
select Str(100,3)--[100]
 
/*�� */
select Str(14.4444,5,4)--[14.44]
 
/* ɾ��ָ�����ȵ��ַ�,����ָ������㴦������һ���ַ� */
select Stuff('abcdefg',2,4,'xxx')--axxxfg
 
/* �����ַ����ʽ,������,�ı����ʽ��ͼ�����һ���� */
select Substring('abcdefg',2,3)--bcd
 
/* ���ر���һ���ַ���UNICODE����ֵ */
select Unicode('a')--97
 
/* ���ؽ�Сд�ַ�ת��Ϊ��д�ַ����ַ����ʽ */
select Upper('a')--'A'
