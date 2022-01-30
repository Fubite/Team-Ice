using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boraddata:MonoBehaviour
{
    Othellodata[,] Othello;
    public Othellodata[,] getOthel { get { return Othello; } }//�Q�b�^�[�@��������֐��݂����ȃ��c�B�ǂݎ���p�Ƃ������z�炵��
    [SerializeField] GameObject Othelloprefab;//�I�Z���̃I�u�W�F�N�g
    public Transform[] mass = new Transform[64];//��}�X���Ƃ̍��W
    private void Start()
    {
        Othello = new Othellodata[8, 8];
        for (int i = 0; i < 8; i++)//�I�Z�����{�[�h�ɐ���
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

        for (int i = 0; i < 8; i++)//������A�����_���ȏꏊ�̃I�Z���Ɠ���ւ�
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
    }//�I�Z���̐��𐔂���֐�
    void Reverse(int h, int v, int directionH, int directionV)
    {
        //�m�F������Wx, y��錾
        int x = h + directionH, y = v + directionV;

        //����ł��邩�m�F���ĂЂ�����Ԃ�
        while (x < 8 && x >= 0 && y < 8 && y >= 0)
        {
            //�����̋�����ꍇ
            if (Othello[x, y] == Othello[h, v])
            {
                //�Ђ�����Ԃ�
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
            //�m�F���W�����ɐi�߂�
            x += directionH;
            y += directionV;
        }
    }//�ׂ��Ђ�����Ԃ��I�Z���̃A��
    public void ReverseAll(int h, int v)
    {
        Othello[h, v].reverse();
        Reverse(h, v, 1, 0);  //�E����
        Reverse(h, v, -1, 0); //������
        Reverse(h, v, 0, -1); //�����
        Reverse(h, v, 0, 1);  //������
        Reverse(h, v, 1, -1); //�E�����
        Reverse(h, v, -1, -1);//�������
        Reverse(h, v, 1, 1);  //�E������
        Reverse(h, v, -1, 1); //��������
    }//�ׂ��Ђ�����Ԃ��I�Z���̃A��
}
