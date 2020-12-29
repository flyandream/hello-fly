using UnityEngine;

public class CurtainDialog : UIDialog<CurtainDialog>
{
    private const string ANIM_NAME_WHITE_TO_BLACK = "WhiteToBlack";//白到黑
    private const string ANIM_NAME_BLACK_TO_WHITE = "BlackToWhite";//黑到白
    private const string ANIM_NAME_WHITE_BLACK_WHITE = "WhiteBlackWhite";//白-黑-白

    public delegate  void OnAnimEventHandle();//动画事件代理

    private OnAnimEventHandle _onBlackToWhiteEndHandle;//黑到白结束回调
    private OnAnimEventHandle _onWhiteToBlackEndHandle;//白到黑结束回调
    private OnAnimEventHandle _onWhiteBlackWhiteMidHandle;//白黑白，中间回调
    private OnAnimEventHandle _onWhiteBlackWhiteEndHandle;//白黑白，结束时回调

    private Animation _anim;//动画组件

    public override void Init()
    {
        base.Init();
       //获取组件
        _anim = PanelTran.GetComponent<Animation>();        
        AnimationEventLoader loader = AnimationEventLoader.BuildBind(_anim);
        loader.AddEvent(_anim.GetClip(ANIM_NAME_BLACK_TO_WHITE), 1, OnAnimEndBlackToWhite);//黑到白，结束回调
        loader.AddEvent(_anim.GetClip(ANIM_NAME_WHITE_TO_BLACK), 1, OnAnimEndWhiteToBlack);//白到黑，结束回调                
        loader.AddEvent(_anim.GetClip(ANIM_NAME_WHITE_BLACK_WHITE), 0.5f, OnAnimMidWhiteBlackWhite);//白黑白-中间回调      
        loader.AddEvent(_anim.GetClip(ANIM_NAME_WHITE_BLACK_WHITE), 1, OnAnimEndWhiteBlackWhite);//白黑白-结束回调      
    }

    //黑到白幕
    public void BlackToWhite(OnAnimEventHandle endHandle=null)
    {
        Show(true);
        //设置回调    
        _onBlackToWhiteEndHandle = endHandle;
        //播放动画   
        _anim.Play(ANIM_NAME_BLACK_TO_WHITE);
    }
    //白到黑
    public void WhiteToBlack(OnAnimEventHandle endHandle = null)
    {
        Show(true);
        //设置回调 
        _onWhiteToBlackEndHandle = endHandle;
        //播放动画
        _anim.Play(ANIM_NAME_WHITE_TO_BLACK);
    }
    //白黑白
    public void WhiteBlackWhite(OnAnimEventHandle midHandle=null,OnAnimEventHandle endHandle=null)
    {
        Show(true);
        //设置回调 
        _onWhiteBlackWhiteMidHandle = midHandle;
        _onWhiteBlackWhiteEndHandle = endHandle;
        //播放动画
        _anim.Play(ANIM_NAME_WHITE_BLACK_WHITE);

    }

    #region 动画回调
    //动画结束回调-黑到白
    private void OnAnimEndBlackToWhite()
    {
        if (_onBlackToWhiteEndHandle != null)
        {
            _onBlackToWhiteEndHandle();
        }
        //隐藏
        Hide();
    }
    //动画结束回调-白到黑
    private void OnAnimEndWhiteToBlack()
    {
        if (_onWhiteToBlackEndHandle != null)
        {
            _onWhiteToBlackEndHandle();
        }    
    }
    
    //白黑白-动画中心回调
    private void OnAnimMidWhiteBlackWhite()
    {
        if (_onWhiteBlackWhiteMidHandle!=null)
        {
            _onWhiteBlackWhiteMidHandle();
        }
    }

    //白黑白-动画结束回调
    private void OnAnimEndWhiteBlackWhite()
    {
        if (_onWhiteBlackWhiteEndHandle != null)
        {
            _onWhiteBlackWhiteEndHandle();
        }
        Hide();
       
    }
    #endregion

   

}
