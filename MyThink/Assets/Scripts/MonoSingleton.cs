using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonoSingleton<T> : MonoBehaviour where T:MonoBehaviour
{
    static T instance;
    public static T Instacne
    {
        get
        {
            if (MonoSingleGameObject.go==null)
            {
                MonoSingleGameObject.go = new GameObject("MonoSingleGameObject");
                DontDestroyOnLoad(MonoSingleGameObject.go);
            }
            if (MonoSingleGameObject.go!=null&&instance == null)
            {
                instance = MonoSingleGameObject.go.AddComponent<T>();
            }

            return instance;
        }

      
           
    }
    //有时候，有的组件场景切换时候回收的
    public static bool destroyOnLoad = false;
    public void AddSceneChangEvent()
    {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        if (destroyOnLoad==true)
        {
            if (instance!=null)
            {
                DestroyImmediate(instance);//立即销毁
            }
        }
    }

   

    public class MonoSingleGameObject
    {
        public static GameObject go;
    }


   
}
