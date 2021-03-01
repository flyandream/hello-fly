using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.View;

//视图管理类
public class WindowManager : BaseSinglton<WindowManager>
{
    Dictionary<WindowType, BaseWindow> windowDic = new Dictionary<WindowType, BaseWindow>();


   //构造函数初始化
   public WindowManager()
    {
        windowDic.Add(WindowType.StoreWindow, new StoreWindow());
    }

    public void Update()
    {
        foreach (var window in windowDic.Values)
        {
            if (window.IsVisible())
            {
                window.Update(Time.deltaTime); 
            }
        }
    }

    //打开窗口
    public BaseWindow OpenWindow(WindowType type)
    {
        BaseWindow window;
        if (windowDic.TryGetValue(type,out window))
        {
            window.Open();
            return window;
        }
        else
        {
            Debug.LogError($"Open Error:{type}");
            return null;
        }
    }


    //关闭窗口

    public void CloseWindow(WindowType type)
    {
        BaseWindow window;
        if (windowDic.TryGetValue(type, out window))
        {
            window.Close();
           
        }
        else
        {
            Debug.LogError($"Open Error:{type}");
           
        }
    }

    //预加载

    public void  PreLoadWindow(ScenesType type)
    {
        foreach (var item in windowDic.Values)
        {
            if (item.GetScencesType()==type)
            {
                item.PreLoad();
            }
        }
    }

    //获取某个类型的所有窗口

    //隐藏掉某个类型的所有窗口

    public void HideAllWindow(ScenesType type,bool isDestroy=false)
    {
        foreach (var item in windowDic.Values)
        {
            if (item.GetScencesType()==type)
            {
                item.Close(isDestroy);
            }
        }
    }

   
}
