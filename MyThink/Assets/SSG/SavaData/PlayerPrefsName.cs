using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 保存数据时的读取
/// </summary>
public class PlayerPrefsName
{
    public const string IsPlayLevelUnlockAnim = "IsPlayLevelUnlockAnim";//是否播放关卡解锁动画
    public const string SoundSwitch = "SoundSwitch";//音效开关
    public const string MusicSwitch = "MusicSwitch";//音乐开关
    public const string IsFirstIn = "IsFirstIn";//是否第一次进入游戏
    public const string IsCompleteGuide = "IsCompleteGuide";//是否完成引导
    public const string IsFirstTutorial = "FirstTutorial";//是否第一次引导
    public const string IsCanSynthesisVehicleTutorial = "CanSynthesisVehicleTutorial";//是否合成车辆引导
    public const string IsFirstStartGameTutorial = "FirstStartGameTutorial";//是否第一次开始游戏引导
    public const string IsFirstGetCoinsTutorial = "FirstGetCoinsTutorial";//是否第一次获得起始资金引导
    public const string IsAgainSynthesisNewVehicleTutorial = "AgainSynthesisNewVehicleTutorial";//是否再一次合成车辆引导
    public const string IsAgainMeetJapanTeamTutorial = "AgainMeetJapanTeamTutorial";//是否再一次遇到日本队引导
    public const string IsFirstNewRegionTutorial = "FirstNewRegionTutorial";//是否新赛季引导
    public const string IsBossJudgeTutorial = "BossJudgeTutorial";//是否Boss关卡分段
    public const string PrepareBossLevelTutorial = "PrepareBossLevelTutorial";//选车出战界面Boss关剧情
    public const string RecentBossLevelId = "RecentBossLevelId";//Boss关卡
    public const string RecentBossChapter = "RecentBossChapter";//Boss章节
    public const string IsFirstAmbulanceTutorial = "FirstAmbulanceTutorial";//是否救护车引导
    public const string DoubleRewardCount = "DoubleRewardCount";
    public const string OpenChestTime = "OpenChestTime";//开宝箱次数
    public const string IsFirstOpenChest = "IsFirstOpenChest";//判断是否第一次开完宝箱
    public const string ChestSumCount = "ChestSumCount";//宝箱总次数
    public const string PassStarCount = "PassStarCount";//总过关星星数
    public const string CurLevelStarCount = "CurLevelStarCount";//当前关卡星星数
    public const string CurChapterUnlock = "CurChapterUnlock";//章节解锁
    public const string ChestDialogBoxBar = "ChestDialogBoxBar";//宝箱碎片进度最大值
    public const string ChestDialogBoxBarNotUI = "ChestDialogBoxBarNotUI";//宝箱碎片进度最大值NotUI
    public const string OpenChestNum= "OpenChestNum";//开新宝箱次数
    public const string IsBackByGarage = "IsBackByGarage";
    public const string FreeCoinTime = "FreeCoinTime";//免费金币次数
    public const string IsFirstLoadingToGarage = "IsFirstLoadingToGarage";//记录打开游戏第一次进主界面
    public const string CarGiftTimeGTR = "CarGiftTimeGTR";//gtr车礼包
    public const string IsFirstUnlockEndlessMode = "IsFirstUnlockEndlessMode";//解锁无尽模式
    public const string IsVehicleGiftEnd = "IsVehicleGiftEnd";//判断汽车礼包是否发放
    public const string VehicleGiftVideoTime = "VehicleGiftVideoTime";//车礼包视频次数
    public const string IsVehicleGiftTutorial = "IsVehicleGiftTutorial";//汽车礼包引导
    public const string IsFirstShowLevelTutorial = "IsFirstShowLevelTutorial";//是否第一次在该关卡进行引导或剧情
    public const string VideoCarCutTimeNum = "VideoCarCutTimeNum";//看视频车冷却cd次数
    public const string VideoCarTime = "VideoCarTime";//看视频获得车次数
    public const string VideoCarShow = "VideoCarShow";//是否显示视频车界面
    public const string TrialCarVideoTime = "TrialCarVideoTime";//看视频试用车次数
    public const string CilckStartCount = "CilckStartCount";//点击出发次数
}

/// <summary>
///PlayerPrefs存储工具
/// </summary>
public class PlayerPrefsTools
{

    public delegate void OnChangeSoundSwitchEventHandle();//音效开关改变
    public static event OnChangeSoundSwitchEventHandle ChangeSoundSwitchEvent;//切换音效事件

    public delegate  void OnChangeMusicSwitchEventHandle();//音乐开关改变
    public static event OnChangeMusicSwitchEventHandle ChangeMusicSwitchEvent;//音乐开关改变事件


