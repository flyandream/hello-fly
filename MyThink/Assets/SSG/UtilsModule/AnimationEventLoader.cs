using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Animation动画事件装载器
/// </summary>
public class AnimationEventLoader : MonoBehaviour
{    
    public delegate void EventHandle();//无参数代理    
    private Animation _animation;    //动画组件       

    private Dictionary<int,EventHandle> _dicEventHandles=new Dictionary<int, EventHandle>();
    private int _eventId;
    //获取事件ID
    private int GetEventId
    {
        get { return _eventId++; }

    }
    //动画的事件ID列表
    private Dictionary<AnimationClip, List<int>> _dicClipEventIds = new Dictionary<AnimationClip, List<int>>();

    #region 绑定动画时间装载器
    public static AnimationEventLoader BuildBind(Animation anim,bool rebuildAnimClip=true)
    {      
        //判断是否有动画事件装载器,避免重复添加
        AnimationEventLoader loader = anim.gameObject.GetComponent<AnimationEventLoader>();
        if (loader!=null)
        {
            return loader;
        }
        //是否需要重建动画片段
        if (rebuildAnimClip)
        {
            UtilsTools.RebuildInstantiateAnimationClips(anim);
        }
        loader =anim.gameObject.AddComponent<AnimationEventLoader>();//构建AnimationEventLoader组件        
        loader.SetAnimation(anim);
        loader.Init();
        return loader;
    }
    //初始化
    private void Init()
    {
        //复位事件ID
        ResetEventId();
    }

    #endregion

    //设置动画组件
    private void SetAnimation(Animation anim)
    {
        _animation = anim;
    }


    //执行空参数
    private void DoEvent(int  eventID)
    {
        EventHandle handle;        
        if (_dicEventHandles.TryGetValue(eventID, out handle))
        {
            //执行代理
            if(handle!=null)
            {
                handle();
            }            
        }
    }

    #region 加入事件

    //加入动画事件
    public void AddEvent(AnimationClip clip, float normalizedTime, EventHandle handle)
    {
        if (clip == null)
        {
            //片段为空，直接返回
            return;
        }
        AnimationEvent animEvent = new AnimationEvent();
        //创建代理，加入字典
        EventHandle eventHandle = new EventHandle(handle);
        animEvent.functionName = "DoEvent";
        //通过hash码作为回调事件的id
        int eventId = GetEventId;
        animEvent.intParameter = eventId;
        animEvent.time = clip.length * normalizedTime;  
        //加入动画事件
        clip.AddEvent(animEvent);          
        _dicEventHandles.Add(eventId,eventHandle);
        AddToClipEventIdList(clip, eventId);
    
    }

    //加入动画事件ID列表
    private void AddToClipEventIdList(AnimationClip clip, int eventId)
    {
        List<int> list;
        if (_dicClipEventIds.TryGetValue(clip, out list))
        {
            list.Add(eventId);
        }
        else
        {
            list = new List<int>();
            list.Add(eventId);
            _dicClipEventIds.Add(clip, list);
        }
    }


    #endregion   
    //移除所有动画事件
    public void RemoveAllEvent()
    {
        foreach (AnimationClip clip in _dicClipEventIds.Keys)
        {
            if (clip != null)
            {
                clip.events = null;//清空动画事件
            }
        }
        _dicClipEventIds.Clear();
        _dicEventHandles.Clear();

        //复位事件ID
        ResetEventId();
    }

    //移除动画片段事件
    public void RemoveClipEvent(AnimationClip clip)
    {
        if (clip == null)
        {
            return;
        }
        List<int> _listEventId;
        _dicClipEventIds.TryGetValue(clip, out _listEventId);
        if (_listEventId != null)
        {
            for (int i = 0; i < _listEventId.Count; i++)
            {
                //移除代理
                _dicEventHandles.Remove(_listEventId[i]);
            }
        }
        //移除片段
        _dicClipEventIds.Remove(clip);
        //清空动画片段事件
        clip.events = null;
    }

    //复位事件id
    private void ResetEventId()
    {
        _eventId = int.MinValue;
    }


}

