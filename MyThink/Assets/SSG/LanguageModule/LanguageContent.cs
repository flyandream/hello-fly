using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageContent
{
    public int Key;//key值
    public string Value;//文本信息

    public LanguageContent(List<string> rowData,int index)
    {
        FormatLanguageData(rowData,index);//序列化信息
    }

    public  void FormatLanguageData(List<string> rowData,int index)
    {
        Key = int.Parse(rowData[0]);//语言key     
        Value = rowData[index].Replace("\\n", "\n");//处理换行符号
    }

}
