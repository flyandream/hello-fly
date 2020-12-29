using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MultiImageToggle : Toggle
{



    private Graphic[] _offGraphics; //关闭时的渲染层
    private Graphic[] _onGraphics; //开启时的渲染层
    

    protected Graphic[] OffGraphics
    {
        get
        {
            if (_offGraphics == null)
            {
                _offGraphics = targetGraphic.GetComponentsInChildren<Graphic>();
            }
            return _offGraphics;
        }
    }

    protected Graphic[] OnGraphics
    {

        get
        {
            if (_onGraphics == null)
            {
                _onGraphics = graphic.GetComponentsInChildren<Graphic>();
            }

            return _onGraphics;
        }
    }


    protected override void OnEnable()
    {
        base.OnEnable();
        onValueChanged.AddListener(OnValueChange);
        RefreshShow();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        onValueChanged.RemoveListener(OnValueChange);        
    }

    private void OnValueChange(bool isOn)
    {
        RefreshShow();
    }


    private void RefreshShow()
    {
        if (this.isOn)
        {
            for (int i = 0; i < OnGraphics.Length; i++)
            {
                OnGraphics[i].enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < OnGraphics.Length; i++)
            {
                OnGraphics[i].enabled = false;
            }
        }
    }
}

