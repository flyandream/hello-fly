using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 意外中断类型
/// </summary>
public enum PointerInterruptType
{
    InterruptDown,//按下中被打断
    InterruptDrag,//拖拽中被打断    
}
public class PointerHandleEvent : MonoBehaviour,IPointerDownHandler, IPointerUpHandler,IPointerEnterHandler,IPointerExitHandler,IBeginDragHandler,IEndDragHandler,IDragHandler
{
   
    //中断事件
    public delegate void OnInterruptHandle(PointerInterruptType interruptType);
    public delegate void OnPointerDownHandle(PointerEventData eventData);//按下
    public delegate void OnPointerUpHandle(PointerEventData eventData);//抬起
    public delegate void OnPointerEnterHandle(PointerEventData eventData);//进入代理
    public delegate void OnPointerExitHandle(PointerEventData eventData);//离开代理
    public delegate void OnBeginDragHandle(PointerEventData eventData);//开始拖拽代理
    public delegate void OnEndDragHandle(PointerEventData eventData);//结束拖拽代理
    public delegate void OnDragHandle(PointerEventData eventData);//拖拽中代理

    public event OnPointerDownHandle OnDown;//按下事件
    public event OnPointerUpHandle OnUp;//按钮抬起事件
    public event OnPointerEnterHandle OnEnter;//进入事件
    public event OnPointerExitHandle OnExit;//离开事件
    public event OnBeginDragHandle OnBeginDragEvent;//开始拖拽
    public event OnEndDragHandle OnEndDragEvent;//结束拖拽事件
    public event OnDragHandle OnDragEvent;//拖拽事件
    public event OnInterruptHandle OnInterrupt;//意外中断事件
    public bool TriggerDown;//触发器，按下 
    public bool TriggerDrag;//触发器，拖拽中    


    #region 处理事件
    public void OnPointerDown(PointerEventData eventData)
    {
        //设置触发器-按下中
        SetTrigger(ref TriggerDown);
        if (OnDown != null)
        {
            OnDown(eventData);//执行回调
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //消耗触发器
        ResetTrigger(ref TriggerDown);
        if (OnUp != null)
        {
            OnUp(eventData);//执行抬起回调
        }
    }

    //进入
    public void OnPointerEnter(PointerEventData eventData)
    {       

        if (OnEnter!=null)
        {
            OnEnter(eventData);
        }
    }
    //离开
    public void OnPointerExit(PointerEventData eventData)
    {      
        if (OnExit!=null)
        {
            OnExit(eventData);
        }
    }

    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        //设置触发器
        SetTrigger(ref TriggerDrag);
        if (OnBeginDragEvent!=null)
        {
            OnBeginDragEvent(eventData);
        }
    }
    //结束拖拽
    public void OnEndDrag(PointerEventData eventData)
    {
        //复位触发器
        ResetTrigger(ref TriggerDrag);
        if (OnEndDragEvent != null)
        {
            OnEndDragEvent(eventData);
        }
    }
    //拖拽中
    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragEvent != null)
        {
            OnDragEvent(eventData);
        }
    }

    

    #endregion


    /// <summary>
    /// 移除所有按下的监听
    /// </summary>
    public void RemoveAllOnDownEvent()
    {       
        OnDown = null;
    }

    /// <summary>
    /// 移除所有抬起的监听
    /// </summary>
    public void RemoveAllOnUpEvent()
    {
        OnUp = null;
    }

    /// <summary>
    /// 移除所有进入监听
    /// </summary>
    public void RemoveAllOnEnterEvent()
    {
        OnEnter = null;
    }

    /// <summary>
    /// 移除所有离开监听
    /// </summary>
    public void RemoveAllOnExitEvent()
    {
        OnExit = null;
    }

    /// <summary>
    /// 移除所有开始拖拽事件
    /// </summary>
    public void RemoveAllOnBeginDragEvent()
    {
        OnBeginDragEvent = null;
    }
    /// <summary>
    /// 移除所有结束拖拽的事件
    /// </summary>
    public void RemoveAllOnEndDragEvent()
    {
        OnEndDragEvent = null;
    }

    /// <summary>
    ///  移除所有拖拽中事件
    /// </summary>
    public void RemoveAllOnDragEvent()
    {
        OnDragEvent = null;
    }
    /// <summary>
    /// 移除意外中断事件
    /// </summary>
    public void RemoveAllOnInterruptEvent()
    {
        OnDragEvent = null;
    }

    /// <summary>
    /// 移除所有订阅的事件
    /// </summary>
    public void RemoveAllEvent()
    {
        OnDown = null;
        OnUp = null;
        OnEnter = null;
        OnExit = null;
        OnBeginDragEvent = null;
        OnEndDragEvent = null;
        OnDragEvent = null;
        OnInterrupt = null;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if(!hasFocus)
        {
            //失去焦点,意外中断
            Interrupt();
        }
    }

    private void OnDisable()
    {
        //意外中断，处理事件
        Interrupt();
    }


    private void Interrupt()
    {      
        if (TriggerDown)
        {
            //按下中被意外中断                       
            if (OnInterrupt != null)
            {
                OnInterrupt(PointerInterruptType.InterruptDown);
                //复位触发器
                ResetTrigger(ref TriggerDown);
            }
        }
        if (TriggerDrag)
        {
            //拖拽中被意外中断              
            if (OnInterrupt != null)
            {
                OnInterrupt(PointerInterruptType.InterruptDrag);
                //复位触发器
                ResetTrigger(ref TriggerDrag);
            }
        }

    }

    //激活触发器
    private void SetTrigger(ref bool trigger)
    {
        trigger = true;
    }
    //复位触发器
    private void ResetTrigger(ref bool trigger)
    {
        trigger = false;
    }



}
