using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///子面板基类
/// </summary>
public class BaseSubDialog : IBaseSubDialog {

    public delegate void DialogShowOrHideEventHandle(bool isVisble);//面板显示隐藏事件代理
    public event DialogShowOrHideEventHandle OnShowOrHideEvent;//面板显示隐藏事件

    protected GameObject _panelGo = null;//子面板对象
    protected Transform _panelTran = null;//子面板Transform      
    protected bool _isVisible = false;
    protected CanvasGroup _canvasGroup;
    public GameObject PanelGo
    {
        get
        {
            return _panelGo;
        }
    }

    public Transform PanelTran
    {
        get { return _panelTran; }
    }

    //绑定子面板
    public virtual void BindSubDialog(GameObject panelGo)
    {
        _panelGo = panelGo;        
        _panelTran = _panelGo.transform;
        _canvasGroup = _panelGo.GetComponent<CanvasGroup>();
        _isVisible = false;
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        Init();
    }

    public bool IsVisble
    {
        get { return _isVisible; }
        set { _isVisible = value; }
    }
    //初始化
    public virtual void Init()
    {

    }

    public virtual void Show()
    {

        if (_isVisible == true)
        {
            return;
        }    
        CanvasShowLogic();//画布显示逻辑
        AfterShow();
        _isVisible = true;
        InvokeOnShowOrHideEvent();//处理面板激活事件
    }
    //面板画布显示逻辑
    public virtual void CanvasShowLogic()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
    }

    public virtual void Hide()
    {
        if (_isVisible == false)
        {
            return;
        }
        CanvasHideLogic();
        AfterHide();
        RecycleCom();//回收组件
        _isVisible = false;
        InvokeOnShowOrHideEvent();//处理面板激活事件
    }

    //面板画布隐藏逻辑
    public virtual void CanvasHideLogic()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }
    //显示后调用
    public virtual void AfterShow()
    {

    }
    //隐藏后调用
    public virtual void AfterHide()
    {

    }
    /// <summary>
    /// 回收组件
    /// </summary>
    public virtual void RecycleCom()
    {

    }
    //更新逻辑
    public virtual void Update()
    {

    }
    //按钮监听
    protected virtual void OnBtnClick(Button btn)
    {

    }

    #region 显示隐藏事件
    /// <summary>
    /// 订阅显示隐藏事件
    /// </summary>
    /// <param name="handle"></param>
    public void SubscibeOnShowOrHideEvent(DialogShowOrHideEventHandle handle)
    {
        OnShowOrHideEvent += handle;
    }
    /// <summary>
    /// 取消订阅显示隐藏事件
    /// </summary>
    /// <param name="handle"></param>
    public void UnsubscribeOnShowOrHideEvent(DialogShowOrHideEventHandle handle)
    {
        OnShowOrHideEvent -= handle;
    }
    /// <summary>
    ///  处理显示隐藏的事件订阅
    /// </summary>
    protected void InvokeOnShowOrHideEvent()
    {
        if (OnShowOrHideEvent != null)
        {
            OnShowOrHideEvent(IsVisble);
        }
    }
    #endregion

}
