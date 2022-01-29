using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField, Header("プレイヤー番号")]
    int p_num;

    [SerializeField, Header("プレイヤーの移動力")]
    float move_power;

    [SerializeField, Header("主人公アニメーション")]
    public Animator anim;

    [SerializeField, Header("オセロ2D入れる")]
    public GameObject board;

    [Header("主人公の操作できるかを管理")]
    public bool move_stop = false;

    private Vector3 p_vec = new Vector3(0, 0, 0);           //主人公へ代入用ベクトル
    bool isMove = false; 

    [SerializeField, Header("移動時間")]
    float moveTime = 1;
    float elapsed = 0; //   経過時間
    Vector3 startPos;
    Vector3 endPos;

    private int direction = 1;      //キャラの方向　


    private bool[,] masu = new bool[8, 8];

    private int x=0,y = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (p_num == 1)
        {
            anim.SetBool("player", true);
        }
        else
        {
            anim.SetBool("player", false);
        }
    }

    private void Move()
    {
        elapsed += Time.deltaTime;
        float t = elapsed / moveTime;

        //補間処理
        transform.position = Vector3.Lerp(startPos, endPos, t);

        //終了時の初期化
        if(t >= 1)
        {
            transform.position = endPos;
            isMove = false;
            elapsed = 0f;

            //アニメーション終了
            anim.SetBool("down", false);
            anim.SetBool("up", false);
            anim.SetBool("right", false);
            anim.SetBool("left", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //数値更新
        //コントローラー入力の絶対値取得
        Vector2 input_abs = new Vector2(Mathf.Abs(Input.GetAxis("HorizontalL_P" + p_num)), Mathf.Abs(Input.GetAxis("VerticalL_P" + p_num)));
        Vector2 input= new Vector2(Input.GetAxis("HorizontalL_P" + p_num), Input.GetAxis("VerticalL_P" + p_num));

        //boardデータの取得
        masu = board.GetComponent<Boraddata>().bord;

        Debug.Log("" + masu[x, 7 - y]);

        //スタート、終了時動かせない
        if (!move_stop)
        {
            //移動処理
            if (!isMove)
            {
                //上下優先処理
                if (input_abs.x < input_abs.y)
                {
                    if (-input.y > 0 && y < 7)//上移動処理
                    {
                        //上にが自分とは違う色の場合
                        if (masu[x, 7 - y - 1] == true)
                        {
                            startPos = transform.position;
                            endPos = transform.position + new Vector3(0, 1, 0);
                            isMove = true;
                            anim.SetBool("up", true);
                            anim.SetFloat("idleDir" + p_num, 0);
                            direction = 0;
                            y++;
                        }
                        
                    }
                    else if (-input.y < 0 && y > 0)//下移動処理
                    {
                        //上にが自分とは違う色の場合
                        if (masu[x, 7 - y + 1] == true)
                        {
                            startPos = transform.position;
                            endPos = transform.position + new Vector3(0, -1, 0);
                            isMove = true;
                            anim.SetBool("down", true);
                            anim.SetFloat("idleDir" + p_num, 1);
                            direction = 1;
                            y--;
                        }
                    }
                }
                else//左右優先処理
                {
                    if (input.x > 0 && x < 7)//右優先処理
                    {
                        //上にが自分とは違う色の場合
                        if (masu[x - 1, 7 - y] == true)
                        {
                            startPos = transform.position;
                            endPos = transform.position + new Vector3(1, 0, 0);
                            isMove = true;
                            anim.SetBool("right", true);
                            anim.SetFloat("idleDir" + p_num, 2);
                            direction = 2;
                            x++;
                        }
                    }
                    else if (input.x < 0 && x > 0)//左優先処理
                    {
                        //上にが自分とは違う色の場合
                        if (masu[x + 1, 7 - y] == true)
                        {
                            startPos = transform.position;
                            endPos = transform.position + new Vector3(-1, 0, 0);
                            isMove = true;
                            anim.SetBool("left", true);
                            anim.SetFloat("idleDir" + p_num, 3);
                            direction = 3;
                            x--;
                        }
                    }
                }
            }
            else
            {
                Move();
            }



            //ひっくり返す処理
            if(direction==0)
            {

            }


        }
    }
}
