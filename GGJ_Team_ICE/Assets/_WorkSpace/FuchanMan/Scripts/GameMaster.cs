using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : SingletonMonoBehaviour<GameMaster>
{
    public enum Rule
    {
        Othello,    //“ü‚ê‘Ö‚¦Ÿ•‰
        Buttle,     //‘Šè‚ğ“ü‚ê‘Ö‚¦‚½•û‚ªŸ‚¿
        Run,        //‹S‚²‚Á‚±
    }
    public Rule rule = Rule.Othello;
}