    //是否播放关卡解锁动画
    public static int IsPlayLevelUnlockAnim
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsName.IsPlayLevelUnlockAnim, -1);
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsPlayLevelUnlockAnim, value);
            PlayerPrefs.Save();
        }
    }
    //看视频车冷却cd次数
    public static int VideoCarCutTimeNum
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsName.VideoCarCutTimeNum, 0);
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsName.VideoCarCutTimeNum, value);
            PlayerPrefs.Save();
        }
    }

    //是否第一次在该关卡进行引导或剧情
    public static bool IsFirstShowLevelTutorial
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsName.IsFirstShowLevelTutorial, 1) == 1;
        }
        set
        {
            if (value)
            {
                PlayerPrefs.SetInt(PlayerPrefsName.IsFirstShowLevelTutorial, 1);
            }
            else
            {
                PlayerPrefs.SetInt(PlayerPrefsName.IsFirstShowLevelTutorial, 0);
            }
            PlayerPrefs.Save();
        }
    }


    //汽车礼包引导
    public static bool IsVehicleGiftTutorial
    {
        get
        {
            if (PlayerPrefs.GetInt(PlayerPrefsName.IsVehicleGiftTutorial, 0) == 1)
            {
                return true;
            }
            return false;
        }
    }
    //设置汽车礼包引导
    public static void SetIsVehicleGiftTutorial(bool isFirst)
    {
        if (isFirst)
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsVehicleGiftTutorial, 1);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsVehicleGiftTutorial, 0);
        }
        PlayerPrefs.Save();
    }

    //判断汽车礼包是否发放
    public static bool IsVehicleGiftEnd
    {
        get
        {
            if (PlayerPrefs.GetInt(PlayerPrefsName.IsVehicleGiftEnd, 0) == 1)
            {
                return true;
            }
            return false;
        }
    }
    //设置汽车礼包是否发放
    public static void SetIsVehicleGiftEnd(bool isFirst)
    {
        if (isFirst)
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsVehicleGiftEnd, 1);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsVehicleGiftEnd, 0);
        }
        PlayerPrefs.Save();
    }

    //判断是否解锁无尽模式
    public static bool IsFirstUnlockEndlessMode
    {
        get
        {
            if (PlayerPrefs.GetInt(PlayerPrefsName.IsFirstUnlockEndlessMode, 0) == 1)
            {
                return true;
            }
            return false;
        }
    }
    //设置是否解锁无尽模式
    public static void SetIsFirstUnlockEndlessMode(bool isFirst)
    {
        if (isFirst)
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsFirstUnlockEndlessMode, 1);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsFirstUnlockEndlessMode, 0);
        }
        PlayerPrefs.Save();
    }

    //判断打开游戏第一次进主界面
    public static bool IsFirstLoadingToGarage
    {
        get
        {
            if (PlayerPrefs.GetInt(PlayerPrefsName.IsFirstLoadingToGarage, 0) == 1)
            {
                return true;
            }
            return false;
        }
    }
    //设置打开游戏第一次进主界面
    public static void SetIsFirstLoadingToGarage(bool isFirst)
    {
        if (isFirst)
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsFirstLoadingToGarage, 1);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsFirstLoadingToGarage, 0);
        }
        PlayerPrefs.Save();
    }
    //章节是否解锁
    public static bool IsUnlockChapter
    {
        get
        {
            if (PlayerPrefs.GetInt(PlayerPrefsName.CurChapterUnlock)==1)
            {
                return true;
            }
            return false;
        }
        
    }
    //设置章节是否解锁
    public static void SetIsUnlockl(int isUnlock)
    {
      
      PlayerPrefs.SetInt(PlayerPrefsName.CurChapterUnlock, isUnlock);
 
      PlayerPrefs.Save();//保存
    }

    //车辆视频是否显示
    public static bool IsVideoCarShow
    {
        get
        {
            if (PlayerPrefs.GetInt(PlayerPrefsName.VideoCarShow) == 1)
            {
                return true;
            }
            return false;
        }

    }
    //设置
    public static void SetIsVideoCarShow(int isUnlock)
    {

        PlayerPrefs.SetInt(PlayerPrefsName.VideoCarShow, isUnlock);

        PlayerPrefs.Save();//保存
    }


    //是否第一次进入游戏
    public static bool IsFirstIn
    {
        get
        {
            if (PlayerPrefs.GetInt(PlayerPrefsName.IsFirstIn, 1) == 1)
            {
                return true;//如果时第一次进入，返回
            }
            return false;          
        }
    }
    //设置是否第一次进入
    public static void SetIsFirstIn(bool isFirstIn)
    {
        if (isFirstIn)
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsFirstIn, 1);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsFirstIn, 0);
        }
        PlayerPrefs.Save();//保存
    }
    //音效是否开启
    public static bool SoundSwitchIsOn
    {
        get
        {
            if (PlayerPrefs.GetInt(PlayerPrefsName.SoundSwitch, 1) == 1)
            {                
                return true;//如果音效开启
            }
            else
            {
                return false;//音效关闭
            }
        }
    }
    //音乐是否开启
    public static bool MusicSwitchIsOn
    {
        get
        {
            if (PlayerPrefs.GetInt(PlayerPrefsName.MusicSwitch, 1) == 1)
            {
                return true;//音效开启
            }
            else
            {
                return false;//音效关闭
            }
        }
    }

    //设置音效开关
    public static void SetSoundSwitch(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt(PlayerPrefsName.SoundSwitch, 1);          
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsName.SoundSwitch,0);
        }
        if (ChangeSoundSwitchEvent != null)
        {
            ChangeSoundSwitchEvent();//执行事件
        }
        PlayerPrefs.Save();//保存
    }
    //设置音乐开关
    public static void SetMusicSwitch(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt(PlayerPrefsName.MusicSwitch, 1);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsName.MusicSwitch,0);
        }
        if (ChangeMusicSwitchEvent != null)
        {
            ChangeMusicSwitchEvent();//执行事件
        }
        PlayerPrefs.Save();//保存
    }


    //是否完成引导
    public static bool IsCompleteGuide
    {
        get
        {
            //if (LevelPassData.Instance.curLevel == null)
            //    return true;
            if (PlayerPrefs.GetInt(PlayerPrefsName.IsCompleteGuide,0)==1)
            {
                return true;
            }
            return false;
        }
    }
    //设置是否完成引导
    public static void SetIsCompleteGuide(bool isComplete)
    {
        if (isComplete)
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsCompleteGuide, 1);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsCompleteGuide,0);
        }
        PlayerPrefs.Save();
    }
    //是否完成救护车引导
    public static bool IsFirstAmbulanceGuide
    {
        get
        {
            if (PlayerPrefs.GetInt(PlayerPrefsName.IsFirstAmbulanceTutorial, 0) == 1)
            {
                return true;
            }
            return false;
        }
    }
    //设置是否完成救护车引导
    public static void SetIsFirstAmbulanceGuide(bool isFirst)
    {
        if (isFirst)
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsFirstAmbulanceTutorial, 1);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsFirstAmbulanceTutorial, 0);
        }
        PlayerPrefs.Save();
    }
    //最近的Boss章节
    public static int RecentBossChapter
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsName.RecentBossChapter, 0);
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsName.RecentBossChapter, value);
            PlayerPrefs.Save();
        }
    }
    //最近的Boss关卡
    public static int RecentBossLevelId
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsName.RecentBossLevelId, 0);
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsName.RecentBossLevelId, value);
            PlayerPrefs.Save();
        }
    }
    //是否第一次新手引导
    public static bool IsFirstTutorial
    {
        get
        {
            if (PlayerPrefs.GetInt(PlayerPrefsName.IsFirstTutorial, 1) == 1)
            {
                return true;
            }
            return false;
        }
    }
    //设置是否第一次新手引导
    public static void SetIsFirstTutorial(bool isFirst)
    {
        if (isFirst)
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsFirstTutorial, 1);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsFirstTutorial, 0);
        }
        PlayerPrefs.Save();
    }
    //是否合成车辆引导
    public static bool IsCanSynthesisVehicleTutorial
    {
        get
        {
            if (PlayerPrefs.GetInt(PlayerPrefsName.IsCanSynthesisVehicleTutorial, 0) == 1)
            {
                return true;
            }
            return false;
        }
    }
    //设置是否can合成车辆引导
    public static void SetIsCanSynthesisVehicleTutorial(bool isCan)
    {
        if (isCan)
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsCanSynthesisVehicleTutorial, 1);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsCanSynthesisVehicleTutorial, 0);
        }
        PlayerPrefs.Save();
    }
    //是否第一次开始游戏引导
    public static bool IsFirstStartGameTutorial
    {
        get
        {
            if (PlayerPrefs.GetInt(PlayerPrefsName.IsFirstStartGameTutorial, 0) == 1)
            {
                return true;
            }
            return false;
        }
    }
    //设置是否第一次开始游戏引导
    public static void SetIsFirstStartGameTutorial(bool isFirst)
    {
        if (isFirst)
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsFirstStartGameTutorial, 1);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsFirstStartGameTutorial, 0);
        }
        PlayerPrefs.Save();
    }
    //是否第一次获得奖励引导
    public static bool IsFirstGetCoinsTutorial
    {
        get
        {
            if (PlayerPrefs.GetInt(PlayerPrefsName.IsFirstGetCoinsTutorial, 0) == 1)
            {
                return true;
            }
            return false;
        }
    }
    //设置是否第一次获得奖励引导
    public static void SetIsFirstGetCoinsTutorial(bool isFirst)
    {
        if (isFirst)
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsFirstGetCoinsTutorial, 1);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsFirstGetCoinsTutorial, 0);
        }
        PlayerPrefs.Save();
    }
    //是否又可以合成新车引导
    public static bool IsAgainSynthesisVehicleTutorial
    {
        get
        {
            if (PlayerPrefs.GetInt(PlayerPrefsName.IsAgainSynthesisNewVehicleTutorial, 0) == 1)
            {
                return true;
            }
            return false;
        }
    }
    //设置是否又可以合成新车引导
    public static void SetIsAgainSynthesisVehicleTutorial(bool isAgain)
    {
        if (isAgain)
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsAgainSynthesisNewVehicleTutorial, 1);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsAgainSynthesisNewVehicleTutorial, 0);
        }
        PlayerPrefs.Save();
    }
    //是否再遇到日本队引导
    public static bool IsAgainMeetJapanTeamTutorial
    {
        get
        {
            if (PlayerPrefs.GetInt(PlayerPrefsName.IsAgainMeetJapanTeamTutorial, 0) == 1)
            {
                return true;
            }
            return false;
        }
    }
    //设置是否再遇到日本队引导
    public static void SetIsAgainMeetJapanTeamTutorial(bool isAgain)
    {
        if (isAgain)
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsAgainMeetJapanTeamTutorial, 1);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsAgainMeetJapanTeamTutorial, 0);
        }
        PlayerPrefs.Save();
    }
    //是否新赛季引导
    public static int UnlockNewRegionTutorial
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsName.IsFirstNewRegionTutorial, -1);
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsFirstNewRegionTutorial, value);
            PlayerPrefs.Save();
        }
    }
    //通过Boss关卡
    public static int PassBossJudgeTutorial
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsName.IsBossJudgeTutorial, -1);
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsBossJudgeTutorial, value);
            PlayerPrefs.Save();
        }
    }
    //Boss关卡选车出战
    public static int PrepareBossLevelTutorial
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsName.PrepareBossLevelTutorial, -1);
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsName.PrepareBossLevelTutorial, value);
            PlayerPrefs.Save();
        }
    }
    //是否第一次开宝箱
    public static bool IsFirstOpenChest
    {
        get
        { 
            if (PlayerPrefs.GetInt(PlayerPrefsName.IsFirstOpenChest,0)==1)
            {
                return true;
            }
            return false;
        }
       
    }

    //设置是否第一次开完宝箱
    public static void SetIsCompleteFirstChest(bool isfirst)
    {
        if (isfirst)
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsFirstOpenChest, 1);

        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsName.IsFirstOpenChest, 0);
        }
        PlayerPrefs.Save();
    }

    //宝箱等级进度
    public static void AddChestLevel(int value)
    {

        PlayerPrefs.SetInt(PlayerPrefsName.ChestDialogBoxBar, GetChestLevel() + value);
    }

    public static void CutChestLevel(int value)
    {

        PlayerPrefs.SetInt(PlayerPrefsName.ChestDialogBoxBar, GetChestLevel() - value);
    }


    public static int GetChestLevel()
   {
        return PlayerPrefs.GetInt(PlayerPrefsName.ChestDialogBoxBar);

   }
    //宝箱等级进度实际非UI
    public static void AddChestLevel(int value,bool isNotUI)
    {

        PlayerPrefs.SetInt(PlayerPrefsName.ChestDialogBoxBarNotUI, GetChestLevel(true) + value);
    }
    //宝箱等级进度实际非UI
    public static void CutChestLevel(int value, bool isNotUI)
    {

        PlayerPrefs.SetInt(PlayerPrefsName.ChestDialogBoxBarNotUI, GetChestLevel(true) - value);
    }

    //宝箱等级进度实际非UI
    public static int GetChestLevel( bool isNotUI)
    {
        return PlayerPrefs.GetInt(PlayerPrefsName.ChestDialogBoxBarNotUI);

    }
    public static int NowOpenChestTime
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsName.OpenChestNum, 0);
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsName.OpenChestNum, value);
            PlayerPrefs.Save();
        }
    }


  
}
