
'xlFileFormat
Option Explicit '强制声明
Const xlCSV=6 'CSV格对应的枚举
Const xlWindowText=20
Const xlUnicodeText=42
Dim excelFloderPath,tableTxtFloderPath,argus
Dim excelPaths	'excel路径
excelFloderPath = createobject("Scripting.FileSystemObject").GetFolder(".").Path+"\DataExcel" 'Excel表的文件夹路径
tableTxtFloderPath=createobject("Scripting.FileSystemObject").GetFolder(".").Path+"\TableTxt" '转换后的文本文件夹路径
Wscript.Echo "正在转换..."
'测试 ExcelToTableTxtAll
if wscript.arguments.Count >0 then
if wscript.arguments(0)="all" then
ExcelToTableTxtAll '全部转换
else 
ExcelToTableTxtOne(wscript.arguments(0)) '单一转换
end if
else 
Wscript.Echo "无参数"
end if
Wscript.Echo "1S后自动关闭..."
Wscript.Sleep 1000


Function ExcelToTableTxtAll
excelPaths=GetExcelPaths(excelFloderPath) '获取所有Excel文件的路径
ExcelToTableTxt(excelPaths)
End Function


'单一文件转换
Function ExcelToTableTxtOne(fileName)
Dim onepaths(0)
fileName=excelFloderPath+"\"+fileName+".xlsx"
onepaths(0)=fileName
ExcelToTableTxt(onepaths)
End Function


'Excel转Txt paths 为xlsx路径数组
Function ExcelToTableTxt(paths)
Dim path '路径
Dim i
Dim txtPath,objExcel,objWorkbook,objWorksheet
Set objExcel = CreateObject("Excel.Application")
for i=0 to UBound(paths)
path=paths(i)
txtPath=Replace(path,excelFloderPath,tableTxtFloderPath)'指定字符串替换
txtPath=Left(txtPath,len(txtPath)-5)'截取字符串
txtPath=txtPath+".txt" '计算出新的txt文件路径
Set objWorkbook = objExcel.Workbooks.Open(path)
objExcel.DisplayAlerts = FALSE
objExcel.Visible = FALSE
Set objWorksheet = objWorkbook.Worksheets("Sheet1")
Wscript.Echo "转换："+path
objWorksheet.SaveAs txtPath, xlUnicodeText
Wscript.Echo "成功"
objExcel.Quit
Wscript.Echo "转换完毕"
Next
End Function

'获取所有Excel的路径
Function GetExcelPaths(floderPath)
Dim excelPath() '返回的excel路径数组
Dim i '索引
Dim fso '流文件操作器
Dim oFolder '文件夹
Dim oFlies '文件
Dim oFlie '单个文件
i=0
Set fso=CreateObject("Scripting.FileSystemObject")
Set oFolder = fso.GetFolder(floderPath) 
Set oFlies=oFolder.Files
For Each oFlie In oFlies
If Right(oFlie.Path,4)="xlsx" Then
ReDim Preserve  excelPath(i) 'Preserve重新调整数组大小时保留数据
excelPath(i)=oFlie.Path '加入数组
i=i+1
End If
Next
GetExcelPaths=excelPath '返回的excel路径
End Function


'截取两个节点中间的字符串
Function GetStrBetween(Str,StartStr,EndStr)  
Dim StartStrPos,EndStrPos,Length,Res
StartStrPos = Instr(Str, StartStr)+Len(StartStr)  
EndStrPos = Instr(Str,EndStr)  
Length = EndStrPos  - StartStrPos   
Res= Mid(Str,StartStrPos,Length)  
GetStrBetween = Res
End Function

'读取文本
Function ReadFromTextFile(FileUrl)
Dim str,stm
set stm =CreateObject("adodb.stream")
stm.Type=1 '2-文本模式读取 1-二进制读取
stm.mode=3 '3-读写 1-读 2-写
stm.open
stm.LoadFromFile(FileUrl)
str=stm.Read
stm.saveToFile FileUrl,2
stm.close
set stm=nothing
ReadFromTextFile=str
End Function

'转换格式写入
Function WriteToTextFile(FileUrl,content,CharSet)
Dim stm
set stm =CreateObject("adodb.stream")
stm.Type=2 '2-文本模式读取 1-二进制读取
stm.mode=3 '3-读写 1-读 2-写
stm.CharSet=CharSet
stm.open
stm.writeText content
stm.saveToFile FileUrl,2
stm.flush
stm.close
set stm=nothing
End Function


