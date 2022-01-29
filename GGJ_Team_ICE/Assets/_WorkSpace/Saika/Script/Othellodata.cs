using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Othellodata :Boraddata
{
    [Header("true=黒")] public bool frontback = true;//このオセロの裏表
    SpriteRenderer data;
    public int pointX,pointY;
    private void Start()
    {
        data = GetComponent<SpriteRenderer>();
    }
    public void Update()
    {
        Debug.Log(omoteura[pointX, pointY]);
        if(Input.GetKeyDown(KeyCode.Space)){
            reverse();
        }
    }
    public void reverse()//ひっくり返す関数
    {
        data = GetComponent<SpriteRenderer>();
        if (frontback)
        {
            data.color = Color.white;
            omoteura[pointX, pointY] = false;
            frontback = false;
        }
        else
        {
            data.color = Color.black;
            omoteura[pointX, pointY] = true;
            frontback = true;
        }
        ReverseAll(pointX, pointY);
    }
    public void instance(int x,int y,bool boolean)
    {
        pointX = x;
        pointY = y;
        set(x, y, boolean);
    }
}