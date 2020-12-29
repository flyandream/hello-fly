
using System;
using System.Collections;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;
using Object = UnityEngine.Object;
using Texture = UnityEngine.Texture;

public class CreateFontEditorWindow : EditorWindow
{
    //创建字体模式
    public enum CreateFontMode
    {
        General,//默认模式
        Sprite,//图片模式
    }

    private CreateFontMode _mode;
    public void SetMode(CreateFontMode mode)
    {
        _mode = mode;
    }

    [MenuItem("Tools/创建字体(sprite)")]
    public static void OpenBySpriteMode()
    {
       CreateFontEditorWindow window= GetWindow<CreateFontEditorWindow>("创建字体（Sprite模式）");
       window.SetMode(CreateFontMode.Sprite);
    }

   [MenuItem("Tools/创建字体(传统模式)")]
    public static void OpenByGeneralMode()
    {
        CreateFontEditorWindow window = GetWindow<CreateFontEditorWindow>("创建字体（General模式）");
        window.SetMode(CreateFontMode.General);
    }



    #region Sprite模式
    private Texture2D tex;
    private string fontName;
    private string fontPath;


    private void SpriteModeOnGui()
    {
        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();
        GUILayout.Label("字体图片：");
        tex = (Texture2D)EditorGUILayout.ObjectField(tex, typeof(Texture2D), true);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("字体名称：");
        fontName = EditorGUILayout.TextField(fontName);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button(string.IsNullOrEmpty(fontPath) ? "选择路径" : fontPath))
        {
            fontPath = EditorUtility.OpenFolderPanel("字体路径", Application.dataPath, "");
            if (string.IsNullOrEmpty(fontPath))
            {
                Debug.Log("取消选择路径");
            }
            else
            {
                fontPath = fontPath.Replace(Application.dataPath, "") + "/";
            }
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("创建"))
        {
            Create();
        }

        GUILayout.EndHorizontal();

