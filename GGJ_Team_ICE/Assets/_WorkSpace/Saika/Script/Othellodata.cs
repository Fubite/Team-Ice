using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Othellodata : MonoBehaviour
{
    [Header("true=çï")] bool frontback = true;
    // Update is called once per frame
    void Update()
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
            transform.Rotate(180, 0, 0);
            frontback = false;
        }
        else
        {
            transform.Rotate(-180, 0, 0);
            frontback = true;
        }
    }
}
