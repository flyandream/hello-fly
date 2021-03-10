using SSG;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QiXunAdManager : BaseSinglton<QiXunAdManager> {


    public override void Init()
    {
        base.Init();
        DontDestroyOnLoad(gameObject);
   
    }


    public void Bulid()
    {

    }


    //Home键插屏
    private void OnApplicationFocus(bool focus)
    {
        bool isFocus = false;
        if (!focus)
        {
            isFocus = true;
        }  
        if (focus)
        {
            if (isFocus)
            {
               // ShowInterstitial(QiXunSdk.PAGE_SWITCHIN);
               
                isFocus = false;
            }          
        }
    }

    //弹出条幅广告方法
    public void ShowBanner(int anchor, string pageType)
    {
   
#if !UNITY_EDITOR
      
            if (QiXunSdkManager.Instance.CurQiXunSdk.HasBanner())
            {
                QiXunSdkManager.Instance.CurQiXunSdk.ShowBanner(anchor, pageType);
            }           
          
                
#endif
    }

    //弹出插屏广告方法
    public void ShowInterstitial(string PageTypes)
    {


#if !UNITY_EDITOR
      
           
       QiXunSdkManager.Instance.CurQiXunSdk.ShowInterstitial(PageTypes, 0);
            
        
#endif
    }



    //判断是否有广告
    public bool HaveVideo(string pageType)
    {

#if !UNITY_EDITOR
        return    QiXunSdkManager.Instance.CurQiXunSdk.HasVideo(pageType);
#endif
        return true;//在Unity环境下默认为 true
    }

    /// <summary>
    /// 播放云步广告，传入回调
    /// </summary>
    public void PlayVideo(int customVal, string pageType, Action<int> onFinish)
    {
#if !UNITY_EDITOR
        QiXunSdkManager.Instance.PlayVideo(customVal,pageType,onFinish);
#endif
    }
    //调用退出接口
    public void ShowExit()
    {
#if !UNITY_EDITOR
        QiXunSdkManager.Instance.CurQiXunSdk.ShowExit();
#else
      //  UtilsTools.ShowExitTipsToExiit();
#endif

    }

    /// <summary>
    /// 隐藏banner
    /// </summary>
    public void HideBanner()
    {
#if !UNITY_EDITOR
        QiXunSdkManager.Instance.CurQiXunSdk.HideBanner();
#endif
    }

    public void ShowGoogleEvaluatePage()
    {
#if !UNITY_EDITOR
        QiXunSdkManager.Instance.CurQiXunSdk.ShowGoogleEvaluatePage();
#endif
    }

    public void ShowNative(float width, float height, float x, float y, string pageType)
    {
#if !UNITY_EDITOR
        //if (SaveDataManager.Instance.IsVip || SaveDataManager.Instance.HasSuperPackageOrNoAdv)
        //{
        //    return;
        //}

        QiXunSdkManager.Instance.CurQiXunSdk.ShowNative( width,  height,  x,  y,  pageType);
#endif
    }
    public void HideNative()
    {
#if !UNITY_EDITOR
        QiXunSdkManager.Instance.CurQiXunSdk.HideNative();
#endif
    }

//    public void TrackEvent(string Event)
//    {
//#if !UNITY_EDITOR
//        QiXunSdkManager.Instance.CurQiXunSdk.TrackEvent(Event);
//#endif
//    }
    public void TrackEvent(string eventId, bool isCountDeviceNums = false)
    {


#if !UNITY_EDITOR
        if (!isCountDeviceNums)
        {
            QiXunSdkManager.Instance.CurQiXunSdk.TrackEvent(eventId);
        }
        else
        {
            if (PlayerPrefs.GetInt("EventID"+eventId, 0) == 0)
            {
                QiXunSdkManager.Instance.CurQiXunSdk.TrackEvent(eventId);
                PlayerPrefs.SetInt("EventID"+eventId, 1);
                PlayerPrefs.Save();
            }
        }
        
#endif
    }


    public void SetUserLevel(int level)
    {
        QiXunSdkManager.Instance.CurQiXunSdk.SetLevel(level);
      
    }


    public void Event(StatEvent eventType)
    {
        QiXunSdkManager.Instance.CurQiXunSdk.Event(eventType.ToString());
    }

    public void Event(StatEvent eventType, string label)
    {
        QiXunSdkManager.Instance.CurQiXunSdk.Event(eventType.ToString(),label);
    }

    public void Event(StatEvent eventType, Dictionary<string, string> attributes)
    {
        QiXunSdkManager.Instance.CurQiXunSdk.Event(eventType.ToString(), attributes);
    }

    public void StartLevel(string levelid)
    {
#if !UNITY_EDITOR
      QiXunSdkManager.Instance.CurQiXunSdk.StartLevel(levelid);
#endif

    }

    public void FinishLevel(string levelid)
    {
#if !UNITY_EDITOR
     QiXunSdkManager.Instance.CurQiXunSdk.FinishLevel(levelid);
#endif

    }

    public void FailLevel(string levelid)
    {
#if !UNITY_EDITOR
      QiXunSdkManager.Instance.CurQiXunSdk.FailLevel(levelid);
    
#endif
    }

        //复合振动
        // @param timings 震动波形，long[等待时间，震动时间，等待时间...] 依此类推
        // @param repeat 重复参数，-1为不重复，其它值为开始重复的索引
    public void VibrateMultiply(long[]times,int repeat)
    {
#if !UNITY_EDITOR
      QiXunSdkManager.Instance.CurQiXunSdk.VibrateMultiply(times, repeat);
#endif

    }

    //单次振动
    //times 震动时长
    //amplitude 震动幅度(范围1~255)

    public void VibrateOneShot(long time,int amplitude)
    {
#if !UNITY_EDITOR
      QiXunSdkManager.Instance.CurQiXunSdk.VibrateOneShot(time, amplitude);
#endif

    }

    //关闭振动
    public void CloseVibrate()
    {
#if !UNITY_EDITOR
      QiXunSdkManager.Instance.CurQiXunSdk.CloseVibrate();
#endif

    }


    public void SetShowHomeInterstitial(bool isNoad)
    {

#if !UNITY_EDITOR
       QiXunSdkManager.Instance.CurQiXunSdk.SetShowHomeInterstitial(isNoad);
#endif
    }
  //打开邮箱
    public void OpenEmail(String receiver, String subject, String text)
    {
#if !UNITY_EDITOR

        QiXunSdkManager.Instance.CurQiXunSdk.OpenEmail(receiver, subject, text);
#endif
    }



}
