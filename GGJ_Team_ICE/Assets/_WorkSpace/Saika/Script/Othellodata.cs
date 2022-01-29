using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Othellodata :Boraddata
{
    [Header("true=��")] public bool frontback = true;//���̃I�Z���̗��\
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
    public void reverse()//�Ђ�����Ԃ��֐�
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
