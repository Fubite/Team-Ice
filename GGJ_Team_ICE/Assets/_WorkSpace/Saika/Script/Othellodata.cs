using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Othellodata : MonoBehaviour
{
    [Header("true=çï")] public bool frontback = true;
    public int pointX,pointY;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            reverse();
        }
    }
    public void reverse()
    {
        if (frontback)
        {
            transform.Rotate(new Vector3(180, 0, 0));
            frontback = false;
        }
        else
        {
            transform.Rotate(new Vector3(-180, 0, 0));
            frontback = true;
        }
    }
    public void instance(int x,int y)
    {
        pointX = x;
        pointY = y;
    }
}
