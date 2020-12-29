using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 二次确认面板
/// </summary>
public class MakeSureEditorWindow : EditorWindow
{
    private static string showDes=string.Empty;
    public delegate  void OnClickHandle();

    private static MakeSureEditorWindow  _windos ;//记录当前
    private static OnClickHandle _onClickHandleYes;//yes按钮代理
    private static OnClickHandle _onClickHandleNo;//no按钮代理    

    void OnEnable()
    {

    }

    void OnGUI()
    {
        GUI.backgroundColor = Color.gray;
        GUI.Box(new Rect(10,10,380,95),"");
        GUI.Label(new Rect(10,10, 380, 95), showDes);
        GUI.backgroundColor = Color.green;
        if (GUI.Button(new Rect(50, 110, 100, 30), "是"))
        {
            if (_onClickHandleYes != null)
            {
                _onClickHandleYes();//执行回调
            }
            //关闭面板
            _windos.Close();
        }
        GUI.backgroundColor = Color.red;
        if (GUI.Button(new Rect(250, 110, 100, 30), "否"))
        {
            if (_onClickHandleNo != null)
            {
                _onClickHandleNo();//执行回调
            }
            //关闭面板
            _windos.Close();
        }        
    }

    //弹出//二次确认框
    public static void Show(string des, OnClickHandle onClickYesCallBack, OnClickHandle onClickNoCallBack=null)
    {

        showDes = des;
        _onClickHandleYes = onClickYesCallBack;//传递回调
        _onClickHandleNo = onClickNoCallBack;//传递回调
        _windos = (MakeSureEditorWindow)EditorWindow.GetWindow(typeof(MakeSureEditorWindow), false, "确认窗口", true);//创建窗口
        _windos.minSize = new Vector2(400, 150);
        _windos.maxSize=new Vector2(400,150);
        _windos.Show();//展示        
    }




}
