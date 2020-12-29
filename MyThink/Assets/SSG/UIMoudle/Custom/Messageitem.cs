using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 通知面板的信息组件
/// </summary>
public class Messageitem : UIComponet
{
    private const string ANIM_NAME_SHOW = "MessageItemShow";
    private Text _txtMessage;//通知文本
    private Animation _anim;//动画组件
    private bool _isCanUse;//是否可用
    //是否可用
    public bool IsCanUse
    {
        get
        {
            return _isCanUse;
        }

    }
    public override void Init()
    {
        base.Init();
        _txtMessage = ComTran.Find("TxtMessage").GetComponent<Text>();
        _anim = ComTran.GetComponent<Animation>();
        _isCanUse = true;//初始化为显示完成
        //加入动画事件
        AnimationEventLoader animationEventLoader =AnimationEventLoader.BuildBind(_anim,true);
        animationEventLoader.AddEvent(_anim.GetClip(ANIM_NAME_SHOW),1,OnAnimShowEnd);
    }
    //动画结束回调
    private void OnAnimShowEnd()
    {
        _isCanUse = true;
    }
    //动画显示信息
    public void ShowMessageByAnim(string message)
    {
        ComTran.SetAsFirstSibling();//放在最上面
        _txtMessage.text = message;
        _isCanUse = false;
        _anim.Play(ANIM_NAME_SHOW);
    }
    //回收组件
    public override void Recycle()
    {
        base.Recycle();
        if (_anim!=null)
        {
            _anim.Stop();
        }
        _isCanUse = false;
    }


}
