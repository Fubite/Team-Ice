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

    [Header("主人公の操作できるかを管理")]
    public bool move_stop = false;

    private Vector3 p_vec = new Vector3(0, 0, 0);           //主人公へ代入用ベクトル
    private Vector3 mem_move_amount = new Vector3(0, 0, 0); //主人公の移動量記憶
    bool isMove = false; 
    private int move_count = 0;

    [SerializeField, Header("移動時間")]
    float moveTime = 1;
    float elapsed = 0; //   経過時間
    Vector3 startPos;
    Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        
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

            anim.SetBool("down", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //数値更新
        //コントローラー入力の絶対値取得
        Vector2 input_abs = new Vector2(Mathf.Abs(Input.GetAxis("HorizontalL_P" + p_num)), Mathf.Abs(Input.GetAxis("VerticalL_P" + p_num)));
        Vector2 input= new Vector2(Input.GetAxis("HorizontalL_P" + p_num), Input.GetAxis("VerticalL_P" + p_num));
        mem_move_amount = this.gameObject.transform.position;
        p_vec = new Vector3(0, 0, 0);

        //スタート、終了時動かせない
        if (!move_stop)
        {
            //移動処理
            if (!isMove)
            {
                //上下優先処理
                if (input_abs.x < input_abs.y)
                {
                    if (-input.y > 0)//上移動処理
                    {
                        startPos = transform.position;
                        endPos = transform.position + new Vector3(0, 1, 0);
                        isMove = true;
                    }
                    else if (-input.y < 0)//下移動処理
                    {
                        startPos = transform.position;
                        endPos = transform.position + new Vector3(0, -1, 0);
                        anim.SetBool("down", true); Debug.Log("bbb");
                        isMove = true;
                    }
                }
                else//左右優先処理
                {
                    if (input.x > 0)//右優先処理
                    {
                        startPos = transform.position;
                        endPos = transform.position + new Vector3(1, 0, 0);
                        isMove = true;
                    }
                    else if (input.x < 0)//左優先処理
                    {
                        startPos = transform.position;
                        endPos = transform.position + new Vector3(-1, 0, 0);
                        isMove = true;
                    }
                }
            }
            else
            {
                Move();
            }
        }
    }
}
