using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// UI空检测
/// </summary>
public class UIRayCast : Graphic
{
    public override void SetMaterialDirty()
    {
    }

    public override void SetVerticesDirty()
    {
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
    }
}
