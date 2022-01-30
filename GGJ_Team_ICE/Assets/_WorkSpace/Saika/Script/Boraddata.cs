using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boraddata:MonoBehaviour
{
    Othellodata[,] Othello;
    public Othellodata[,] getOthel { get { return Othello; } }//ゲッター　ざっくり関数みたいなヤツ。読み取り専用とかいう奴らしい
    [SerializeField] GameObject Othelloprefab;//オセロのオブジェクト
    public Transform[] mass = new Transform[64];//一マスごとの座標
    private void Start()
    {
        Othello = new Othellodata[8, 8];
        for (int i = 0; i < 8; i++)//オセロをボードに生成
        {
            for (int j = 0; j < 8; j++)
            {
                GameObject obj = Instantiate(Othelloprefab, mass[i*8+j].position,Quaternion.identity);
                Othello[i, j] = new Othellodata(obj, true);
                if (j > 3)
                {
                    Othello[i, j].reverse();
                }
            }
        }

        for (int i = 0; i < 8; i++)//生成後、ランダムな場所のオセロと入れ替え
        {
            for (int j = 0; j < 8; j++)
            {
                if (i == 0 && j == 7 || i == 7 && j == 0)
                {
                    continue;
                }
                int x=Random.Range(0,7), y=Random.Range(0,7);
                if(x == 0 && y == 7 || x == 7 && y == 0)
                {
                    continue;
                }
                if (Othello[i, j].frontback != Othello[x, y].frontback)
                {
                    Othello[i, j].reverse();
                    Othello[x, y].reverse();
                }
            }
        }
    }
    public int getcount(bool boolean)
    {
        int x=0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (Othello[i, j].frontback == boolean)
                {
                    x++;
                }
            }
        }
        return x;
    }//オセロの数を数える関数
    void Reverse(int h, int v, int directionH, int directionV)
    {
        //確認する座標x, yを宣言
        int x = h + directionH, y = v + directionV;

        //挟んでいるか確認してひっくり返す
        while (x < 8 && x >= 0 && y < 8 && y >= 0)
        {
            //自分の駒だった場合
            if (Othello[x, y] == Othello[h, v])
            {
                //ひっくり返す
                int x2 = h + directionH, y2 = v + directionV;
                while (!(x2 == x && y2 == y))
                {
                    //Othello[x2, y2] = Othello[h, v];//set
                    Othello[x2, y2].reverse();
                    x2 += directionH;
                    y2 += directionV;
                }
                break;
            }
            //確認座標を次に進める
            x += directionH;
            y += directionV;
        }
    }//隣をひっくり返すオセロのアレ
    public void ReverseAll(int h, int v)
    {
        Othello[h, v].reverse();
        Reverse(h, v, 1, 0);  //右方向
        Reverse(h, v, -1, 0); //左方向
        Reverse(h, v, 0, -1); //上方向
        Reverse(h, v, 0, 1);  //下方向
        Reverse(h, v, 1, -1); //右上方向
        Reverse(h, v, -1, -1);//左上方向
        Reverse(h, v, 1, 1);  //右下方向
        Reverse(h, v, -1, 1); //左下方向
    }//隣をひっくり返すオセロのアレ
}
