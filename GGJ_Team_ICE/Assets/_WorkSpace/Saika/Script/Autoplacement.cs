using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autoplacement : MonoBehaviour
{
    //[SerializeField] Boraddata boraddata;
    [SerializeField] Othellodata othello;
    [SerializeField] GameObject[] gameboard=new GameObject[64];
    int[] count = new int[2];
    // Start is called before the first frame update
    private void Start()
    {
        placment();
    }
    public void placment()//Ž©“®”z’u
    {
        for(int i = 0; i < 64; i++)
        {
            othello.mass[i / 8, i % 8] = gameboard[i].transform;
            Othellodata pack = Instantiate(othello, gameboard[i].transform.position,transform.rotation);
            pack.instance(i / 8, i % 8,true);
            //‚V¶‰º@‚T‚W‰Eã
            if (i == 7)
            {
                pack.reverse();
                count[0]++;
                //othello.set(i / 8, i % 8, false);
                continue;
            }
            if (i == 58)
            {
                count[1]++; 
                //othello.set(i / 8, i % 8,true);
                continue;
            }
            if (Random.Range(0, 999) % 3 <= 1 && count[0] < gameboard.Length / 2)
            {
                pack.reverse();
                pack.frontback = false;
                count[0]++;//”’
                //othello.set(i / 8, i % 8, false);
            }
            else
            {
                count[1]++;
                //othello.set(i / 8, i % 8, true);
            }
            //boraddata.set(i / 8, i % 8, pack.frontback);
        }
    }
}
