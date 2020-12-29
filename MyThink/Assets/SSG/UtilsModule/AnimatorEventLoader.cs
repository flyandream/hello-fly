using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

/// <summary>
/// Animator动画事件装载器
/// </summary>
public class AnimatorEventLoader : MonoBehaviour
{
    //事件代理
    public delegate void EventHandle();               
    private  Animator _animator;//动画状态机组件 
    //动画事件字典
    private Dictionary<int,EventHandle> _dicEventHandles=new Dictionary<int, EventHandle>();
    //动画的事件ID列表
    private Dictionary<AnimationClip,List<int>> _dicClipEventIds=new Dictionary<AnimationClip, List<int>>();
    private int _eventId;
    //获取事件ID
    private int GetEventId
    {
        get { return _eventId++; } 

    }

    public static AnimatorEventLoader BuildBind(Animator animator,bool rebuildAnimClip=true)
    {
        AnimatorEventLoader result = animator.gameObject.GetComponent<AnimatorEventLoader>();
        if(result!=null)
        {
            return result;
        }
        //如果需要重建动画片段
        if (rebuildAnimClip)
        {
            UtilsTools.RebuildInstantiateAnimatorClips(animator);
        }
        result = animator.gameObject.AddComponent<AnimatorEventLoader>();
        result.SetAnimator(animator);
        result.Init();
        return result;        
    }

    private void SetAnimator(Animator animator)
    {
        _animator = animator;
    }

    //初始化
    private void Init()
    {
      ResetEventId();
    }

    //加入事件
    public void AddEvent(AnimationClip clip, float normalizedTime, EventHandle handle)
    {
        if (clip == null)
        {
            //片段为空，直接返回
            return;
        }
        //创建动画事件
        AnimationEvent animEvent = new AnimationEvent();
        EventHandle eventHandle = new EventHandle(handle);//创建代理
        animEvent.functionName = "DoEvent";
        //代理哈希码作为ID
        int eventID = GetEventId;
        animEvent.intParameter = eventID;//设置事件参数
        animEvent.time = clip.length * normalizedTime;
        //加入动画事件
        clip.AddEvent(animEvent);             
        //加入字典
        _dicEventHandles.Add(eventID,eventHandle);
        //加入动画片段事件列表
        AddToClipEventIdList(clip, eventID);
    }

    /// <summary>
    /// 处理事件，eventID：事件ID
    /// </summary>
    /// <param name="eventID"></param>
    private void DoEvent(int  eventID)
    {
      //查找字典对应的代理，执行代理
        EventHandle handle=null;
        if(_dicEventHandles.TryGetValue(eventID, out handle))
        {
            if (handle!=null)
            {
                handle();//执行回调
            }            
        }
    }

    //加入动画事件ID列表
    private void AddToClipEventIdList(AnimationClip clip,int eventId)
    {
        List<int> list;
        if (_dicClipEventIds.TryGetValue(clip, out list))
        {
            list.Add(eventId);
        }
        else
        {
            list=new List<int>();
            list.Add(eventId);
            _dicClipEventIds.Add(clip, list);
        }
    }

    //移除所有动画事件
    public void RemoveAllEvent()
    {
        foreach (AnimationClip clip in _dicClipEventIds.Keys)
        {
            if (clip!=null)
            {
                clip.events = null;//清空动画事件
            }         
        }
        _dicClipEventIds.Clear();
        _dicEventHandles.Clear();

        //ID复位
        ResetEventId();


    }
    //移除动画片段事件
    public void RemoveClipEvent(AnimationClip clip)
    {
        if (clip == null)
        {
            return;
        }
        List<int> listEventId;
        _dicClipEventIds.TryGetValue(clip, out listEventId);
        if (listEventId!=null)
        {
            for (int i = 0; i < listEventId.Count; i++)
            {
                //移除代理
                _dicEventHandles.Remove(listEventId[i]);
            }
        }
        //移除片段
        _dicClipEventIds.Remove(clip);
        //清空动画片段事件
        clip.events = null;        
    }

    //通过动画事件ID移除对应的事件
    public void RemoveEventForEventId(int eventID)
    {
        _dicEventHandles.Remove(eventID);                        
    }

    
    //复位事件id
    private void ResetEventId()
    {
        _eventId = int.MinValue;
    }

}
