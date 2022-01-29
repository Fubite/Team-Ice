using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Autoplacement : MonoBehaviour
{
    [SerializeField] GameObject othello;
    [SerializeField] GameObject[] board=new GameObject[64];
    public float delay = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        placement();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void placement()
    {
        for (int i = 0; i < 64; i++)
        {
            Instantiate(othello, board[i].transform.position + new Vector3(0, 1, 0), transform.rotation);
        }
    }
}
