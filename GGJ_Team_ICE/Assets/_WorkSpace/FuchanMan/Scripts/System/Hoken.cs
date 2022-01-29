using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class othellodate
{
    /// <summary>
    /// コンストラクタ
    /// othell = 自身のオブジェクト
    /// color = 現在の色(true=黒,false=白)
    /// </summary>
    public othellodate(GameObject _othell,bool _color)
    {
        othell = _othell;
        mySr = othell.GetComponent<SpriteRenderer>();
        myAnim = othell.GetComponent<Animator>();
        color = _color;
        col = new Color[2];
        col[0] = new Color(1, 1, 1, 1);
        col[1] = new Color(0, 0, 0, 1);
        mySr.color = col[color ? 1 : 0];
        isAnimEnd = true;
    }
    //自身のオブジェクト
    readonly GameObject othell = null;
    readonly Animator myAnim = null;
    //自身の色(true=黒,false=白)
    bool color;
    public bool Color => color; //自身の色のゲッター
    Color[] col;  //実際の色
    public SpriteRenderer mySr;    //スプライトレンダラー
    //アニメーションが終了しているかどうか
    bool isAnimEnd;
    public bool IsAnimEnd => isAnimEnd; //アニメーション終了フラグのゲッター
    //
    public void Reverse()
    {
        color = !color;
        mySr.color = col[color ? 1 : 0];
        isAnimEnd = false;
    }
    public IEnumerator ReverseAnim()
    {
        if (!myAnim)    //アニメーションの取得に失敗していれば即終了
            yield break;
        myAnim.SetTrigger("Reverse");
        yield return new WaitForSeconds(0.2f);
        isAnimEnd = true;
        yield break;
    }
}

public class Hoken : MonoBehaviour
{
    [SerializeField] GameObject othelloPrefab;
    //オセロデータの配列
    private othellodate[,] othellodates;
    public othellodate[,] Othellodates => othellodates;

    private void Start()
    {
        othellodates = new othellodate[8, 8];
        for(int y = 0; y < 8; ++y)
            for(int x = 0; x < 8; ++x)
            {
                //半分ずつ配置
                GameObject othello = Instantiate(othelloPrefab);
                othello.transform.position = new Vector3(x, -y, 0);
                othellodates[x, y] = new othellodate(othello, y <= 3); 
            }
        //その後シャッフル
        for (int y = 0; y < 8; ++y)
            for (int x = 0; x < 8; ++x)
            {
                if ((x == 0 && y == 0) || (x == 7 && y == 7))
                    continue;    //初期位置だったら処理しない
                Vector2 ranPos = new Vector2(Random.Range(0, 8), Random.Range(0, 8));
                if (ranPos == Vector2.zero || ranPos == new Vector2(7, 7))
                    continue;   //もしも初期位置だったら処理しない
                //入れ替え処理
                othellodates[x, y].Reverse();
                othellodates[(int)ranPos.x, (int)ranPos.y].Reverse();
            }
    }

    /// <summary>
    /// ひっくり返す数を数える
    /// color = 開始地点の色(ひっくり返った後)
    /// x,y = 開始地点
    /// dir = 方向
    /// </summary>
    int ChackReverseCount(bool color, int x, int y, Vector2 dir)
    {
        int size = 0;
        int nx = x, ny = y;
        while(true)
        {
            nx += (int)dir.x;
            ny += (int)dir.y;
            if (nx < 0 || 7 < nx || ny < 0 || 7 < ny)
            {
                return 0;   //盤外ならそもそも挟めてないので0を返し終了
            }
            if (othellodates[nx, ny].Color != color)
            {
                size++; //開始地点の色と異なるならカウントを進め、次を調べる
            }
            else
            {
                return size;    //開始地点と同じ色が来たならこれまでのカウント分を返す
            }
        }
    }

    /// <summary>
    /// ひっくり返す処理
    /// x,y = ひっくり返す座標
    /// </summary>
    public void Reverse(int x, int y)
    {
        othellodates[x, y].Reverse();
        Vector2 dir = new Vector2(-1, -1);  //左上から
        for (int nx = x - 1; nx <= x + 1; ++nx)
        {
            for (int ny = y - 1; ny <= y + 1; ++ny)
            {
                if (nx == x && ny == y)
                {
                    dir.y++;
                    continue;   //自身と同じ座標は処理しない
                }
                if (nx < 0 || 7 < nx || ny < 0 || 7 < ny)
                {
                    dir.y++;
                    continue;   //盤外なら処理しない
                }
                int size = ChackReverseCount(othellodates[x, y].Color, x, y, dir);
                for (int i = 1; i <= size; ++i)
                {
                    othellodates[x + ((int)dir.x * i), y + ((int)dir.y * i)].Reverse();
                }
                dir.y++;
            }
            dir.x++;
            dir.y = -1;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Reverse(4, 4);
        }
    }

}
