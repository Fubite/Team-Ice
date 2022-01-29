using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autoplacement : MonoBehaviour
{
    [SerializeField] Othellodata othello;
<<<<<<< Updated upstream
    [SerializeField] GameObject[] gameboard=new GameObject[64];
    int[] count = new int[2];
=======
    [SerializeField] GameObject[] gameboard;
    int[] count= {0,0};
>>>>>>> Stashed changes
    // Start is called before the first frame update
    private void Start()
    {
        placment();
    }
<<<<<<< Updated upstream
    public void placment()
    {
        for(int i = 0; i < gameboard.Length; i++)
        {
            Othellodata pack= Instantiate(othello,gameboard[i].transform.position+new Vector3(0,1,0), transform.rotation);
            if (Random.Range(0, 9)%2 == 0&&count[0]<gameboard.Length/2)
            {
                pack.reverse();
=======
    private void Update()
    {
        Debug.Log("•F" + count[0] + "”’F" + count[1]);

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
>>>>>>> Stashed changes
                count[0]++;
            }
        }
    }
}
