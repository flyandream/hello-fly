using UnityEditor;
using UnityEngine;

/// <summary>
/// UI层级排列窗口
/// </summary>
public class UISortOrderWindow : EditorWindow
{

    public enum UISortLayer
    {         
        Default,
        UILow,
        UIMid,
        UIHeight,
        UITop,        
    }
    public GameObject Target;//预览目标
    public Canvas[] Canvases; //画布
    public ParticleSystem[] ParticleSystems; // 粒子
    private Vector2 scrollViewPos=Vector2.zero;

    public static void BindAndShow(GameObject target)
    {
        UISortOrderWindow window = CreateInstance<UISortOrderWindow>();
        window.Target = target;
        window.Show();        
        window.minSize = new Vector2(600, 800);
    }

    private void OnGUI()
    { 
        //TODO//绘制刷新
        if (Target == null)
        {
            //关闭面板
            Close();
            return;
        }
        //获取画布和特效
        Canvases = Target.GetComponentsInChildren<Canvas>();
        ParticleSystems = Target.GetComponentsInChildren<ParticleSystem>();
        //显示当前层级次序        
        //GUILayout.BeginArea(new Rect(0,0,400,20),);        
        GUILayout.Label("节点：");
        EditorGUILayout.ObjectField(Target, typeof(GameObject));
        TitleGui();
        //数据条目
        int itemCount = Canvases.Length + ParticleSystems.Length;
        scrollViewPos = GUILayout.BeginScrollView(scrollViewPos, GUILayout.Width(600), GUILayout.Height(600));
        //预览画布节点
        for (int i = 0; i < Canvases.Length; i++)
        {
            SerializeShowCanvasItem(Canvases[i]);
        }
        //预览特效节点
        for (int i = 0; i < ParticleSystems.Length; i++)
        {
            SerializeShowParticleSystemItem(ParticleSystems[i]);
        }
        GUILayout.EndScrollView();
    }

    private void TitleGui()
    {
        GUILayout.BeginHorizontal();        
        GUILayout.Label("Componet", GUILayout.Width(200));
        GUILayout.Label("Override", GUILayout.Width(100));
        GUILayout.Label("SortLayer", GUILayout.Width(100));
        GUILayout.Label("OrderInLayer", GUILayout.ExpandWidth(true));
        GUILayout.EndHorizontal();
        
    }
    //序列化显示(Canvas)
    private void SerializeShowCanvasItem(Canvas canvas)
    {
        GUILayout.BeginHorizontal();
        EditorGUILayout.ObjectField(canvas, typeof(Canvas), GUILayout.Width(200));
        canvas.overrideSorting=GUILayout.Toggle(canvas.overrideSorting, "Override",GUILayout.Width(100));    
        if (canvas.overrideSorting)
        {
            UISortLayer sortLayer = GetSortLayer(canvas.sortingLayerName);
            //层级
            sortLayer= (UISortLayer)EditorGUILayout.EnumPopup(sortLayer,GUILayout.Width(100));
            canvas.sortingLayerName = GetSortLayerName(sortLayer);

            canvas.sortingOrder = EditorGUILayout.IntField(canvas.sortingOrder, GUILayout.Width(100));
        }
        GUILayout.EndHorizontal();
    }
    //序列化显示(ParticleSystem)
    private void SerializeShowParticleSystemItem(ParticleSystem particleSystem)
    {
        GUILayout.BeginHorizontal();
        Renderer renderer = particleSystem.GetComponent<Renderer>();
        if (renderer == null)
        {
            return;
        }        
        EditorGUILayout.ObjectField(particleSystem, typeof(ParticleSystem), GUILayout.Width(200));
        GUILayout.Toggle(true, "Override", GUILayout.Width(100));        
        UISortLayer sortLayer = GetSortLayer(renderer.sortingLayerName);
        //层级
        sortLayer = (UISortLayer)EditorGUILayout.EnumPopup(sortLayer, GUILayout.Width(100));
        renderer.sortingLayerName = GetSortLayerName(sortLayer);
        renderer.sortingOrder = EditorGUILayout.IntField(renderer.sortingOrder, GUILayout.Width(100));
        GUILayout.EndHorizontal();
    }


    public UISortLayer GetSortLayer(string sortLayerName)
    {
       if (sortLayerName == SortLayers.UILow)
        {
            return UISortLayer.UILow;
        }
        else if (sortLayerName == SortLayers.UIMid)
        {
            return UISortLayer.UIMid;
        }
        else if (sortLayerName == SortLayers.UIHeight)
        {
            return UISortLayer.UIHeight;
        }
        else if (sortLayerName == SortLayers.UITop)
        {
            return UISortLayer.UITop;
        }
       else
       {
           return UISortLayer.Default;
        }
       
    }

    public string GetSortLayerName(UISortLayer sortLayer)
    {
        switch (sortLayer)
        {            
            case UISortLayer.UILow:
                return SortLayers.UILow;
                break;
            case UISortLayer.UIMid:
                return SortLayers.UIMid;
                break;
            case UISortLayer.UIHeight:
                return SortLayers.UIHeight;
                break;
            case UISortLayer.UITop:
                return SortLayers.UITop;
                break;
            default:
                return SortLayers.Default;
                break;
        }
    }
}
