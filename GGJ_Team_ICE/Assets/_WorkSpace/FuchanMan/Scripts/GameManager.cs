using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//debug
using UnityEngine.SceneManagement;
//
public class GameManager : MonoBehaviour
{
    [SerializeField]
    Text stateTxt = null;
    enum STATE
    {
        START,
        GAME,
        END,
    }
    STATE state;

    float elapsed;  //経過時間計測用

    void Start()
    {
        Ready();
    }


    //ゲーム開始時宣言
    public void Ready()
    {
        state = STATE.START;
        stateTxt.text = "START";
        elapsed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        switch(state)
        {
            case STATE.START:
                if(elapsed > 2f)
                {
                    state = STATE.GAME;
                    stateTxt.text = "GAME";
                    elapsed = 0f;
                }
                break;
            case STATE.GAME:
                if (elapsed > 2f)
                {
                    state = STATE.END;
                    stateTxt.text = "END";
                    elapsed = 0f;
                }
                break;
            case STATE.END:
                if(Input.GetButton("Submit"))
                {
                    SceneManager.LoadScene("TestResult");   
                }
                break;
        }
    }
}
