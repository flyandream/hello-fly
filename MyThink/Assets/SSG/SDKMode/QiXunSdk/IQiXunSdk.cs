using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 奇迅SDK接口
/// </summary>
public interface IQiXunSdk
{
    void Init();
    void ShowInterstitial();
    void ShowInterstitial(String pageType, int Type);
    void ShowInterstitial(String pageType, int adType, int customVal);
    void SetShowHomeInterstitial(bool isNoad);
    void ShowSpecialInterstitial(bool isShowBefore, String pageType, int adType, int customVal);
    void ShowBanner(int anchor,string pageType);
    void HideBanner();
    bool HasBanner();
    void ShowVideo( int customVal,string pageType );
    bool HasVideo( string pageType );
    void ShowMore( string pageType );
    bool HasMore( string pageType );
    void ShowNative( float width, float height, float x, float y, string pageType );
    bool HasNative( string pageType );
    void HideNative();
    bool HasInterstitialGift( String pageType );

    bool HasInterstitial( String pageType );
    void ShowGift( String pageType, int customVal );

    bool CheckCtrl(String key);

    void TrackEvent(String key);

    void ShowExit();

    void SetNotificationText(String title, String longTimePass, String oneDayPass);

    void SetLevel(int level);

    void StartLevel(string level);

    void FinishLevel(string level);
    //友盟
    void Event(string eventType);

    void Event(string eventTypr,string label);

    void Event(string eventType, Dictionary<string, string> attributes);
    void FailLevel(string level);
    void ShowGoogleEvaluatePage();

    void VibrateOneShot(long time,int amplitude);

    void VibrateMultiply(long[] times,int repeat );

    void CloseVibrate();

    void VerifyPurchase(String orderId, String itemSku, String itemToken, String developerPayload, double price, String priceCurrencyCode);

    void LaunchPurchaseFlow(string sku);

    void OpenEmail(string receiver,string subject,string text );

}

