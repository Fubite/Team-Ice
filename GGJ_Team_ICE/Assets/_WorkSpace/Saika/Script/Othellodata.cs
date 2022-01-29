using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Othellodata : MonoBehaviour
{
    [Header("true=•\")] public bool frontback = true;
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
}
