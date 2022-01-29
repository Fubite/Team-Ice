using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Othellodata : MonoBehaviour
{
<<<<<<< Updated upstream
    [Header("true=•\")] public bool frontback = true;
    private void Update()
=======
    [Header("true=•")] bool frontback = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
>>>>>>> Stashed changes
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            reverse();
<<<<<<< Updated upstream
        }
=======
        }   
>>>>>>> Stashed changes
    }
    public void reverse()
    {
        if (frontback)
        {
<<<<<<< Updated upstream
            transform.Rotate(new Vector3(180, 0, 0));
=======
            transform.Rotate(180, 0, 0);
>>>>>>> Stashed changes
            frontback = false;
        }
        else
        {
<<<<<<< Updated upstream
            transform.Rotate(new Vector3(-180, 0, 0));
=======
            transform.Rotate(-180, 0, 0);
>>>>>>> Stashed changes
            frontback = true;
        }
    }
}
