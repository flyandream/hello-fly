using UnityEngine;

    public interface IUIDialog
    {
        UIDialogType DialogType { get; }
        UICanvasType CanvasType { get; }
        bool IsVisble { get; set; }
        GameObject PanelGo { get; }
        Transform PanelTran { get; }
        int SiblingIndex { get; }//所在层级
        int OffsetSiblingIndex { get; }//所在层级
        void Init();//初始化
        void BindDialogType(UIDialogType uiDialogType);//绑定面板
        void Show();//显示方法
        void CanvasShowLogic();//画布显示逻辑
        void Hide();//隐藏方法
        void HideByAnim();//动画隐藏
        void CanvasHideLogic();//画布隐藏逻辑
        void AfterShow();//显示后调用
        void AfterHide();//隐藏之后调用
        void RecycleCom();//回收组件
        void UnloadTexture();//释放图片资源
        void SetHierarchy(bool isLastIndex, int index);//设置层级
        void Preload();//预加载
        void LoadStaticInfo();//加载静态文本
        void OnLanguageChanged();//当语言切换时
        void IsDestroy();



    }



