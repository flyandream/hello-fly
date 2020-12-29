using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 横向进度条
/// </summary>
public class FilledHorizontalImgBar
{
    private RectTransform _rectTransform;//宽度
    private Image _img;//图片

    public Image Img
    {
        get { return _img; }
    }

    public RectTransform RectTransform
    {
        get { return _rectTransform; }
    }

    private float _validWidth;//有效宽度
    private float _lockWidth;//锁死宽度


    public float FillAmount
    {
        get { return (_rectTransform.sizeDelta.x - _lockWidth) / _validWidth; }
    }
    //构造滑动条
    public FilledHorizontalImgBar(Image img, float width,float lockWidth=0)
    {
        _rectTransform = img.rectTransform;
        _img = img;
        _validWidth = width- lockWidth; //计算有效宽度
        _lockWidth = lockWidth;
    }
    //设置图片长度
    public void SetFillAmount(float per)
    {
        if (per > 1)
        {
            per = 1;
        }
        if (per < 0)
        {
            per = 0;
        }
        _rectTransform.sizeDelta = new Vector2(_lockWidth+_validWidth * per, _rectTransform.sizeDelta.y);//设置长度
    }
}