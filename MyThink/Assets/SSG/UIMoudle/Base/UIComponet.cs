using System;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// UI动态组件基类, 继承Mono
/// </summary>
public class UIComponet : IUIComponet
{
    protected GameObject _comGo;//动态组件Gameobject
    protected Transform _comTran;//动态组件Transform
    private RectTransform rectTran;//动态组件rectTransform
    protected UIComType _comType;//组件类型     

    //无限列表使用
    private int _dataIndex;//数据索引
    private bool _isValidInLoopList = false;//在循环列表中是否有效

    public bool IsVisble
    {
        get { return _comGo.activeSelf; }
    }

    public UIComType ComType { get { return _comType; } }

    public GameObject ComGo
    {
        get { return _comGo; }
    }

    public Transform ComTran
    {
        get { return _comTran; }
    }

    public RectTransform RectTran
    {
        get
        {
            if (rectTran == null)
            {
                rectTran = _comTran as RectTransform;
            }
            return rectTran;
        }

    }
    //数据索引,循环列表使用
    public int DataIndex
    {
        get
        {
            return _dataIndex;
        }
    }

    public bool IsValidInLoopList
    {
        get
        {
            return _isValidInLoopList;
        }
    }

    /// <summary>
    /// 构造函数，type为当前动态组件类型，用于查找路径，进行实例化，father为挂载的物体
    /// </summary>
    /// <param name="type"></param>
    /// <param name="father"></param>
    public UIComponet()
    {
        //_comType = type;//设置当前类型
        //_comGo = UIManager.Instance.CreateComRes(type);////向UImanager获取组件资源                     
        //_comTran = _comGo.transform;
        //Reposition(father);//从新定位            
        //Init();//进行初始化                               
    }


    public void BuildCom(UIComType type, Action action , Transform father = null)
    {
        _comType = type;//设置当前类型
                        // _comGo = UIManager.Instance.CreateComRes(type);////向UImanager获取组件资源          
        UIManager.Instance.CreateComRes(type, (go) =>
        {


            _comTran = _comGo.transform;
            Reposition(father);//从新定位            
            Init();//进行初始化        
          
            if (action!=null)
            {
                action.Invoke();
            }
        });

    }

    public virtual void Init()
    {

    }

    //重复使用时
    public virtual void ReUse()
    {

    }



    //回收组件
    public virtual void Recycle()
    {


        UIManager.Instance.RecyleCom(_comType, this);//回收组件到UImanager
                                                     //回收图片
        UnloadTexture();


    }
    //释放图片资源
    public virtual void UnloadTexture()
    {

    }

    //是否有效（激活动态组件）
    public void SetVisble(bool isVisble)
    {
        ComGo.SetActive(isVisble);
    }

    //按钮监听
    public virtual void OnBtnClick(Button btn)
    {

    }

    //重新定位
    public void Reposition(Transform father)
    {
        _comTran.SetParent(father);//设置父物体
        _comTran.localScale = Vector3.one;
        _comTran.localPosition = Vector3.zero;
    }


    #region 无限循环列表使用
    //循环列表中重新定位
    public bool SycnValidAndPositionInUILoopList(int index, UILoopList uiLoopList)
    {
        _dataIndex = index;
        if (uiLoopList.DataList == null)
        {
            //如果数据为空，隐藏组件,直接返回
            SetVisble(false);
            _isValidInLoopList = false;
            return false;
        }
        //索引无效
        if (index < 0 || index >= uiLoopList.DataList.Count)
        {
            //如果数据为空，隐藏组件,直接返回
            SetVisble(false);
            _isValidInLoopList = false;
            return false;
        }
        //如果有效,定位位置
        rectTran.localPosition = uiLoopList.GetCellPosition(_dataIndex);
        SetVisble(true);
        _isValidInLoopList = true;
        return true;
    }

    public virtual void UpdateDataItem(object data)
    {

    }

    #endregion

    #region 静态方法
    //创建组件实例
    public static T CreateInstance<T>(UIComType comType, Transform father) where T : UIComponet
    {
        T com;
        if (!UIManager.Instance.HaveCom(comType))
        {
            //如果没有池组件，通过构建代理构建
            com = UIContent.Instance.CreateComInstance(comType) as T;
        }
        else
        {
            com = UIManager.Instance.GetCom(comType) as T;
            //重复使用
            com.ReUse();
        }
        //设置位置       
        com.SetVisble(true);
        com.Reposition(father);
       
        
        return com;
    }
    #endregion





}
