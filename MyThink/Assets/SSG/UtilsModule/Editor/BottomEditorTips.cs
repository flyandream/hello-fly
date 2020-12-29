using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 底部提示
/// </summary>
public class BottomEditorTips
{

    public string Msg;//提示文本
    public bool IsValid;//是否有效

    public void Render()
    {
        if(!IsValid)
        {
            return;
        }
        //绘制背景
        EditorGUI.DrawRect(new Rect(0, Screen.height - 60, Screen.width,40),new Color(0.7f, 0.7f, 0.7f,0.7f));
        EditorGUI.TextField(new Rect(0, Screen.height - 60, Screen.width, 40),Msg, CustomGUIStyle.NewFontGuiStyle(Color.black, 15,TextAnchor.UpperCenter));
    }

    public void SetMsg(string msg)
    {
        IsValid = true;
        Msg = msg;
    }

    public void Clear()
    {
        IsValid = false;
    }

}

