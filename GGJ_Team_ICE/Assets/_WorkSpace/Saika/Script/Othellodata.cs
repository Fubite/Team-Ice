using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Othellodata :Boraddata
{
    [Header("true=çï")] public bool frontback = true;//Ç±ÇÃÉIÉZÉçÇÃó†ï\
    SpriteRenderer data;
    public int pointX,pointY;
    private void Start()
    {
        data = GetComponent<SpriteRenderer>();
    }
    public void Update()
    {
        frontback = get(pointX, pointY);
    }
    public void reverse()//Ç–Ç¡Ç≠ÇËï‘Ç∑ä÷êî
    {
        data = GetComponent<SpriteRenderer>();
        if (frontback)
        {
            data.color = Color.white;
            omoteura[pointX, pointY] = false;
        }
        else
        {
            data.color = Color.black;
            omoteura[pointX, pointY] = true;
        }
        ReverseAll(pointX, pointY);
    }
    public void instance(int x,int y)
    {
        pointX = x;
        pointY = y;
    }
}
