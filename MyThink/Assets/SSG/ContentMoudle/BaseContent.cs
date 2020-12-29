using System.Collections.Generic;
/// <summary>
/// 数据载体基类
/// </summary>
public  abstract class BaseContent 
{
    public int Id;
    //解析格式
    public virtual void FormatTxtData(List<string> rowData)
    {
        Id = int.Parse(rowData[0]);
    }

}