        GUILayout.EndVertical();

    }

    private void Create()
    {
        if (tex == null)
        {
            Debug.LogWarning("创建失败，图片为空！");
            return;
        }

        if (string.IsNullOrEmpty(fontPath))
        {
            Debug.LogWarning("字体路径为空！");
            return;
        }

        if (fontName == null)
        {
            Debug.LogWarning("创建失败，字体名称为空！");
            return;
        }
        else
        {
            if (File.Exists(Application.dataPath + fontPath + fontName + ".fontsettings"))
            {
                Debug.LogError("创建失败，已存在同名字体文件");
                return;
            }

            if (File.Exists(Application.dataPath + fontPath + fontName + ".mat"))
            {
                Debug.LogError("创建失败，已存在同名字体材质文件");
                return;
            }
        }

        string selectionPath = AssetDatabase.GetAssetPath(tex);
        if (selectionPath.Contains("/Resources/"))
        {
            string selectionExt = Path.GetExtension(selectionPath);
            if (selectionExt.Length == 0)
            {
                Debug.LogError("创建失败！");
                return;
            }

            string fontPathName = fontPath + fontName + ".fontsettings";
            string matPathName = fontPath + fontName + ".mat";
            float lineSpace = 0.1f;
            //string loadPath = selectionPath.Remove(selectionPath.Length - selectionExt.Length).Replace("Assets/Resources/", "");
            string loadPath = selectionPath.Replace(selectionExt, "")
                .Substring(selectionPath.IndexOf("/Resources/") + "/Resources/".Length);
            Sprite[] sprites = Resources.LoadAll<Sprite>(loadPath);
            if (sprites.Length > 0)
            {
                Material mat = new Material(Shader.Find("GUI/Text Shader"));
                mat.SetTexture("_MainTex", tex);
                Font m_myFont = new Font();
                m_myFont.material = mat;
                CharacterInfo[] characterInfo = new CharacterInfo[sprites.Length];
                for (int i = 0; i < sprites.Length; i++)
                {
                    if (sprites[i].rect.height > lineSpace)
                    {
                        lineSpace = sprites[i].rect.height;
                    }
                }

                for (int i = 0; i < sprites.Length; i++)
                {
                    Sprite spr = sprites[i];
                    CharacterInfo info = new CharacterInfo();
                    try
                    {
                        info.index = System.Convert.ToInt32(spr.name);
                    }
                    catch
                    {
                        Debug.LogError("创建失败，Sprite名称错误！");
                        return;
                    }

                    Rect rect = spr.rect;
                    float pivot = spr.pivot.y / rect.height - 0.5f;
                    if (pivot > 0)
                    {
                        pivot = -lineSpace / 2 - spr.pivot.y;
                    }
                    else if (pivot < 0)
                    {
                        pivot = -lineSpace / 2 + rect.height - spr.pivot.y;
                    }
                    else
                    {
                        pivot = -lineSpace / 2;
                    }

                    int offsetY = (int)(pivot + (lineSpace - rect.height) / 2);
                    info.uvBottomLeft = new Vector2((float)rect.x / tex.width, (float)(rect.y) / tex.height);
                    info.uvBottomRight = new Vector2((float)(rect.x + rect.width) / tex.width,
                        (float)(rect.y) / tex.height);
                    info.uvTopLeft = new Vector2((float)rect.x / tex.width,
                        (float)(rect.y + rect.height) / tex.height);
                    info.uvTopRight = new Vector2((float)(rect.x + rect.width) / tex.width,
                        (float)(rect.y + rect.height) / tex.height);
                    info.minX = 0;
                    info.minY = -(int)rect.height - offsetY;
                    info.maxX = (int)rect.width;
                    info.maxY = -offsetY;
                    info.advance = (int)rect.width;
                    characterInfo[i] = info;
                }

                AssetDatabase.CreateAsset(mat, "Assets" + matPathName);
                AssetDatabase.CreateAsset(m_myFont, "Assets" + fontPathName);
                m_myFont.characterInfo = characterInfo;
                EditorUtility.SetDirty(m_myFont);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh(); //刷新资源
                Debug.Log("创建字体成功");

            }
            else
            {
                Debug.LogError("图集错误！");
            }
        }
        else
        {
            Debug.LogError("创建失败,选择的图片不在Resources文件夹内！");
        }
    }

    #endregion


    #region 传统模式
    public Texture FontTex;//字体图片
    public TextAsset FntConfig;//字体配置


    private void GeneralModeOnGui()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("字体图片：");
        FontTex = EditorGUILayout.ObjectField(FontTex, typeof(Texture)) as Texture;
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Fnt文件：");
        FntConfig = EditorGUILayout.ObjectField(FntConfig, typeof(TextAsset)) as TextAsset;
        GUILayout.EndHorizontal();

        if (GUILayout.Button("一键生成字体文件"))
        {
            //校验文件
            if (FontTex!=null&& FntConfig!=null)
            {
                //创建
                CreateFont(FontTex, FntConfig);
            }

        }

    }

    private void CreateFont(Texture fontTex,TextAsset fntConfig)
    {
        //修改字体文件类型
        string pathTex = AssetDatabase.GetAssetPath(fontTex);
        Debug.Log("图片路径："+pathTex);
        string pathConfig = AssetDatabase.GetAssetPath(fntConfig);
        Debug.Log("配置路径：" + pathConfig);
        TextureImporter importer=AssetImporter.GetAtPath(pathTex) as TextureImporter;
        importer.textureType = TextureImporterType.Default;
        importer.alphaSource = TextureImporterAlphaSource.FromInput;
        importer.alphaIsTransparency = true;
        importer.sRGBTexture = true;
        importer.SaveAndReimport();
        AssetDatabase.Refresh();

        //字体名称
        string fontName = fntConfig.name;
        int pointIndex = pathConfig.LastIndexOf(".");
        string fontSavePath = pathConfig.Remove(pointIndex, pathConfig.Length - pointIndex);
        //字体文件路径
        string fontPath = fontSavePath + ".fontsettings";
        string matPath = fontSavePath + ".mat";

        Material mat = (Material)AssetDatabase.LoadAssetAtPath(matPath, typeof(Material));
        if (mat == null)
        {
            //创建材质球
            mat = new Material(Shader.Find("GUI/Text Shader"));
            mat.SetTexture("_MainTex", fontTex);
            AssetDatabase.CreateAsset(mat, matPath);
        }
        else
        {
            mat.shader = Shader.Find("GUI/Text Shader");
            mat.SetTexture("_MainTex", fontTex);
        }

        //创建字体文件
        Font font = (Font)AssetDatabase.LoadAssetAtPath(fontPath, typeof(Font));
        if (font == null)
        {
            font = new Font(fontPath);
            AssetDatabase.CreateAsset(font, fontPath);
        }
        //关联文字文件和材质
        font.material = mat;
        Debug.Log("导出路径：" + fontPath);

        //解析字体位置
        AnalysisFntConfig(font, FntConfig);
        AssetDatabase.Refresh();
        EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath(fontPath,typeof(Object)));
    }



    public void AnalysisFntConfig(Font font, TextAsset fntConfig)
    {
        if (font == null || fntConfig == null)
        {
            Debug.LogError("请设置font和textAsset.");
            return;
        }

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(fntConfig.text);


        int totalWidth = Convert.ToInt32(xmlDocument["font"]["common"].Attributes["scaleW"].InnerText);
        int totalHeight = Convert.ToInt32(xmlDocument["font"]["common"].Attributes["scaleH"].InnerText);

        XmlElement xml = xmlDocument["font"]["chars"];
        ArrayList characterInfoList = new ArrayList();


        for (int i = 0; i < xml.ChildNodes.Count; ++i)
        {
            XmlNode node = xml.ChildNodes[i];
            if (node.Attributes == null)
            {
                continue;
            }

            int index = Convert.ToInt32(node.Attributes["id"].InnerText);
            int x = Convert.ToInt32(node.Attributes["x"].InnerText);
            int y = Convert.ToInt32(node.Attributes["y"].InnerText);
            int width = Convert.ToInt32(node.Attributes["width"].InnerText);
            int height = Convert.ToInt32(node.Attributes["height"].InnerText);
            int xOffset = Convert.ToInt32(node.Attributes["xoffset"].InnerText);
            int yOffset = Convert.ToInt32(node.Attributes["yoffset"].InnerText);
            int xAdvance = Convert.ToInt32(node.Attributes["xadvance"].InnerText);
            CharacterInfo info = new CharacterInfo();
            Rect uv = new Rect();
            uv.x = (float)x / totalWidth;
            uv.y = (float)(totalHeight - y - height) / totalHeight;
            uv.width = (float)width / totalWidth;
            uv.height = (float)height / totalHeight;
            info.index = index;
            info.uvBottomLeft = new Vector2(uv.xMin, uv.yMin);
            info.uvBottomRight = new Vector2(uv.xMax, uv.yMin);
            info.uvTopLeft = new Vector2(uv.xMin, uv.yMax);
            info.uvTopRight = new Vector2(uv.xMax, uv.yMax);
            info.minX = xOffset;
            info.maxX = xOffset + width;
            info.minY = -yOffset - height;
            info.maxY = -yOffset;
            info.advance = xAdvance;
            info.glyphWidth = width;
            info.glyphHeight = height;
            characterInfoList.Add(info);
        }

        font.characterInfo = characterInfoList.ToArray(typeof(CharacterInfo)) as CharacterInfo[];
        Debug.Log("生成成功.");
    }


    #endregion


    private void OnGUI()
    {
        switch (_mode)
        {
            case CreateFontMode.General:
                GeneralModeOnGui();
                break;
            case CreateFontMode.Sprite:
                SpriteModeOnGui();
                break;
        }
    
    }






   

}