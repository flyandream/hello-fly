using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// UI效果数据，单例
/// </summary>
public   class UIEffectData : BaseSinglton<UIEffectData>
{
       
    #region 常用Material效果
    //置灰材质
    public Material GrayMat;
    //UI默认Shader
    public Shader UIDefaultShader;

    #endregion

    public  override void Init()
    {
        base.Init();       
        DontDestroyOnLoad(gameObject);
        //加载效果资源       
      // GrayMat = UtilsTools.LoadMaterial("SSG_UI/UIImageGrey");
      //  UIDefaultShader = Shader.Find("UI/Default");
    }

    public override void Build()
    {
        base.Build();
    }
}
