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
    Image HowImg = null;
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
            SoundManager.Instance.SePlayer.Play("Select");
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
        if (HowImg)
            HowImg.enabled = false;
        SoundManager.Instance.BgmPlayer.Play("BGM4");
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

        //操作説明画像表示中
        if (HowImg.enabled)
        {
            if (Input.GetButtonDown("Submit"))
            {
                SoundManager.Instance.SePlayer.Play("Decision");
                HowImg.enabled = false;
            }
            return;
        }

        //スティック入力
        float inputy = -Input.GetAxis("Vertical");
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
        if (Input.GetButtonDown("Submit") || Input.GetKeyDown(KeyCode.Space))
        {
            switch(menu)
            {
                case MENU.START:
                    SoundManager.Instance.SePlayer.Play("Decision");
                    SimpleFadeManager.Instance.FadeSceneChange("Game");
                    break;
                case MENU.NAVI:
                    SoundManager.Instance.SePlayer.Play("Decision");
                    HowImg.enabled = true;
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
