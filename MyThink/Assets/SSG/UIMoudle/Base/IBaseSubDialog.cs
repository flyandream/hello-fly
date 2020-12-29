using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//子面板
public interface IBaseSubDialog  {
    bool IsVisble { get; set; }
    GameObject PanelGo { get; }
    Transform PanelTran { get; }
    //绑定子面板
    void BindSubDialog(GameObject panelGo);
    void Init();//初始化
    void Show();//显示方法
    void CanvasShowLogic();//画布显示逻辑
    void Hide();//隐藏方法
    void CanvasHideLogic();//画布隐藏逻辑
    void AfterShow();//显示后调用
    void AfterHide();//隐藏之后调用
    void RecycleCom();//回收组件
}
