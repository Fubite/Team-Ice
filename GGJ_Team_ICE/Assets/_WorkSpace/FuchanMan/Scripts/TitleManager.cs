using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        menu = MENU.START;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit"))
        {
            switch(menu)
            {
                case MENU.START:
                    SimpleFadeManager.Instance.FadeSceneChange("TestGame");
                    //SceneManager.LoadScene("TestGame");
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
