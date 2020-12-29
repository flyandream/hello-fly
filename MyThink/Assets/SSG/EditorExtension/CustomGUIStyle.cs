using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 自定义GUIStyle
/// </summary>
public class CustomGUIStyle
{
 
    /// <summary>
    /// 白字，顶端居中对齐
    /// </summary>
    public static GUIStyle WhiteFont_UpperCenter
    {
        get
        {
            GUIStyle style=new GUIStyle();
            style.normal.textColor = Color.white;
            style.alignment=TextAnchor.UpperCenter;
            return style;
        }
    }
    /// <summary>
    /// /白字，居中对齐
    /// </summary>
    public static GUIStyle WhiteFont_MiddleCenter
    {
        get
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.MiddleCenter;
            return style;
        }
    }

    /// <summary>
    /// /蓝字，居中对齐
    /// </summary>
    public static GUIStyle BlueFont_MiddleCenter
    {
        get
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = new Color(131/255F,192/255F,239/255F);
            style.alignment = TextAnchor.MiddleCenter;
            return style;
        }
    }

    public static GUIStyle NewFontGuiStyle(Color color,int fontSize,TextAnchor textAnchor)
    {
       // GUI.skin.customStyles
        GUIStyle style = new GUIStyle();
        style.normal.textColor = color;
        style.fontSize = fontSize;
        style.alignment = textAnchor;
        return style;
    }


}
