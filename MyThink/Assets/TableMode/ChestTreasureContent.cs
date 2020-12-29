using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTreasureContent : BaseContent
{

    public List<ChestTreasureInt> carDebrisInts = new List<ChestTreasureInt>();
    public int CoinNum;//金币数量

    public override void FormatTxtData(List<string> rowData)
    {
        base.FormatTxtData(rowData);
        AnalysisCarDebrisInt(rowData[1]);
        CoinNum = int.Parse(rowData[2]);
    }

    private void AnalysisCarDebrisInt(string rowData)
    {
        carDebrisInts = ContentTools.FormatToChestTreasureIntList(rowData, "ChestTreasure", Id);
    }




}
