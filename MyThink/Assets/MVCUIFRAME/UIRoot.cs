using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 缓存对象
/// </summary>
public class UIRoot 
{
    public static Transform transform;
    public static Transform recyclePool;//回收的窗体，隐藏
    public static Transform workstation;//前台显示，工作的窗体
    public static Transform noticestation;//提示类型的窗体
   static  bool isInit = false;
    public static void Init()
    {
        if (transform==null)
        {
            var obj = Resources.Load<GameObject>("UI/UIRoot");
            transform = GameObject.Instantiate(obj).transform;
        }

        if (recyclePool == null)
        {
            recyclePool = transform.Find("recyclepool");
        }

        if (workstation == null)
        {
            workstation = transform.Find("workstation");
        }

        if (noticestation == null)
        {
            noticestation = transform.Find("noticestation");
        }
        isInit = true;
    }

    public static void SetParent(Transform window, bool isOpen,bool isTipsWindow=false)
    {
        if (isInit == false)
        {
            Init();
        }
        if (isOpen==true)
        {
            if (isTipsWindow)
            {
                window.SetParent(noticestation, false);
            }
            else
            {
                window.SetParent(workstation, false);
            }

        }
        else
        {
            window.SetParent(recyclePool,false);
        }
    }

}
