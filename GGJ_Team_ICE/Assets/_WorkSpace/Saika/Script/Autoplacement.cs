using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autoplacement : MonoBehaviour
{
    [SerializeField] Othellodata othello;
    [SerializeField] GameObject[] gameboard=new GameObject[64];
    int[] count = new int[2];
    // Start is called before the first frame update
    private void Start()
    {
        placment();
    }
    public void placment()
    {
        for(int i = 0; i < gameboard.Length; i++)
        {
            Othellodata pack= Instantiate(othello,gameboard[i].transform.position+new Vector3(0,1,0), transform.rotation);
            pack.instance(i / 8, i % 8);
            //‚V¶‰º@‚T‚W‰Eã
            if (i == 7)
            {
                pack.reverse();
                count[0]++;
                continue;
            }
            if (i == 58)
            {
                count[1]++;
                continue;
            }
            if (Random.Range(0, 999)%3 <= 1&&count[0]<gameboard.Length/2)
            {
                pack.reverse();
                count[0]++;//”’
            }
            else
            {
                count[1]++;
            }
        }
        Debug.Log("•F" + count[1] + "”’F" + count[0]);
    }
}
