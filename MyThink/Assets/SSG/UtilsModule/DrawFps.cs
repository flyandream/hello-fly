using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 绘制帧率
/// </summary>
public class DrawFps : BaseSinglton<DrawFps> {
        [SerializeField]
        private float fpsByDeltatime = 1.5f;
        private float passedTime = 0.0f;
        private int frameCount = 0;
        private float realtimeFPS = 0.0f;
        private GUIStyle _guiStyle;

        public override void Init()
        {
            base.Init();
        DontDestroyOnLoad(gameObject);
        _guiStyle=new GUIStyle();
        _guiStyle.fontSize = 30;
        _guiStyle.normal.textColor = Color.yellow;
    }

        public override void Build()
        {
            base.Build();
        }

        void Update()
        {
            GetFPS();
        }
        private void GetFPS()
        {
            //第二种方式
            frameCount++;
            passedTime += Time.deltaTime;
            if (passedTime >= fpsByDeltatime)
            {
                realtimeFPS = frameCount / passedTime;
                passedTime = 0.0f;
                frameCount = 0;
            }
        }


    private void OnGUI()
    {
        Color oldColor = GUI.contentColor;
        GUI.contentColor=Color.green;
        GUI.Label(new Rect(Screen.width-100,5,100,40), realtimeFPS.ToString("f1"), _guiStyle);
    }

}
