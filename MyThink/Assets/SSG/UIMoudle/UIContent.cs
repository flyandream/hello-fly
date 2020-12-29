using System;
using System.Collections.Generic;

//面板资源类型
public enum UIDialogType
{
    //Curtain, //幕布面板
   // Message, //消息面板
    SettlementDialog,//结算面板
    GameControlDialog,//控制面板
    MainDialog,//主界面
    VehicleSeletDialog,//选择车出战
    VehicleInfoDialog,//车辆信息面板
    ChestDialog,//宝箱面板
    FailDialog,//失败面板
    ResuitPoppupDialog,//结算过渡面板
    LevelInfoDialog,//关卡结算面板
   // LevelTestDialog,//关卡测试
    LoadingDialog,//加载面板
    OptionDialog,//设置面板
    PausePopDialog,//暂停面板
    RebirthPopDialog,//复活面板
    ReceiveAwardDialog,//喜提大奖面板
    RegionDialog,//章节切换面板
    UnlockPopDialog,//章节解锁提示界面
    BegionDialog,//开始游戏界面
    TutorialDialog,//新手引导界面
    BackDialog,//背景界面
    TaskExplainDialog,//任务说明界面
    VehicleUpgradePromptDialog,//任务说明界面
    UpGradeDialog,//升级界面
    GetCarAndRisingStarDialog,//解锁车或者升星车界面
    VehicleShowDialog,//看车界面
    GetDebrisDialog,//获得碎片界面
    PrefaceDialog,//序言界面
    SynthesisNewCarTipDialog,//合成新车提示面板
    ResourcesNotEnoughTipdialog,//资源不够提示面板 
    CoinTipDialog,//金币提示
    EndlessTaskExplainDialog,//无尽模式任务说明界面
    EndlessSettlementDialog,//无尽模式结算面板
    GiveVehiclePanel,//奖励汽车面板
}

//动态资源组件
public enum UIComType
{
    MessageItem, //提示信息    
 
}

//预加载数量
public class PreloadNum
{


}

public delegate UIComponet BuildComHandle();

public class UIContent
{
 
    private static UIContent _instance = null;
    private static readonly object lylock = new object();
    //UI面板资源路径 
    public Dictionary<UIDialogType, DialogPath> _dicDialogPath = new Dictionary<UIDialogType, DialogPath>();
    //动态组件资源路径
    private Dictionary<UIComType, string> _dicComPath = new Dictionary<UIComType, string>(); 
    //构建组件代理
    private Dictionary<UIComType, BuildComHandle> _dicBuildComHandles = new Dictionary<UIComType, BuildComHandle>();     

    private string _canvasLowPath; //底层画布
    private string _canvasMidPath; //中层画布
    private string _canvasHeightPath; //高层画布
    private string _canvasTopPath; //顶层画布    

    private string _eventSystemPath;
    private string _uiCameraPath; //UI相机路径

