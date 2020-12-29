using UnityEngine;
/// <summary>
/// 游戏配置
/// </summary>
public class GameConfig
{
    //persistentDataPath路径
    public static string PERSISTENT_PATH_DATA = Application.persistentDataPath;

    #region 测试模式
    public const bool isTestMode = false;
    #endregion

    #region 屏幕适配
    public const int DESGIN_PPI= 350;//设计PPI
    public const float DESGIN_WIDTN_HEIGHT_RATIO = 0.5625F;//设置宽长比
    #endregion

    #region 国内国外版本区分
    public enum Package
    {
        Inland,
        Overseas
    }

    public const Package PackageType = Package.Inland;
    #endregion


    public static GarageCameraState CameraState { get; set; }

    public static CarProperties CarPropertiesUp { get; set; }

    #region 能量

    public const int RECOVERY_ENERGY_TIME = 300;//恢复时间-300秒
    public const int MAX_ENERGY_COUNT = 10;//能量最大值10

    #endregion

    //解锁章节需要的星星数
    public const int LOCK_STAR = 0;
    public const int LOCK_CHAPTER_ONE_STAR = 30;
    public const int LOCK_CHAPYER_TWO_STAR = 75;

    //解锁章节的关卡
    public const int LOCK_CHAPTER_ONE_LEVEL_ID = 10;

    //汽车速度显示的转化系数
    public const float SpeedTransferCoefficient = 2.5f;


    public static bool IsRealName = false;


    public static bool IsGameInOrOut = false;


    public static int MaxSpeed = 350;
    public static int MaxNitrogen = 15;
    public static int MaxShield = 80;
    public static int AcceleraLerp = 5;
    public static int MaxAccelera = 35;

    public static float MaxChestDialogValue = 100;

    public static int VideoCoin = 3000;

    public static Color ColorWhite = Color.white;
    public static Color ColorGrey = new Color(152 / 255f, 150 / 255f, 152 / 255f, 255 / 255f);
    public static Color colorNewGrey = new Color(138 / 255f, 128 / 255f, 152 / 255f, 255 / 255f);
    public static Color colorYellow = new Color(254 / 255f, 193 / 255f, 0 / 255f, 255 / 255f);
    public static Color colorRed = new Color(254 / 255f, 31 / 255f, 0 / 255f, 255 / 255f);
    public static Color colorFlagGreen = new Color(0 / 255f, 231 / 255f, 128 / 255f, 255 / 255f);//00E780  //0.207.85.255//00CF55
    public static Color colorGreen = new Color(195 / 255f, 251 / 255f, 18 / 255f, 255 / 255f);//C3FB12  //165.254.0.255// A5FE00


    public static string CarConfigSumPath = "Assets/01.Config/Data/CarConfigSum.asset";
    public static string LevelDataConfigPath = "Assets/01.Config/Data/LevelData.asset";

}

public enum DamageSource
{
    Null,
    Normal,
    Truck,
    Ambulance,
    DrumpPanel,
    Barrier,
    Box,
}

//车库内相机状态
public enum GarageCameraState
{
    AnimationCamera,
    DragCamera,
    CarInfoCamera,
    CarSelectCamera,
    GetNewCarCamera,
    GiftCarShowCamera
}
//升级车属性状态
public enum CarProperties
{
    Acceleration,
    MaxSpeed,
    Nitrogen,
    Shield
}




