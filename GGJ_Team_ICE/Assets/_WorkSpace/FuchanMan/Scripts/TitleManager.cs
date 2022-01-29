using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//debug
using UnityEngine.SceneManagement;
//
public class TitleManager : MonoBehaviour
{
    enum MENU
    {
        NONE,
        START,
        END,
    }
    MENU menu;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Submit"))
        {
            switch(menu)
            {
                case MENU.START:
                    SceneManager.LoadScene("Game");
                    break;

                case MENU.END:
    #if UNITY_EDITOR
          UnityEditor.EditorApplication.isPlaying = false;
    #elif UNITY_STANDALONE
          UnityEngine.Application.Quit();
    #endif
                    break;
            }
        }
    }
}
