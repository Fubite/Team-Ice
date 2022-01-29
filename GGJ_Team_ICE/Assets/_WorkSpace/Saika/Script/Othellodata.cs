using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Othellodata
{
    [Header("true=黒")] public bool frontback;//このオセロの裏表
    GameObject Object;
    SpriteRenderer spriteRenderer;
    public Othellodata(GameObject gameobject,bool boolean)
    {
        this.Object = gameobject;
        this.frontback = boolean;
        spriteRenderer = this.Object.GetComponent<SpriteRenderer>();
    }
    public void reverse()//ひっくり返す関数
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