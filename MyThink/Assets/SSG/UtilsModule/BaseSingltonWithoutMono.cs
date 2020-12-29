using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 单例模式，不需要继承mono
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseSingltonWithoutMono<T> :IBaseSinglton where T: BaseSingltonWithoutMono<T>,new()
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
                        _instance=new T();
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
