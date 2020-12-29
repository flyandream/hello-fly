using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCarDebrisContent : BaseContent
{

    public List<CarDebrisInt> carDebrisInts = new List<CarDebrisInt>();
    public int CarDebrisLevel;//碎片进度条
    public int OpenChestNum;
    public int ShowRewardId;//显示奖励id
    public int ResidueDegree;//剩余次数
    public override void FormatTxtData(List<string> rowData)
    {
        base.FormatTxtData(rowData);
        AnalysisCarDebrisInt(rowData[1]);
        CarDebrisLevel = int.Parse(rowData[2]);
        OpenChestNum = int.Parse(rowData[3]);
        ShowRewardId = int.Parse(rowData[4]);
        ResidueDegree = int.Parse(rowData[5]);
    }

    private void AnalysisCarDebrisInt(string rowData)
    {
        carDebrisInts = ContentTools.FormatToCarDebrisIntList(rowData, "LevelCarDebris", Id);
    }
}
