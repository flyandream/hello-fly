using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

//创建UI组件编辑窗口
public class UIHierarchy
{
    [MenuItem("GameObject/复制UI路径", priority = 0)]
    private static void CopyUIGameHierarchyPath()
    {
        Object selectObj = Selection.activeObject;
        GameObject obj = selectObj as GameObject;
        if (obj == null)
        {
            return;
        }
        string path = UtilsEditorTool.GetUICompentPath(obj.transform, obj.name);
        GUIUtility.systemCopyBuffer = path;
        Debug.Log("复制Ui组件路径到剪贴板：" + path);
    }

    //创建枪械资源
    [MenuItem("GameObject/UI/UIColorImage(变色用)\tDrawCall+1", false)]
    private static void CreateUIColorImage()
    {
        //获取当前选中的节点
        GameObject clone=new GameObject("ColorImage");
        clone.transform.SetParent(Selection.activeTransform);
        UIColorImage image= clone.AddComponent<UIColorImage>();
        image.transform.localPosition=Vector3.zero;
        image.transform.localScale=Vector3.one;
        Selection.activeGameObject = clone;
    }

    //创建枪械资源
    [MenuItem("GameObject/UI/UIColorText(变色用)\tDrawCall+1", false)]
    private static void CreateColorText()
    {
        //获取当前选中的节点
        GameObject clone = new GameObject("ColorText");
        clone.transform.SetParent(Selection.activeTransform);
        UIColorText image = clone.AddComponent<UIColorText>();
        image.transform.localPosition = Vector3.zero;
        image.transform.localScale = Vector3.one;
        Selection.activeGameObject = clone;
    }


    [MenuItem("GameObject/UI/横向SrollView", false)]
    private static void CreateHorSrollView()
    {
        Object obj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/03_UI/Prefab/ScrollView_Horizontal.prefab");
      GameObject clone=  GameObject.Instantiate(obj, Selection.activeTransform)as GameObject;
        clone.name = "ScrollView_Horizontal";
        Selection.activeGameObject = clone;
    }

   
    [MenuItem("GameObject/UI/纵向SrollView", false)]
    private static void CreateVerSrollView()
    {
        Object obj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/03_UI/Prefab/ScrollView_Vertical.prefab");
        GameObject clone = GameObject.Instantiate(obj, Selection.activeTransform) as GameObject;
        clone.name = "ScrollView_Vertical";
        Selection.activeGameObject = clone;
    }



    [MenuItem("GameObject/UI/横向SrollView(带无限列表)", false)]
    private static void CreateHorSrollViewLoop()
    {
        Object obj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/03_UI/Prefab/ScrollView_Horizontal_Loop.prefab");
        GameObject clone = GameObject.Instantiate(obj, Selection.activeTransform) as GameObject;
        clone.name = "ScrollView_Horizontal_Loop";
        Selection.activeGameObject = clone;
    }


    [MenuItem("GameObject/UI/纵向SrollView(带无限列表)", false)]
    private static void CreateVerSrollViewLoop()
    {
        Object obj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/03_UI/Prefab/ScrollView_Vertical_Loop.prefab");
        GameObject clone = GameObject.Instantiate(obj, Selection.activeTransform) as GameObject;
        clone.name = "ScrollView_Vertical_Loop";
        Selection.activeGameObject = clone;
    }

    [MenuItem("GameObject/UI/调整SortLayer\tUILow", false)]
    private static void RefreshUISortOrderToLow()
    {
      OverrideUISortLayer(Selection.gameObjects,UICanvasType.Low);
    }

    [MenuItem("GameObject/UI/调整SortLayer\tUIMid", false)]
    private static void RefreshUISortOrderToMid()
    {
        OverrideUISortLayer(Selection.gameObjects, UICanvasType.Mid);
    }


    [MenuItem("GameObject/UI/调整SortLayer\tUIHeight", false)]
    private static void RefreshUISortOrderToHeight()
    {
        OverrideUISortLayer(Selection.gameObjects, UICanvasType.Height);
    }


    [MenuItem("GameObject/UI/调整SortLayer\tUITop", false)]
    private static void RefreshUISortOrderToTop()
    {
        OverrideUISortLayer(Selection.gameObjects, UICanvasType.Top);
    }

    //覆盖渲染层级
    private static void OverrideUISortLayer(GameObject[] targets,UICanvasType canvasType)
    {        
        string sortLayerName = SortLayers.Default;
        switch (canvasType)
        {
            case UICanvasType.Low:
                sortLayerName = SortLayers.UILow;
                break;
            case UICanvasType.Mid:
                sortLayerName = SortLayers.UIMid;
                break;
            case UICanvasType.Height:
                sortLayerName = SortLayers.UIHeight;
                break;
            case UICanvasType.Top:
                sortLayerName = SortLayers.UITop;
                break;
        }
        //定位成绩        
        foreach (GameObject gameObject in targets)
        {
            //画布部分
            Canvas[] canvases = gameObject.GetComponentsInChildren<Canvas>();
            for (int i = 0; i < canvases.Length; i++)
            {
                if (canvases[i].overrideSorting)
                {
                    //设置层级
                    canvases[i].sortingLayerName = sortLayerName;                                                       
                    Debug.Log("(Canvas)设置成功：" + HierarchyPath(canvases[i].gameObject));
                }
                else
                {
                    continue;
                }
            }
            //特效部分
            ParticleSystem[] particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < particleSystems.Length; i++)
            {
                Renderer renderer = particleSystems[i].GetComponent<Renderer>();
                renderer.sortingLayerName = sortLayerName;
                Debug.Log("(ParticleSystem)设置成功：" + HierarchyPath(renderer.gameObject));
            }
        }
    }

    private static string HierarchyPath(GameObject gameObject)
    {
        string result = gameObject.name;
        if (gameObject.transform.parent != null)
        {
            result= HierarchyPath(gameObject.transform.parent.gameObject)+"/" +result;
        }
        return result;
    }

    [MenuItem("GameObject/UI/预览渲染层级", false)]
    private static void PreviewUISortOrderInWindow()
    {
        if (Selection.activeGameObject!=null)
        {
            UISortOrderWindow.BindAndShow(Selection.activeGameObject);
        }
    }

}
