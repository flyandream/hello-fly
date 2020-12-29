using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 模型导入设置
/// </summary>
public class ModelImportSettingEditorWindow : EditorWindow
{

    #region 修改模型大小部分

    private string _scaleFactorStr = string.Empty;

    #endregion



    [MenuItem("Tools/ 模型导入设置")]
    public static void OpenWindow()
    {
        ModelImportSettingEditorWindow window = CreateInstance<ModelImportSettingEditorWindow>();
        window.titleContent=new GUIContent("模型导入设置");
        window.maxSize = new Vector2(400, 200);
        window.minSize = new Vector2(400, 200);
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Box("模型导入设置", GUILayout.ExpandWidth(true));

        ////设置模型大小
        OnGuiForSetScaleFactor();

        ////--展开2U
        OnGuiForSetModel2Uvs();

    }

    #region 批量修改模型大小
    //修改模型大小
    private void SetModleScaleFactor(List<ModelImporter> list,float size)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].globalScale = size;
            //保存
            list[i].SaveAndReimport();                
        }
    }
    //确认
    private void MakeSureSetScaleFactor(float size)
    {
        List<ModelImporter> list = GetCurSelectModelImports();
        string tips = string.Format("有效条目：{0}，\t确定修改ScaleFactor为{1}吗？", list.Count, size);
        MakeSureEditorWindow.Show(tips, () => { SetModleScaleFactor(list,size); },null);
    }
    //OnGui显示
    private void OnGuiForSetScaleFactor()
    {
        GUILayout.Label("批量缩放");
        GUILayout.BeginHorizontal();
        GUILayout.Label("大小", GUILayout.Width(50));
        _scaleFactorStr = EditorGUILayout.TextField(_scaleFactorStr, GUILayout.ExpandWidth(true));
        if (GUILayout.Button("修改", GUILayout.Width(100)))
        {
            if (_scaleFactorStr.IsNormalized())
            {
                Debug.Log("数值有误！");
            }
            else
            {
                float size = float.Parse(_scaleFactorStr);
                MakeSureSetScaleFactor(size);
            }
        }
        GUILayout.EndHorizontal();
    }

    #endregion

    #region 批量展开2U
    //OnGui显示
    private void OnGuiForSetModel2Uvs()
    {
        GUILayout.Label("开启/关闭 Lightmap Uvs");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("展开"))
        {
            //TODO
            MakeSureSetModelLightmapUvs(true);
        }
        if (GUILayout.Button( "关闭"))
        {
            //TODO
            MakeSureSetModelLightmapUvs(false);
        }
        GUILayout.EndHorizontal();
        GUILayout.Label("开启/关闭 Swap Uvs");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("展开"))
        {
            MakeSureSetModelSwapUvs(true);
        }
        if (GUILayout.Button("关闭"))
        {
            MakeSureSetModelSwapUvs(false);
        }
        GUILayout.EndHorizontal();
    }

    #region Lightmap Uvs 部分
    //二次确认，开启，关闭LightmapUvs
    private void MakeSureSetModelLightmapUvs(bool isOpen)
    {
        List<ModelImporter> list = GetCurSelectModelImports();
        string tips;
        if (isOpen)
        {
            tips = string.Format("有效条目：{0}，确定展开2Uvs？", list.Count);
        }
        else
        {
            tips = string.Format("有效条目：{0}，确定关闭2Uvs？", list.Count);
        }
        MakeSureEditorWindow.Show(tips, () => { SetModelLightmapUvs(list, isOpen); }, null);

    }
    //设置LightmapUvs
    private void SetModelLightmapUvs(List<ModelImporter> list, bool isOpen)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].generateSecondaryUV = isOpen;
            //保存
            list[i].SaveAndReimport();
        }
    }
    #endregion

    #region SwapUvs 部分
    //二次确认，开启，关闭SwapUvs
    private void MakeSureSetModelSwapUvs(bool isOpen)
    {
        List<ModelImporter> list = GetCurSelectModelImports();
        string tips;
        if (isOpen)
        {
             tips = string.Format("有效条目：{0}，确定展开2Uvs？", list.Count);
        }
        else
        {
             tips = string.Format("有效条目：{0}，确定关闭2Uvs？", list.Count);
        }
        MakeSureEditorWindow.Show(tips, () => { SetModelSwapUvs(list, isOpen); }, null);

    }
    //设置Swap Uvs
    private void SetModelSwapUvs(List<ModelImporter> list, bool isOpen)
    {
        for (int i = 0; i < list.Count; i++)
        {            
            list[i].swapUVChannels = isOpen;
            //保存
            list[i].SaveAndReimport();
        }
    }
    #endregion




    #endregion




    //获取modelimport
    private static bool TryGetModelImporter(GameObject gObj, out ModelImporter importer)
    {
        //获取gobj路径
        string path = AssetDatabase.GetAssetPath(gObj);
        Debug.Log(path);
        ModelImporter modelImporter = AssetImporter.GetAtPath(path) as ModelImporter;
        if (modelImporter != null)
        {
            importer = modelImporter;
            return true;
        }
        importer = null;
        return false;         
    }

    //获取当前选择的有效模型导入设置
    private List<ModelImporter> GetCurSelectModelImports()
    {
        List<ModelImporter> list = new List<ModelImporter>();
        GameObject[] selectGameObjects = Selection.gameObjects;
        for (int i = 0; i < selectGameObjects.Length; i++)
        {
            ModelImporter importer;
            if (TryGetModelImporter(selectGameObjects[i], out importer))
            {
                //加入列表
                list.Add(importer);
            }
        }
        return list;
    }



}
