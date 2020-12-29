using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 过渡图片
/// </summary>
public class FillAmountLerpImg
{
    private const float WAIT_TO_LERP_TIME = 0.5f;
    private Image _img;//图片
    private float _curLerpTime;// 当前插值时间
    private float _lerpTime;//插值时间
    private bool _isNeedLerp;//需要插值
    private float _aimFillAmount;//目标值
    private float _waitLerpTime;
    private bool _useWaitTime;//是否使用等待插值时间
    // 构造函数
    public FillAmountLerpImg(Image img, float lerpTime, bool useWaitTime)
    {
        _img = img;
        _lerpTime = lerpTime;
        _aimFillAmount = 1;
        _curLerpTime = 0;
        _useWaitTime = useWaitTime;
        _isNeedLerp = false;
    }

    public void Update()
    {

        if (!_isNeedLerp)
        {
            return;
        }
        if (_useWaitTime)
        {
            _waitLerpTime += Time.deltaTime;
            if (_waitLerpTime > WAIT_TO_LERP_TIME)
            {
                _curLerpTime += Time.deltaTime;
                float lerpProc = Mathf.Clamp(_curLerpTime / _lerpTime, 0, 1);//插值进度
                _img.fillAmount = Mathf.Lerp(_img.fillAmount, _aimFillAmount, lerpProc);
            }
        }
        else
        {
            _curLerpTime += Time.deltaTime;
            float lerpProc = Mathf.Clamp(_curLerpTime / _lerpTime, 0, 1);//插值进度
            _img.fillAmount = Mathf.Lerp(_img.fillAmount, _aimFillAmount, lerpProc);
        }



    }
    //插值时间复位
    private void ResetCurLerpTime()
    {
        _waitLerpTime = 0;
        _curLerpTime = 0;
    }

    //开启插值
    public void OpenLerp(float aimFillAmount)
    {


        if (_isNeedLerp && _aimFillAmount == aimFillAmount)
        {
            return;
        }
        _aimFillAmount = aimFillAmount;//设置目标位置     
        _isNeedLerp = true;
        ResetCurLerpTime(); //时间复位

    }

    //关闭插值
    public void CloseLerp()
    {
        if (!_isNeedLerp)
        {
            return;
        }
        _isNeedLerp = false;
    }

}
