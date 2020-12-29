using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 闪烁文本
/// </summary>
public class FlashTxt
{
    private Color _colorPrimitive;//原始颜色
    private Color _colorChange;//改变的颜色
    private Text _txt;//闪烁的文本
    private int dirFlag;//方向标志（取1或者-1）
    private bool _isNeedFlash;//是否需要闪烁
    private float _flashTime;//闪烁时间
    private float _curFlashLerpTime;//当前闪烁插值时间
    public FlashTxt(Text txt, Color colorPrimitive, Color colorChange, float flashTime)
    {
        _txt = txt;
        _colorPrimitive = colorPrimitive;
        _colorChange = colorChange;
        _flashTime = flashTime;
        dirFlag = 1;
    }
    public void Update()
    {

        if (!_isNeedFlash)
        {
            return;
        }
        _curFlashLerpTime += Time.deltaTime;
        float lerpProc = Mathf.Clamp(_curFlashLerpTime / _flashTime, 0, 1);//插值进度
        if (dirFlag == 1)
        {
            //渐变成颜色2
            _txt.color = Color.Lerp(_txt.color, _colorChange, lerpProc);
            if (lerpProc == 1)//如果进度等于1
            {
                ChangeDirFlag();
                ResetLerpTime();//插值时间复位
            }
        }
        else
        {
            //恢复颜色1
            _txt.color = Color.Lerp(_txt.color, _colorPrimitive, lerpProc);
            if (lerpProc == 1)//如果进度等于1
            {
                ChangeDirFlag();
                ResetLerpTime();//插值时间复位
            }
        }
    }

    //切换当前标志位方向
    private void ChangeDirFlag()
    {
        dirFlag *= -1;
    }

    //复位插值时间
    private void ResetLerpTime()
    {
        _curFlashLerpTime = 0;
    }

    //开启闪烁
    public void OpenFlash()
    {
        if (_isNeedFlash)
        {
            return;
        }
        _isNeedFlash = true;
        _txt.color = _colorPrimitive;//颜色复位
        ResetLerpTime();//插值时间复位
    }
    //关闭闪烁
    public void CloseFlash()
    {
        if (!_isNeedFlash)
        {
            return;
        }
        _isNeedFlash = false;
        _txt.color = _colorPrimitive;//颜色复位
    }

}
