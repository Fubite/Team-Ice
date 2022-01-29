using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boraddata:MonoBehaviour
{
    public bool[,] omoteura =new bool[8,8];//���̃}�X�ɂ���I�Z���̗��\
    public Transform[,] mass = new Transform[8,8];//��}�X���Ƃ̍��W
    public bool get(int x,int y)
    {
        return omoteura[x,y];
    }
    public void set(int x,int y,bool frontback)
    {
        this.omoteura[x, y] = frontback;
    }

    void Reverse(int h, int v, int directionH, int directionV)
    {
        //�m�F������Wx, y��錾
        int x = h + directionH, y = v + directionV;

        //����ł��邩�m�F���ĂЂ�����Ԃ�
        while (x < 8 && x >= 0 && y < 8 && y >= 0)
        {
            //�����̋�����ꍇ
            if (omoteura[x, y] == omoteura[h,v])
            {
                //�Ђ�����Ԃ�
                int x2 = h + directionH, y2 = v + directionV;
                while (!(x2 == x && y2 == y))
                {
                    omoteura[x2, y2] = omoteura[h,v];//set
                    x2 += directionH;
                    y2 += directionV;
                }
                break;
            }
        //�m�F���W�����ɐi�߂�
        x += directionH;
        y += directionV;
        }
    }
    public void ReverseAll(int h, int v)
    {
        Reverse(h, v, 1, 0);  //�E����
        Reverse(h, v, -1, 0); //������
        Reverse(h, v, 0, -1); //�����
        Reverse(h, v, 0, 1);  //������
        Reverse(h, v, 1, -1); //�E�����
        Reverse(h, v, -1, -1);//�������
        Reverse(h, v, 1, 1);  //�E������
        Reverse(h, v, -1, 1); //��������
    }
}