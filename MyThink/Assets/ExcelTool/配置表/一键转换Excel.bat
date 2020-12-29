@echo off
set "execlPath=%cd%\DataExcel"
if exist %execlPath% (echo Excel文件路径：%execlPath%)else (echo 找不到路径%execlPath%,退出&goto:End)
set "tableTxtPath=%cd%\TableTxt"
if exist %tableTxtPath% (echo Txt文件路径：%tableTxtPath%)else (echo 找不到路径%tableTxtPath%,退出&goto:End)
echo.
echo ----------------------路径读取成功----------------------
:loop
echo.
echo ************************************************
echo *  all：转换全部，文件名：单一转换 exit:退出   *
echo ************************************************
set /p comm=
if %comm% equ all ( start /wait CScript ReadExcel.vbs all & start /wait ChangeCoding.exe "all" &goto loop)
if %comm% equ exit (exit)
set "excelFile=%execlPath%\%comm%.xlsx"
rem echo 检查:%excelFile%...
if exist %excelFile% (echo 转换：%excelFile% & start /wait CScript ReadExcel.vbs %comm% & start /wait ChangeCoding.exe %comm% &echo 成功！) else (echo %excelFile% 不存在！& echo. & echo. )
goto loop

:ShowTips
set /a num=%~1
echo %num%
goto:eof

:End
pause & exit
goto:eof