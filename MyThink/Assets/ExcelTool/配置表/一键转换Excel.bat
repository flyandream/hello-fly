@echo off
set "execlPath=%cd%\DataExcel"
if exist %execlPath% (echo Excel�ļ�·����%execlPath%)else (echo �Ҳ���·��%execlPath%,�˳�&goto:End)
set "tableTxtPath=%cd%\TableTxt"
if exist %tableTxtPath% (echo Txt�ļ�·����%tableTxtPath%)else (echo �Ҳ���·��%tableTxtPath%,�˳�&goto:End)
echo.
echo ----------------------·����ȡ�ɹ�----------------------
:loop
echo.
echo ************************************************
echo *  all��ת��ȫ�����ļ�������һת�� exit:�˳�   *
echo ************************************************
set /p comm=
if %comm% equ all ( start /wait CScript ReadExcel.vbs all & start /wait ChangeCoding.exe "all" &goto loop)
if %comm% equ exit (exit)
set "excelFile=%execlPath%\%comm%.xlsx"
rem echo ���:%excelFile%...
if exist %excelFile% (echo ת����%excelFile% & start /wait CScript ReadExcel.vbs %comm% & start /wait ChangeCoding.exe %comm% &echo �ɹ���) else (echo %excelFile% �����ڣ�& echo. & echo. )
goto loop

:ShowTips
set /a num=%~1
echo %num%
goto:eof

:End
pause & exit
goto:eof