    public static UIContent Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (lylock)
                {
                    if (_instance == null)
                    {
                        _instance = new UIContent();
                        //初始化
                        _instance.Init();
                    }
                }
            }
            return _instance;
        }
    }

    #region 属性封装

    public string EventSystemPath
    {
        get { return _eventSystemPath; }
    }

    public string UiCameraPath
    {
        get { return _uiCameraPath; }
    }

    public string CanvasMidPath
    {
        get { return _canvasMidPath; }
    }

    public string CanvasLowPath
    {
        get { return _canvasLowPath; }
    }

    public string CanvasHeightPath
    {
        get { return _canvasHeightPath; }
    }

    public string CanvasTopPath
    {
        get { return _canvasTopPath; }
    }

    #endregion

    //主动构建
    public void Build()
    {
       
    }

    //初始化
    public void Init()
    {
        //画布路径        
        _canvasLowPath = "UICanvas_Low";
        _canvasMidPath = "UICanvas_Mid";
        _canvasHeightPath = "UICanvas_Height";
        _canvasTopPath = "UICanvas_Top";
        //相机路径
        _uiCameraPath = "UICamera";
        //EventSystem路径
        _eventSystemPath = "EventSystem";

        #region 关联面板            

        //绑定资源和面板实例                       
        //幕布面板         

        //  BindDialog(UIDialogType.Curtain, UICanvasType.Top, "CurtainDialog", CurtainDialog.Instance, true);         
        //消息面板         
        // BindDialog(UIDialogType.Message, UICanvasType.Top, "MessageDialog", MessageDialog.Instance, true);

       

        #endregion

        #region 关联动态组件                     

        //消息组件
        //  BindCom<Messageitem>(UIComType.MessageItem, "MessageItem"); //绑定组件         
        #endregion


    }
    //关联主场景界面
    public void BindMainScence(Action onComplete =null)
    {

        

        //加载面板
      //  BindDialog(UIDialogType.LoadingDialog, UICanvasType.Top, "LoadingPanel", LoadingDialog.Instance, true);
      

        UIManager.Instance.CheckDestroy();
        UIManager.Instance.CheckPreload();
        onComplete?.Invoke();
    }

    //关联游戏场景界面
    public void BindGameScence(Action onComplete = null)
    {
       
        UIManager.Instance.CheckDestroy();
        UIManager.Instance.CheckPreload();
        onComplete?.Invoke();

    }

    #region 绑定

    /// <summary>
    ///  绑定面板资源面板路径,dialogType:面板类型，path：资源路径，canvasType：面板所属画布，isPreload：是否预加载
    /// </summary>
    /// <param name="dialogType"></param>
    /// <param name="path"></param>
    /// <param name="canvasType"></param>
    /// <param name="isPreload"></param>
    private void BindDialog(UIDialogType dialogType, UICanvasType canvasType, string path, IUIDialog dialog,bool isPreload)
    {
        if (_dicDialogPath.ContainsKey(dialogType))
        {
            throw new Exception("已经存在重复key!");
        }
        _dicDialogPath.Add(dialogType, new DialogPath(dialogType, path, canvasType, isPreload));
        dialog.BindDialogType(dialogType);
        UIManager.Instance.AddDialogToDic(dialog); //添加实例到UI管理器            
    }

    /// <summary>
    ///  获取面板资源路径,uiDialogType:面板类型
    /// </summary>
    /// <param name="uiDialogType"></param>
    /// <returns></returns>
    public string GetDialogPath(UIDialogType uiDialogType)
    {
        if (_dicDialogPath.ContainsKey(uiDialogType))
        {
            return _dicDialogPath[uiDialogType].ResPath; //返回路径
        }
        throw new Exception("UIContent未绑定" + uiDialogType + "面板路径");
    }

    /// <summary>
    /// 获取面板所属画布类型,uiDialogType:面板类型
    /// </summary>
    /// <param name="uiDialogType"></param>
    /// <returns></returns>
    public UICanvasType GetDialogCanvasType(UIDialogType uiDialogType)
    {
        if (_dicDialogPath.ContainsKey(uiDialogType))
        {
            return _dicDialogPath[uiDialogType].CanvasType; //返回路径
        }
        throw new Exception("UIContent未绑定" + uiDialogType + "面板路径");
    }

    /// <summary>
    /// 是否需要预加载，uiDialogType:面板类型
    /// </summary>
    /// <param name="uiDialogType"></param>
    /// <returns></returns>
    public bool NeedPreload(UIDialogType uiDialogType)
    {
        if (_dicDialogPath.ContainsKey(uiDialogType))
        {
            return _dicDialogPath[uiDialogType].IsPreload; //是否需要预加载
        }
        throw new Exception("UIContent未绑定" + uiDialogType + "面板路径");
    }

    /// <summary>
    ///绑定动态组件资源路径，comType:组件类型，path：组件资源路径
    /// </summary>
    /// <param name="comType"></param>
    /// <param name="path"></param>
    public void BindCom<T>(UIComType comType, string path) where T : UIComponet, new()
    {
        if (!_dicComPath.ContainsKey(comType) && !_dicBuildComHandles.ContainsKey(comType))
        {
            _dicComPath.Add(comType, path);
            _dicBuildComHandles.Add(comType, () => { return CreateComponet<T>(comType); });
            return;
        }
        throw new Exception("已经存在重复key!");
    }

    //创建组件  
    private static T CreateComponet<T>(UIComType comType) where T : UIComponet, new()
    {
        T t = new T();
        t.BuildCom(comType, () => {});
        return t;
    }

    /// <summary>
    /// 获取动态组件资源路径，comType:组件类型
    /// </summary>
    /// <param name="comType"></param>
    /// <returns></returns>
    public string GetComPath(UIComType comType)
    {
        if (_dicComPath.ContainsKey(comType))
        {
            return _dicComPath[comType];
        }
        throw new Exception("UIContent未绑定" + comType + "路径");
    }

    /// <summary>
    /// 创建组件实例，comType:组件实例
    /// </summary>
    /// <param name="comType"></param>
    /// <returns></returns>
    public UIComponet CreateComInstance(UIComType comType)
    {
        if (_dicBuildComHandles.ContainsKey(comType))
        {
            return _dicBuildComHandles[comType]();
        }
        throw new Exception("UIContent未绑定" + comType + "创建代理");
    }

    #endregion

    //清理//TODO
    public void Clear()
    {
        _instance = null;
    }
}

//面板路径你
public class DialogPath
{
    public string ResPath; //资源路径
    public UIDialogType DialogType; //面板类型
    public UICanvasType CanvasType; //画布类型
    public bool IsPreload; //是否预加载

    public DialogPath(UIDialogType dialogType, string path, UICanvasType canvasType, bool isPreload)
    {
        DialogType = dialogType;
        ResPath = path;
        CanvasType = canvasType;
        IsPreload = isPreload;
    }
}