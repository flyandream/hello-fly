using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 配置表数据模块工具类,各种静态方法帮助解析数据
/// </summary>
public class ContentTools  {

    /// <summary>
    /// 将字符串数据解析成颜色
    /// </summary>
    public static Color FormatStringToColor(string content)
    {
        Color result;
        string[] colorStrs=content.Split('|');//分割字符串
        if (colorStrs.Length!=4)//如果不满足四位数证明数据有误
        {
            throw new Exception(content+"不满足颜色格式!,转化失败");
        }
        float red = float.Parse(colorStrs[0])/255;//红色
        float green = float.Parse(colorStrs[1]) / 255;//绿色
        float blue = float.Parse(colorStrs[2]) / 255;//蓝色
        float alpha= float.Parse(colorStrs[3]) / 255;//透明度
        result =new Color(red,green,blue,alpha);
        return result;
    }



    /// <summary>
    /// 字符串转布尔型，1为true,其它值为fasle
    /// </summary>
    /// <returns></returns>
    public static bool FormatStringToBool(string content)
    {
        return content == "1" ? true : false;
    }
    /// <summary>
    /// 字符串转vector3,用"|"分隔
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public static Vector3 FormatStringToVector3(string content)
    {        
        string[] strs= content.Split('|');//用“|‘分割
        if (strs.Length!=3)
        {
            throw new Exception("不满足Vector3格式，格式化失败！");
        }
        return new Vector3(float.Parse(strs[0]),float.Parse(strs[1]),float.Parse(strs[2]));
    }

    /// <summary>
    /// 字符串转string[],用“|”分割
    /// </summary>
    /// <returns></returns>
    public static string[] FormatStringToStringArray(string content)
    {
        string[] strs = content.Split('|');//用'|'分割
        return strs;
    }


    /// <summary>
    /// 字符串转string[],用symbol符号分割
    /// </summary>
    /// <returns></returns>
    public static string[] FormatStringToStringArrayBySymbol(char symbol, string content)
    {
        string[] strs = content.Split(symbol);//用'symbol'分割
        return strs;
    }

    /// <summary>
    /// 字符串转到CarDebrisInt列表
    /// </summary>
    /// <param name="rowData"></param>
    /// <returns></returns>
    public static List<CarDebrisInt> FormatToCarDebrisIntList(string rowData, string excelName = "---", int rowDataId = -1)
    {
        List<CarDebrisInt> result = new List<CarDebrisInt>();
        //用~先分割
        string[] strs = ContentTools.FormatStringToStringArrayBySymbol('~', rowData);
        for (int i = 0; i < strs.Length; i++)
        {
            //继续用‘|’分割
            string[] strs2 = ContentTools.FormatStringToStringArray(strs[i]);
#if UNITY_EDITOR
            //校验数据准确性
            if (strs2.Length != 2)
            {
                string errorTips = string.Format("数据格式出错！位置：{0} \tID：{1} \t内容：{2}", excelName, rowDataId, rowData);
                throw new Exception(errorTips);
            }
#endif
            int index = i;
            int carDebrisId = int.Parse(strs2[0]);
            int carDebrisNum = int.Parse(strs2[1]);
            CarDebrisInt config = new CarDebrisInt(index,carDebrisId, carDebrisNum);
            //加入列表
            result.Add(config);
        }
        return result;
    }

    /// <summary>
    /// 字符串转到ChestTreasureInt列表
    /// </summary>
    /// <param name="rowData"></param>
    /// <returns></returns>
    public static List<ChestTreasureInt> FormatToChestTreasureIntList(string rowData, string excelName = "---", int rowDataId = -1)
    {
        List<ChestTreasureInt> result = new List<ChestTreasureInt>();
        //用~先分割
        string[] strs = ContentTools.FormatStringToStringArrayBySymbol('~', rowData);
        for (int i = 0; i < strs.Length; i++)
        {
            //继续用‘|’分割
            string[] strs2 = ContentTools.FormatStringToStringArray(strs[i]);
#if UNITY_EDITOR
            //校验数据准确性
            if (strs2.Length != 2)
            {
                string errorTips = string.Format("数据格式出错！位置：{0} \tID：{1} \t内容：{2}", excelName, rowDataId, rowData);
                throw new Exception(errorTips);
            }
#endif
            int index = i;
            int carDebrisId = int.Parse(strs2[0]);
            int carDebrisNum = int.Parse(strs2[1]);
            ChestTreasureInt config = new ChestTreasureInt(index, carDebrisId, carDebrisNum);
            //加入列表
            result.Add(config);
        }
        return result;
    }


}




