using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boraddata:MonoBehaviour
{
    public bool[,] bord =new bool[8,8];
    public Transform[,] mass = new Transform[8,8];
    public bool get(int x,int y)
    {
        return bord[x,y];
    }
    public void set(int x,int y,bool frontback)
    {
        bord[x, y] = frontback;
    }

    //1方向にひっくり返す
    void Reverse(int h, int v, int directionH, int directionV)
    {
        //確認する座標x, yを宣言
        int x = h + directionH, y = v + directionV;

        //挟んでいるか確認してひっくり返す
        while (x < 8 && x >= 0 && y < 8 && y >= 0)
        {
            //自分の駒だった場合
            if (bord[x, y] == bord[h,v])
            {
                //ひっくり返す
                int x2 = h + directionH, y2 = v + directionV;
                while (!(x2 == x && y2 == y))
                {
                    bord[x2, y2] = bord[h,v];//set
                    x2 += directionH;
                    y2 += directionV;
                }
                break;
            }
        //確認座標を次に進める
        x += directionH;
        y += directionV;
        }
    }
    //全方向にひっくり返す
    public void ReverseAll(int h, int v)
    {
        Reverse(h, v, 1, 0);  //右方向
        Reverse(h, v, -1, 0); //左方向
        Reverse(h, v, 0, -1); //上方向
        Reverse(h, v, 0, 1);  //下方向
        Reverse(h, v, 1, -1); //右上方向
        Reverse(h, v, -1, -1);//左上方向
        Reverse(h, v, 1, 1);  //右下方向
        Reverse(h, v, -1, 1); //左下方向
    }
}