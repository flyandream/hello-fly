using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SSG;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Animations;

#endif

public class UtilsEditorTool
{
#if UNITY_EDITOR

    #region 颜色

    public static Color GUI_COLOR_WINE_RED = new Color(215F / 255, 47F / 255, 122F / 255);

    #endregion

    /// <summary>
    ///  获取对应动画控制器对应名称的状态，animator:动画控制器，stateName：状态名称，layerIndex：层级，默认0
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateName"></param>
    /// <param name="layerIndex"></param>
    /// <returns></returns>
    public static AnimatorState GetAnimatorState(Animator animator, string stateName, int layerIndex = 0)
    {
        AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;
        if (animatorController != null)
        {
            AnimatorStateMachine stateMachine = animatorController.layers[layerIndex].stateMachine;
            for (int i = 0; i < stateMachine.states.Length; i++)
            {
                if (stateName == stateMachine.states[i].state.name)
                {
                    return stateMachine.states[i].state;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// 获取所有动画控制器状态列表，animator：动画控制器，layerIndex：层级，默认为0
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="layerIndex"></param>
    /// <returns></returns>
    public static List<AnimatorState> GetAnimaorSates(Animator animator, int layerIndex = 0)
    {
        List<AnimatorState> result = new List<AnimatorState>();

        AnimatorController animatorController = animator.runtimeAnimatorController as AnimatorController;
        if (animatorController != null)
        {
            AnimatorStateMachine stateMachine = animatorController.layers[layerIndex].stateMachine;
            for (int i = 0; i < stateMachine.states.Length; i++)
            {
                result.Add(stateMachine.states[i].state);
            }
        }

        return result;
    }

    /// <summary>
    /// 从动画组件中获取动画片段
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public static AnimationClip GetClipFormAnimatorState(AnimatorState state)
    {
        return (state.motion as AnimationClip);
    }


    #region 打开文件夹，文件

    /// <summary>
    /// 打开文件夹，path：文件夹路径
    /// </summary>
    /// <param name="path"></param>
    public static void OpenDirectory(string path)
    {
        if (string.IsNullOrEmpty(path)) return;

        path = path.Replace("/", "\\");
        if (!Directory.Exists(path))
        {
            Debug.LogError("No Directory: " + path);
            return;
        }

        System.Diagnostics.Process.Start("explorer.exe", path);
    }

    /// <summary>
    /// 打开文件，filepath：文件夹路径
    /// </summary>
    /// <param name="filepath"></param>
    public static void OpenFile(string filepath)
    {
        if (string.IsNullOrEmpty(filepath)) return;

        filepath = filepath.Replace("/", "\\");
        if (!File.Exists(filepath))
        {
            Debug.LogError("No File: " + filepath);
            return;
        }

        System.Diagnostics.Process.Start("explorer.exe", filepath);
    }

    /// <summary>
    /// 创建文件夹，
    /// </summary>
    public static void CreateFolder(string folderPath)
    {
        //如果存在对应的文件夹，直接返回
        if (Directory.Exists(folderPath))
        {
            return;
        }

        Directory.CreateDirectory(folderPath);
        //更新资源
        AssetDatabase.Refresh();
    }

    //检查文件
    public static bool CheckFile(string filePath)
    {
        //如果存在对应的文件夹，直接返回
        return File.Exists(filePath);
    }

    #endregion

    #region 文件路径操作

    /// <summary>
    /// 剔除文件路径后缀
    /// </summary>
    /// <returns></returns>
    public static string RemoveFilePathSuffix(string filePath)
    {
        int indexPoint = filePath.IndexOf('.');
        return filePath.Remove(indexPoint, filePath.Length - indexPoint);
    }


    #endregion

    #region 移动文件夹内容

    public static void MoveFolderContentTo(string oldFolder, string newFolder)
    {
#if UNITY_EDITOR
        if (Directory.Exists(oldFolder))
        {
            Debug.Log("将文件夹\t" + oldFolder + "\t移到--->\t" + newFolder);
            UtilsEditorTool.CreateFolder(newFolder);
            DirectoryInfo directoryInfo = new DirectoryInfo(oldFolder);

            //移动文件 
            foreach (FileInfo item in directoryInfo.GetFiles())
            {
                string temPath = newFolder + "/" + item.Name;
                if (File.Exists(temPath))
                {
                    //存在相同的文件,询问是否覆盖           
                    if (EditorUtility.DisplayDialog("提示", "存在同名文件:" + temPath + "，是否覆盖？", "是"))
                    {
                        File.Delete(temPath);
                        item.MoveTo(temPath);
                    }
                }
                else
                {
                    item.MoveTo(temPath);
                }
            }

            foreach (DirectoryInfo item in directoryInfo.GetDirectories())
            {
                string temPath = newFolder + "/" + item.Name;
                if (Directory.Exists(temPath))
                {
                    //存在相同的文件夹,询问是否覆盖           
                    if (EditorUtility.DisplayDialog("提示", "存在同名文件夹：" + temPath + "，是否合并？", "是"))
                    {
                        //组合文件夹
                        MoveFolderContentTo(oldFolder + "/" + item.Name, temPath);
                    }
                }
                else
                {
                    item.MoveTo(temPath);
                }
            }
        }
        else
        {
            Debug.Log("不存在文件夹：" + oldFolder);
        }
#endif
    }

    #endregion

    #region 获取组件路径

    /// <summary>
    ///   获取UI组件层级结构路径
    /// </summary>
    /// <param name="tran"></param>
    /// <param name="curStr"></param>
    /// <returns></returns>
    public static string GetUICompentPath(Transform tran, string curStr)
    {
        string result = curStr;
        //父节点为空，返回复制路径
        if (tran.parent == null)
        {
            return result;
        }

        //父节点的父节点为空
        if (tran.parent.parent == null)
        {
            //返回复制路径
            return curStr;
        }

        //父节点的上两层节点为空
        if (tran.parent.parent.parent == null)
        {
            //返回复制路径
            return curStr;
        }

        //加上父节点的名称到路径
        result = tran.parent.name + "/" + curStr;
        return GetUICompentPath(tran.parent, result);
    }

    /// <summary>
    /// 获取组件层级路径
    /// </summary>
    /// <param name="tran"></param>
    /// <param name="curStr"></param>
    /// <returns></returns>
    public static string GetGameObjectHierarchyPath(Transform tran)
    {
        Transform parent = null;
        string result = tran.name;
        parent = tran.parent;
        while (parent != null && parent.parent != null)
        {
            result = parent.name + "/" + result;
            parent = parent.parent;
        }

        return result;
    }

    #endregion

    #region 移除动画组件

    public static void RemoveAllAnimCom(Transform root)
    {
        int animatiorCount = 0;
        int animationCount = 0;
        foreach (Transform child in root)
        {
            Animator anim = child.GetComponent<Animator>();
            Animation animation = child.GetComponent<Animation>();
            if (anim != null)
            {
                GameObject.DestroyImmediate(anim); //移除Animator组件
                animatiorCount++;
            }

            if (animation != null)
            {
                GameObject.DestroyImmediate(animation); //移除Animation组件
                animationCount++;
            }

            RemoveAllAnimCom(child.transform);
        }

        Debug.Log("移除动画组件成功，一共：" + (animatiorCount + animationCount) + " 其中:\n  Animator: " + animatiorCount +
                  "     Animation:" + animationCount);
    }

    #endregion

    #region 关闭/开启阴影接受

    public static void CloseMeshReciveShadow(Transform root)
    {
        MeshRenderer renderRoot = root.GetComponent<MeshRenderer>();
        if (renderRoot != null)
        {
            renderRoot.receiveShadows = false;
        }

        foreach (Transform child in root)
        {
            MeshRenderer render = child.GetComponent<MeshRenderer>();
            if (render != null)
            {
                render.receiveShadows = false;
            }

            CloseMeshReciveShadow(child.transform);
        }

        Debug.Log("关闭阴影成功！");
    }

    /// <summary>
    /// 打开阴影接受
    /// </summary>
    /// <param name="root"></param>
    public static void OpenMeshReciveShadow(Transform root)
    {
        MeshRenderer renderRoot = root.GetComponent<MeshRenderer>();
        if (renderRoot != null)
        {
            renderRoot.receiveShadows = true;
        }

        foreach (Transform child in root)
        {
            MeshRenderer render = child.GetComponent<MeshRenderer>();
            if (render != null)
            {
                render.receiveShadows = true;
            }

            CloseMeshReciveShadow(child.transform);
        }

        Debug.Log("开启阴影接受成功！");
    }

    #endregion

    #region 设置LightmapScale大小为0

    public static void SetMeshLightMapScaleZero(Transform root)
    {
        MeshRenderer renderRoot = root.GetComponent<MeshRenderer>();
        if (renderRoot != null)
        {
            renderRoot.lightmapScaleOffset =
                new Vector4(0, 0, renderRoot.lightmapScaleOffset.z, renderRoot.lightmapScaleOffset.w);
            renderRoot.realtimeLightmapScaleOffset = Vector4.zero;
        }

        foreach (Transform child in root)
        {
            MeshRenderer render = child.GetComponent<MeshRenderer>();
            if (render != null)
            {
                render.lightmapScaleOffset =
                    new Vector4(0, 0, render.lightmapScaleOffset.z, render.lightmapScaleOffset.w);
            }

            SetMeshLightMapScaleZero(child.transform);
        }

        Debug.Log("设置LightmapScale大小为0,成功！");
    }

    #endregion

    #region 创建预制物

    /// <summary>
    /// 创建预制物，路径格式：[Assets/xxx]
    /// </summary>
    /// <param name="go"></param>
    /// <param name="path"></param>
    public static GameObject CreatePrefab(GameObject go, string assetsLoadPath)
    {
        string path = assetsLoadPath + ".prefab";
        Object prefab = PrefabUtility.CreatePrefab(path, go);
        Debug.Log("预制物导出路径:" + path);
        return PrefabUtility.ReplacePrefab(go, prefab, ReplacePrefabOptions.ConnectToPrefab);
    }

    #endregion

    #region 资源加载

    /// <summary>
    /// 从Editor Default Resources加载GameObject,path需要带文件.后缀
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static GameObject EditorLoadGameObject(string path)
    {
        Object res = EditorGUIUtility.Load(path);
        GameObject clone = GameObject.Instantiate(res) as GameObject;
        clone.SetActive(true);
        return clone;
    }


    /// <summary>
    /// 从Editor Default Resources加载Texture,path需要带文件.后缀
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Texture EditorLoadTexture(string path)
    {
        Texture result = EditorGUIUtility.Load(path) as Texture;
        return result;
    }

    /// <summary>
    /// AssetDataBase加载，path：[格式：Assets/XXXXX.后缀]
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static GameObject AssetLoadGameObject(string path)
    {
        return GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(path)) as GameObject;
    }

    /// <summary>
    /// AssetDataBase加载Sprite,path:[格式：Assets/XXXXX.后缀]
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Sprite AssetLoadSprite(string path)
    {
        return AssetDatabase.LoadAssetAtPath<Sprite>(path);
    }

    /// <summary>
    ///  AssetDataBase加载Texture,path:[格式：Assets/XXXXX.后缀]
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Texture AssetLoadTexture(string path)
    {
        return AssetDatabase.LoadAssetAtPath<Texture>(path);
    }


    /// <summary>
    ///  AssetDataBase加载Texture2D,path:[格式：Assets/XXXXX.后缀]
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Texture2D AssetLoadTexture2D(string path)
    {
        return AssetDatabase.LoadAssetAtPath<Texture2D>(path);
    }

    /// <summary>
    /// AssetDataBase加载Mat,path:[格式：Assets/XXXXX.后缀]
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Material AssetLoadMat(string path)
    {
        return AssetDatabase.LoadAssetAtPath<Material>(path);
    }



    /// <summary>
    /// AssetDataBase加载PhysicMaterial,path:[格式：Assets/XXXXX.后缀]
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static PhysicMaterial AssetLoadPhysicMaterial(string path)
    {
        return AssetDatabase.LoadAssetAtPath<PhysicMaterial>(path);
    }


    /// <summary>
    /// 加载文件夹下所有文件
    /// </summary>
    /// <typeparam name="T">转换的类型</typeparam>
    /// <param name="folderPath">文件夹路径，全路径</param>
    /// <param name="searchPattern">筛选参数</param>
    /// <returns></returns>
    public static List<T> AssetLoadFolderObjects<T>(string folderPath, string searchPattern = "*") where T : Object
    {
        List<T> result = new List<T>();
        string assetPath = folderPath.Replace(Application.dataPath, "Assets");
        if (Directory.Exists(folderPath))
        {
            DirectoryInfo direction = new DirectoryInfo(folderPath);
            FileInfo[] files = direction.GetFiles(searchPattern, SearchOption.AllDirectories);
            Debug.Log("文件夹路径" + folderPath);
            Debug.Log("有效文件" + files.Length);
            for (int i = 0; i < files.Length; i++)
            {
                //忽略掉.meta文件
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }

                string loadPath = assetPath + "/" + files[i].Name; //加载路径
                result.Add(AssetDatabase.LoadAssetAtPath<T>(loadPath));
                //Debug.Log("Name:" + files[i].Name); //打印出来这个文件架下的所有文件
            }
        }

        return result;
    }



    /// <summary>
    /// 查找所有文件
    /// </summary>
    public static List<FileInfo> FindAllFile(string folderPath, string searchPattern = "*")
    {
        List<FileInfo> result = new List<FileInfo>();
        if (Directory.Exists(folderPath))
        {
            DirectoryInfo direction = new DirectoryInfo(folderPath);
            foreach (DirectoryInfo info in direction.GetDirectories())
            {
                FileInfo[] files = info.GetFiles(searchPattern, SearchOption.AllDirectories);
                result.AddRange(files);
            }
        }

        return result;
    }

    #endregion

    #region 截图

    /// <summary>
    /// 保存RenderTex为Png,filePath:全路径，不需要后缀,return:生成的图片路径
    /// </summary>
    /// <param name="renderTex"></param>
    /// <param name="filePath"></param>
    public static void SaveRenderToTex2D(RenderTexture renderTex, string filePath)
    {
        RenderTexture.active = renderTex;
        Texture2D tex2D = new Texture2D(renderTex.width, renderTex.height);
        tex2D.ReadPixels(new Rect(0, 0, tex2D.width, tex2D.height), 0, 0);
        tex2D.Apply();
        string path = filePath + ".png";
        System.IO.File.WriteAllBytes(path, tex2D.EncodeToPNG());
        Debug.Log("图片保存成功" + path);
        //刷新图片
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 保存renderTexture为Sprite2D
    /// </summary>
    public static void SaveRenderToSprite2D(RenderTexture renderTex, string filePath)
    {
        RenderTexture.active = renderTex;
        Texture2D tex2D = new Texture2D(renderTex.width, renderTex.height);
        tex2D.ReadPixels(new Rect(0, 0, tex2D.width, tex2D.height), 0, 0);
        tex2D.Apply();
        string path = filePath + ".png";
        System.IO.File.WriteAllBytes(path, tex2D.EncodeToPNG());
        //刷新图片
        AssetDatabase.Refresh();
        Debug.Log("Sprite2D保存成功" + path);
        string assetPath = path.Replace(Application.dataPath, "");
        assetPath = "Assets" + assetPath;
        TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        textureImporter.textureType = TextureImporterType.Sprite;
        textureImporter.spriteImportMode = SpriteImportMode.Single;
        //保存
        textureImporter.SaveAndReimport();
    }

    #endregion

    #region 创建材质

    /// <summary>
    /// 创建材质球
    /// </summary>
    /// <param name="shaderName"></param>
    /// <param name="path"></param>
    public static void CreateMat(Material mat, string path)
    {
        AssetDatabase.CreateAsset(mat, path + ".mat");
        AssetDatabase.Refresh();
    }

    #endregion


    [MenuItem("Tools/过滤未使用的LanguageId")]
    private static void FiltrationInvalidLanguageId()
    {
        //语言适配表路径
        string languageFilePath = "" ;
        //无效Id列表
        List<string> listInvalidId = new List<string>();
        //CS文件路径
        List<FileInfo> listFile = UtilsEditorTool.FindAllFile(Application.dataPath, "*.cs");
        Debug.Log("比对的CS文件共：" + listFile.Count + "个");
        Regex reg = new Regex(@"[\d]+");
        byte[] bytesContent;
        //数据放入resource文件夹下
        try
        {
            bytesContent = Resources.Load<TextAsset>(languageFilePath).bytes;

        }
        catch (Exception)
        {

            throw new Exception(languageFilePath + "读取失败！检查路径Resources/" + languageFilePath);
        }
        var dt = CsvFileReader.ReadAll(new System.IO.MemoryStream(bytesContent), Encoding.UTF8, 1);//以UTF-8的编码读取
        for (int i = 0; i < dt.rows.Count; i++)
        {
            listInvalidId.Add(dt.rows[i][0]);
        }
        for (int i = 0; i < listFile.Count; i++)
        {
            StreamReader sr = File.OpenText(listFile[i].FullName);
            string content = sr.ReadToEnd();
            sr.Close();
            MatchCollection mcs = reg.Matches(content);
            foreach (Match mc in mcs)
            {
                if (mc.Value.Length >= 7)
                {
                    listInvalidId.Remove(mc.Value);
                }
            }
        }

        for (int i = 0; i < listInvalidId.Count; i++)
        {
            Debug.Log(listInvalidId[i]);
        }

    }

    [MenuItem("SSG/配置表/打开配置表所在文件夹")]
    private static void OpenConfigFolder()
    {
        OpenDirectory(Application.dataPath + "/07_Config/配置表");
    }

    [MenuItem("SSG/配置表/打开语言适配表")]
    private static void OpenLanugageFile()
    {
        OpenFile(Application.dataPath + "/07_Config/配置表/DataExcel/Language.xlsx");
    }


    [MenuItem("SSG/配置表/打开特效配置表")]
    private static void OpenFXFile()
    {
        OpenFile(Application.dataPath + "/07_Config/配置表/DataExcel/FX.xlsx");
    }


    [MenuItem("SSG/配置表/打开音效配置表")]
    private static void OpenSoundFile()
    {
        OpenFile(Application.dataPath + "/07_Config/配置表/DataExcel/Sounds.xlsx");
    }



#endif

    #region 修改脚本

    public static void ReplaceCsScriptContext(string scriptPath, string oldStr, string newStr, int startIndex = 0,
        int count = 1)
    {
        string[] strLine = File.ReadAllLines(scriptPath);
        for (int i = 0; i < strLine.Length; i++)
        {
            if (strLine[i].Contains(oldStr))
            {
                //修改为             
                strLine[i] = strLine[i].Replace(oldStr, newStr);
                Debug.Log("修改脚本文本:" + oldStr + "  -->" + strLine[i]);
                count -= 1;
                if (count <= 0)
                {
                    break;
                }
            }
        }

        File.WriteAllLines(scriptPath, strLine);
    }

    #endregion

    #region 弹出提示

    public static void ShowBoxMessage(string msg, Action yesHandle = null)
    {
#if UNITY_EDITOR
        if (EditorUtility.DisplayDialog("提示", msg, "是"))
        {
            if (yesHandle != null)
            {
                yesHandle();
            }
        }
#endif
    }

    #endregion

    #region 拓展Scene

    //获取鼠标位置的与指定层的射线情况
    public static bool GetMouseRayHitInScene(out RaycastHit hit, float rayDistance, int layerMask)
    {
        hit = new RaycastHit();
#if UNITY_EDITOR
        //预览绘制范围
        if (Event.current != null)
        {
            HandleUtility.AddDefaultControl(0);
            // RaycastHit hit;          
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            if (Physics.Raycast(ray, out hit, rayDistance, layerMask))
            {
                return true;
            }
        }
#endif
        return false;
    }

    #endregion

    #region 判断输入

    //按钮按下
    public static bool GetKeyDown(KeyCode keyCode)
    {
        if (Event.current == null)
        {
            return false;
        }

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == keyCode)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// 判断按钮按下
    /// </summary>
    /// <param name="mouseType"></param>
    /// <returns></returns>
    public static bool GetMouseDown(int mouseType)
    {
        //0左键，1右键，2中键
        if (Event.current == null)
        {
            return false;
        }

        if (Event.current.button == mouseType && Event.current.isMouse && Event.current.type == EventType.MouseDown &&
            Event.current.modifiers == EventModifiers.None)
        {
            return true;
        }
        ////鼠标左键，特殊处理
        //if (mouseType==0)
        //{            

        //}
        //else
        //{
        //   //其它按键统一处理
        //    if (Event.current.button == mouseType)
        //    {
        //        return true;
        //    }
        //}       
        return false;
    }


    /// <summary>
    /// 判断按钮抬起
    /// </summary>
    /// <param name="mouseType"></param>
    /// <returns></returns>
    public static bool GetMouseUp(int mouseType)
    {
        //0左键，1右键，2中键
        if (Event.current == null)
        {
            return false;
        }

        if (Event.current.button == mouseType && Event.current.isMouse && Event.current.type == EventType.MouseUp &&
            Event.current.modifiers == EventModifiers.None)
        {
            return true;
        }

        return false;
    }

    public static bool GetMouse(int mouseType)
    {
        //0左键，1右键，2中键
        if (Event.current == null)
        {
            return false;
        }

        if (Event.current.button == mouseType && Event.current.isMouse && Event.current.type != EventType.MouseUp &&
            Event.current.type != EventType.MouseMove && Event.current.modifiers == EventModifiers.None)
        {
            return true;
        }

        return false;
    }

    //获取输入事件
    public static bool GetInputEventType(EventType type)
    {
        Event e = Event.current;
        if (e == null)
        {
            return false;
        }

        if (e.type == type)
        {
            return true;
        }

        return false;
    }

    //判断组合键
    public static bool CheckCombinKey(EventModifiers modifiers, EventType type, KeyCode keyCode)
    {
        Event e = Event.current;
        if (e == null)
        {
            return false;
        }

        if (e.modifiers == modifiers && e.type == type && e.keyCode == keyCode)
        {
            return true;
        }

        return false;
    }

    //判断组合键
    public static bool CheckCombinKey(EventModifiers modifiers, EventType type, int mouseType)
    {
        Event e = Event.current;
        if (e == null)
        {
            return false;
        }

        if (e.modifiers == modifiers && e.type == type && e.button == mouseType)
        {
            return true;
        }

        return false;
    }

    #endregion
}