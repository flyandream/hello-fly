using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// RectTransform设置
/// </summary>
public class RectTransformSetting : MonoBehaviour
{
    public List<RectTransformData>RectTransformDatas=new List<RectTransformData>();    
    //  对本组件右键设置功能按钮
    [ContextMenu("记录当前位置添加到数据中")]
    private void RecordRectTransformData()
    {
        if (transform.GetComponent<RectTransform>() != null)
        {
            RectTransformData data = new RectTransformData(gameObject.GetComponent<RectTransform>(),"Setting"+ RectTransformDatas.Count);
            RectTransformDatas.Add(data);
        }
        else
        {
            Debug.LogError("不存在RectTransform组件！");
        }
    }
    //  对本组件右键设置功能按钮
    [ContextMenu("清空所有位置信息")]
    private void ClearAllRectTransformData()
    {
        RectTransformDatas.Clear();
    }

    //设置 坐标组件对应的参数
    public void SetRectTransformProperty(RectTransformData data)
    {
        RectTransform rectTransform = (RectTransform)transform;
        UITools.SetAnchor(rectTransform, data.AnchorPresets, (int)data.AnchoredPosition.x, (int)data.AnchoredPosition.y);
        UITools.SetPivot(rectTransform, data.PivotPresets);       
        rectTransform.localPosition = data.Pos;
        rectTransform.sizeDelta =new Vector2(data.Width,data.Height);    
        rectTransform.localEulerAngles = data.Rotation;
        rectTransform.localScale = data.Scale;
    }

    /// <summary>
    /// 加载设置数据,设置当前的坐标
    /// </summary>
    /// <param name="index"></param>
    public void LoadtSettingData(int index)
    {
        if(RectTransformDatas.Count>index)
        {
            SetRectTransformProperty(RectTransformDatas[index]);
        }
    }


}

/// <summary>
/// RectTransform组件数据
/// </summary>
[Serializable]
public class RectTransformData
{    
    public string DataDes;//位置说明
    public Vector3 Pos;//坐标
    public float Width;//宽度
    public float Height;//高度        
    public AnchorPresets AnchorPresets;//anchor类型
    public Vector3 AnchoredPosition;//Anchor位置
    public PivotPresets PivotPresets;//锚点类型
    public Vector3 Rotation;//旋转轴
    public Vector3 Scale;//缩放比

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="rectTransform"></param>
    /// <param name="dataDes"></param>
    public RectTransformData(RectTransform rectTransform,string dataDes= "Default Setting")
    {
        DataDes = dataDes;
        Pos = rectTransform.localPosition;
        Width = rectTransform.sizeDelta.x;
        Height = rectTransform.sizeDelta.y;
        AnchorPresets = UITools.GetAnchorPresets(rectTransform);
        AnchoredPosition = rectTransform.anchoredPosition;
        PivotPresets = UITools.GetPivotPresets(rectTransform);
        Rotation = rectTransform.localEulerAngles;
        Scale = rectTransform.localScale;
    }

}

