using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Othellodata :Boraddata
{
    [Header("true=黒")] public bool frontback = true;//このオセロの裏表
    SpriteRenderer data;
    public int pointX,pointY;
    public void reverse()//ひっくり返す関数
    {
        data = GetComponent<SpriteRenderer>();
        if (frontback)
        {
            data.color=Color.white;
            frontback = false;
        }
        else
        {
            data.color = Color.black;
            frontback = true;
        }
        ReverseAll(pointX, pointY);
    }
    public void instance(int x,int y)
    {
        pointX = x;
        pointY = y;
    }
}
