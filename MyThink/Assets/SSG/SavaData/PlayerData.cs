using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
[Serializable]
public class PlayerData
{

    public int gold;
    public int lastUnlockLevel;
    public int lastUnlockChapter;
    public int carAddLife;
    public int playerExp;
    public int energy;
    public long LastRecoveryEnergyTime;
   // public DateTime LastRecoveryEnergyTime; //上次恢复能量的时间
    public List<CarBounghtItem> boughtCars = new List<CarBounghtItem>();
    public List<LevalData> levelDatas = new List<LevalData>();
    

}

[Serializable]
public class CarBounghtItem
{
    public int id;
    public int carLevel;
    public int acceleraLevel;
    public int maxSpeedLevel;
    public int nitrogenLevel;
    public int shieldlevel;
    public CarBounghtItem(int id)
    {
        this.id = id;
    }
    public CarBounghtItem(int id,int carlevel,int acceleralevel,int maxSpeedLevel,int nitrogenLevel,int shieldlevel)
    {
        this.id = id;
        this.carLevel = carlevel;
        this.acceleraLevel = acceleralevel;
        this.maxSpeedLevel = maxSpeedLevel;
        this.nitrogenLevel = nitrogenLevel;
        this.shieldlevel = shieldlevel;
    }

}

[Serializable]
public class LevalData
{
    public int chapter;
    public int id;
    public int task1;
    public int task2;
    public LevalData(int chapter,int id,int task1,int task2)
    {
        this.chapter = chapter;
        this.id = id;
        this.task1 = task1;
        this.task2 = task2;
    }

}








