using UnityEngine;
using UnityEngine.UI;

public class UITools
{
    //空精灵
    public const string UI_SPRITE= "SSG_UI/UI_Sprite";
    //空贴图
    public const string UI_TEXTURE = "SSG_UI/UI_Texture";

    /// <summary>
    /// 加载Sprite
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Sprite LoadSprite(string path)
    {
        return Resources.Load(path, typeof (Sprite)) as Sprite;
    }

    /// <summary>
    /// 加载Texture2D
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Texture2D LoadTexture2D(string path)
    {
        return Resources.Load(path, typeof(Texture2D)) as Texture2D;
    }

    /// <summary>
    /// 设置文本颜色,color：颜色字符串例如#54E5FA,txtContent需要加上颜色变化的内容
    /// </summary>
    /// <param name="color"></param>
    /// <param name="txt"></param>
    /// <returns></returns>
    public static string SetTxtColor(string color, string txtContent)
    {
        return "<color=" + color + ">" + txtContent + "</color>";
    }

    /// <summary>
    /// 设置Anchor,rectTransform:位置组件，anchorPresets：anchor类型，offsetX:偏移量，offSetY：Y偏移量
    /// </summary>
    public static void SetAnchor(RectTransform rectTransform,AnchorPresets anchorPresets,int offsetX=0,int offSetY=0)
    {
        RectTransformExtensions.SetAnchor(rectTransform, anchorPresets, offsetX,offSetY);
    }

    /// <summary>
    /// 设置锚点，rectTransform：位置组件，pivotPreset：锚点类型
    /// </summary>
    public static void SetPivot(RectTransform rectTransform, PivotPresets pivotPreset)
    {
        RectTransformExtensions.SetPivot(rectTransform, pivotPreset);
    }

    /// <summary>
    /// 获取Anchor类型,rectTransform:UI坐标组件
    /// </summary>
    /// <returns></returns>
    public static AnchorPresets GetAnchorPresets(RectTransform rectTransform)
    {
       return  RectTransformExtensions.GetAnchorPresets(rectTransform);
    }
    /// <summary>
    /// 获取锚点类型,rectTransform:UI坐标组件
    /// </summary>
    /// <returns></returns>
    public static PivotPresets GetPivotPresets(RectTransform rectTransform)
    {
        return RectTransformExtensions.GetPivotPresets(rectTransform);
    }
    /// <summary>
    /// 复制RectTransform的参数
    /// </summary>
    /// <param name="target"></param>
    /// <param name="refer"></param>
    public static void CopyRectTransformParam(RectTransform target,RectTransform refer)
    {
        target.anchorMax = refer.anchorMax;
        target.anchorMin = refer.anchorMin;
        target.anchoredPosition = refer.anchoredPosition;
        target.anchoredPosition3D = refer.anchoredPosition3D;
        target.offsetMax = refer.offsetMax;
        target.offsetMin = refer.offsetMin;
        target.pivot = refer.pivot;        
        target.sizeDelta = refer.sizeDelta;
        target.position = refer.position;
        target.rotation = refer.rotation;
        target.localScale = refer.localScale;
    }

    /// <summary>
    /// 置灰
    /// </summary>
    /// <param name="btn"></param>
    /// <param name="isTrue"></param>
    public static void SetGray(Button btn,bool isTrue)
    {
        Image[] imgs = btn.GetComponentsInChildren<Image>();
        if (isTrue)
        {
            //置灰
            for (int i = 0; i < imgs.Length; i++)
            {
                imgs[i].material = UIEffectData.Instance.GrayMat;
            }
        }
        else
        {
            //置灰
            for (int i = 0; i < imgs.Length; i++)
            {
                imgs[i].material = null;
            }
        }
    }

    /// <summary>
    /// 置灰,img:需要置灰的图片
    /// </summary>
    /// <param name="img"></param>
    /// <param name="isTrue"></param>
    public static void SetGray(Image img, bool isTrue)
    {     
        if (isTrue)
        {
            img.material = UIEffectData.Instance.GrayMat;            
        }
        else
        {
            img.material = null;
        }
    }


    /// <summary>
    /// 释放图片资源
    /// </summary>
    /// <param name="img"></param>
    public static void UnloadTexture(Image img)
    {
        //用基础图片代替
        img.sprite = UITools.LoadSprite(UI_SPRITE);        
    }
    /// <summary>
    /// 释放图片资源
    /// </summary>
    /// <param name="img"></param>
    public static void UnloadTexture(RawImage img)
    {
        //用基础图片代替
        img.texture = LoadTexture2D(UI_TEXTURE);        
    }


}

