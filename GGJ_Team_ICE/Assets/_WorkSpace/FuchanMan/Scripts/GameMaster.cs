using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : SingletonMonoBehaviour<GameMaster>
{
    public enum Rule
    {
        Othello,    //����ւ�����
        Buttle,     //��������ւ�����������
        Run,        //�S������
    }
    public Rule rule = Rule.Othello;
}
