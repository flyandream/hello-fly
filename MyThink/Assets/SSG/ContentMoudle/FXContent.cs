using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 特效配置数据
/// </summary>
public class FXContent : BaseContent
{
    //资源路径
    public string Path;
    public override void FormatTxtData(List<string> rowData)
    {
        base.FormatTxtData(rowData);
        Path = rowData[2];
    }
}
