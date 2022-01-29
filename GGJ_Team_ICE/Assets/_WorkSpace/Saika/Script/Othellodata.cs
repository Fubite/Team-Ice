using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Othellodata
{
    [Header("true=��")] public bool frontback;//���̃I�Z���̗��\
    GameObject Object;
    SpriteRenderer spriteRenderer;
    public Othellodata(GameObject gameobject,bool boolean)
    {
        this.Object = gameobject;
        this.frontback = boolean;
        spriteRenderer = this.Object.GetComponent<SpriteRenderer>();
    }
    public void reverse()//�Ђ�����Ԃ��֐�
    {
        if (frontback)
        {
            frontback = false;
            spriteRenderer.color = Color.white;
        }
        else
        {
            frontback = true;
            spriteRenderer.color = Color.black;
        }
    }
}