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
    Text txtCount;

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
        ChangeState(STATE.START);

    }

    void CountTextInit()
    {
        txtCount.text = "";
    }

    void ChangeResult()
    {
        SimpleFadeManager.Instance.FadeSceneChange("TestResult");
    }


    void ChangeState(STATE _state)
    {
        switch (_state)
        {
            case STATE.START:
                stateTxt.text = "START";
                txtCount.text = "3";
                currentTime = finishTime;
                txtTime.text = "Time : " + Mathf.CeilToInt(currentTime);
                elapsed = 0.0f;
                break;
            case STATE.GAME:
                txtCount.text = "START!";
                stateTxt.text = "GAME";
                elapsed = 0f;
                Invoke("CountTextInit", 1);
                break;
            case STATE.END:
                txtCount.text = "FINISH!";
                stateTxt.text = "END";
                Invoke("CountTextInit", 2);
                Invoke("ChangeResult", 2);
                break;
        }
        state = _state;
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        switch(state)
        {
            case STATE.START:
                txtCount.text = (Mathf.FloorToInt(3 - elapsed) + 1).ToString();
                if(elapsed >= 3f)
                {
                    ChangeState(STATE.GAME);
                }
                break;
            case STATE.GAME:
                currentTime -= Time.deltaTime;
                txtTime.text = "Time : " + Mathf.CeilToInt(currentTime);
                if (0 >= currentTime)
                {
                    ChangeState(STATE.END);
                }
                break;
            case STATE.END:

                break;
        }
    }
}
