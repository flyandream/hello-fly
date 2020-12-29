using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LerpGridLayoutGroup : GridLayoutGroup
{

    private const float LERP_SPEED = 2;//插值速度    
    Dictionary<Transform,Vector3> _dicV3Old=new Dictionary<Transform, Vector3>();//  原始位置    
    Dictionary<Transform, Vector3> _dicV3New = new Dictionary<Transform, Vector3>();// 新位置    
    private bool _haveChangePos=false;
    private float LerpTime;

    public override void CalculateLayoutInputHorizontal()
    {      
        base.CalculateLayoutInputHorizontal();        
        
    }

    public override void CalculateLayoutInputVertical()
    {
        base.CalculateLayoutInputVertical();        
    }

    public override void SetLayoutHorizontal()
    {
        //记录原始位置
        RecordOldPos();
        //获取子组件的原始水平位置
        base.SetLayoutHorizontal();
        ////记录新的目标的位置        
        RecordNewPos();
        ////设置为旧的位置
        AsycToOld();
        ////发生变化
        _haveChangePos = true;
        //设置平滑时间
        LerpTime = 0;
    }

    public override void SetLayoutVertical()
    {
        //记录原始位置
        RecordOldPos();
        base.SetLayoutVertical();
        ////记录新的目标的位置        
        RecordNewPos();
        ////设置为旧的位置
        AsycToOld();
        ////发生变化
        _haveChangePos= true;
        LerpTime = 0;
    }

    //记录原始坐标
    private void RecordOldPos()
    {
        _dicV3Old.Clear();       
        foreach (Transform child in transform)
        {
            _dicV3Old.Add(child, child.position);
        }     
    }

    //记录新X坐标
    private void RecordNewPos()
    {
        _dicV3New.Clear();
        foreach (Transform child in transform)
        {
            _dicV3New.Add(child, child.position);
        }
    }

    //同步到旧的X位置
    private void AsycToOld()
    {
        foreach (Transform child in transform)
        {
            if (_dicV3Old.ContainsKey(child))
            {
                child.position=new Vector3(_dicV3Old[child].x, _dicV3Old[child].y, _dicV3Old[child].z);
            }
        }
    }

    private void Update()
    {
        if (_haveChangePos)
        {
            LerpTime += Time.deltaTime*LERP_SPEED;
            foreach (Transform child in transform)
            {
                if (_dicV3New.ContainsKey(child))
                {
                  //如果目标坐标存在此对象
                    Vector3 tempV3= Vector3.Lerp(child.position, _dicV3New[child], LerpTime);
                    child.position = tempV3;                                        
                }
            }
            if (LerpTime>=1)
            {
                _haveChangePos = false;
            }

        }    
    }


}
