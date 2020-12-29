using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using  SSG;

/// <summary>
/// 数据管理器
/// </summary>
public class ContentManager 
{
    #region 单例模式
    private static ContentManager _instance = null;
    private static readonly object _lock = new object();
    public static ContentManager Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ContentManager();
                        _instance.Init();//执行初始化
                    }
                }
            }
            return _instance;
        }
    }
    //主动构建
    public void Build()
    {

    }
    private void Init()
    {
        //_dicCarDebrisContents = LoadData<LevelCarDebrisContent>("ConfigTable/LevelCarDebris");
       
    }
    #endregion

    #region 数据字典（只读）
  //  private Dictionary<int, LevelCarDebrisContent> _dicCarDebrisContents = new Dictionary<int, LevelCarDebrisContent>();//每关碎片奖励
  


    #endregion


    #region 数据字典Get属性
  //  public Dictionary<int, LevelCarDebrisContent> DicCarDbrisContents
  //  {
  //      get
  //      {
  //          return _dicCarDebrisContents;
  //      }
  //  }

   

    #endregion



    #region 加载，查找
    //读取数据到字典
    private static Dictionary<int, T> LoadData<T>(string path) where T : BaseContent, new()
    {
        return ReadDateTableToDic<T>(path);
    }
    //数据转化为列表
    public List<T> DataToList<T>(Dictionary<int, T> dic) where T : BaseContent, new()
    {
        return dic.Values.ToList();//返回列表
    }
    //查找数据
    public T FindContent<T>(int id, Dictionary<int, T> dic) where T : BaseContent
    {
        if (dic.ContainsKey(id))
        {
            return dic[id];
        }
        throw new Exception(typeof(T).ToString() + "不存在id:" + id + "的数据条目！");
    }


    #endregion

    #region 读取数据
    //读取数据
    private static List<T> ReadDateTableToList<T>(string path) where T : BaseContent, new()
    {
        //数据放入resource文件夹下
        var bytesContent = Resources.Load<TextAsset>(path).bytes;
        var dt = CsvFileReader.ReadAll(new System.IO.MemoryStream(bytesContent), Encoding.UTF8, 1);//以UTF-8的编码读取
        List<T> listDate = new List<T>();
        for (int i = 0; i < dt.rows.Count; i++)
        {
            T data = new T();
            data.FormatTxtData(dt.rows[i]);
            listDate.Add(data);
        }
        return listDate;
    }

    //读取数据到字典
    private static Dictionary<int, T> ReadDateTableToDic<T>(string path) where T : BaseContent, new()
    {
        byte[] bytesContent;
        //数据放入resource文件夹下
        try
        {
            bytesContent = Resources.Load<TextAsset>(path).bytes;

        }
        catch (Exception)
        {

            throw new Exception(path+"读取失败！检查路径Resources/"+path);
        }
        var dt = CsvFileReader.ReadAll(new System.IO.MemoryStream(bytesContent), Encoding.UTF8, 1);//以UTF-8的编码读取
        Dictionary<int, T> dicDate = new Dictionary<int, T>();
        for (int i = 0; i < dt.rows.Count; i++)
        {
            T data = new T();
            data.FormatTxtData(dt.rows[i]);
            dicDate.Add(data.Id, data);
        }
        return dicDate;
    }
    #endregion

    //清空
    public static void ClearToNull()
    {
        _instance = null;
    }





}

