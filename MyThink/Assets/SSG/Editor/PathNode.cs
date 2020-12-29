using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SSG
{
    /// <summary>
    /// 路径节点
    /// </summary>
    public class PathNode : ScriptableObject
    {
        public string Path
        {
            get
            {
#if UNITY_EDITOR
                string path = UnityEditor.AssetDatabase.GetAssetPath(this);
                int removeCount = ("/"+this.name + ".asset").Length;
                 path=  path.Remove(path.Length - removeCount, removeCount);
                //Debug.Log(path);
                //Debug.Log("路径" + UnityEditor.AssetDatabase.GetAssetPath(this));
                return path;
#endif
                throw new Exception("只允许在UnityEditor下调用！");
            }
        }

    }

}
