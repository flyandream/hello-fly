using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Test : MonoBehaviour
{
   

    private void Start()
    {
        StatManager.instance.Add(new UMengStat());
    }

    private void Update()
    {
        
    }
    //射线检测碰撞
    void RayTest()
    {

        Ray ray = new Ray(transform.position, transform.forward * 100);//定义一个射线对象,包含射线发射的位置transform.position，发射距离transform.forward*100；
        Debug.DrawLine(transform.position,transform.position+transform.forward*100,Color.red);
        RaycastHit hitInfo;   //定义一个RaycastHit变量用来保存被撞物体的信息；
        if (Physics.Raycast(ray,out hitInfo, 100)) //如果碰撞到了物体，hitInfo里面就包含该物体的相关信息；
        {
            //hitInfo.point:碰撞点的位置；
            //hitInfo.normal:与碰撞点所在平面垂直的向量；
            //hitInfo.collider.gameobject:可以得到该物体上的所有信息了；
        }

    }

}


