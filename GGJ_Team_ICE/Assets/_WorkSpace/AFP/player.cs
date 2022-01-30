using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField, Header("プレイヤー番号")]
    int p_num;

    [SerializeField, Header("プレイヤーの移動力")]
    float move_power;

    [SerializeField, Header("攻撃した後のクールタイム(フレーム)")]
    int stop_fram;

    [SerializeField, Header("主人公アニメーション")]
    public Animator anim;

    [SerializeField, Header("オセロ2D入れる")]
    public Boraddata board;

    [SerializeField, Header("死亡スプライト")]
    public Sprite deth_img;

    [Header("(public)主人公の操作できるかを管理")]
    public bool move_stop = false;

    [Header("(public)主人公が死ぬとtrueになる")]
    public bool deth = false;

    private Vector3 p_vec = new Vector3(0, 0, 0);           //主人公へ代入用ベクトル
    bool isMove = false; 

    [SerializeField, Header("移動時間")]
    float moveTime = 1;
    float elapsed = 0; //   経過時間
    Vector3 startPos;
    Vector3 endPos;

    private int direction = 1;              //キャラの方向　
    private bool[,] masu = new bool[8, 8];  //ボードの升目のオセロ情報取得よう変数
    public int x = 0, y = 0;                //主人公のボードにおける座標
    private bool masu_check = false;        //キャラの移動先チェック用変数　false = 白　true = 黒
    private bool first = true;              //最初しか動かない
    private bool attck_stop = false;        //移動中攻撃を止める処理
    private int stop_count = 0;             //攻撃した後、一瞬動けないカウント


    // Start is called before the first frame update
    void Start()
    {
        //p1,p2の区別用
        if (p_num == 1)
        {
            anim.SetBool("player", true);
            masu_check = false;
            x = 0;y = 0;
        }
        else
        {
            anim.SetBool("player", false);
            masu_check = true;
            x = 7; y = 7;
        }
        this.gameObject.GetComponent<SpriteRenderer>().sprite = deth_img;
        stop_count = stop_fram;
    }

    private void Move()
    {
        elapsed += Time.deltaTime;
        float t = elapsed / moveTime;

        //補間処理
        transform.position = Vector3.Lerp(startPos, endPos, t);

        attck_stop = true;

        //終了時の初期化
        if (t >= 1)
        {
            transform.position = endPos;
            isMove = false;
            elapsed = 0f;
            attck_stop = false;

            //アニメーション終了
            anim.SetBool("down", false);
            anim.SetBool("up", false);
            anim.SetBool("right", false);
            anim.SetBool("left", false);
        }
    }

    IEnumerator DethEvent()
    {
        Debug.Log("deth");
        SimpleFadeManager.Instance.FadeColor = Color.white;
        SimpleFadeManager.Instance.Fade(0.05f, true);
        yield return new WaitForSeconds(0.05f);
        //  死亡状態に切り替え
        GameObject.Destroy(this.GetComponent<Animator>());
        this.gameObject.GetComponent<SpriteRenderer>().sprite = deth_img;
        //

        SimpleFadeManager.Instance.FadeColor = Color.black;
        yield break;
    }


    // Update is called once per frame
    void Update()
    {
        //数値更新
        //コントローラー入力の絶対値取得
        Vector2 input_abs = new Vector2(Mathf.Abs(Input.GetAxis("HorizontalL_P" + p_num)), Mathf.Abs(Input.GetAxis("VerticalL_P" + p_num)));
        Vector2 input= new Vector2(Input.GetAxis("HorizontalL_P" + p_num), Input.GetAxis("VerticalL_P" + p_num));

        //boardデータの取得
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                masu[i, j] = board.getOthel[i, j].frontback;
            }
        }

        //Debug.Log("" + masu[x, 7 - y]);
        //for (int i = 0; i < 8; i++)
        //{
        //    Debug.Log(i + ":" + board.get(8, 8)[0, i]);
        //}

        //スタート、終了時動かせない
        if (!move_stop)
        {
            //移動処理
            if (!isMove)
            {
                //攻撃時のクールタイム
                if (stop_fram <= stop_count)
                {
                    //上下優先処理
                    if (input_abs.x < input_abs.y)
                    {
                        //上移動処理
                        if (-input.y > 0 && y < 7)
                        {
                            //上に何もない時且つ、自分とは違う色の場合
                            if (y != 7 && masu[x, 7 - y - 1] == masu_check)
                            {
                                startPos = transform.position;
                                endPos = transform.position + new Vector3(0, move_power, 0);
                                isMove = true;
                                anim.SetBool("up", true);
                                y++;
                            }
                            //board.GetComponent<Boraddata>().omoteura[2,2 ] = false;

                            anim.SetFloat("idleDir" + p_num, 0);
                            direction = 0;
                        }
                        //下移動処理
                        else if (-input.y < 0 && y > 0)
                        {
                            //上にが自分とは違う色の場合
                            if (y != 0 && masu[x, 7 - y + 1] == masu_check)
                            {
                                startPos = transform.position;
                                endPos = transform.position + new Vector3(0, -move_power, 0);
                                isMove = true;
                                anim.SetBool("down", true);
                                y--;
                            }
                            anim.SetFloat("idleDir" + p_num, 1);
                            direction = 1;
                        }
                    }
                    //左右優先処理
                    else
                    {
                        //右優先処理
                        if (input.x > 0 && x < 7)
                        {
                            //上にが自分とは違う色の場合
                            if (x != 7 && masu[x + 1, 7 - y] == masu_check)
                            {
                                startPos = transform.position;
                                endPos = transform.position + new Vector3(move_power, 0, 0);
                                isMove = true;
                                anim.SetBool("right", true);
                                x++;
                            }
                            anim.SetFloat("idleDir" + p_num, 2);
                            direction = 2;
                        }
                        //左優先処理
                        else if (input.x < 0 && x > 0)
                        {
                            //上にが自分とは違う色の場合
                            if (x != 0 && masu[x - 1, 7 - y] == masu_check)
                            {
                                startPos = transform.position;
                                endPos = transform.position + new Vector3(-move_power, 0, 0);
                                isMove = true;
                                anim.SetBool("left", true);
                                x--;
                            }
                            anim.SetFloat("idleDir" + p_num, 3);
                            direction = 3;
                        }
                    }
                }
            }
            else
            {
                Move();
            }


            //クールタイム用カウント
            stop_count++;

            if (!attck_stop)
            {
                //ひっくり返す処理
                if (direction == 0)
                {
                    //自分の色と逆の時
                    if (y != 7 && masu[x, 7 - y - 1] != masu_check)
                    {
                        //その色を自分の色に変える
                        if (Input.GetButtonDown("ButtonA_P" + p_num))
                        {
                            board.ReverseAll(x, 7 - y - 1);
                            stop_count = 0;
                        }
                    }
                }
                if (direction == 1)
                {
                    //自分の色と逆の時
                    if (y != 0 && masu[x, 7 - y + 1] != masu_check)
                    {
                        //その色を自分の色に変える
                        if (Input.GetButtonDown("ButtonA_P" + p_num))
                        {
                            board.ReverseAll(x, 7 - y + 1);
                            stop_count = 0;
                        }
                    }
                }
                if (direction == 2)
                {
                    //自分の色と逆の時
                    if (x != 7 && masu[x + 1, 7 - y] != masu_check)
                    {
                        //その色を自分の色に変える
                        if (Input.GetButtonDown("ButtonA_P" + p_num))
                        {
                            board.ReverseAll(x + 1, 7 - y);
                            stop_count = 0;
                        }
                    }
                }
                if (direction == 3)
                {
                    //自分の色と逆の時
                    if (x != 0 && masu[x - 1, 7 - y] != masu_check)
                    {
                        //その色を自分の色に変える
                        if (Input.GetButtonDown("ButtonA_P" + p_num))
                        {
                            board.ReverseAll(x - 1, 7 - y);
                            stop_count = 0;
                        }
                    }
                }
            }
        }
        //死亡処理
        if (deth == true)
        {
            if (first)
            {
                StartCoroutine(DethEvent());
                first = false;
            }
        }
    }
}
