using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class CarDebrisInt
{
    public int IndexCode; //索引序号
    public int CarDebrisId;//车碎片id
    public int CarDebrisNum;//车碎片数量

    //构造函数
    public CarDebrisInt(int index, int carDebrisId,int carDebrisNum)
    {
        IndexCode = index;
        CarDebrisId = carDebrisId;
        CarDebrisNum = carDebrisNum;
    }


}
