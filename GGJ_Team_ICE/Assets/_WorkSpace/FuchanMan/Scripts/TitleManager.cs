using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    enum MENU
    {
        START,
        NAVI,
        END,
    }
    MENU menu;
    [SerializeField]
    Boraddata boraddata;
    float boradElapsed = 0f;
    float boradTime = 0f;
    [SerializeField]
    Image[] btnImgs;
    Outline[] outlines;
    Color outlineColor = Color.black;
    bool isNeutral = true;
    float elapsed = 1f;
    int Menu
    {
        set
        {
            elapsed = 1f;
            outlines[(int)menu].effectColor = Color.clear;
            menu = menu + value;
            outlines[(int)menu].effectColor = Color.black;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        menu = MENU.START;
        outlines = new Outline[btnImgs.Length];
        for(int i = 0; i < btnImgs.Length; ++i)
        {
            outlines[i] = btnImgs[i].gameObject.GetComponent<Outline>();
            outlines[i].effectColor = new Color(0, 0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        boradElapsed += Time.deltaTime;
        if (boradElapsed > boradTime)
        {
            boraddata.getOthel[Random.Range(0, 8), Random.Range(0, 8)].reverse();
            boradTime = Random.Range(0.0f, 0.2f);
            boradElapsed = 0f;
        }

        if (SimpleFadeManager.Instance.IsFade)
            return;
        //スティック入力
        float inputy = Input.GetAxis("Vertical");
        elapsed += Time.deltaTime;
        if (inputy > 0.3f)
        {
            if (isNeutral)
            {
                if (menu != MENU.START)
                    Menu = -1;
                isNeutral = false;
            }
        }
        else if(inputy < -0.3f)
        {
            if (isNeutral)
            {
                if (menu != MENU.END)
                    Menu = 1;
                isNeutral = false;
            }
        }
        else
        {
            isNeutral = true;
        }
        //アウトラインのアニメーション
        float alpha = 0f;
        if (elapsed < 1)
        {
            alpha = elapsed / 1;
        }
        else if(elapsed < 2)
        {
            alpha = 1 - elapsed / 2;
        }
        else
        {
            elapsed = 0f;
        }
        outlines[(int)menu].effectColor = new Color(0, 0, 0, alpha);
        //決定
        if (Input.GetButtonDown("Submit"))
        {
            switch(menu)
            {
                case MENU.START:
                    SimpleFadeManager.Instance.FadeSceneChange("TestGame");
                    break;
                case MENU.NAVI:

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
