using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GizmosDrawTools
{

    /// <summary>
    /// 绘制圆圈，支持扇形
    /// </summary>
    /// <param name="center"></param>
    /// <param name="normal"></param>
    /// <param name="from"></param>
    /// <param name="angle"></param>
    /// <param name="radius"></param>
    public static  void DrawWireArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius,Color color)
    {
#if UNITY_EDITOR
        //记录旧颜色
        var oldColor = UnityEditor.Handles.color;        
        UnityEditor.Handles.color = color;        
        UnityEditor.Handles.DrawWireArc(center, normal, from, angle, radius);
        UnityEditor.Handles.color = oldColor;
#endif
    }


    /// <summary>
    /// 绘制扇形区域
    /// </summary>
    /// <param name="center"></param>
    /// <param name="normal"></param>
    /// <param name="from"></param>
    /// <param name="angle"></param>
    /// <param name="radius"></param>
    public static void DrawSolidArc(Vector3 center, Vector3 normal, Vector3 from, float angle, float radius,Color color)
    {
#if UNITY_EDITOR
        var oldColor = UnityEditor.Handles.color;      
        color.a = 0.1f;
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.DrawSolidArc(center, normal, from, angle, radius);
        UnityEditor.Handles.color = oldColor;
#endif
    }

    /// <summary>
    /// 绘制实心圆
    /// </summary>
    public static void DrawLocalSolidCircle(Transform target,float raduis,Color color)
    {
        #if UNITY_EDITOR
        DrawSolidArc(target.position, target.up, target.forward, 360, raduis, color);
        #endif
    }


    /// <summary>
    /// 绘制Labal标签
    /// </summary>
    /// <param name="position"></param>
    /// <param name="txt"></param>
    public static void DrawLabal(Vector3 position,string txt,GUIStyle style= null)
    {
        #if UNITY_EDITOR
        if (style == null)
        {
            UnityEditor.Handles.Label(position, txt);
        }
        else
        {
            UnityEditor.Handles.Label(position, txt, style);
        }
     #endif  
    }

    /// <summary>
    /// 划线
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="color"></param>
    public static void DrawLine(Vector3 p1,Vector3 p2,Color color)
    {
#if UNITY_EDITOR
        var oldColor = UnityEditor.Handles.color;      
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.DrawLine(p1, p2);
        UnityEditor.Handles.color = oldColor;
#endif
    }

    /// <summary>
    /// 绘制凸包
    /// </summary>
    public static void DrawConvexPolygon( Color color,params Vector3[] points)
    {
#if UNITY_EDITOR
        var oldColor = UnityEditor.Handles.color;
        color.a = 0.1f;
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.DrawAAConvexPolygon(points);
        UnityEditor.Handles.color = oldColor;
#endif
    }

}
