using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IStat
{
    void Init();
    /// <summary>
    /// 开始关卡，关卡ID需要在对应的统计后台添加
    /// </summary>
    /// <param name="level"></param>
    void StartLevel(string level);

    /// <summary>
    /// 关卡失败，关卡ID需要在对应的统计后台添加
    /// </summary>
    /// <param name="level"></param>
    void FailLevel(string level);

    /// <summary>
    /// 关卡完成，关卡ID需要在对应的统计后台添加
    /// </summary>
    /// <param name="level"></param>
    void FinishLevel(string level);

    /// <summary>
    /// 玩家等级或关卡统计
    /// </summary>
    /// <param name="level"></param>
    void SetUserLevel(int level);

    /// <summary>
    /// 事件统计 事件ID需要在对应的统计后台添加
    /// </summary>
    /// <param name="eventType"></param>
    void Event(StatEvent eventType);

    //分类标签
    void Event(StatEvent eventType,string label);
    /// <summary>
    /// 计数事件 如果是UMENG 事件ID需要在对应的统计后台添加
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="attributes"></param>
    void Event(StatEvent eventType, Dictionary<string, string> attributes);

    /// <summary>
    /// 计算事件 如果是UMENG 事件ID需要在对应的统计后台添加
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="attributes"></param>
    /// <param name="value"></param>
    void Event(StatEvent eventType, Dictionary<StatEventParam, string> attributes, int value);

    /// <summary>
    /// 通过自定义字符串来统计，事件类型无法在枚举中定义请使用此接口
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="attributes"></param>
    /// <param name="value"></param>
    void CustomEvent(string eventType, Dictionary<StatEventParam, string> attributes, int value);

    /// <summary>
    /// 通过商店购买道具，如果需要统计在指定关卡购买道具请使用计算事件
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    /// <param name="price"></param>
    void Buy(string item, int amount, double price);

   // /// <summary>
   // /// 奖励虚拟货币
   // /// </summary>
   // /// <param name="coin">虚拟货币数量</param>
   // /// <param name="source">奖励来源</param>
   // void Bonus(double coin, StatBonusSource source);
   //
   // /// <summary>
   // /// 奖励道具
   // /// </summary>
   // /// <param name="item"></param>
   // /// <param name="amount"></param>
   // /// <param name="price"></param>
   // /// <param name="source"></param>
   // void Bonus(string item, int amount, double price, StatBonusSource source);

    /// <summary>
    /// 使用道具
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    /// <param name="price">使用道具单价</param>
    void Use(string item, int amount, double price);

    void OnApplicationPause(bool pauseStatus);
}


