using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour {
  
    //更新回调
    public delegate  void UpdateDelegate();
    public event UpdateDelegate UpdateEvent;
    //FixedUpdate回调
    public delegate void FixedUpdateEventHandle();
    public event FixedUpdateEventHandle FixedUpdateEvent;

    public delegate void LateUpdateEventHandle();
    public event LateUpdateEventHandle LateUpdateEvent;

    private static TimerManager _instance=null;
    private  static readonly  object _lock=new object();
    private List<DelayHandleInstance>  _listDelayHandleInstances=new List<DelayHandleInstance>();//延迟执行的实例
    private  List<DelayHandleInstance> _listUnscaleDelayHandleInstacne=new List<DelayHandleInstance>();//不受TimeScale影响的延迟回调
    

    public static TimerManager Instance
    {

        get
        {
            if (_instance==null)
            {
                lock (_lock)
                {
                    if (_instance==null)
                    {
                        GameObject clone=new GameObject("(Singleton)UITimerManager");
                        DontDestroyOnLoad(clone);//不摧毁
                        _instance = clone.AddComponent<TimerManager>();

                    }
                }
            }

            return _instance;
        }
    }


    private void Init()
    {

    }

    //构建时间管理器
    public void Build()
    {

    }

    void Update () {
        
        
	    if (UpdateEvent!=null)
	    {
	        UpdateEvent();//如果有UI面板加入事件中，执行Update
	    }
        for (int i = _listDelayHandleInstances.Count-1; i>= 0; i--)
        {
            _listDelayHandleInstances[i].WaitTime -= Time.deltaTime;//时间递减
            if (_listDelayHandleInstances[i].WaitTime<=0)// 如果时间为0
            {
                _listDelayHandleInstances[i].Execute();//执行代理
                //移出延时代理实例列表
                _listDelayHandleInstances.Remove(_listDelayHandleInstances[i]);
            }        
        }

        for (int i = _listUnscaleDelayHandleInstacne.Count - 1; i >= 0; i--)
        {
            _listUnscaleDelayHandleInstacne[i].WaitTime -= Time.unscaledDeltaTime;//时间递减
            if (_listUnscaleDelayHandleInstacne[i].WaitTime <= 0)// 如果时间为0
            {
                _listUnscaleDelayHandleInstacne[i].Execute();//执行代理
                //移出延时代理实例列表
                _listUnscaleDelayHandleInstacne.Remove(_listUnscaleDelayHandleInstacne[i]);
            }
        }

    }

    private void FixedUpdate()
    {
        if (FixedUpdateEvent!=null)
        {
            FixedUpdateEvent();
        }
    }

    private void LateUpdate()
    {
        if (LateUpdateEvent != null)
        {
            LateUpdateEvent();
        }
    }

    /// <summary>
    /// 添加等待调用
    /// </summary>    
    public DelayHandleInstance AddDelayInvoke(float waitTime, DelayHandleInstance.WaitToDoHandle handle)
    {   
            DelayHandleInstance delayHandleInstance=new DelayHandleInstance(waitTime,handle);
            _listDelayHandleInstances.Add(delayHandleInstance);//加入列表
          return delayHandleInstance;
    }

    /// <summary>
    /// 添加不受TimeScale影响的等待调用
    /// </summary>    
    public DelayHandleInstance AddUnScaleDelayInvoke(float waitTime, DelayHandleInstance.WaitToDoHandle handle)
    {
        DelayHandleInstance delayHandleInstance = new DelayHandleInstance(waitTime, handle);
        _listUnscaleDelayHandleInstacne.Add(delayHandleInstance);//加入列表
        return delayHandleInstance;
    }


    //延迟执行回调实例
    public class DelayHandleInstance
    {
        public delegate void WaitToDoHandle();//等待调用代理
        public float WaitTime;
        public WaitToDoHandle Handle;
        public DelayHandleInstance(float waitTime,WaitToDoHandle handle)
        {
            WaitTime = waitTime;
            Handle = handle;
        }
        //执行
        public void Execute()
        {
            if (Handle != null)
            {
                Handle();// 执行代理
            }
        }
        //去除代理
        public void ClearHandle()
        {
            Handle = null;
        }

    }

}
