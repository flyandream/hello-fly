using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class StatManager
{
    List<IStat> _statList = new List<IStat>();

    static StatManager _instance;

    public static StatManager instance { get { return _instance ?? (_instance = new StatManager()); } }

  

    public void Add(IStat stat)
    {
        if (!_statList.Contains(stat))
        {
            _statList.Add(stat);
        }
    }

    public void StartLevel(string level)
    {
        for (int i = 0; i < _statList.Count; i++)
        {
            _statList[i].StartLevel(level);
        }
    }

    public void FailLevel(string level)
    {
        for (int i = 0; i < _statList.Count; i++)
        {
            _statList[i].FailLevel(level);
        }
    }

    public void FinishLevel(string level)
    {
        for (int i = 0; i < _statList.Count; i++)
        {
            _statList[i].FinishLevel(level);
        }
    }

    public void Event(StatEvent eventType)
    {
       // Debug.Log("stat event:" + eventType.ToString());
        for (int i = 0; i < _statList.Count; i++)
        {
            _statList[i].Event(eventType);
        }
    }

    public void Event(StatEvent eventType,string label)
    {
       // Debug.Log("stat event:" + eventType.ToString());
        for (int i = 0; i < _statList.Count; i++)
        {
            _statList[i].Event(eventType,label);
        }
    }
    /// <summary>
    /// 首次事件
    /// </summary>
    /// <param name="eventType">事件类型</param>
    public void FirstEvent(StatEvent eventType)
    {
        string eventKey = "event_" + eventType.ToString();
        if (PlayerPrefs.HasKey(eventKey))
        {
            return;
        }
        Event(eventType);
        PlayerPrefs.SetInt(eventKey, 1);
    }

    public void FirstEvent(StatEvent eventType,string label)
    {
        string eventKey = "event_" + eventType.ToString()+ label;
        if (PlayerPrefs.HasKey(eventKey))
        {
            return;
        }
        Event(eventType,label);
        PlayerPrefs.SetInt(eventKey, 1);
    }

    public void FirstEvent(StatEvent eventType, Dictionary<string, string> attributes)
    {
        string eventKey = "event_" + eventType.ToString();
        if (PlayerPrefs.HasKey(eventKey))
        {
            return;
        }
        Event(eventType, attributes);
        PlayerPrefs.SetInt(eventKey, 1);
    }


    public void Event(StatEvent eventType, Dictionary<string, string> attributes)
    {
        for (int i = 0; i < _statList.Count; i++)
        {
            _statList[i].Event(eventType, attributes);
        }
    }

    public void Event(StatEvent eventType, Dictionary<StatEventParam, string> attributes, int value)
    {
        for (int i = 0; i < _statList.Count; i++)
        {
            _statList[i].Event(eventType, attributes, value);
        }
    }

    public void CustomEvent(string eventType, Dictionary<StatEventParam, string> attributes, int value)
    {
        for (int i = 0; i < _statList.Count; i++)
        {
            _statList[i].CustomEvent(eventType, attributes, value);
        }
    }

    public void SetUserLevel(int level)
    {
        for (int i = 0; i < _statList.Count; i++)
        {
            _statList[i].SetUserLevel(level);
        }
    }

    public void Buy(string item, int amount, double price)
    {
        for (int i = 0; i < _statList.Count; i++)
        {
            _statList[i].Buy(item, amount, price);
        }
    }

   // public void Bonus(double coin, StatBonusSource source)
   // {
   //     for (int i = 0; i < _statList.Count; i++)
   //     {
   //         _statList[i].Bonus(coin, source);
   //     }
   // }
   //
   // public void Bonus(string item, int amount, double price, StatBonusSource source)
   // {
   //     for (int i = 0; i < _statList.Count; i++)
   //     {
   //         _statList[i].Bonus(item, amount, price, source);
   //     }
   // }

    public void Use(string item, int amount, double price)
    {
        for (int i = 0; i < _statList.Count; i++)
        {
            _statList[i].Use(item, amount, price);
        }
    }


    public void OnApplicationPause(bool pauseStatus)
    {
        for (int i = 0; i < _statList.Count; i++)
        {
            _statList[i].OnApplicationPause(pauseStatus);
        }
    }
}

