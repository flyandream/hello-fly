using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 通知面板
/// </summary>
public class MessageDialog : UIDialog<MessageDialog>
{
    private const string ANIM_NAME_SHOW = "MessageItemShowOnly";
    private Transform _messageItemRoot;//信息父节点
    private List<Messageitem> _listMessageItem=new List<Messageitem>();//信息组件列表
    private CanvasGroup _messageItemCanvasGroup;//消息组件canvasgroup
    private Animation _messageItemAnim;//消息动画组件
    private Text _txtInfo;//文本-消息        

    public override void Init()
    {
        base.Init();
        _messageItemRoot = PanelTran.Find("MessageContent");
        _messageItemCanvasGroup = PanelTran.Find("MessageItemOnly").GetComponent<CanvasGroup>();
        _messageItemAnim = PanelTran.Find("MessageItemOnly").GetComponent<Animation>();
        _txtInfo = PanelTran.Find("MessageItemOnly/TxtMessage").GetComponent<Text>();  
    }

    public override void AfterShow()
    {
        base.AfterShow();
        _messageItemCanvasGroup.alpha = 0;

    }

    public override void AfterHide()
    {
        base.AfterHide();    
    }

    public override void RecycleCom()
    {
        base.RecycleCom();
        for (int i = 0; i < _listMessageItem.Count; i++)
        {
            _listMessageItem[i].Recycle();//回收组件
        }
        _listMessageItem.Clear();

    }

    /// <summary>
    /// 显示信息,多条
    /// </summary>
    public void ShowMsgMore(string message)
    {
        Show(true);
     //   for (int i = 0; i < _listMessageItem.Count; i++)//遍历列表
     //   {
     //       if (_listMessageItem[i].IsCanUse)
     //       {
     //           //如果可用
     //           _listMessageItem[i].ShowMessageByAnim(message);
     //           return;//返回
     //       }
     //   }
     //   Messageitem messageitem=UIComponet.CreateInstance<Messageitem>(UIComType.MessageItem, _messageItemRoot);
     //   _listMessageItem.Add(messageitem);//加入列表   
     //   messageitem.ShowMessageByAnim(message);      
         _messageItemCanvasGroup.alpha = 0;
         _txtInfo.text = message;
         _messageItemAnim.Stop();
         _messageItemAnim.Play(ANIM_NAME_SHOW);
    }

    /// <summary>
    /// 显示信息，单条
    /// </summary>
    public void ShowMsgOnly(string message)
    {
        Show(true);
        _messageItemCanvasGroup.alpha = 0;
        _txtInfo.text = message;
        _messageItemAnim.Stop();
        _messageItemAnim.Play(ANIM_NAME_SHOW);
       
    }

    public override void Preload()
    {
        base.Preload();
       // UIManager.Instance.PreloadCom<Messageitem>(UIComType.MessageItem, 3);
    }
}
