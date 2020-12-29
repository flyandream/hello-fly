using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI动态组件
/// </summary>
public interface IUIComponet
{
    bool IsVisble { get; }//是否激活
    UIComType ComType { get; }//组件类型
    GameObject ComGo { get; }//组件父GameObject
    Transform ComTran { get; }//组件父Transform
    void Init();//初始化
    void Recycle();//回收组件

    void UnloadTexture();//释放图片资源

    void SetVisble(bool isVisble);//设置是否有效

    void OnBtnClick(Button btn);//按钮回调




}
