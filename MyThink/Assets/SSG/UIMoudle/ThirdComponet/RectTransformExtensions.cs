using UnityEngine;
/// <summary>
/// Anchor位置
/// </summary>
public enum AnchorPresets
{
    TopLeft,
    TopCenter,
    TopRight,

    MiddleLeft,
    MiddleCenter,
    MiddleRight,

    BottomLeft,
    BottomCenter,
    BottomRight,
    BottomStretch,

    VertStretchLeft,
    VertStretchRight,
    VertStretchCenter,

    HorStretchTop,
    HorStretchMiddle,
    HorStretchBottom,

    StretchAll
}
/// <summary>
/// 锚点位置
/// </summary>
public enum PivotPresets
{
    TopLeft,
    TopCenter,
    TopRight,

    MiddleLeft,
    MiddleCenter,
    MiddleRight,

    BottomLeft,
    BottomCenter,
    BottomRight,
}
/// <summary>
/// 
/// </summary>
public static class RectTransformExtensions
{  
    private static readonly Vector2 vTopLeft = new Vector2(0, 1);//左上
    private static readonly Vector2 vMidLeft = new Vector2(0, 0.5f);//左中
    private static readonly Vector2 vBottomLeft = new Vector2(0, 0);//左下

    private static readonly Vector2 vTopRight = new Vector2(1, 1);//右上
    private static readonly Vector2 vMidRight = new Vector2(1, 0.5f);//右中
    private static readonly Vector2 vBottomRight = new Vector2(1, 0f);//右下

    private static readonly Vector2 vTopCenter = new Vector2(0.5f, 1);//中上 
    private static readonly Vector2 vMidCenter = new Vector2(0.5f, 0.5f);//中中
    private static readonly Vector2 vBottomCenter = new Vector2(0.5f, 0);//中下



    /// <summary>
    /// 设置Anchor
    /// </summary>
    /// <param name="source"></param>
    /// <param name="allign"></param>
    /// <param name="offsetX"></param>
    /// <param name="offsetY"></param>
    public static void SetAnchor( RectTransform source, AnchorPresets allign, int offsetX = 0, int offsetY = 0)
    {
        source.anchoredPosition = new Vector3(offsetX, offsetY, 0);

        switch (allign)
        {
            case (AnchorPresets.TopLeft)://左上
            {
                    source.anchorMin = vTopLeft;
                    source.anchorMax = vTopLeft;
                    break;
                }
            case (AnchorPresets.TopCenter)://中上
                {
                    source.anchorMin =vTopCenter;
                    source.anchorMax = vTopCenter;
                    break;
                }
            case (AnchorPresets.TopRight)://右上
            {
                source.anchorMin = vTopRight;
                    source.anchorMax = vTopRight;
                    break;
                }

            case (AnchorPresets.MiddleLeft)://左中
            {
                source.anchorMin = vMidLeft;
                    source.anchorMax = vMidLeft;
                    break;
                }
            case (AnchorPresets.MiddleCenter)://中心
            {
                source.anchorMin = vMidCenter;
                    source.anchorMax = vMidCenter;
                    break;
                }
            case (AnchorPresets.MiddleRight)://右中
            {
                source.anchorMin = vMidRight;
                    source.anchorMax = vMidRight;
                    break;
                }

            case (AnchorPresets.BottomLeft)://左下
            {
                source.anchorMin = vBottomLeft;
                source.anchorMax = vBottomLeft;
                    break;
                }
            case (AnchorPresets.BottomCenter)://中下
            {
                source.anchorMin = vBottomCenter;
                    source.anchorMax = vBottomCenter;
                    break;
                }
            case (AnchorPresets.BottomRight)://右下
            {
                source.anchorMin = vBottomRight;
                    source.anchorMax = vBottomRight;
                    break;
                }

            case (AnchorPresets.HorStretchTop)://顶部水平延伸
            {
                source.anchorMin = vTopLeft;
                source.anchorMax = vTopRight;
                    break;
                }
            case (AnchorPresets.HorStretchMiddle)://中心水平延伸
            {
                source.anchorMin = vMidLeft;
                source.anchorMax = vMidRight;
                    break;
                }
            case (AnchorPresets.HorStretchBottom)://底部水平延伸
            {
                source.anchorMin = vBottomLeft;
                source.anchorMax = vBottomRight;
                    break;
                }

            case (AnchorPresets.VertStretchLeft)://左部垂直延伸
            {
                source.anchorMin = vBottomLeft;
                source.anchorMax = vTopLeft;
                    break;
                }
            case (AnchorPresets.VertStretchCenter)://中心垂直延伸
            {
                source.anchorMin = vBottomCenter;
                    source.anchorMax = vTopCenter;
                    break;
                }
            case (AnchorPresets.VertStretchRight)://右端垂直延伸
            {
                source.anchorMin = vBottomRight;
                source.anchorMax = vTopRight;
                    break;
                }

            case (AnchorPresets.StretchAll)://全展开
            {
                source.anchorMin = vBottomLeft;
                source.anchorMax = vTopRight;
                    break;
                }
        }
    }

