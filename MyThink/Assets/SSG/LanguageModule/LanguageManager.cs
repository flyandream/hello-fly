using System;
using System.Collections.Generic;
using System.Text;
using SSG;
using UnityEngine;


//语言类型
public enum LanguageType
{    
    Chinese=1,//中文
    English=2,//英文
}

/// <summary>
///     语言适配管理器
/// </summary>
public class LanguageManager
{
    //语言类型变化触发
    public event OnValueChangeEventHandle OnLanguageTypeChange;

    private const string PLAYER_PREFS_NAME = "LanguageType";//存档语言类型
    public const string LANGUAGE_PATH = "ConfigTable/Language";//语言适配文件路径
    private const int ROW_OFFSET = 2;//行号偏移量（语言类型读取数据时使用）
    
    private static LanguageManager _instance; //
    private static readonly object _lock = new object();

    private Dictionary<int,LanguageContent> _dicLanguageContents=new Dictionary<int, LanguageContent>();//语言适配信息字典

    private LanguageType _curLanguageType;//当前语言类型
    //当前语言类型
    public LanguageType CurLanguageType
    {
        get { return _curLanguageType; }        
    }

    public static LanguageManager Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {                      
                        _instance=new LanguageManager();
                        _instance.Init();//初始化
                    }
                }
            }
            return _instance;
        }
    }


    #region  外部调用

    //切换语言类型
    public void ChangeLanguageType(LanguageType targetType)
    {
        bool isChange = targetType != _curLanguageType;        
        //保存语言数据
        PlayerPrefs.SetInt(PLAYER_PREFS_NAME,(int)targetType);
        PlayerPrefs.Save();
        //重新执行初始化
        Init();
        if (isChange)
        {
            //如果发生改变，触发语言变化事件
            if (OnLanguageTypeChange!=null)
            {
                OnLanguageTypeChange();
            }
        }
    }
    /// <summary>
    /// 获取语言适配内容
    /// </summary>
    public string GetValue(int key)
    {
        if (_dicLanguageContents.ContainsKey(key))
        {
            return _dicLanguageContents[key].Value;//返回文本值
        }
        return "####none###";//找不到
    }
    /// <summary>
    /// 获取语言适配内容，把%s符号的位置的替换成strs字符串
    /// </summary>
    /// <returns></returns>
    public string GetValueToFormat(int key,params string[] contents)
    {     
        if (_dicLanguageContents.ContainsKey(key))
        {
            return FormatReplace(_dicLanguageContents[key].Value, "%s", contents);            
        }
        return "####none###";//找不到
    }

    #endregion

    //初始化
    private void Init()    
    {
        //读取游戏存档，获取当前的游戏文本
        _curLanguageType = GetCurLanguage();//设置当前的语言类型
        //根据语言类型初始化当前的适配文本
        LoadLanguageData(_curLanguageType);//读取文本数据

    }
    /// <summary>
    /// 读取语言文本数据
    /// </summary>    
    private void LoadLanguageData(LanguageType languageType)
    {
        _dicLanguageContents.Clear();//清空数据字典
        byte[] bytesContent;
        //数据放入resource文件夹下
        try
        {
            bytesContent = Resources.Load<TextAsset>(LANGUAGE_PATH).bytes;

        }
        catch (Exception)
        {

            throw new Exception(LANGUAGE_PATH + "读取失败！检查路径Resources/" + LANGUAGE_PATH);
        }
        var dt = CsvFileReader.ReadAll(new System.IO.MemoryStream(bytesContent), Encoding.UTF8, 1);//以UTF-8的编码读取

        int rowIndex = (int) languageType + ROW_OFFSET;//计算语言类型所对应的行号
        for (int i = 0; i < dt.rows.Count; i++)
        {                    
            LanguageContent content = new LanguageContent(dt.rows[i], rowIndex);
           _dicLanguageContents.Add(content.Key,content);
        }
    }
    //获取当前的语言类型
    private LanguageType GetCurLanguage()
    {
       return (LanguageType) PlayerPrefs.GetInt(PLAYER_PREFS_NAME, (int)LanguageType.English);//默认为英文
    }

    /// <summary>
    /// 匹配字符串，依次替换,匹配项目不足时，返回，替换字符串不足时，返回
    /// </summary>
    /// <returns></returns>
    private string FormatReplace(string originalString, string matchString,params string[] replaceString)
    {
        int matchStringCount = matchString.Length;
        int indexToCheck = 0;
        int indexContent = 0;
        indexToCheck = originalString.IndexOf(matchString, indexToCheck);//匹配的位置
        while (true)
        {
            if (indexToCheck != -1&& indexContent<replaceString.Length)
            {
                //如果有有效替换的内容,重新构造字符串
                originalString = originalString.Remove(indexToCheck, matchStringCount)
                    .Insert(indexToCheck, replaceString[indexContent]); //重新拼接字符串              
                indexToCheck = originalString.IndexOf(matchString, indexToCheck + replaceString[indexContent].Length);//匹配的位置
                indexContent++;
            }
            else
            {
                return originalString;
            }
        }             
    }

    /// <summary>
    /// 语言随系统语言进行设置，如果是简体中文，繁体中文吗，默认设置为中文，其它设置为英文
    /// </summary>
    public void LanugageDependentSystem()
    {
        switch (Application.systemLanguage)
        {
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseSimplified:
                case SystemLanguage.ChineseTraditional:
                ChangeLanguageType(LanguageType.Chinese);
                break;
            default:
                ChangeLanguageType(LanguageType.English);
                break;                
        }
        
    }


    //清空
    public void ClearToNull()
    {
        _instance = null;
    }



}