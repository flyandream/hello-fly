using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//约束只能是class类型
public class Singleton<T> where T: new()
{
    static T instance;
    public static T Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new T();

            }
            return instance;
        }
    }

}
