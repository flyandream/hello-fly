using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// 数据处理
/// </summary>
public class GameManagerEX
{
    #region 单例模式
    private static GameManagerEX _instance = null;
    private static readonly object _lock = new object();

    public static GameManagerEX Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new GameManagerEX();
                        _instance.Init(); //执行初始化
                                          // DontDestroyOnLoad(_instance.gameObject);
                    }
                }
            }
            return _instance; //返回
        }
    }


    #endregion

    public PlayerData playerData;

    private void Init()
    {
        // Save();
        playerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(PLAY_DATA));//序列化存档数据

    }
    private void Start()
    {


        //每日事件刷新
        HandelForLogin += EveryDayNeedRefrashTime.Instance.ResetOperationParam;
        IsNewDay();


    }
    //构建
    public void Build()
    {
        //是否第一次进入游戏
        if (PlayerPrefsTools.IsFirstIn)
        {
            FirstSave();

            PlayerPrefsTools.SetIsFirstIn(false);
        }
        Start();


    }



    public static string PLAY_DATA = "PlayerData";


    //金币改变的事件
    public event Action OnChangeCoin;
    //修改金币
    public void ModifyGold(int num)
    {
        playerData.gold += num;
        Save();
        OnChangeCoin?.Invoke();
    }

    //能量改变的事件
    public event Action OnChangeEnergy;

    //修改能量
    public void ModifyEnergy(int num)
    {

        playerData.energy += num;

        OnChangeEnergy?.Invoke();
        Save();
    }

    //玩家等级
    public static string PLAYER_EXP_KEY = "PlayerExpKey";
    int _playerExp;
    public int PlayerExp
    {
        get
        {
            _playerExp = PlayerPrefs.HasKey(PLAYER_EXP_KEY) ? PlayerPrefs.GetInt(PLAYER_EXP_KEY) : 0;
            return _playerExp;
        }
        set
        {
            _playerExp = value;
            PlayerPrefs.SetInt(PLAYER_EXP_KEY, _playerExp);
        }
    }

    /// 当前使用车辆
    public static string USE_VEHICLE_KEY = "UseVehicle";

    public int CurUseVehicle
    {
        get
        {
            return PlayerPrefs.HasKey(USE_VEHICLE_KEY) ? PlayerPrefs.GetInt(USE_VEHICLE_KEY) : 1;
        }
        set
        {
            PlayerPrefs.SetInt(USE_VEHICLE_KEY, value);
        }
    }

    public static string FIRST_START_TIME_KEY = "firstStartTime";
    public DateTime FirstStartTime
    {
        get
        {
            if (PlayerPrefs.HasKey(FIRST_START_TIME_KEY))
            {
                float utc = PlayerPrefs.GetFloat(FIRST_START_TIME_KEY);
                return DateTime.FromFileTimeUtc((long)utc);
            }
            else
            {
                return DateTime.UtcNow;
            }
        }
        set
        {
            if (!PlayerPrefs.HasKey(FIRST_START_TIME_KEY))
            {
                int addHours = (UnityEngine.Random.Range(0, 4) + 2) * 12; //随机 24 36 48 60 72小时后出现礼包

                PlayerPrefs.SetFloat(FIRST_START_TIME_KEY, (float)value.ToFileTimeUtc());
            }
        }
    }


    #region 判断是否同一天
    public delegate void HandelForEvent();//事件
    public event HandelForEvent HandelForLogin;//登陆事件

    public static string LastLoginDay = "LastLoginDay";
    public static string LastLoginMonth = "LastLoginMonth";
    public static string LastLoginYear = "LastLoginYear";

    private void UpdateNowTime()
    {
        PlayerPrefs.SetInt(LastLoginDay, DateTime.Now.Day);
        PlayerPrefs.SetInt(LastLoginMonth, DateTime.Now.Month);
        PlayerPrefs.SetInt(LastLoginYear, DateTime.Now.Year);
        PlayerPrefs.Save();
    }

    public bool IsNewDay()
    {
        if (PlayerPrefs.GetInt(LastLoginDay, 0) != DateTime.Now.Day || PlayerPrefs.GetInt(LastLoginMonth, 0) != DateTime.Now.Month || PlayerPrefs.GetInt(LastLoginYear, 0) != DateTime.Now.Year)
        {
            if (HandelForLogin != null)
            {
                HandelForLogin();
            }

            UpdateNowTime();
            Debug.Log("New Day");
            return true;
        }
        return false;
    }

    #endregion

    #region 存档
    public void Save()
    {

        var json = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(PLAY_DATA, json);

    }

    public void FirstSave()
    {
        if (playerData == null)
        {
            playerData = new PlayerData();
            UnlockLevel(CurLevel);
            UnlockChapter(CurLevelChapter);

            ModifyEnergy(GameConfig.MAX_ENERGY_COUNT);
            //设置上一次获取能量的时间
            playerData.LastRecoveryEnergyTime = UtilsTools.ConvertDateTimeToTimeStamp(DateTime.Now);
            Save();
        }
        var json = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(PLAY_DATA, json);
    }
    #endregion
    public static string CUR_LEVEL_KEY = "CurLevelKey";
    int _curLevel;
    public Action<int> onChangeCurLevel;
    public int CurLevel
    {
        get
        {
            _curLevel = PlayerPrefs.HasKey(CUR_LEVEL_KEY) ? PlayerPrefs.GetInt(CUR_LEVEL_KEY) : 1;
            return _curLevel;
        }
        set
        {
            _curLevel = value;
            PlayerPrefs.SetInt(CUR_LEVEL_KEY, _curLevel);
            if (onChangeCurLevel != null)
            {
                onChangeCurLevel(_curLevel);
            }
        }
    }

    public static string CUR_LEVEL_CHAPTER = "CurLevelChapter";
    public int CurLevelChapter
    {

        get
        {
            return PlayerPrefs.HasKey(CUR_LEVEL_CHAPTER) ? PlayerPrefs.GetInt(CUR_LEVEL_CHAPTER) : 1;
        }
        set
        {
            PlayerPrefs.SetInt(CUR_LEVEL_CHAPTER, value);
        }

    }


    public int NowVideoCarGiftTime
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefsName.VehicleGiftVideoTime, 0);
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefsName.VehicleGiftVideoTime, value);
            PlayerPrefs.Save();

        }
    }


    #region 关卡存档处理

    public void UnlockLevel(int groupId)
    {

        playerData.lastUnlockLevel = groupId;


        Save();
    }

    public bool IsUnlock(int groupId)
    {

        return playerData.lastUnlockLevel <= groupId;

    }

    #endregion

    //解锁章节

    public void UnlockChapter(int chapterid)
    {
        playerData.lastUnlockChapter = chapterid;
        Save();

    }

    public bool IsUnlockChapter(int chapterid)
    {
        return playerData.lastUnlockChapter <= chapterid;
    }


    #region 解锁车辆



    //购买车
    public CarBounghtItem Buy(int carID)
    {
        if (GetBoughtCar(carID) == null)
        {

            CarBounghtItem carBounghtItem = new CarBounghtItem(carID);
            playerData.boughtCars.Add(carBounghtItem);

            UpCarPro(carID, 1, 1, 1, 1, 1);
            Save();
            return carBounghtItem;
        }
        return null;
    }

    public CarBounghtItem GetBoughtCar(int carId)
    {
        for (int i = 0; i < playerData.boughtCars.Count; i++)
        {
            if (playerData.boughtCars[i].id == carId)
            {
                return playerData.boughtCars[i];
            }
        }
        return null;
    }




    public CarBounghtItem GetCarBounghtItem(int carid)
    {
        for (int i = 0; i < playerData.boughtCars.Count; i++)
        {
            if (playerData.boughtCars[i].id == carid)
            {
                return playerData.boughtCars[i];
            }
        }
        return null;
    }

    public CarBounghtItem UpCarPro(int id, int carlevel = 0, int acceleralevel = 0, int maxSpeedLevel = 0, int nitrogenLevel = 0, int shieldlevel = 0)
    {

        var carBounghtItem = GetCarBounghtItem(id);
        carBounghtItem.carLevel = carlevel;
        carBounghtItem.acceleraLevel = acceleralevel;
        carBounghtItem.maxSpeedLevel = maxSpeedLevel;
        carBounghtItem.nitrogenLevel = nitrogenLevel;
        carBounghtItem.shieldlevel = shieldlevel;
        return carBounghtItem;


    }


    public CarBounghtItem CurVehicle
    {
        get
        {
            return GetBoughtCar(CurUseVehicle);
        }

    }
    #endregion

    //记录子事件
    public LevalData UnlockTaskTarget(int chapterid, int levelid, int taskone, int tasktwo)
    {

        var getlevalData = GetLevalData(chapterid, levelid);


        if (getlevalData == null)
        {
            LevalData levalData = new LevalData(chapterid, levelid, taskone, tasktwo);
            playerData.levelDatas.Add(levalData);
            return levalData;

        }
        else
        {
            if (getlevalData.task1 == 0)
            {
                getlevalData.task1 = taskone;
            }
            if (getlevalData.task2 == 0)
            {
                getlevalData.task2 = tasktwo;
            }

            return getlevalData;
        }

    }

    public LevalData CurlevalData
    {
        get
        {
            return GetLevalData(CurLevelChapter, CurLevel);
        }

    }


    public LevalData GetLevalData(int chapterid, int levelid)
    {
        for (int i = 0; i < playerData.levelDatas.Count; i++)
        {
            if (playerData.levelDatas[i].id == levelid && playerData.levelDatas[i].chapter == chapterid)
            {
                return playerData.levelDatas[i];
            }
        }
        return null;
    }








    /// <summary>
    /// 时间换算
    /// </summary>
    /// <returns>The time.</returns>
    /// <param name="inputTime">输入的时间单位秒</param>
    public string UpdateTime(int inputTime, bool isNeedHour = true)
    {
        string temp;
        int hour = 0, minute = 0, seconds;
        seconds = inputTime;

        if (seconds > 60)
        {
            minute = seconds / 60;
            seconds %= 60;
        }
        if (minute > 60)
        {
            hour = minute / 60;
            minute %= 60;
        }
        string hourTemp = (hour < 10) ? "0" + hour : hour.ToString();
        string minTemp = (minute < 10) ? "0" + minute : minute.ToString();
        string secTemp = (seconds < 10) ? "0" + seconds : seconds.ToString();

        if (isNeedHour)
        {
            temp = minTemp + ":" + secTemp;
        }
        else
        {
            temp = hourTemp + ":" + minTemp;
        }



        return temp;
    }

    #region 油量恢复

    /// <summary>
    /// DateTime时间格式转换为时间戳格式
    /// </summary>
    /// <param name="time"> DateTime时间格式</param>
    public int ConvertDateTimeToTimeStamp(System.DateTime time)
    {
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        return (int)(time - startTime).TotalSeconds;
    }

    /// <summary>
    /// 获取当前时间的时间戳
    /// </summary>    
    public int GetTimeNowTimeStamp()
    {
        return ConvertDateTimeToTimeStamp(DateTime.Now);
    }

    #endregion

    #region 能量恢复

    public void SetEnergy(int value)
    {
        if (value != playerData.energy)
        {
            //边界处理
            if (value > GameConfig.MAX_ENERGY_COUNT)
            {
                value = GameConfig.MAX_ENERGY_COUNT;

            }
            playerData.energy = value;
            OnChangeEnergy?.Invoke();
        }
        Save();
    }

    //检查能量  
    public void CheckEnergyRecovery()
    {
        //上一次能量恢复的时间
        DateTime lastTime = UtilsTools.TimeSpanToDateTime(playerData.LastRecoveryEnergyTime);
        //计算时间相差
        int sec = UtilsTools.CalculTimeSpanSec(lastTime, DateTime.Now);
        if (sec < 0)
        {
            sec = 0;
            playerData.LastRecoveryEnergyTime = UtilsTools.ConvertDateTimeToTimeStamp(DateTime.Now);
            Save();
            return;
        }
        int addEnergy = sec / GameConfig.RECOVERY_ENERGY_TIME;
        //计算总能量
        int totalEnergy = playerData.energy + addEnergy;
        if (totalEnergy >= GameConfig.MAX_ENERGY_COUNT)
        {
            //能量满了
            totalEnergy = Mathf.Max(playerData.energy, GameConfig.MAX_ENERGY_COUNT);
            //设置能量                        
            SetEnergy(totalEnergy);
            //记录当前时间为恢复时间
            playerData.LastRecoveryEnergyTime = UtilsTools.ConvertDateTimeToTimeStamp(DateTime.Now);
            Save();
        }
        else
        {
            //增加能量
            ModifyEnergy(addEnergy);
            //计算最后一次恢复时的时间戳
            int lastStamp = (int)UtilsTools.ConvertDateTimeToTimeStamp(lastTime) + addEnergy * GameConfig.RECOVERY_ENERGY_TIME;
            //转换时间戳到datatime
            lastTime = UtilsTools.TimeSpanToDateTime(lastStamp);
            playerData.LastRecoveryEnergyTime = lastStamp;
            //保存恢复的时间戳  
            //  DateTime saveTime = UtilsTools.TimeSpanToDateTime(playerData.LastRecoveryEnergyTime);
            //  saveTime = lastTime;
            Save();
        }
    }

    //尝试获取下一次能量恢复倒计时
    public bool TryGetNextRecoveryEnergyCutDownTime(out int cutDown)
    {
        cutDown = 0;
        if (playerData.energy >= GameConfig.MAX_ENERGY_COUNT)
        {
            //如果能量大于或者等于最大值
            return false;
        }
        //现在相差的时间
        int sec = UtilsTools.CalculTimeSpanSec(UtilsTools.TimeSpanToDateTime(playerData.LastRecoveryEnergyTime), DateTime.Now);
        if (sec < 0)
        {
            //Data.LastRecoveryEnergyTime = DateTime.Now;
            sec = 0;
        }
        cutDown = GameConfig.RECOVERY_ENERGY_TIME - sec;
        //如果小于0，限制为0
        if (cutDown < 0)
        {
            cutDown = 0;
            //检测能量
            CheckEnergyRecovery();
        }
        return true;
    }

    //当能量发生改变
    private void WhenEnergyReduce()
    {
        Debug.Log(playerData.LastRecoveryEnergyTime);
        if (playerData.energy == GameConfig.MAX_ENERGY_COUNT - 1)
        {
            //如果扣除能量后,能量刚好等于最大能量-1            
            //重新设置恢复时间
            playerData.LastRecoveryEnergyTime = UtilsTools.ConvertDateTimeToTimeStamp(DateTime.Now);
        }
    }

    #endregion

}
   


