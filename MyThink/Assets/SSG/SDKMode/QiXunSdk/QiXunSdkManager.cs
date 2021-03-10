using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
///国外包：奇迅SDK管理器
/// </summary>
public class QiXunSdkManager
{
    public static bool ENABLE = true;
    public static int VEDIO_CUSTOM_REVIVE = 0;
    public static int VEDIO_CUSTOM_ADDMONEY = 1;

    public static int INTERSECT_CUSTOM_EXIT = 6;
    public static int INTERSECT_CUSTOM_FIRST = 2;
    public static int INTERSECT_CUSTOM_STARTLEVEL = 3;
    public static int INTERSECT_CUSTOM_PAUSE = 4;
    public static int INTERSECT_CUSTOM_ENDLEVEL = 5;
    public static int INTERSECT_CUSTOM_FAIL = 7;

   private static QiXunSdkManager _instace;

    private static  readonly  object _lock=new object();
    public static QiXunSdkManager Instance 
    { 
        get 
        {
            if (_instace == null)
            {
                lock (_lock)
                {
                    if (_instace==null)
                    {
                        _instace = new QiXunSdkManager();//实例化奇迅SDK管理器
                        _instace.Init();//执行初始化
                    }
                }
            }            
            return _instace;
        } 
    }
    IQiXunSdk _curQiXunSdk;
    float _startTime;


    /// <summary>
    /// 主动构建
    /// </summary>
    public void Build()
    {

    }

    private void Init()
    {
        _curQiXunSdk = new QiXunSdk();//构造奇迅SDK实例
        _startTime = Time.time;
        _curQiXunSdk.Init();
        //创建回调物体
        GameObject clone=new GameObject("Global");
        GameObject.DontDestroyOnLoad(clone);
        clone.AddComponent<QiXunSdkCallback>();//加入回调组件
    }

    #region 属性封装
    public IQiXunSdk CurQiXunSdk
    {
        get { return _curQiXunSdk; }
        set { _curQiXunSdk = value; }
    }
    #endregion

    Action<int> _onCallback;
    Action<int> _onIntersectCallback;
    public void InvokeVedioCallback( int customVal )
    {
        if( _onCallback != null )
        {
            _onCallback( customVal );
        }
    }

    public void InvokeIntersectCallback( int customVal )
    {
        if( _onIntersectCallback != null )
        {
            _onIntersectCallback( customVal );
        }
        if ( customVal == QiXunSdkManager.INTERSECT_CUSTOM_EXIT)
        {
            Application.Quit();
        }
    }

    public void PlayVideo( int customVal, string pagetype, Action<int> onFinish )
    {
        _onCallback = onFinish;
        _curQiXunSdk.ShowVideo(customVal, pagetype);
    }

    Dictionary<int, float> _mapIntesectTime = new Dictionary<int, float>();

    public void ShowInterstitial( string pageType, int customVal, Action<int> onClosed )
    {
            if (_mapIntesectTime.ContainsKey(customVal))
            {
                if (Time.time - _startTime > 4 * 60) //游戏时间大于4分钟
                {
                    if (Time.time - _mapIntesectTime[customVal] > 120) //间隔大于2分钟
                    {
                        _onIntersectCallback = onClosed;
                        _mapIntesectTime[customVal] = Time.time;
                        _curQiXunSdk.ShowInterstitial(pageType, 0, customVal);

                    }
                    else
                    {
                        if (onClosed != null)
                        {
                            onClosed(customVal);
                        }
                    }
                }
                else
                {
                    if (Time.time - _mapIntesectTime[customVal] > 90) //间隔大于90秒
                    {
                        _onIntersectCallback = onClosed;
                        _mapIntesectTime[customVal] = Time.time;
                        _curQiXunSdk.ShowInterstitial(pageType, 0, customVal);

                    }
                    else
                    {
                        if (onClosed != null)
                        {
                            onClosed(customVal);
                        }
                    }
                }

            }
            else
            {
                _onIntersectCallback = onClosed;
                _curQiXunSdk.ShowInterstitial(pageType, 0, customVal);
            }
        
    }
}

