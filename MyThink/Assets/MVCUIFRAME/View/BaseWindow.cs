using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View
{

    //抽象类不能实现方法体
    public class BaseWindow
    {
        protected Transform transform;
        protected string resName;//资源名称
        protected bool resident;//是否常驻
        protected bool visible;//是否可见
        protected WindowType windowType; //窗体类型
        protected ScenesType scenesType; //场景类型

        //UI 控件 按钮
        protected Button[] buttonList;//按钮列表

        //需要给子类提供的接口

        //初始化
        protected virtual void Awake()
        {

            buttonList = transform.GetComponentsInChildren<Button>(true);
            RegisterUIEvent();
        }


        //UI事件的注册
        protected virtual void RegisterUIEvent()
        {

        }


        //添加监听游戏事件
        protected virtual void OnAddListener()
        {

        }

        //移除游戏事件
        protected virtual void OnRemoveListener()
        {

        }


        //每次打开
        protected virtual void OnEnable()
        {

        }


        //每次关闭
        protected virtual void OnDisable()
        {

        }

        //每祯更新
        public virtual void Update(float dateTime)
        {

        }

        public void Open()
        {
            if (transform == null)
            {
                if (Create())
                {
                    Awake();//初始化
                }

            }
            if (transform.gameObject.activeSelf == false)
            {
                UIRoot.SetParent(transform, true, windowType == WindowType.TipsWindow);
                transform.gameObject.SetActive(true);
                visible = true;
                OnEnable();//调用激活时候的事件
                OnAddListener();//添加事件

            }


        }

        public void Close(bool isDestroy = false)
        {
            if (transform.gameObject.activeSelf == true)
            {
                OnRemoveListener(); //移除游戏事件接口
                OnDisable();//隐藏时候触发的事件
                if (isDestroy == false)
                {
                    if (resident)
                    {
                        transform.gameObject.SetActive(false);
                        UIRoot.SetParent(transform, false, false);
                    }
                    else
                    {
                        GameObject.Destroy(transform.gameObject);
                        transform = null;
                    }
                }
                else
                {
                    GameObject.Destroy(transform.gameObject);
                    transform = null;
                }

            }
            //不可见状态
            visible = false;
        }
        //预加载
        public void PreLoad()
        {
            if (transform == null)
            {
                if (Create())
                {
                    Awake();
                }
            }
        }


        public ScenesType GetScencesType()
        {
            return scenesType;
        }

        public WindowType GetWindowType()
        {
            return windowType;
        }

        //获取根节点
        public Transform GetRoot()
        {
            return transform;
        }

        //是否可见
        public bool IsVisible()
        {
            return visible;
        }

        //是否常驻
        public bool IsResident()
        {
            return resident;
        }

        //内部
        private bool Create()
        {
            if (string.IsNullOrEmpty(resName))
            {
                return false;
            }
            if (transform == null)
            {
                var obj = Resources.Load<GameObject>(resName);
                if (obj == null)
                {
                    Debug.LogError("未找到ui预制物{windowType}");
                    return false;
                }
                transform = GameObject.Instantiate(obj).transform;
                transform.gameObject.SetActive(false);
                UIRoot.SetParent(transform, false, windowType == WindowType.LoginWindow);
                return true;
            }
            return true;
        }
    }
}
//窗口类型
public enum WindowType
{
    LoginWindow,
    StoreWindow,
    TipsWindow
}
/// <summary>
/// 场景类型，提供根据场景类型进行预加载
/// </summary>
public enum ScenesType
{
    None,
    Login,
    Battle
}

