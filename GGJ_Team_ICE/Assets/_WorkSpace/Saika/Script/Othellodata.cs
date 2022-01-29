using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Othellodata
{
    [Header("true=çï")] public bool frontback;//Ç±ÇÃÉIÉZÉçÇÃó†ï\
    GameObject Object;
    SpriteRenderer spriteRenderer;
    public Othellodata(GameObject gameobject,bool boolean)
    {
        this.Object = gameobject;
        this.frontback = boolean;
        spriteRenderer = this.Object.GetComponent<SpriteRenderer>();
    }
    public void reverse()//Ç–Ç¡Ç≠ÇËï‘Ç∑ä÷êî
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