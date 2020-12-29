using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public delegate void DialogVisbleChangedEventHandle(bool isVisble);//面板显示隐藏事件代理
public class UIDialog<T> : IUIDialog where T : new()
{
    public event DialogVisbleChangedEventHandle OnVisbleChangedEvent;//面板显示隐藏事件
    protected static readonly object lylock = new object();
    protected static T _Instance;
    public static T Instance
    {
        get
        {
            if (_Instance == null)
            {
                lock (lylock)
                {
                    if (_Instance == null)
                    {
                        _Instance = new T();
                    }
                }
            }
            return _Instance;
        }
    }
    protected GameObject _panelGo = null;//面板对象
    protected Transform _panelTran = null;//面板Transform             
    protected UIDialogType _dialogType;//面板类型  
    private UICanvasType _canvasType;//画布类型
    protected bool _isVisible = false;
    protected CanvasGroup _canvasGroup;
    protected bool _hadPreLoad = false;//是否进行过预加载
    bool _isPreloading = false;
    protected bool _languageChangedTrigger = true;//语言改变触发
    protected bool _isloadComPlete = false;//面板是否加载完成

    //面板根GameObject
    public GameObject PanelGo
    {
        get
        {
            return _panelGo;
        }
    }
    //面板根Transform
    public Transform PanelTran
    {
        get { return _panelTran; }
    }

    //面板当前的所在的层级
    public int SiblingIndex
    {
        get
        {
            if (PanelTran != null)
            {
                return PanelTran.GetSiblingIndex();
            }
            return 0;//默认层级为0
        }
    }

    //面板在所在偏移的层级
    public int OffsetSiblingIndex
    {
        get
        {
            if (PanelTran != null)
            {
                return PanelTran.GetSiblingIndex() - UIManager.Instance.GetMaxSiblingIndex(_canvasType);
            }
            return 0;//默认层级为0
        }
    }

    #region 属性
    //面板是否激活
    public bool IsVisble
    {
        get { return _isVisible; }
        set { _isVisible = value; }
    }

    //面板类型
    public UIDialogType DialogType
    {

        get { return _dialogType; }
    }

    public UICanvasType CanvasType
    {
        get
        {
            return _canvasType;
        }
    }
    #endregion

    //绑定面板类型
    public void BindDialogType(UIDialogType uiDialogType)
    {
        _dialogType = uiDialogType;
        _canvasType = UIContent.Instance.GetDialogCanvasType(_dialogType);
    }
    //初始化
    public virtual void Init()
    {
        //监听语言变化事件
        LanguageManager.Instance.OnLanguageTypeChange += SubscriptionLanguageChanged;
        LanguageManager.Instance.OnLanguageTypeChange += OnLanguageChangedImmediate;
    }

    #region 显示，隐藏

    //显示逻辑
    public virtual void Show()
    {

        if (_isVisible == true)
        {
            return;
        }
        if (_panelGo == null && !_hadPreLoad)
        {
            if (_isPreloading)
            {
                TimerManager.Instance.StartCoroutine(DelayShowWnd());
            }
            else
            {
                //实例化Panel
                InstantiatePanel(() =>
                {

                    CanvasShowLogic();//画布显示逻辑
                    AfterShow();
                    _isVisible = true;
                    InvokeVisbleChangedEvent();//处理显示订阅
                    _isloadComPlete = true;
                });
            }

        }
        else
        {
            CanvasShowLogic();//画布显示逻辑
            AfterShow();
            _isVisible = true;
            InvokeVisbleChangedEvent();//处理显示订阅
            _isloadComPlete = true;
        }
    }

    IEnumerator DelayShowWnd()
    {
        while (_isPreloading)
        {
            yield return null;
        }
        CanvasShowLogic();//画布显示逻辑
        AfterShow();
        _isVisible = true;
        InvokeVisbleChangedEvent();//处理显示订阅
        _isloadComPlete = true;
    }

    //带层级参数的显示逻辑
    public virtual void Show(bool isLast, int offsetIndex = 0)
    {
        Show();//调用显示逻辑

        SetHierarchy(isLast, offsetIndex);
    }
    //面板画布显示逻辑
    public virtual void CanvasShowLogic()
    {

        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;

    }

