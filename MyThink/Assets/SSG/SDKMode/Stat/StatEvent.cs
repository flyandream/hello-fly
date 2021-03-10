
/// <summary>
/// 自定义统计事件类型 PS:添加后需要保证相应统计平台后台也添加 与Firebase预定义事件名称一致
/// </summary>
public enum StatEvent
{

    FirstOpen,
    WatchVideoOver,
    StartLoading,
    EndLoading,
    //
    BeginGuideWelcome,
    BeginGuideOvertake,
    BeginGuideTurnLeft,
    BeginGuideTurnRight,
    BeginGuideJump,
    BeginGuideSprint,
    CompleteBeginGuideHandle,
    CompleteBeginGuideLevel,
    CompleteBeginGuideLevelOnclickBtn,
    BeginGuideCompleteSynthesisVehicle,
    BeginGuideCompleteFirstStartGame,
    BeginGuideCompleteGetReward,
    BeginGuideMilitaryExercise,
    BeginGuideSynthesisNewVehicle,
    BeginGuideAgainMeetJapanTeam,
    BeginGuideOneBoss,
    BeginGuideUnlockRegion2,
    BeginGuideTwoBoss1,
    BeginGuideTwoBoss2,
    BeginGuideUnlockRegion3,
    BeginGuideThreeBoss1,
    BeginGuideThreeBoss2,
    //

    StartEndlessGameLevelOnePlayer,
    StartEndlessGameLevelTwoPlayer,
    StartEndlessGameLevelThreePlayer,
    StartEndlessGameLevelOne,
    StartEndlessGameLevelTwo,
    StartEndlessGameLevelThree,
    CompleteEndlessGameLevelOneDistance,
    CompleteEndlessGameLevelTwoDistance,
    CompleteEndlessGameLevelThreeDistance,
    CompleteEndlessGameLevelOneCoins,
    CompleteEndlessGameLevelTwoCoins,
    CompleteEndlessGameLevelThreeCoins,

