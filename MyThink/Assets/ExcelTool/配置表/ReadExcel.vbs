
'xlFileFormat
Option Explicit 'ǿ������
Const xlCSV=6 'CSV���Ӧ��ö��
Const xlWindowText=20
Const xlUnicodeText=42
Dim excelFloderPath,tableTxtFloderPath,argus
Dim excelPaths	'excel·��
excelFloderPath = createobject("Scripting.FileSystemObject").GetFolder(".").Path+"\DataExcel" 'Excel����ļ���·��
tableTxtFloderPath=createobject("Scripting.FileSystemObject").GetFolder(".").Path+"\TableTxt" 'ת������ı��ļ���·��
Wscript.Echo "����ת��..."
'���� ExcelToTableTxtAll
if wscript.arguments.Count >0 then
if wscript.arguments(0)="all" then
ExcelToTableTxtAll 'ȫ��ת��
else 
ExcelToTableTxtOne(wscript.arguments(0)) '��һת��
end if
else 
Wscript.Echo "�޲���"
end if
Wscript.Echo "1S���Զ��ر�..."
Wscript.Sleep 1000


Function ExcelToTableTxtAll
excelPaths=GetExcelPaths(excelFloderPath) '��ȡ����Excel�ļ���·��
ExcelToTableTxt(excelPaths)
End Function


'��һ�ļ�ת��
Function ExcelToTableTxtOne(fileName)
Dim onepaths(0)
fileName=excelFloderPath+"\"+fileName+".xlsx"
onepaths(0)=fileName
ExcelToTableTxt(onepaths)
End Function


'ExcelתTxt paths Ϊxlsx·������
Function ExcelToTableTxt(paths)
Dim path '·��
Dim i
Dim txtPath,objExcel,objWorkbook,objWorksheet
Set objExcel = CreateObject("Excel.Application")
for i=0 to UBound(paths)
path=paths(i)
txtPath=Replace(path,excelFloderPath,tableTxtFloderPath)'ָ���ַ����滻
txtPath=Left(txtPath,len(txtPath)-5)'��ȡ�ַ���
txtPath=txtPath+".txt" '������µ�txt�ļ�·��
Set objWorkbook = objExcel.Workbooks.Open(path)
objExcel.DisplayAlerts = FALSE
objExcel.Visible = FALSE
Set objWorksheet = objWorkbook.Worksheets("Sheet1")
Wscript.Echo "ת����"+path
objWorksheet.SaveAs txtPath, xlUnicodeText
Wscript.Echo "�ɹ�"
objExcel.Quit
Wscript.Echo "ת�����"
Next
End Function

'��ȡ����Excel��·��
Function GetExcelPaths(floderPath)
Dim excelPath() '���ص�excel·������
Dim i '����
Dim fso '���ļ�������
Dim oFolder '�ļ���
Dim oFlies '�ļ�
Dim oFlie '�����ļ�
i=0
Set fso=CreateObject("Scripting.FileSystemObject")
Set oFolder = fso.GetFolder(floderPath) 
Set oFlies=oFolder.Files
For Each oFlie In oFlies
If Right(oFlie.Path,4)="xlsx" Then
ReDim Preserve  excelPath(i) 'Preserve���µ��������Сʱ��������
excelPath(i)=oFlie.Path '��������
i=i+1
End If
Next
GetExcelPaths=excelPath '���ص�excel·��
End Function


'��ȡ�����ڵ��м���ַ���
Function GetStrBetween(Str,StartStr,EndStr)  
Dim StartStrPos,EndStrPos,Length,Res
StartStrPos = Instr(Str, StartStr)+Len(StartStr)  
EndStrPos = Instr(Str,EndStr)  
Length = EndStrPos  - StartStrPos   
Res= Mid(Str,StartStrPos,Length)  
GetStrBetween = Res
End Function

'��ȡ�ı�
Function ReadFromTextFile(FileUrl)
Dim str,stm
set stm =CreateObject("adodb.stream")
stm.Type=1 '2-�ı�ģʽ��ȡ 1-�����ƶ�ȡ
stm.mode=3 '3-��д 1-�� 2-д
stm.open
stm.LoadFromFile(FileUrl)
str=stm.Read
stm.saveToFile FileUrl,2
stm.close
set stm=nothing
ReadFromTextFile=str
End Function

'ת����ʽд��
Function WriteToTextFile(FileUrl,content,CharSet)
Dim stm
set stm =CreateObject("adodb.stream")
stm.Type=2 '2-�ı�ģʽ��ȡ 1-�����ƶ�ȡ
stm.mode=3 '3-��д 1-�� 2-д
stm.CharSet=CharSet
stm.open
stm.writeText content
stm.saveToFile FileUrl,2
stm.flush
stm.close
set stm=nothing
End Function


