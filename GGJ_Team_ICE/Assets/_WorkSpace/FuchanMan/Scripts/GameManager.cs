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
    Canvas gameCanvas = null;
    [SerializeField]
    Text txtTime = null;
    [SerializeField]
    Text txtCount = null;
    [SerializeField]
    Canvas resultCanvas = null;
    [SerializeField]
    Sprite[] winSprite = new Sprite[2];
    [SerializeField]
    Image winCharaImg = null;
    //ゲームのステート
    enum STATE
    {
        START,
        GAME,
        END,
        RESULT
    }
    STATE state;

    float elapsed;  //経過時間計測用
    int blackCnt = 0;   //黒の数
    int whiteCnt = 0;   //白の数

    //勝者 true:1pWin false:2pwin
    bool winner = true;

    void Start()
    {
        rule = GameMaster.Instance.rule;
        Ready(rule);
    }


    //ゲーム開始時宣言
    public void Ready(GameMaster.Rule _rule)
    {
        gameCanvas.enabled = true;
        resultCanvas.enabled = false;
        ChangeState(STATE.START);
    }

    //時間切れ
    void TimeUp()
    {
        for(int x = 0; x < 8; ++x)
        {
            for(int y = 0; y < 8; ++y)
            {
                blackCnt++;
                whiteCnt++;
            }
        }
        StartCoroutine(CountUp());
    }

    //個数を数える
    IEnumerator CountUp()
    {
        yield break;
    }

    void CountTextInit()
    {
        txtCount.text = "";
    }

    void ChangeState(STATE _state)
    {
        switch (_state)
        {
            case STATE.START:
                txtCount.text = "3";
                currentTime = finishTime;
                txtTime.text = "Time : " + Mathf.CeilToInt(currentTime);
                break;
            case STATE.GAME:
                txtCount.text = "START!";
                Invoke("CountTextInit", 1);
                break;
            case STATE.END:
                txtCount.text = "FINISH!";
                Invoke("CountTextInit", 2);
                break;
            case STATE.RESULT:
                gameCanvas.enabled = false;
                resultCanvas.enabled = true;
                winCharaImg.sprite = winSprite[0];
                break;
        }
        elapsed = 0f;
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
                if(elapsed > 2f)
                {
                    ChangeState(STATE.RESULT);
                }
                break;
            case STATE.RESULT:
                if(Input.GetButtonDown("Submit"))
                {
                    SimpleFadeManager.Instance.FadeSceneChange("TestTitle");
                }
                break;
        }
    }
}
