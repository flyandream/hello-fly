using SSG;
using UnityEditor;
/// <summary>
/// 自定义Asset文件创建器
/// </summary>
public class CustomAssetCreator
{
  
    //创建路径节点
    [MenuItem("Assets/SSG/Create/PathNode")]
    private static void CreatePathNode()
    {
        CreateAssetEditorWindow.CreateAsset<PathNode>();
    }


}
