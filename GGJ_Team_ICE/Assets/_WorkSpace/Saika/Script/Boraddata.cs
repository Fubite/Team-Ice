using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boraddata:MonoBehaviour
{
    Othellodata[,] Othello;
    public Othellodata[,] getOthel { get { return Othello; } }
    [SerializeField] GameObject Othelloprefab;
    public Transform[] mass = new Transform[64];//一マスごとの座標
    private void Start()
    {
        Othello = new Othellodata[8, 8];
        for (int i = 0; i < 8; i++)
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

        for (int i = 0; i < 8; i++)
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
    }
    //public bool[,] get(int x, int y)
    //{
    //    return omoteura;
    //}
    //public void set(int x,int y,bool frontback)
    //{
    //    omoteura[x, y] = frontback;
    //    Debug.Log(x+","+y+"("+omoteura[x, y]+")");
    //}
    //private void Update()
    //{
    //    Debug.Log(getcount(true));
    //}

    //void Reverse(int h, int v, int directionH, int directionV)
    //{
    //    //確認する座標x, yを宣言
    //    int x = h + directionH, y = v + directionV;

    //    //挟んでいるか確認してひっくり返す
    //    while (x < 8 && x >= 0 && y < 8 && y >= 0)
    //    {
    //        //自分の駒だった場合
    //        if (omoteura[x, y] == omoteura[h,v])
    //        {
    //            //ひっくり返す
    //            int x2 = h + directionH, y2 = v + directionV;
    //            while (!(x2 == x && y2 == y))
    //            {
    //                omoteura[x2, y2] = omoteura[h,v];//set
    //                x2 += directionH;
    //                y2 += directionV;
    //            }
    //            break;
    //        }
    //    //確認座標を次に進める
    //    x += directionH;
    //    y += directionV;
    //    }
    //}
    //public void ReverseAll(int h, int v)
    //{
    //    Reverse(h, v, 1, 0);  //右方向
    //    Reverse(h, v, -1, 0); //左方向
    //    Reverse(h, v, 0, -1); //上方向
    //    Reverse(h, v, 0, 1);  //下方向
    //    Reverse(h, v, 1, -1); //右上方向
    //    Reverse(h, v, -1, -1);//左上方向
    //    Reverse(h, v, 1, 1);  //右下方向
    //    Reverse(h, v, -1, 1); //左下方向
    //}
    //public int getcount(bool boolean)//入れた引数の数を数える関数　制限時間終了時の集計用
    //{
    //    int x = 0;
    //    for (int i = 0; i < 8; i++)
    //    {
    //        for (int j = 0; j < 8; j++)
    //        {
    //            if (omoteura[i, j] == boolean)
    //            {
    //                x++;
    //            }
    //        }
    //    }
    //    return x;
    //}
}