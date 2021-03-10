using UnityEngine;
using System.Collections;
/// <summary>
/// 奇迅SDK回调
/// </summary>
public class QiXunSdkCallback : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void VedioPlayedCallback(string strCustomVal)
    {
        QiXunSdkManager.Instance.InvokeVedioCallback( int.Parse( strCustomVal.Trim() ));
        //广告观看成功！
      
    }

    public void GiftPlayedCallback(string strCustomVal)
    {
    }

    public void ExitCallback( string strCustomVal )
    {
        int val = int.Parse(strCustomVal.Trim());
        if( val == 1 )
        {
            Application.Quit();
        }
        else
        {

        }
    }

    public void InterstitialPlayedCallback(string strCustomVal)
    {
        
    }

    public void ShopCallBack(string sku)
    {
        Debug.Log(sku);

       

    }

    public void InterstitialClosed( string strCustomVal )
    {
        QiXunSdkManager.Instance.InvokeIntersectCallback(int.Parse(strCustomVal.Trim()));

    }

}
