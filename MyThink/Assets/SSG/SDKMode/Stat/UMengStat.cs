using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class UMengStat : IStat
{
    public UMengStat()
    {
        Init();
    }
    public void Init()
    {
      
    }

    public void StartLevel(string level)
    {
        QiXunAdManager.Instance.StartLevel(level);
    }

    public void FailLevel(string level)
    {
        QiXunAdManager.Instance.FailLevel(level);
    }

    public void FinishLevel(string level)
    {
        QiXunAdManager.Instance.FinishLevel(level);
    }

    public void SetUserLevel(int level)
    {
        QiXunAdManager.Instance.SetUserLevel(level);
    }


    public void Event(StatEvent eventType)
    {
        QiXunAdManager.Instance.Event(eventType);
    }

    public void Event(StatEvent eventType,string label)
    {
        QiXunAdManager.Instance.Event(eventType, label);
    }

    public void Event(StatEvent eventType, Dictionary<string, string> attributes)
    {
        QiXunAdManager.Instance.Event(eventType, attributes);
    }

    public void Event(StatEvent eventType, Dictionary<StatEventParam, string> attributes, int value)
    {
      //  GA.Event(GetEventName(eventType), ConvertEventParams(attributes), value);
    }

    public void Buy(string item, int amount, double price)
    {
      //  GA.Buy(item, amount, price);
    }

    string GetEventName(StatEvent eventType)
    {
        return eventType.ToString();
    }

  //
  // public void Bonus(double coin, StatBonusSource source)
  // {
  //     GA.Bonus(coin, (GA.BonusSource)((int)source));
  // }
  //
  // public void Bonus(string item, int amount, double price, StatBonusSource source)
  // {
  //     GA.Bonus(item, amount, price, (GA.BonusSource)((int)source));
  // }


    public void Use(string item, int amount, double price)
    {
       // GA.Use(item, amount, price);
    }

    Dictionary<string, string> ConvertEventParams(Dictionary<StatEventParam, string> attributes)
    {
        var stringAttributes = new Dictionary<string, string>();
        var iter = attributes.GetEnumerator();
        while (iter.MoveNext())
        {
            string key = iter.Current.Key.ToString();
            stringAttributes[key] = iter.Current.Value;
        }
        return stringAttributes;
    }
    Dictionary<string, string> ConvertEventParams(Dictionary<string, string> attributes)
    {
        var stringAttributes = new Dictionary<string, string>();
        var iter = attributes.GetEnumerator();
        while (iter.MoveNext())
        {
            string key = iter.Current.Key.ToString();
            stringAttributes[key] = iter.Current.Value;
        }
        return stringAttributes;
    }

    public void OnApplicationPause(bool pauseStatus)
    {
        //throw new NotImplementedException();
    }


    public void CustomEvent(string eventType, Dictionary<StatEventParam, string> attributes, int value)
    {
        //throw new NotImplementedException();
       // GA.Event(eventType, ConvertEventParams(attributes), value);
    }
}

