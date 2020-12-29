using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 层级
/// </summary>
public class LayersName
{
    //---Unity自带---
    public const string Default = "Default"; //Default
    public const string TransparentFx = "TransparentFx"; //TransparentFx
    public const string IngnoreRaycast = "IngnoreRaycast"; //IngnoreRaycast
    public const string Water = "Water"; //Water
    public const string UI = "UI"; //UI    
    //---自定义---
 

    #region Unity自带层级
    //Default
    public const int LayerDefault = 0;
    //TransparentFx       
    public const int LayerTransparentFx = 1;
    //IngnoreRaycast
    public const int LayerIgnoreRaycast = 2;
    //Water
    public const int LayerWater = 4;    
    //UI
    public const int LayerUI = 5;
    #endregion

    #region 自定义层级
  
    #endregion
}
