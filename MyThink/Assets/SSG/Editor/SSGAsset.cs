using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  SSG
{
    /// <summary>
    /// SSG资源
    /// </summary>
    public class SSGAsset : ScriptableObject
    {


        //资源路径
        public const string ASSET_PATH = "SSG_PATH";
        //对象池节点位置
        public PathNode PoolModuleNode;
        //存档节点位置
        public PathNode SaveDataModuleNode;

        public static  SSGAsset Instance
        {
            get
            {
                return Resources.Load<SSGAsset>(ASSET_PATH);
            }
        }

     

    }


}
