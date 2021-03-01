using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class StroeCtrl : Singleton<StroeCtrl>
    {

        public void SavaPro(Prop prop)
        {
            StartModel.Instance.Add(prop);


        }

        public Prop GetProp(int id)
        {
            return StartModel.Instance.propDic[id];
        }

    }

