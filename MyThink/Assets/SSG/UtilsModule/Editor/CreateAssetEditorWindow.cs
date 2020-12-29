using System.IO;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
/// <summary>
/// Asset文件创建窗口
/// </summary>
public class CreateAssetEditorWindow : EditorWindow
{
    public static string AssetTypeName;

    public static void CreateAsset<T>(Texture2D icon=null,string resourceFile=null) where  T: ScriptableObject
    {
        AssetTypeName = typeof(T).ToString();
        ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
            CreateInstance<CustomAsset>(),
        GetSelectedPathOrFallback() + "/New "+AssetTypeName+".asset",
            icon, resourceFile);
    }

    //获取当前的路径
    public static string GetSelectedPathOrFallback()
    {
        string path = "Assets";
        foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
                break;
            }
        }
        return path;
    }
}




/// <summary>
/// 资源创建基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class CustomAsset: EndNameEditAction
{
    public override void Action(int instanceId, string pathName, string resourceFile)
    {
        Debug.Log("创建路径：" + pathName);
        // 实例化类  Bullet
        ScriptableObject asset = CreateInstance(CreateAssetEditorWindow.AssetTypeName);
        if (asset != null)
        {
            AssetDatabase.CreateAsset(asset, pathName);
            AssetDatabase.Refresh();
        }
    }
}