    BeginGuideExplainItem_SpeedRace,
    BeginGuideExplainItem_FractionRace,
    BeginGuideCompleteVehicleGift,
    BeginGuideExplainItem_LimitedTimeRace,
    BeginGuideExplainItem_CheckRace,
    BeginGuideExplainItem_Ambulance,
    BeginGuideExplainItem_OvertakeRace,
    BeginGuideExplainItem_StageRace,
    BeginGuideEndlessMode,
    BeginGuideLevel5Obstacle,
    //
    StartGame,
    FirstPass_Level,
    EveryLevelPlayerNum,
    EveryLevelFirstPlayerNum,
    EveryLevelFirstPassPlayerNum,
    EveryLevelPassPlayerNum,
    EveryLevelFailPlayerNum,
    EveryLevelFirstFailPlayerNum,
    EveryLevelAddTimeShowNum,
    EveryLevelAddTimePlayerNum,
    EveryLevelClickWatchTimeNum,
    EveryLevelClickWatchTimePlayerNum,
    EveryLevelTimeWatchVideoNum,
    EveryLevelFirstTimeWatchVideoNum,
    EveryLevelClickTImeNoNum,
    EveryLevelFirstClickTImeNoNum,
    EveryLevelNitrogenNum,
    EveryLevelPassRemainTime,
    EveryLevelFirstNitrogenNum,
    EveryCarOwnNum,
    EveryLevelPickupCoinsNum,
    EveryLevelChestGetCoinsNum,
    EveryLevelPassByOneStarPlayers,
    EveryLevelPassByTwoStarPlayers,
    EveryLevelPassByThreeStarPlayers,
    EveryLevelFirstPassByOneStarPlayers,
    EveryLevelFirstPassByTwoStarPlayers,
    EveryLevelFirstPassByThreeStarPlayers,
    EveryLevelStuntNum,
    EveryLevelFirstEnterChestNum,
    EveryLevelFinshMainTargeCrashDeathNum,
    EveryLevelCrashNum,
    EveryLevelCrashNormalCarNum,
    EveryLevelCrashTruckNum,
    EveryLevelCrashAmbulanceNum,
    EveryLevelCrashDrumpPanelNum,
    EveryLevelClickAgainNum,
    EveryLevelClickLeaveNum,
    EveryLevelOverTakeNum,
    EveryLevelFirstClickStartNum,
    EveryLevelFirstCLickBackNum,
    EveryLevelFirstEnterAwardNum,
    EveryLevelDrumpNum,
    EveryLevelStunpLeapNum,
    EveryLevelChooseLevelNum,
    EveryLevelChooseLevelStartNum,
    EveryChestOpenPlayerNum,
    EveryLevelNoCarConsumeNum,
    EveryLevelCarConsumeOneNum,
    EveryLevelCarConsumeTwoNum,
    EveryLevelCarConsumeThreeNum,
    EveryLevelCarConsumeMoreNum,
    EveryLevelPassTimeRemainPercentOneNum,//(10%)
    EveryLevelPassTimeRemainPercentTwoNum,//(10%-%30)
    EveryLevelPassTimeRemainPercentThreeNum,//(30%-60%)
    EveryLevelPassTimeRemainPercentFourNum,//(60%以上)
    EveryLevelFirstNoCarConsumeNum,
    EveryLevelFirstCarConsumeOneNum,
    EveryLevelFirstCarConsumeTwoNum,
    EveryLevelFirstCarConsumeThreeNum,
    EveryLevelFirstCarConsumeMoreNum,
    EveryLevelFirstPassTimeRemainPercentOneNum,//(10%)
    EveryLevelFirstPassTimeRemainPercentTwoNum,//(10%-%30)
    EveryLevelFirstPassTimeRemainPercentThreeNum,//(30%-60%)
    EveryLevelFirstPassTimeRemainPercentFourNum,//(60%以上)
    ShowClickStartVideoNum,
    ClickStartVideoNum,
    FinshClickStartVideoNum,
    ShowClickStartVideoCount,
    ClickStartVideoCount,
    FinshClickStartVideoCount,
    EveryLevelFirstEnterSettltNum,
    EveryLevelFailClickContinue,
    EveryLevelFailClickUp,
    EveryLevelSuccessCarForce,
    EveryLevelFailCarForce,
    EveryLevelResidueCoinNum,
    FirstCoinNoToUpLevelNum,
    FirstPieceNoEnoughLevelNum,
    EveryCarAcceUpLevelNum,
    EveryCarMaxSpeedUpLevelNum,
    EveryCarShieldUpLevelNum,
    EveryCarNitrogenUpLevelNum,
    EveryLevelCarMaxForceNum,
    EveryCarAcceUpLevelPlayer,
    EveryCarMaxSpeedUpLevelPlayer,
    EveryCarShieldUpLevelPlayer,
    EveryCarNitrogenUpLevelPlayer,
    EveryCarStarOneNum,
    EveryCarStarTwoNum,
    EveryCarStarThreeNum,
    EveryCarStarFourNum,
    EveryCarStarFiveNum,
    ShowVideoCarPieceNum,
    ClickVideoCarPieceNum,
    FinshVideoCarPieceNum,
    ShowVideoCoinNum,
    ClickVideoCoinNum,
    FinshVideoCoinNum,
    FirstShowVideoCarPieceNum,
    FirstClickVideoCarPieceNum,
    FirstFinshVideoCarPieceNum,
    FirstShowVideoCoinNum,
    FirstClickVideoCoinNum,
    FirstFinshVideoCoinNum,
    PlayUseChargeNum,
    GetSponSorCarNum,
    EveryLevelSuccessForce,
    EveryLevelFailForce,
    EveryLevelMidWayExitPlayer,
    EveryLevelMidWayExitNum,
    ShowCutCarVideoTimeNum,
    ShowCutCarVideoTimePlayer,
    CliclCutCarVideoTimeNum,
    ClickCutCarVideoTimePlayer,
    FinishCutCarVideoTimeNum,
    FinishCutCarVideoTimePlayer,
    VehicleTrialShowNum,
    VehicleTrialShowPlayer,
    VehicleTrialClickNum,
    VehicleTrialClickPlayer,
    VehicleTrialFinishNum,
    VehicleTrialFinishPlayer,
    VehiclePushShowNum,
    VehiclePushShowPlayer,
    VehiclePushClickNum,
    VehiclePushClickPlayer,
    VehiclePushFinishNum,
    VehiclePushFinishPlayer,
    VehiclePushShowImageNum,
    VehiclePushShowImagePlayer,
    VehiclePushClickImageNum,
    VehiclePushClickImagePlayer,
    VehiclePushGarageShowNum,
    VehiclePushGarageShowPlayer,
    VehiclePushGarageClickNum,
    VehiclePushGarageClickPlayer,
    VehiclePushGarageFinishNum,
    VehiclePushGarageFinishPlayer,
    ShopShowNum,
    ShopShowPlayer,
    EveryCarDerClickNum,
    EveryCarDerClickPlayer,
    EveryCarDerFinishNum,
    EveryCarDerFinishPlayer,

}

/// <summary>
/// 自定义统计事件参数 PS:添加后需要保证相应统计平台后台也添加
/// </summary>
public enum StatEventParam
{
    /// <summary>
    /// 默认占位参数
    /// </summary>
    Default,
    /// <summary>
    /// 关卡参数
    /// </summary>
    Level,
    

    Guide_ID,
   
    //点击次数
    Click_Count,
    Coins
}


public enum StatBonusSource
{
    /// <summary>
    /// 系统奖励
    /// </summary>
    System = 1,
    /// <summary>
    /// 通关奖励
    /// </summary>
    PassLevel = 2,
    /// <summary>
    /// 转盘奖励
    /// </summary>
    RotaryReward,
    /// <summary>
    /// 每日登录奖励
    /// </summary>
    DailyLogin,
    /// <summary>
    /// 活动奖励
    /// </summary>
    Activity,
}

public enum StatPaySource
{
    GooglePlay = 25,
    AppleStore = 26,
}
