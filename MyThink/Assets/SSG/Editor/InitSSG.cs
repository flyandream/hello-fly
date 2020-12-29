using SSG;
using UnityEngine;
using UnityEditor;
public class InitSSG 
{

    [MenuItem("SSG/初始化框架",false,1)]
    private static void Init()
    {
        Debug.Log("初始化框架...");
        //生成所需文件夹
        Debug.Log("1.生成所需文件夹...");
        //生成必须文件夹
        UtilsEditorTool.CreateFolder(Application.dataPath+ "/04_Environment");
        UtilsEditorTool.CreateFolder(Application.dataPath + "/05_Character");
        UtilsEditorTool.CreateFolder(Application.dataPath + "/06_Effect");        
        Debug.Log("成功！");

        Debug.Log("2.生成UI框架所需标签");
        //初始化UI渲染层
        InitUISortLayer();
        Debug.Log("成功！");



    }

    private static  void InitUISortLayer()
    {
        //添加SortLayer标签
        //TagTools.
        TagTools.AddSortingLayer("UILow");
        TagTools.AddSortingLayer("UIMid");
        TagTools.AddSortingLayer("UIHeight");
        TagTools.AddSortingLayer("UITop");

    }
 
}
