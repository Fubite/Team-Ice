using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ResultManager : MonoBehaviour
{
    bool winner = true;
    // Start is called before the first frame update
    void Start()
    {
        winner = GameMaster.Instance.winner;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Submit"))
        {
            SimpleFadeManager.Instance.FadeSceneChange("TestTitle");
        }
    }
}
