using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using UnityEngine.Profiling;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using System.Collections;


#region 常用委托
public delegate void OnValueChangeEventHandle();//值变化事件委托
public delegate void OnBtnClickHandle();//按钮点击委托
public delegate  void VoidHandle();//空委托
#endregion



/// <summary>
/// 常用方法
/// </summary>
public class UtilsTools
{

    #region 暂停，继续游戏

    //暂停游戏
    public static void PauseGame()
    {
        Time.timeScale = 0;
    }

    //继续游戏
    public static void ResumeGame()
    {
        Time.timeScale = 1;
    }

    #endregion

    #region Tag标签和Layer设置

    /// <summary>
    /// 设置标签，target：为设置为物体，tag:标签名
    /// </summary>
    public static void SetTag(GameObject target, string tag)
    {
        target.tag = tag; //设置标签
    }

    /// <summary>
    /// 设置layer层级，target：目标，layer：层级,ignoreObj:排除的物体
    /// </summary>
    /// <param name="target"></param>
    /// <param name="layer"></param>
    public static void SetLayer(GameObject target, int layer, params GameObject[] ignoreObj)
    {
        //便利所有子物体，设置层级
        target.layer = layer;
        foreach (Transform child in target.transform)
        {
            bool needSetLayer = true;
            for (int i = 0; i < ignoreObj.Length; i++)
            {
                if (child.gameObject == ignoreObj[i])
                {
                    needSetLayer = false;
                    break;
                }
            }
            if (needSetLayer)
            {
                SetLayer(child.gameObject, layer);
            }
        }
    }



    #endregion

    #region 资源加载和创建


    /// <summary>
    /// 加载GameObject,path:资源路径
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static GameObject LoadGameObject(string path)
    {
        return GameObject.Instantiate(Resources.Load(path, typeof(GameObject))) as GameObject;
    }

    /// <summary>
    /// 加载GameObject,path：资源路径，potition:生成的坐标，rotation：生成的朝向
    /// </summary>
    /// <param name="path"></param>
    /// <param name="potition"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public static GameObject LoadGameObject(string path, Vector3 potition, Quaternion rotation)
    {
        return GameObject.Instantiate(Resources.Load(path, typeof(GameObject)), potition, rotation) as GameObject;
    }

    /// <summary>
    /// 加载GameObject,path：资源路径，parent:父物体
    /// </summary>
    /// <param name="path"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public static GameObject LoadGameObject(string path, Transform parent)
    {
        return GameObject.Instantiate(Resources.Load(path, typeof(GameObject)), parent) as GameObject;
    }

    /// <summary>
    /// 复制物体
    /// </summary>   
    public static GameObject CopyCreateGameObject(GameObject goPrefab, Transform parent)
    {
        GameObject clone = GameObject.Instantiate(goPrefab, parent);
        return clone;
    }


    /// <summary>
    /// 加载Texture2D,path：加载的路径
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Texture2D LoadTexture2D(string path)
    {
        return Resources.Load(path, typeof (Texture2D)) as Texture2D;
    }

    /// <summary>
    /// 加载Sprite,path:加载的路径
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Sprite LoadSprite(string path)
    {
        return Resources.Load(path, typeof (Sprite)) as Sprite;
    }


    /// <summary>
    /// 加载Texture2D,path：加载的路径
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Texture LoadTexture(string path)
    {
        return Resources.Load(path, typeof (Texture)) as Texture;
    }

    /// <summary>
    /// 加载材质,path：加载的路径
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Material LoadMaterial(string path)
    {
        return Resources.Load<Material>(path);
    }

 
    /// <summary>
    /// 加载动画片段，animPath：动画路径，clipName：动画片段名称
    /// </summary>
    /// <param name="animPath"></param>
    /// <param name="clipName"></param>
    /// <returns></returns>
    public static AnimationClip LoadAnimationClip(string animPath, string clipName)
    {
        //获取包含的动画Id
        AnimationClip[] clips = Resources.LoadAll<AnimationClip>(animPath); //读取所有的动画片段
        for (int i = 0; i < clips.Length; i++)
        {
            if (clips[i].name == clipName)
            {
                return clips[i];
            }
        }
        throw new Exception("资源文件夹：" + animPath + "找不到动画名称为：" + clipName);
    }

