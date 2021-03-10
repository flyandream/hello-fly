using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveryDayNeedRefrashTime : BaseSinglton<EveryDayNeedRefrashTime>
{

    public override void Init()
    {
        base.Init();
      
    }
    public override void Build()
    {
        base.Build();
    }

    #region 重置运营参数
    public void ResetOperationParam()
    {
        ResetDoubleRewardCount();
        ResetFreeCoinCount();
        ResetTrialVechicleCount();
        ResetCilckStartCount();

    }
    #endregion
    /// <summary>
    /// 重置双倍奖励次数
    /// </summary>
    private void ResetDoubleRewardCount()
    {
        PlayerPrefs.SetInt(PlayerPrefsName.DoubleRewardCount, 5);
        PlayerPrefs.Save();
    }
    /// <summary>
    /// 当前双倍奖励次数
    /// </summary>
    /// <returns></returns>
    public  int NowDoubleRewardCount()
    {
        return PlayerPrefs.GetInt(PlayerPrefsName.DoubleRewardCount, 0);
    }

    /// <summary>
    /// 重置免费金币次数
    /// </summary>
    private void ResetFreeCoinCount()
    {
        PlayerPrefs.SetInt(PlayerPrefsName.FreeCoinTime, 10);
        PlayerPrefs.Save();
    }
    /// <summary>
    /// 当前免费金币次数
    /// </summary>
    /// <returns></returns>
    public int NowFreeCoinCount
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsName.FreeCoinTime, 0);
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsName.FreeCoinTime, value);
            PlayerPrefs.Save();
        }
    }


    //每天看视频试用次数
    private void ResetTrialVechicleCount()
    {
        PlayerPrefs.SetInt(PlayerPrefsName.TrialCarVideoTime, 1);
        PlayerPrefs.Save();
    }

    public int NowTrialVechicleCount
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsName.TrialCarVideoTime, 0);
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsName.TrialCarVideoTime, value);
            PlayerPrefs.Save();
        }
    }

    //每天点击触发次数
    private void ResetCilckStartCount()
    {
        PlayerPrefs.SetInt(PlayerPrefsName.CilckStartCount, 0);
        PlayerPrefs.Save();
    }

    public int NowCilckStartCount
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsName.CilckStartCount, 0);
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsName.CilckStartCount, value);
            PlayerPrefs.Save();
        }
    }
}
