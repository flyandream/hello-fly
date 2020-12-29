using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;


//UI管理器
public class UIManager : MonoBehaviour
{
    private const float MAX_CANVAS_DIS = 1000;
    //面板字典
    public Dictionary<UIDialogType, IUIDialog> _dicDialogs = new Dictionary<UIDialogType, IUIDialog>();
    //组件对象池
    private Dictionary<UIComType, Stack<UIComponet>> _dicComPool = new Dictionary<UIComType, Stack<UIComponet>>();

    private static UIManager _instance = null;
    private static readonly object lylock = new object();

    private Transform _canvasLowTran; //低层画布transfrom
    private Transform _canvasMidTran; //中层画布transfrom
    private Transform _canvasHeightTran; //高层画布transfrom
    private Transform _canvasTopTran; //顶层画布transfrom

    private Canvas _canvasLow; //低层画布
    private Canvas _canvasMid; //中层画布
    private Canvas _canvasHeight; //高层画布
    private Canvas _canvasTop; //顶层画布

    private GameObject _comPool;
    public Camera _uiCamera;
    private EventSystem _eventSystem;
    public List<GameObject> desgameObjects = new List<GameObject>();

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (lylock)
                {
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("(Singleton)UIManager");
                        DontDestroyOnLoad(go);
                        _instance = go.AddComponent<UIManager>();
                    }
                }
            }
            return _instance;
        }
    }

    #region 属性封装

    // 最大层级索引
    public int GetMaxSiblingIndex(UICanvasType canvasType)
    {
        return GetCanvasTran(canvasType).childCount - 1;
    }

    //UI相机
    public Camera UiCamera
    {
        get { return _uiCamera; }
    }

    public Canvas CanvasLow
    {
        get { return _canvasLow; }
    }

    public Canvas CanvasMid
    {
        get { return _canvasMid; }
    }

    public Canvas CanvasHeight
    {
        get { return _canvasHeight; }
    }

    public Canvas CanvasTop
    {
        get { return _canvasTop; }
    }

    //面板是否在画布的最后显示
    public bool IsLastShowDialog(IUIDialog uiDialog)
    {
        //如果有一个面板与检测面板同层级，并且显示中即IsVisble=true,并且层级索引大于检测面板，返回false
        //其它情况都属于显示在最后
        //遍历面板
        foreach (IUIDialog dialog in _dicDialogs.Values)
        {
            //如果所在画布类型一样，并且层级索引大于检测的面版，并且
            if (dialog.CanvasType == uiDialog.CanvasType && dialog.IsVisble &&
                dialog.SiblingIndex > uiDialog.SiblingIndex)
            {
                //如果有一个大与检测的面板
                return false;
            }
        }
        return true;
    }

    //画布最后显示的面板索引
    public int LastShowDialogSiblingIndex(UICanvasType canvasType)
    {
        int lastSiblingIndex = 0;
        foreach (IUIDialog dialog in _dicDialogs.Values)
        {
            //如果所在画布类型一样，并且层级索引大于检测的面版，并且
            if (dialog.CanvasType == canvasType && dialog.IsVisble && dialog.SiblingIndex > lastSiblingIndex)
            {
                lastSiblingIndex = dialog.SiblingIndex;
            }
        }
        return lastSiblingIndex;
    }

    #endregion


    //初始化
    public void Init( Action onCreated =null )
    {



        //创建Ui池
        _comPool = new GameObject("UIManagerPool");
        Debug.Log("create com pool");
        GameObject.DontDestroyOnLoad(_comPool); //不摧毁

        SetupUiCamera(()=>{

            Debug.Log(_comPool != null);
            //构建UIContent单例
            UIContent.Instance.Build();
            //构建UI效果数据
            UIEffectData.Instance.Build();


            //检查预加载
           // CheckPreload();
            onCreated?.Invoke();
            
        });
 
    }

    #region 面板相关

    private async void SetupUiCamera(Action action)
    {
        // var clone = await Addressables.InstantiateAsync(UIContent.Instance.UiCameraPath).Task;
        //
        // _uiCamera = clone.GetComponent<Camera>();
        // GameObject.DontDestroyOnLoad(clone);
        Debug.Log(UIContent.Instance.CanvasLowPath);
        //var clonelow = await Addressables.InstantiateAsync(UIContent.Instance.CanvasLowPath).Task;
        var clonelow = GameObject.Find(UIContent.Instance.CanvasLowPath);
        _canvasLowTran = clonelow.transform;
        _canvasLow = _canvasLowTran.GetComponent<Canvas>();

        _canvasLow.worldCamera = _uiCamera; //设置渲染的相机     
        _canvasLow.planeDistance = MAX_CANVAS_DIS - (int)UICanvasType.Low;   //设置距离
        _canvasLow.sortingLayerName = SortLayers.UILow;//渲染层级
        _canvasLow.sortingOrder = 100;

        GameObject.DontDestroyOnLoad(clonelow);

        //var cloneMid = await Addressables.InstantiateAsync(UIContent.Instance.CanvasMidPath).Task;
        var cloneMid = GameObject.Find(UIContent.Instance.CanvasMidPath);
        _canvasMidTran = cloneMid.transform;
        _canvasMid = _canvasMidTran.GetComponent<Canvas>();

        _canvasMid.worldCamera = _uiCamera; //设置渲染的相机
        _canvasMid.planeDistance = MAX_CANVAS_DIS - (int)UICanvasType.Mid;//设置距离
        _canvasMid.sortingLayerName = SortLayers.UIMid;//渲染层级
        _canvasMid.sortingOrder = 400;

        GameObject.DontDestroyOnLoad(cloneMid);

        //var clonehigh = await Addressables.InstantiateAsync(UIContent.Instance.CanvasHeightPath).Task;
        var clonehigh = GameObject.Find(UIContent.Instance.CanvasHeightPath);
        _canvasHeightTran = clonehigh.transform;
        _canvasHeight = _canvasHeightTran.GetComponent<Canvas>();

        _canvasHeight.worldCamera = _uiCamera; //设置渲染的相机
        _canvasHeight.planeDistance = MAX_CANVAS_DIS - (int)UICanvasType.Height;//设置距离
        _canvasHeight.sortingLayerName = SortLayers.UIHeight;//渲染层级
        _canvasHeight.sortingOrder = 600;

        GameObject.DontDestroyOnLoad(clonehigh);

        //var clonetop = await Addressables.InstantiateAsync(UIContent.Instance.CanvasTopPath).Task;
        var clonetop = GameObject.Find(UIContent.Instance.CanvasTopPath);
        _canvasTopTran = clonetop.transform;
        _canvasTop = _canvasTopTran.GetComponent<Canvas>();

        _canvasTop.worldCamera = _uiCamera; //设置渲染的相机
        _canvasTop.planeDistance = MAX_CANVAS_DIS - (int)UICanvasType.Top;//设置距离
        _canvasTop.sortingLayerName = SortLayers.UITop;//渲染层级
        _canvasTop.sortingOrder = 800;

        GameObject.DontDestroyOnLoad(clonetop);

        //var cloneEventsys = await Addressables.InstantiateAsync(UIContent.Instance.EventSystemPath).Task;
        var cloneEventsys = GameObject.Find(UIContent.Instance.EventSystemPath);
        _eventSystem = cloneEventsys.GetComponent<EventSystem>();
        GameObject.DontDestroyOnLoad(cloneEventsys);

      


        if (action!=null)
        {
            action.Invoke();
        }
    }


    //面板类实例化时调用,加载完成时的回调
    public async void CreateDialog(UIDialogType uiDialogType, UICanvasType uiCanvasType, Action<GameObject> action)
    {
        //创建面板
        string path = UIContent.Instance.GetDialogPath(uiDialogType);
        GameObject clone = await Addressables.InstantiateAsync(path, GetCanvasTran(uiCanvasType)).Task;
       // if (uiDialogType != UIDialogType.LoadingDialog )
       // {
            desgameObjects.Add(clone);
       // }
        if (action != null)
            action.Invoke(clone);
       GameObject.DontDestroyOnLoad(clone); //不摧毁
        //return clone;
    }

    public void Show(UIDialogType uiDialogType)
    {
        if (_dicDialogs.ContainsKey(uiDialogType))
        {
            _dicDialogs[uiDialogType].Show();
        }
    }

    public void Hide(UIDialogType uiDialogType)
    {
        if (_dicDialogs.ContainsKey(uiDialogType))
        {
            _dicDialogs[uiDialogType].Hide();
        }
    }

    //隐藏画布下所有的面板
    public void HideCanvasDialog(UICanvasType canvasType)
    {
        foreach (var dialog in _dicDialogs)
        {
            if (dialog.Value.CanvasType == canvasType)
            {
                dialog.Value.Hide();
            }
        }
    }

    //隐藏所有面板
    public void HideAllDialog()
    {
        foreach (var dialog in _dicDialogs)
        {
            dialog.Value.Hide();
        }
    }

    //获取画布节点
    public Transform GetCanvasTran(UICanvasType canvasType)
    {
        switch (canvasType)
        {
            case UICanvasType.Low:
                return _canvasLowTran;
            case UICanvasType.Mid:
                return _canvasMidTran;
            case UICanvasType.Height:
                return _canvasHeightTran;
            case UICanvasType.Top:
                return _canvasTopTran;
            default: //默认返回低层画布节点
                return _canvasLowTran;
        }
    }

    // 设置显示层级
    public void SetDialogHierarchy(UIDialogType uiDialogType, bool isLastIndex, int offsetIndex = 0)
    {
        if (!_dicDialogs.ContainsKey(uiDialogType))
        {
            return;
        }
        if (isLastIndex)
        {
            _dicDialogs[uiDialogType].SetHierarchy(true, 0); //设置为最后一层
            return;
        }
        _dicDialogs[uiDialogType].SetHierarchy(false, offsetIndex); //设置在指定位置
    }

    //添加面板
    public void AddDialogToDic(IUIDialog baseDialog)
    {
        if (_dicDialogs.ContainsKey(baseDialog.DialogType))
        {
            return;
        }
        _dicDialogs.Add(baseDialog.DialogType, baseDialog);
    }

    #endregion

    /// <summary>
    /// 创建组件资源
    /// </summary>    
    public async void  CreateComRes(UIComType comType,Action<GameObject> action)
    {
        GameObject clone = await Addressables.InstantiateAsync(UIContent.Instance.GetComPath(comType)).Task;
        GameObject.DontDestroyOnLoad(clone); //不摧毁
        if (action!=null)
        {
            action.Invoke(clone);
        }
        
        //  //获取资源路径
        // CreateGoFromResources(UIContent.Instance.GetComPath(comType));
    }

    public void RecyleCom(UIComType comType, UIComponet com)
    {
        if (!_dicComPool.ContainsKey(comType))
        {
            _dicComPool.Add(comType, new Stack<UIComponet>()); //创建栈到字典
        }
        _dicComPool[comType].Push(com); //加入栈         
        Debug.Log("recyle com");
        com.ComTran.transform.SetParent(_comPool.transform); //设置父物体
        //隐藏物体
        com.SetVisble(false);
    }

    //是否有组件
    public bool HaveCom(UIComType comType)
    {
        if (_dicComPool.ContainsKey(comType))
        {
            return _dicComPool[comType].Count > 0;
        }
        return false;
    }

    //获取组件在池的数量
    private int GetComCountInPool(UIComType comType)
    {
        if (_dicComPool.ContainsKey(comType))
        {
            return _dicComPool[comType].Count;
        }
        return 0;
    }

    //获取组件
    public UIComponet GetCom(UIComType comType)
    {
        return _dicComPool[comType].Pop();
    }

    private GameObject CreateGoFromResources(string path)
    {
         return GameObject.Instantiate(Resources.Load(path, typeof (GameObject))) as GameObject;

    }
   
    private GameObject CreateGoFromResources(string path, Transform parent)
    {
        return GameObject.Instantiate(Resources.Load(path, typeof (GameObject)), parent) as GameObject;

    }

    //关闭所有UI事件点击系统
    public void CloseEventSystem()
    {
        _eventSystem.enabled = false;
    }

    //开启UI事件点击系统
    public void OpenEventSystem()
    {
        _eventSystem.enabled = true;
    }

    //清理//TODO
    public void Clear()
    {
        _instance = null;
    }

    public void Destroy()
    {
        if (desgameObjects==null)
        {
            return;
        }

        for (int i = 0; i < desgameObjects.Count; i++)
        {
            UnityEngine.Object.Destroy(desgameObjects[i]);
        }


        _dicDialogs.Clear();
        UIContent.Instance._dicDialogPath.Clear();
 
    }
    //检测预加载
    public void CheckPreload()
    {
        foreach (UIDialogType dialogType in _dicDialogs.Keys)
        {
            if (UIContent.Instance.NeedPreload(dialogType))
            {
                _dicDialogs[dialogType].Preload();
            }
        }
        
    }

    //检查是否被销毁
    public void CheckDestroy()
    {
        foreach (UIDialogType dialogType in _dicDialogs.Keys)
        {

            _dicDialogs[dialogType].IsDestroy();
           
        }
    }


    //预加载组件
    public void PreloadCom<T>(UIComType comType, int count)where  T:UIComponet,new()
    {
        //查看组件数量          
        int needNum = count - GetComCountInPool(comType);
        for (int i = 0; i < needNum; i++)
        {
            //通过代理创建
            T t = UIContent.Instance.CreateComInstance(comType)as T;
            //回收到池中
            t.Recycle();
        }
        
    }
    
}