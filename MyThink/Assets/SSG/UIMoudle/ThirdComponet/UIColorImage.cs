using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 变色图片
/// </summary>
public class UIColorImage : Image
{
    private bool _hadCreateMat = false;

    void OnEnable()
    {
        if (Application.isPlaying)
        {
            if (_hadCreateMat == false)
            {
                material = new Material(UIEffectData.Instance.UIDefaultShader);
                _hadCreateMat = true;
            }
        }
        
    }
    //覆写对颜色的操作
    public override Color color
    {
        get
        {
            if (_hadCreateMat)
            {
                //如果有材质
                return material.GetColor("_Color");
            }
            return base.color;
        }
        set
        {
            if(_hadCreateMat)
            {
                //如果有材质
                material.SetColor("_Color",value);
            }
            else
            {
                base.color = value;
            }   
        }
    }


}