    //显示后调用
    public virtual void AfterShow()
    {

        if (_languageChangedTrigger)
        {
            //如果语言发生改变
            LoadStaticInfo();//读取静态信息
            OnLanguageChanged();//处理语言变化事件
            _languageChangedTrigger = false;//复位标志位
        }

    }

    //隐藏
    public virtual void Hide()
    {
        if (_isVisible == false)
        {
            return;
        }
        CanvasHideLogic();
        AfterHide();
        RecycleCom();//回收组件
        UnloadTexture();//回收图片资源
                        //释放资源
        Resources.UnloadUnusedAssets();
        _isVisible = false;
        InvokeVisbleChangedEvent();//处理显隐藏逻辑订阅     


    }
    //动画隐藏
    public virtual void HideByAnim()
    {

    }

    //面板画布隐藏逻辑
    public virtual void CanvasHideLogic()
    {

        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;

    }

    //隐藏后调用
    public virtual void AfterHide()
    {

    }
    //显示/隐藏变化事件           
    protected void InvokeVisbleChangedEvent()
    {
        if (OnVisbleChangedEvent != null)
        {
            OnVisbleChangedEvent(IsVisble);
        }
    }

    #endregion

    #region 回收，释放资源
    /// <summary>
    /// 回收组件
    /// </summary>
    public virtual void RecycleCom()
    {

    }
    //释放图片资源
    public virtual void UnloadTexture()
    {

    }
    #endregion

    #region 面板层级变化
    /// <summary>
    /// 设置层级
    /// </summary>
    /// <param name="isLast"></param>
    /// <param name="index"></param>
    public virtual void SetHierarchy(bool isLast, int offsetIndex = 0)
    {
        if (!_panelTran)
        {
            return;
        }
        if (isLast)
        {
            //判断是否已经显示在最后方
            if (!UIManager.Instance.IsLastShowDialog(this))
            {
                _panelTran.SetAsLastSibling();//设置为最后一个
            }
            return;
        }
        int aimSiblingIndex = UIManager.Instance.LastShowDialogSiblingIndex(_canvasType) + offsetIndex;//计算目标层级
        if (aimSiblingIndex < 0)
        {
            aimSiblingIndex = 0;//不可以小于0
        }
        _panelTran.SetSiblingIndex(aimSiblingIndex);

    }

    #endregion

    #region 预加载
    /// <summary>
    /// 预加载面板
    /// </summary>
    public virtual void Preload()
    {
        //避免重复预加载
        if (_hadPreLoad)
        {
            return;
        }
        if (_panelGo == null)
        {
            InstantiatePanel();
        }


    }
    #endregion

    #region 更新
    public virtual void Update()
    {

    }
    #endregion

    #region 按钮事件

    //按钮监听
    protected virtual void OnBtnClick(Button btn)
    {

    }
    #endregion

    #region 语言相关
    //订阅语言变化
    private void SubscriptionLanguageChanged()
    {
        _languageChangedTrigger = true;//复位标志位
    }
    //读取静态文本
    public virtual void LoadStaticInfo()
    {

    }

    //当语言切换时
    public virtual void OnLanguageChanged()
    {

    }

    //当语言切换时
    public virtual void OnLanguageChangedImmediate()
    {

    }


    #endregion
    public virtual void IsDestroy()
    {
        if (_panelGo == null)
        {
            _hadPreLoad = false;
            _isPreloading = false;
        }
    }



    //实例面板预制物
    private void InstantiatePanel(Action onCreate = null)
    {
        _isPreloading = true;
        UIManager.Instance.CreateDialog(_dialogType, _canvasType, (_panelGo) =>
          {
              _isPreloading = false;
              _hadPreLoad = true;
              _panelTran = _panelGo.transform;
              _canvasGroup = _panelGo.GetComponent<CanvasGroup>();
              _panelGo.SetActive(true);
              _canvasGroup.alpha = 0;
              _canvasGroup.blocksRaycasts = false;
                //执行初始化
                Init();

              onCreate?.Invoke();
          });


    }


}
