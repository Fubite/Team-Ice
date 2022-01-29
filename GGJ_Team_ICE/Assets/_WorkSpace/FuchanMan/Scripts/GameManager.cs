using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //ゲームのルール
    GameMaster.Rule rule;

    [SerializeField, Header("制限時間")]
    float finishTime = 60f;
    float currentTime;
    [SerializeField]
    Text txtTime;

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
        rule = GameMaster.Instance.rule;
        Ready(rule);
    }


    //ゲーム開始時宣言
    public void Ready(GameMaster.Rule _rule)
    {
        state = STATE.START;
        stateTxt.text = "START";
        currentTime = finishTime;
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
                currentTime -= Time.deltaTime;
                if (0 >= currentTime)
                {
                    state = STATE.END;
                    stateTxt.text = "END";
                    elapsed = 0f;
                }
                break;
            case STATE.END:
                if(Input.GetButton("Submit"))
                {
                    SimpleFadeManager.Instance.FadeSceneChange("TestResult");   
                }
                break;
        }
    }
}
