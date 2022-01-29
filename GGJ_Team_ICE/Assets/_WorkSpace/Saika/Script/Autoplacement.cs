using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autoplacement : MonoBehaviour
{
    [SerializeField] Othellodata othello;
    [SerializeField] GameObject[] gameboard;
    int[] count= {0,0};
    // Start is called before the first frame update
    private void Start()
    {
        placment();
    }
    public void placment()
    {

        for(int i = 0; i <gameboard.Length;i++)
        {
            Othellodata pack = Instantiate(othello, gameboard[i].transform.position + new Vector3(0, 1, 0), transform.rotation);//¶¬Žž‚Í•
            if (Random.Range(0, 999) % 10 <= 5 && count[1] < gameboard.Length/2)
            {
                pack.reverse();
                count[1]++;//”’
            }
            else
            {
                count[0]++;
            }
        }
    }
}
