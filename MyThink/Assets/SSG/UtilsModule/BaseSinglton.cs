using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 单例基类
/// </summary>
public   class BaseSinglton<T> : MonoBehaviour, IBaseSinglton where T : BaseSinglton<T>
{
    private static T _instance = null;
    private static readonly object _lock = new object();
    public static T Instance 
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        GameObject clone = new GameObject("(Singlton)" + typeof (T)); //创建单例的物体
                        _instance = clone.AddComponent<T>();
                        DontDestroyOnLoad(clone);
                        _instance.Init();
                    }
                }
            }
            return _instance;
        }

    }
     //初始化
    public virtual void Init()
    {

    }

    public virtual void Build()
    {

    }

}