    /// <summary>
    /// 设置Poivot
    /// </summary>
    /// <param name="source"></param>
    /// <param name="preset"></param>
    public static void SetPivot( RectTransform source, PivotPresets preset)
    {

        switch (preset)
        {
            case (PivotPresets.TopLeft)://左上
                {
                    source.pivot = vTopLeft;
                    break;
                }
            case (PivotPresets.TopCenter)://中上
            {
                source.pivot = vTopCenter;
                    break;
                }
            case (PivotPresets.TopRight)://右上
            {
                source.pivot = vTopRight;
                    break;
                }

            case (PivotPresets.MiddleLeft)://左中
            {
                source.pivot = vMidLeft;
                    break;
                }
            case (PivotPresets.MiddleCenter)://中心
            {
                source.pivot = vMidCenter;
                    break;
                }
            case (PivotPresets.MiddleRight)://右中
                {
                    source.pivot =vMidRight;
                    break;
                }

            case (PivotPresets.BottomLeft)://左下
                {
                source.pivot = vBottomLeft;
                    break;
                }
            case (PivotPresets.BottomCenter)://中下
            {
                source.pivot = vBottomCenter;
                    break;
                }
            case (PivotPresets.BottomRight)://右下
                {
                    source.pivot =vBottomRight;
                    break;
                }
        }
    }

    /// <summary>
    /// 获取Anchor
    /// </summary>
    /// <returns></returns>
    public static AnchorPresets GetAnchorPresets(RectTransform source)
    {           
        Vector2 anchorMin = source.anchorMin;
        Vector2 anchorMax = source.anchorMax;

        if (anchorMin == vTopLeft && anchorMax == vTopLeft)
        {
            return AnchorPresets.TopLeft;
        }
        else if(anchorMin == vTopCenter && anchorMax == vTopCenter)
        {
            return AnchorPresets.TopCenter;
        }else if (anchorMin == vTopRight && anchorMax == vTopRight)
        {
            return AnchorPresets.TopRight;
        }
        else if (anchorMin == vMidLeft && anchorMax == vMidLeft)
        {
            return AnchorPresets.MiddleLeft;
        }
        else if (anchorMin == vMidCenter && anchorMax == vMidCenter)
        {
            return AnchorPresets.MiddleCenter;
        }
        else if (anchorMin == vMidRight && anchorMax == vMidRight)
        {
            return AnchorPresets.MiddleRight;
        }
        else if (anchorMin == vBottomLeft && anchorMax == vBottomLeft)
        {
            return AnchorPresets.BottomLeft;
        }
        else if (anchorMin == vBottomCenter && anchorMax == vBottomCenter)
        {
            return AnchorPresets.BottomCenter;
        }
        else if (anchorMin == vBottomRight && anchorMax == vBottomRight)
        {
            return AnchorPresets.BottomRight;
        }
        else if (anchorMin == vTopLeft && anchorMax == vTopRight)
        {
            return AnchorPresets.HorStretchTop;
        }
        else if (anchorMin == vMidLeft && anchorMax == vMidRight)
        {
            return AnchorPresets.HorStretchMiddle;
        }
        else if (anchorMin == vBottomLeft && anchorMax == vBottomRight)
        {
            return AnchorPresets.HorStretchBottom;
        }
        else if (anchorMin == vBottomLeft && anchorMax == vTopLeft)
        {
            return AnchorPresets.VertStretchLeft;
        }
        else if (anchorMin == vBottomCenter && anchorMax == vTopCenter)
        {
            return AnchorPresets.VertStretchCenter;
        }
        else if (anchorMin == vBottomRight && anchorMax == vTopRight)
        {
            return AnchorPresets.VertStretchRight;
        }
        else if (anchorMin == vBottomLeft && anchorMax == vTopRight)
        {
            return AnchorPresets.VertStretchRight;
        }

        return AnchorPresets.MiddleCenter;//默认返回中心
    }

    public static PivotPresets GetPivotPresets(RectTransform source)
    {
        Vector2 pivot = source.pivot;//暂存锚点

        if (pivot ==vTopLeft)
        {
            return PivotPresets.TopLeft;
        }else if (pivot == vTopCenter)
        {
            return PivotPresets.TopCenter;
        }
        else if (pivot == vTopRight)
        {
            return PivotPresets.TopRight;
        }
        else if (pivot == vMidLeft)
        {
            return PivotPresets.MiddleLeft;
        }
        else if (pivot == vMidCenter)
        {
            return PivotPresets.MiddleCenter;
        }
        else if (pivot == vMidRight)
        {
            return PivotPresets.MiddleRight;
        }
        else if (pivot == vBottomLeft)
        {
            return PivotPresets.BottomLeft;
        }
        else if (pivot == vBottomCenter)
        {
            return PivotPresets.BottomCenter;
        }
        else if (pivot == vBottomRight)
        {
            return PivotPresets.BottomRight;
        }

        return  PivotPresets.MiddleCenter;//默认返回中心锚点
    }


}
