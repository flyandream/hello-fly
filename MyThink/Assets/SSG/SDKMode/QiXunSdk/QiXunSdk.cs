using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class QiXunSdk : IQiXunSdk
{
    public static int TOP = 48;
    public static int BOTTOM = 80;
    public static string PAGE_GIFT = "gift";//礼物盒
    public static  string PAGE_HOME = "home";//首页
    public static  string PAGE_MAIN = "main";//内容页面（游戏：过关等）
    public static  string PAGE_PAUSE = "pause";//暂停
    public static  string PAGE_EXIT = "exit";//退出
    public static  string PAGE_LOADING = "loading";//加载
    public static  string PAGE_SWITCHIN = "switchin";//从桌面切入到应用内
    public static  string PAGE_APPOUT = "appout";//应用外部
    public static  string PAGE_SUCCESS = "success";//成功
    public static string PAGE_FAIL = "failed";//失败
    public static string PAGE_DOUBLE = "double";//双倍奖励
    //public static string PAGE_Resurgence = "resurgence";//复活
    public static string PAGE_EVALUATION = "evaluate";//评论
    public static string PAGE_CLUBINFO = "clubinfo";//评论
    public const string EMIAL_URL = "huntingfever2019@outlook.com";//邮箱地址
    public static string PAGE_TRY = "try";//试用
    public static string PAGE_STARTGAME = "StarGame";

    //猎人插屏专用pageType
    public const string INTERSTITIAL_PAGE_LV_GROUP = "lvset";//关卡组
    public const string INTERSTITIAL_PAGE_PAUSE= "pause";//暂停
    public const string INTERSTITIAL_PAGE_SHOP = "shop";//商城
    public const string INTERSTITIAL_PAGE_SUCCESS= "success";//关卡胜利
    public const string INTERSTITIAL_PAGE_FAIL= "fail";//关卡失败
    public const string INTERSTITIAL_PAGE_HOME_PAGE = "homepage";//主菜单
    public const string INTERSTITIAL_PAGE_NEW_GUN= "newgun";//新手武器补给

    public bool enable { get; set; }

    protected AndroidJavaObject _aJavaObject;

    public void CallMethod(string name, params object[] agrus)
    {
        if( !QiXunSdkManager.ENABLE )
        {
            return;
        }
#if !UNITY_EDITOR
        if ( Application.platform == RuntimePlatform.Android )
        {
            if (_aJavaObject == null)
            {
                Debug.LogError("android java object is null, current platform : " + Application.platform);
                return;
            }
            if (agrus.Length != 0)
                _aJavaObject.Call(name, agrus);
            else
                _aJavaObject.Call(name);
            Debug.Log("Call java method : " + name + " , agrus : " + agrus);
        }
#endif
    }

    public T CallMethod<T>(string name, params object[] agrus)
    {
        if (!QiXunSdkManager.ENABLE)
        {
            return default(T);
        }
#if !UNITY_EDITOR
        if( Application.platform == RuntimePlatform.Android )
        {
            if (_aJavaObject == null)
            {
                Debug.LogError("android java object is null, current platform : " + Application.platform);
                return default(T);
            }
            Debug.Log("Call java method : " + name + " , agrus : " + agrus);
            return _aJavaObject.Call<T>(name, agrus);
        }
        else
        {
            return default(T);
        }
#else
        return default(T);
#endif
    }

    public void Init()
    {

#if !UNITY_EDITOR
        if( Application.platform == RuntimePlatform.Android )
        {
            AndroidJavaClass cl = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
          
            _aJavaObject = cl.GetStatic<AndroidJavaObject>("currentActivity");
            if ( _aJavaObject == null )
            {
                Debug.LogError("Can not fetch the android java object");
            }
        }
#endif
    }

    private static AndroidJavaObject ToJavaHashMap(Dictionary<string, string> dic)
    {
        var hashMap = new AndroidJavaObject("java.util.HashMap");
        var putMethod = AndroidJNIHelper.GetMethodID(hashMap.GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
        var arguments = new object[2];
        foreach (var entry in dic)
        {
            using (var key = new AndroidJavaObject("java.lang.String", entry.Key))
            {
                using (var val = new AndroidJavaObject("java.lang.String", entry.Value))
                {
                    arguments[0] = key;
                    arguments[1] = val;
                    AndroidJNI.CallObjectMethod(hashMap.GetRawObject(), putMethod, AndroidJNIHelper.CreateJNIArgArray(arguments));
                }
            }
        }
        return hashMap;
    }




    public void ShowInterstitial()
    {
        CallMethod( "ShowInterstitial" );
    }

    public void ShowInterstitial(string pageType, int type)
    {
        CallMethod("ShowInterstitial", pageType, type);
    }

    public void ShowInterstitial( string pageType, int adType, int customVal )
    {
        CallMethod( "ShowInterstitial", pageType, adType, customVal );
    }

    public void ShowSpecialInterstitial( bool isShowBefore, string pageType, int adType, int customVal )
    {
        CallMethod( "ShowSpecialInterstitial", isShowBefore, pageType, adType, customVal );
    }

    public virtual void ShowBanner(int anchor,string pageType )
    {
         CallMethod( "ShowBanner", anchor, pageType );
    }

    public void HideBanner()
    {
        CallMethod( "HideBanner" );
    }

    public bool HasBanner()
    {
        return CallMethod<bool>("HasBanner");
    }

    public void ShowVideo(int customVal, string pageType )
    {
        CallMethod( "ShowVideo", customVal, pageType );
    }

    public bool HasVideo( string pageType )
    {
#if UNITY_EDITOR
     return true;
#else
     return CallMethod<bool>("HasVideo", pageType);
#endif
    }

    public void ShowMore( string pageType )
    {
        CallMethod("ShowMore", pageType );
    }

    public bool HasMore( string pageType )
    {
        return CallMethod<bool>( "HasMore", pageType );
    }

    public void ShowNative(float width, float height, float x, float y, string pageType )
    {
        CallMethod("ShowNative", width, height, x, y,pageType);
    }

    public bool HasNative(string pageType)
    {
        return CallMethod<bool>( "HasNative" );
    }

    public void HideNative()
    {
        CallMethod( "HideNative" );
    }

    public bool HasInterstitialGift( string pageType )
    {
        return CallMethod<bool>( "HasInterstitialGift", pageType );
    }

    public void ShowGift(string pageType, int customVal)
    {
        CallMethod( "ShowGift", pageType, customVal );
    }

    public bool CheckCtrl( string key )
    {
        return CallMethod<bool>( "CheckCtrl", key );
    }

    public bool HasInterstitial( String pageType )
    {
        return CallMethod<bool>("HasInterstitial", pageType);
    }

    public void TrackEvent( String key )
    {
        CallMethod("TrackAdjustEvent", key );
    }

    public void Event(string eventType)
    {
        CallMethod("Event", eventType);
    }

    public void Event(string eventType, string label)
    {
        CallMethod("Event", eventType,label);
    }


    public void Event(string eventType, Dictionary<string, string> attributes)
    {
        CallMethod("Event", eventType, ToJavaHashMap(attributes));

    }

    public void StartLevel(string level)
    {
        CallMethod("StartLevel", level);
    }

    public void FinishLevel(string level)
    {
        CallMethod("FinishLevel", level);
    }

    public void FailLevel(string level)
    {
        CallMethod("FailLevel", level);
    }

    public void ShowExit()
    {
        CallMethod("ShowExit");
    }

    public void SetNotificationText(String title, String longTimePass, String oneDayPass)
    {
        CallMethod("SetNotificationText", title, longTimePass, oneDayPass );
    }

    public void SetLevel( int level )
    {
        CallMethod( "SetLevel", level );
    }

    public void ShowGoogleEvaluatePage()
    {
        CallMethod("ShowGoogleEvaluatePage");
    }

    public void VerifyPurchase(String orderId, String itemSku, String itemToken, String developerPayload, double price, String priceCurrencyCode)
    {
        CallMethod("verifyPurchase");
    }
    public void LaunchPurchaseFlow(string sku)
    {
        CallMethod("launchPurchaseFlow", sku);
    }
    public void SetShowHomeInterstitial(bool isNoad)
    {
        CallMethod("SetShowHomeInterstitial", isNoad);
    }

    public void VibrateOneShot(long time,int amplitude)
    {

        CallMethod("VibrateOneShot", time, amplitude);
    }

    public void VibrateMultiply(long[] times,int repeat)
    {
        CallMethod("VibrateMultiply", times, repeat);
    }

    public void CloseVibrate()
    {
        CallMethod("CloseVibrate");
    }

    public void OpenEmail(string receiver, string subject, string text)
    {
        CallMethod("OpenEmail", receiver, subject, text);
    }
}

