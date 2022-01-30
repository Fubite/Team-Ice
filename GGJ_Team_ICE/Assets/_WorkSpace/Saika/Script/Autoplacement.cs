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
        //placment();
    }
//    public void placment()//é©ìÆîzíu
//    {
//        for(int i = 0; i < 64; i++)
//        {
//            Othellodata pack = Instantiate(othello, gameboard[i].transform.position,transform.rotation);
//            if (i % 8 >= 3)
//            {
//                pack.reverse();
//            }
//            ////ÇVç∂â∫Å@ÇTÇWâEè„
//            //if (i == 7)
//            //{
//            //    pack.reverse();
//            //    count[0]++;
//            //    //othello.set(i / 8, i % 8, false);
//            //    continue;
//            //}
//            //if (i == 58)
//            //{
//            //    count[1]++; 
//            //    //othello.set(i / 8, i % 8,true);
//            //    continue;
//            //}

//        }
//    }
}
