@echo off

set uploaddir=python

( 
    echo ===============================================================
    echo  GoAgent����˲������, ��ʼ�ϴ�%uploaddir%�����
    echo ===============================================================
    echo.
    echo ����������appid, ���appid����^|�Ÿ���
) && (
    @cd /d "%~dp0" 
) && (
    if exist ".appcfg_cookies" (@del /f /q .appcfg_cookies)
) && (
    set PYTHONSCRIPT="import sys;sys.path.insert(0, 'uploader.zip');import appcfg;appcfg.main()"
) && (
    "..\local\proxy.exe"
) && (
    echo.
    echo �ϴ��ɹ��������Exception KeyError����, �༭proxy.ini�����appid���ȥ��лл���밴������˳�����
)

@pause>NUL

  
  
@echo off