    /// <summary>
    /// 加载动画片片段数组
    /// </summary>
    /// <param name="animPath"></param>
    /// <returns></returns>
    public static AnimationClip[] LoadAnimationClips(string animPath)
    {
        //获取包含的动画Id
        AnimationClip[] clips = Resources.LoadAll<AnimationClip>(animPath); //读取所有的动画片段
        return clips;       
    }

    #endregion

    #region 时间相关
    /// <summary>
    /// 时间戳转DateTime
    /// </summary>
    /// <param name="span"></param>
    /// <returns></returns>
    public static DateTime TimeSpanToDateTime(long span)
    {
        DateTime time = DateTime.MinValue;
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
        time = startTime.AddSeconds(span);
        return time;
    }

    /// <summary>
    /// DateTime时间格式转换为Unix时间戳格式
    /// </summary>
    /// <param name="time"> DateTime时间格式</param>
    /// <returns>Unix时间戳格式</returns>
    public static long ConvertDateTimeToTimeStamp(System.DateTime time)
    {
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        return (long) (time - startTime).TotalSeconds;
    }

    /// <summary>
    /// 获取当前时间的时间戳
    /// </summary>    
    public static long GetTimeNowTimeStamp()
    {
        return ConvertDateTimeToTimeStamp(DateTime.Now);
    }
    /// <summary>
    /// 是否是同一天
    /// </summary>
    /// <param name="date1">日期1</param>
    /// <param name="data2">日期2</param>
    /// <returns></returns>
    public static bool IsSameDay(DateTime date1,DateTime data2)
    {
        if (date1.Year==data2.Year&&date1.Month==data2.Month&&date1.Day==data2.Day)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 计算开始日期到结束日期相差的天数，后减去前，可存在负值
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public static int CalculTimeSpanDay(DateTime startDate,DateTime endDate)
    {   
        TimeSpan sp = endDate.Subtract(startDate);
        return sp.Days;
    }

    /// <summary>
    /// 计算开始日期到结束日期相差的秒数，后减去前，可存在负值
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public static int CalculTimeSpanSec(DateTime startDate, DateTime endDate)
    {

        long stampSecEnd = ConvertDateTimeToTimeStamp(endDate);
        long stampSecStart= ConvertDateTimeToTimeStamp(startDate);
        //如果不是同一天，判断是不是前一天
        //DateTime start = Convert.ToDateTime(startDate.ToShortDateString());
        //DateTime end = Convert.ToDateTime(endDate.ToShortDateString());
        //TimeSpan sp = end - start;             
        return (int) (stampSecEnd - stampSecStart);        
    }

    /// <summary>
    /// 格式化秒为时，分，秒，totalSec：总时长（单位：秒）
    /// </summary>
    /// <param name="sec"></param>
    public static void FormatTimeStrToHMS(int totalSec,out int hour,out int min,out int sec)
    {
        hour = totalSec/3600;//时
        min = (totalSec - hour*3600)/60;//分
        sec = (totalSec - hour*3600 - min*60);
    }


    /// <summary>
    /// 格式化秒为分，秒，totalSec：总时长（单位：秒）
    /// </summary>
    /// <param name="sec"></param>
    public static void FormatTimeStrToMS(int totalSec,  out int min, out int sec)
    {     
        min =totalSec / 60;//分
        sec = totalSec %60;//秒
    }

    #endregion

    #region   文本相关

    /// <summary>
    /// 设置文本颜色,color：颜色字符串例如#54E5FA,txtContent需要加上颜色变化的内容
    /// </summary>
    /// <param name="color"></param>
    /// <param name="txt"></param>
    /// <returns></returns>
    public static string SetTxtColor(string color, string txtContent)
    {
        return "<color=" + color + ">" + txtContent + "</color>";
    }

    #endregion

    #region 层级相关

    /// <summary>
    /// 获取根节点Transform
    /// </summary>
    /// <param name="targeTransform"></param>
    /// <returns></returns>
    public static Transform FindRootParentTransform(Transform targeTransform)
    {
        if (targeTransform.parent != null)
        {
            return FindRootParentTransform(targeTransform.parent);
        }
        else
        {
            return targeTransform;
        }
    }

    #endregion

    #region 打印内存

    public static void PrintMemory()
    {
        long usedsize = Profiler.GetMonoUsedSizeLong();
        long heapsize = Profiler.GetMonoHeapSizeLong();
        long reservedsize = heapsize - usedsize;
        Debug.Log("使用内存=" + usedsize*1.0f/1024/1024 + "M");
        Debug.Log("剩余内存=" + reservedsize*1.0f/1024/1024 + "M");
        Debug.Log("托管堆内存=" + heapsize*1.0f/1024/1024 + "M");
    }
    #endregion
    
    #region 屏幕适应优化
    public static void  FitScreen(int curPPi)
    {
        //如果当前设备的PPI小于设计的PPI
        if(curPPi<GameConfig.DESGIN_PPI)
        {
            //不做处理
            return;
        }
        //长2平方+宽2平方=(长1平方+宽1平方)*PPI2平方/PPI1平方
        float parm = (Mathf.Pow(Screen.width, 2) +
                         Mathf.Pow(Screen.width * GameConfig.DESGIN_WIDTN_HEIGHT_RATIO, 2)) * Mathf.Pow(GameConfig.DESGIN_PPI, 2) /
                         Mathf.Pow(curPPi, 2);

        //以长为准
        float fixWidth=Mathf.Sqrt(parm / (Mathf.Pow(GameConfig.DESGIN_WIDTN_HEIGHT_RATIO, 2) + 1));//最佳屏幕宽        
        float fixHeight = fixWidth*GameConfig.DESGIN_WIDTN_HEIGHT_RATIO;//最佳屏幕高

        //如果宽度比屏幕宽大
        if ((int)fixWidth>Screen.width)
        {
            //不处理
            return;
        }
        //设置屏幕宽高
        Screen.SetResolution((int)fixWidth,(int)fixHeight, true);
    }
    #endregion

    #region 动画相关
    /// <summary>
    /// 获取animtor中对应动画片段的长度,animator:动画控制器，clipName：片段名称
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="clipName"></param>
    /// <returns></returns>
    public static float GetClipLength( Animator animator, string clipName)
    {
        if (null == animator || string.IsNullOrEmpty(clipName) || null == animator.runtimeAnimatorController)
            return 0;
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        AnimationClip[] animClips = ac.animationClips;
        if (null == animClips || animClips.Length <= 0) return 0;
        //遍历查找动画片段时长长度       
        for (int i = 0; i < animClips.Length; i++)
        {

            if (animClips[i]!=null&& animClips[i].name== clipName)
            {
                return animClips[i].length;
            }
        }
        return 0;
    }

    /// <summary>
    /// 获取动画片段的时长，animation：动画组件，clipName：片段名称
    /// </summary>
    /// <param name="animation"></param>
    /// <param name="clipName"></param>
    /// <returns></returns>
    public static float GetClipLength(Animation animation,string clipName)
    {
        AnimationClip  clip= animation.GetClip(clipName);
        if (clip==null)
        {
            return 0;
        }
        return clip.length;
    }

    /// <summary>
    ///  获取Animator所有动画片段
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="layerIndex"></param>
    /// <returns></returns>
    public static AnimationClip[] GetAnimatorClips(Animator animator, int layerIndex = 0)
    {
        return animator.runtimeAnimatorController.animationClips;
    }


    /// <summary>
    /// 获取Animator所有动画，并返回字典，key:动画名称，value动画片段
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="layerIndex"></param>
    /// <returns></returns>
    public static Dictionary<string,AnimationClip>GetAnimatorClipsToDic(Animator animator)
    {

        Dictionary<string,AnimationClip> result=new Dictionary<string, AnimationClip>();        
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        for (int i = 0; i < clips.Length; i++)
        {
            result.Add(clips[i].name,clips[i]);
        }
        return result;
    }

    /// <summary>
    /// 重建动画片段实例，animation：动画组件，allClips：是否所有的动画片段都需要创建新的，clipName：动画片段名称（allClips为fasle才生效）
    /// </summary>
    /// <param name="animation"></param>
    /// <param name="allClips"></param>
    /// <param name="clipName"></param>
    public static void RebuildInstantiateAnimationClips(Animation animation, bool allClips = true, string clipName = "")
    {
        if (allClips)
        {
            //创建片段字典缓存当前的片段
            Dictionary<string, AnimationClip> dicAnimClips = new Dictionary<string, AnimationClip>();
            //如果所有动画片段都需要重新构建
            foreach (AnimationState state in animation)
            {
                AnimationClip clip = AnimationClip.Instantiate(state.clip);
                dicAnimClips.Add(state.clip.name, clip);
            }
            foreach (var animClip in dicAnimClips)
            {
                //去除对应的动画片段，加入新创建的动画片段
                animation.RemoveClip(animClip.Key);
                animation.AddClip(animClip.Value, animClip.Key);
            }
        }
        else
        {
            //如果是重新构建单个动画
            AnimationClip clip = AnimationClip.Instantiate(animation.GetClip(clipName));
            animation.RemoveClip(clipName);
            animation.AddClip(clip, clipName);
        }
    }
    /// <summary>
    /// 重建动画片段实例，animator：动画控制器组件，allClips：是否所有的动画片段都需要创建新的，clipName：动画片段名称（allClips为fasle才生效）
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="allClips"></param>
    /// <param name="clipName"></param>

    public static void RebuildInstantiateAnimatorClips(Animator animator, bool allClips = true, string clipName = "")
    {
        AnimatorOverrideController overrideController = new AnimatorOverrideController();
        overrideController.runtimeAnimatorController = animator.runtimeAnimatorController;
        //如果所有片段都需要重建
        if (allClips)
        {

            AnimationClip[] clips = overrideController.animationClips;
            Dictionary<string, AnimationClip> dicAnimationClips = new Dictionary<string, AnimationClip>();
            //创建新的动画片段实例   
            for (int i = 0; i < clips.Length; i++)
            {
                AnimationClip tempClip = AnimationClip.Instantiate(clips[i]);
                tempClip.name = clips[i].name;
                try
                {
                    dicAnimationClips.Add(tempClip.name, tempClip);
                }
                catch (Exception)
                {
                    Debug.LogError("存在同名的动画片段：" + tempClip.name);
                }
            }

            foreach (var clipInfo in dicAnimationClips)
            {
                overrideController[clipInfo.Key] = clipInfo.Value;
            }
        }
        else
        {
            //创建新的动画片段实例   
            AnimationClip tempClip = AnimationClip.Instantiate(overrideController[clipName]);
            tempClip.name = clipName;
            overrideController[clipName] = tempClip;
        }
        animator.runtimeAnimatorController = null;
        animator.runtimeAnimatorController = overrideController;
    }

    /// <summary>
    /// 判断是否播放对应的动画片段，animator：动画状态机，clipName：动画片段名称，layer:层级，默认为0
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="clipName"></param>
    /// <param name="layer"></param>
    /// <returns></returns>
    public static bool IsPlayingAnimClip(Animator animator, string clipName, int layer = 0)
    {
        return animator.GetCurrentAnimatorClipInfo(layer)[0].clip.name == clipName;
    }


    //使animation不受timescale控制
   public static IEnumerator Play(Animation animation, string clipName, bool useTimeScale, System.Action onComplete = null)
    {
        if (!useTimeScale)
        {
            AnimationState _currState = animation[clipName];
            bool isPlaying = true;
            float _startTime = 0F;
            float _progressTime = 0F;
            float _timeAtLastFrame = 0F;
            float _timeAtCurrentFrame = 0F;
            float deltaTime = 0F;
            animation.Play(clipName);
            _timeAtLastFrame = Time.realtimeSinceStartup;
            while (isPlaying)
            {
                _timeAtCurrentFrame = Time.realtimeSinceStartup;
                deltaTime = _timeAtCurrentFrame - _timeAtLastFrame;
                _timeAtLastFrame = _timeAtCurrentFrame;

                _progressTime += deltaTime;
                if (_currState==null)
                {
                    yield break;
                }
                _currState.normalizedTime = _progressTime / _currState.length;
                animation.Sample();
                if (_progressTime >= _currState.length)
                {
                    if (_currState.wrapMode != WrapMode.Loop)
                    {
                        isPlaying = false;
                    }
                    else
                    {
                        _progressTime = 0.0f;
                    }
                }
                yield return new WaitForEndOfFrame();
            }
            yield return null;
            if (onComplete != null)
            {
                onComplete();
            }
        }
        else
        {
            animation.Play(clipName);
        }
    }

    #endregion

    #region 角度转换
    /// <summary>
    /// //切换角度到[-180,+180]，angle:当前角度
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static float GetAngleTo180(float angle)
    {
        angle %= 360;
        if (angle > 180)
        {
            return angle -= 360;
        }
        else if(angle<-180)
        {
            return angle + 360;
        }
        return angle;
    }

    #endregion

    #region 向量相关计算
    /// <summary>
    /// Determine the signed angle between two vectors, with normal 'n'
    /// as the rotation axis.
    /// </summary>
    public static float Vector3AngleFormTo(Vector3 from, Vector3 to, Vector3 n)
    {
        return Mathf.Atan2(
            Vector3.Dot(n, Vector3.Cross(from, to)),
            Vector3.Dot(from, to)) * Mathf.Rad2Deg;
    }
    /// <summary>
    ///   计算二维向量夹角
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static float Vector2Angle(Vector2 from, Vector2 to)
     {
        float angle;       
        Vector3 cross = Vector3.Cross(from, to);
         angle = Vector2.Angle(from, to);
       return cross.z > 0 ? -angle : angle;
     }

    #endregion

    #region 数学计算
    /// <summary>
    /// 随机取正负数+1/-1
    /// </summary>
    public static int RandomPlusOrMinus
    {
        get
        {
            if (UnityEngine.Random.value < 0.5)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
    }

    #endregion

    #region GUI相关

    public static Color MixForGuiColor(Color color)
    {
        return new Color(color.r+1,color.g+1,color.b+1);
    }


    public static void DrawTestModeTip()
    {
        if (GameConfig.isTestMode)
        {
            GUI.backgroundColor = Color.white;
            GUI.Label(new Rect(Screen.width - 140, Screen.height - 30, 140, 20), "【#测试模式#】", WarnGuiStyle);
        }
    }

    private static GUIStyle _warnGuiStyle = null;


    public static GUIStyle WarnGuiStyle
    {
        get
        {
            if(_warnGuiStyle==null)
            {
                _warnGuiStyle=new GUIStyle();
                _warnGuiStyle.normal.textColor=Color.white;
                _warnGuiStyle.fontSize = 20;
            }
            return _warnGuiStyle;
        }
    }

    #endregion

    #region Mesh创建


    /// <summary>
    /// 创建圆mesh,raduis：半径，segments:段数
    /// </summary>
    public static Mesh CreateCircle(float raduis,int segments)
    {
        return CreateCircularAnnulus(raduis, 0, 360, segments);
    }

    /// <summary>
    /// 创建扇形,raduis：半径，angledegree：扇形度数，segments:段数
    /// </summary>
    public static Mesh CreateCircularSector(float raduis, float angledegree, int segments)
    {
        return CreateCircularAnnulus(raduis, 0, angledegree, segments);
    }


    /// <summary>
    /// //创建圆环mesh，radius：外圆半径，innerradius：内圆半径，angledegree:圆角度，segments：线段数
    /// </summary>
    public static Mesh CreateCircularAnnulus(float radius, float innerradius, float angledegree, int segments)
    {
        //vertices(顶点):
        int vertices_count = segments * 2 + 2;              //因为vertices(顶点)的个数与triangles（索引三角形顶点数）必须匹配
        Vector3[] vertices = new Vector3[vertices_count];
        float angleRad = Mathf.Deg2Rad * angledegree;
        float angleCur = angleRad;
        float angledelta = angleRad / segments;
        for (int i = 0; i < vertices_count; i += 2)
        {
            float cosA = Mathf.Cos(angleCur);
            float sinA = Mathf.Sin(angleCur);

            vertices[i] = new Vector3(radius * cosA, 0, radius * sinA);
            vertices[i + 1] = new Vector3(innerradius * cosA, 0, innerradius * sinA);
            angleCur -= angledelta;
        }

        //triangles:
        int triangle_count = segments * 6;
        int[] triangles = new int[triangle_count];
        for (int i = 0, vi = 0; i < triangle_count; i += 6, vi += 2)
        {
            triangles[i] = vi;
            triangles[i + 1] = vi + 3;
            triangles[i + 2] = vi + 1;
            triangles[i + 3] = vi + 2;
            triangles[i + 4] = vi + 3;
            triangles[i + 5] = vi;
        }

        //uv:
        Vector2[] uvs = new Vector2[vertices_count];
        for (int i = 0; i < vertices_count; i++)
        {
            uvs[i] = new Vector2(vertices[i].x / radius / 2 + 0.5f, vertices[i].z / radius / 2 + 0.5f);
        }

        //负载属性与mesh
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        return mesh;

    }


    #endregion

    #region 相机层级

    /*
    camera.cullingMask = ~(1 << x);  // 渲染除去层x的所有层      
    camera.cullingMask &= ~(1 << x); // 关闭层x      
    camera.cullingMask |= (1 << x);  // 打开层x    
    camera.cullingMask = 1 << x + 1 << y + 1 << z; // 摄像机只显示第x层,y层,z层
    */
    /// <summary>
    ///  设置相机渲染层级，exceptLayers不显示，其它都开启
    /// </summary>
    public static void SetCameraCullingMaskExcept(Camera camera,params int[] exceptLayers)
    {       
        if (exceptLayers.Length>0)
        {
            int value = 1 << exceptLayers[0];
            for (int i = 1; i < exceptLayers.Length; i++)
            {
                value += 1 << exceptLayers[i];
            }
            camera.cullingMask = ~(value);  // 除去层value的所有层  
        }
        else
        {
            throw new Exception("layers大小为0");
        }
    }


    /// <summary>
    ///  设置相机渲染层级，layers：显示的层级
    /// </summary>
    public static void SetCameraCullingMask(Camera camera, params int[] layers)
    {
        if (layers.Length > 0)
        {
            int value = 1 << layers[0];
            for (int i = 1; i < layers.Length; i++)
            {
                value += 1 << layers[i];
            }
            camera.cullingMask = (value); //设置渲染层级
        }
        else
        {
            throw new Exception("layers大小为0");
        }
      
    }

    /// <summary>
    /// 开启渲染层，layers:需要增加开启的层
    /// </summary>
    /// <param name="camera"></param>
    /// <param name="layers"></param>
    public static void OpenCameraCullingMask(Camera camera, params int[] layers)
    {
        if (layers.Length > 0)
        {
            int value = 1 << layers[0];
            for (int i = 1; i < layers.Length; i++)
            {
                value += 1 << layers[i];
            }
            camera.cullingMask |= (value);  //增加渲染层级
        }
        else
        {
            throw new Exception("layers大小为0");
        }
    }

    /// <summary>
    ///  关闭渲染层级，layers：需要关闭的层
    /// </summary>
    /// <param name="camera"></param>
    /// <param name="layers"></param>
    public static void CloseCameraCullingMask(Camera camera, params int[] layers)
    {
        if (layers.Length > 0)
        {
            int value = 1 << layers[0];
            for (int i = 1; i < layers.Length; i++)
            {
                value += 1 << layers[i];
            }
            camera.cullingMask &= ~(value);  //关闭渲染层级
        }
        else
        {
            throw new Exception("layers大小为0");
        }
    }




    #endregion

    #region 寻路区域相关
    /// <summary>
    /// 在指定的寻路区域中，areaMaskName判断的寻路区域，position：位置
    /// </summary>
    /// <param name="areaMaskName"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public static bool IsInNavAreaMask(string areaMaskName, Vector3 position)
    {
        NavMeshHit hit;
        int areaMask = 1 << (NavMesh.GetAreaFromName(areaMaskName));
        if (!NavMesh.SamplePosition(position, out hit, 0, NavMesh.AllAreas))
        {
            if ((hit.mask & areaMask) != 0)
            {

                return true;
            }
        }

        return false;
    }


    #endregion

}
