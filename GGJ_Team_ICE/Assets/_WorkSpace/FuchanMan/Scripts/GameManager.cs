using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField,Header("�{�[�h�f�[�^")]
    Boraddata boradData = null;

    [SerializeField]
    player w_player;    //���v���C���[
    [SerializeField]
    player b_player;    //���v���C���[

    [SerializeField, Header("��������")]
    float finishTime = 60f;
    float currentTime;
    [SerializeField]
    Canvas gameCanvas = null;
    [SerializeField]
    Text txtTime = null;
    [SerializeField]
    Text txtCount = null;
    [SerializeField]
    Image whiteSmallImg = null;
    [SerializeField]
    Image blackSmallImg = null;
    [SerializeField]
    Text whiteTxt = null;
    [SerializeField]
    Text blackTxt = null;
    [SerializeField]
    Image blackImg = null; 
    [SerializeField]
    Text blackSumCntTxt = null;
    [SerializeField]
    Image whiteImg = null;
    [SerializeField]
    Text whiteSumCntTxt = null;
    [SerializeField]
    Canvas resultCanvas = null;
    [SerializeField]
    Sprite[] winSprite = new Sprite[2];
    [SerializeField]
    Image winCharaImg = null;
    [SerializeField]
    Text winText = null;
    //�Q�[���̃X�e�[�g
    enum STATE
    {
        START,
        GAME,
        END,
        RESULT
    }
    STATE state;

    float elapsed;  //�o�ߎ��Ԍv���p
    int blackCnt = 0;   //���̐�
    int whiteCnt = 0;   //���̐�

    //���� 0:�������� 1:1pWin 2:2pwin
    int winnerID = 0;

    void Start()
    {
        Ready();
    }


    //�Q�[���J�n���錾
    public void Ready()
    {
        w_player.move_stop = true;
        b_player.move_stop = true;
        SoundManager.Instance.BgmPlayer.Play("BGM2");
        gameCanvas.enabled = true;
        resultCanvas.enabled = false;
        blackImg.enabled = false;
        whiteImg.enabled = false;
        blackSumCntTxt.text = "";
        whiteSumCntTxt.text = "";
        blackCnt = 0;
        whiteCnt = 0;
        winnerID = 0;
        ChangeState(STATE.START);
    }

    //���Ԑ؂�
    void TimeUp()
    {
        blackCnt = boradData.getcount(true);
        whiteCnt = boradData.getcount(false);
        winnerID = whiteCnt > blackCnt ? 2 : blackCnt > whiteCnt ? 1 : 0; 
        StartCoroutine(CountUp());
    }

    //���𐔂���
    IEnumerator CountUp()
    {
        yield return new WaitForSeconds(2);
        blackImg.enabled = true;
        whiteImg.enabled = true;
        int b_count = 0;
        int w_count = 0;
        while(true)
        {
            if(b_count < blackCnt)
            {
                b_count++;
                blackSumCntTxt.text = b_count.ToString();   
            }
            if(w_count < whiteCnt)
            {
                w_count++;
                whiteSumCntTxt.text = w_count.ToString();
            }
            if(b_count + w_count >= blackCnt + whiteCnt)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(2);
        blackImg.enabled = false;
        whiteImg.enabled = false;
        blackSumCntTxt.text = "";
        whiteSumCntTxt.text = "";
        ChangeState(STATE.RESULT);
        yield break;
    }

    IEnumerator ShowWinChara()
    {
        winCharaImg.sprite = winSprite[winnerID - 1];
        float time = 0f;
        float late = 0.2f;
        while (true)
        {
            time += Time.deltaTime;
            winCharaImg.rectTransform.localScale = new Vector3(0.5f + time / late, 0.5f + time / late, 1f);
            if (late < time)
            {
                winCharaImg.rectTransform.localScale = new Vector3(1.5f, 1.5f, 1);
                time = 0f;
                break;
            }
            yield return null;
        }
        late = 0.1f;
        while(true)
        {
            time += Time.deltaTime;
            winCharaImg.rectTransform.localScale = new Vector3(1.5f - time / (late * 2), 1.5f - time / (late * 2), 1f);
            if (late < time)
            {
                winCharaImg.rectTransform.localScale = Vector3.one;
                break;
            }
            yield return null;
        }
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
                w_player.move_stop = false;
                b_player.move_stop = false;
                SoundManager.Instance.SePlayer.Play("Start(End)");
                txtCount.text = "START!";
                Invoke("CountTextInit", 1);
                break;
            case STATE.END:
                w_player.move_stop = true;
                b_player.move_stop = true;
                SoundManager.Instance.SePlayer.Play("Start(End)");
                SoundManager.Instance.BgmPlayer.Stop();
                txtCount.text = "FINISH!";
                whiteSmallImg.enabled = false;
                blackSmallImg.enabled = false;
                whiteTxt.text = "";
                blackTxt.text = "";
                Invoke("CountTextInit", 2);
                break;
            case STATE.RESULT:
                SoundManager.Instance.BgmPlayer.Play("BGM3");
                gameCanvas.enabled = false;
                resultCanvas.enabled = true;
                if(winnerID > 0)
                {
                    winText.text = "Winner!";
                    StartCoroutine(ShowWinChara());
                }
                else
                {
                    //��������
                    winText.text = "Draw";
                    winCharaImg.color = Color.clear;
                }
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
                whiteTxt.text = boradData.getcount(false).ToString();
                blackTxt.text = boradData.getcount(true).ToString();
                if (0 >= currentTime)
                {
                    TimeUp();
                    ChangeState(STATE.END);
                }
                if (b_player.deth)
                {
                    winnerID = 2;
                    ChangeState(STATE.END);
                }
                else if (w_player.deth)
                {
                    winnerID = 1;
                    ChangeState(STATE.END);
                }
                break;
            case STATE.END:
                if (0 < currentTime)
                {
                    if (elapsed >= 2f)
                    {
                        ChangeState(STATE.RESULT);
                    }
                }
                break;
            case STATE.RESULT:
                if(Input.GetButtonDown("Submit") || Input.GetKeyDown(KeyCode.Space))
                {
                    SimpleFadeManager.Instance.FadeSceneChange("Title");
                }
                break;
        }
    }
}
