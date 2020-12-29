using UnityEditor;
using UnityEngine;
/// <summary>
/// Hierarchy拓展
/// </summary>
public static class HierarchyExtension
{

    [MenuItem("GameObject/复制层级结构路径", priority = 1)]
    private static void CopyGameObjectHierarchyPath()
    {
        Object selectObj = Selection.activeObject;
        GameObject obj = selectObj as GameObject;
        if (obj == null)
        {
            return;
        }
        string path = UtilsEditorTool.GetGameObjectHierarchyPath(obj.transform);
        GUIUtility.systemCopyBuffer = path;
        Debug.Log("复制层级结构到剪贴板：" + path);
    }


}
