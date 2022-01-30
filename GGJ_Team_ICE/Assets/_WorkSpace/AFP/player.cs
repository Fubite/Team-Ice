using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField, Header("�v���C���[�ԍ�")]
    int p_num;

    [SerializeField, Header("�v���C���[�̈ړ���")]
    float move_power;

    [SerializeField, Header("��l���A�j���[�V����")]
    public Animator anim;

    [SerializeField, Header("�I�Z��2D�����")]
    public Boraddata board;

    [Header("(public)��l���̑���ł��邩���Ǘ�")]
    public bool move_stop = false;

    [Header("(public)��l�������ʂ�true�ɂȂ�")]
    public bool deth = false;

    private Vector3 p_vec = new Vector3(0, 0, 0);           //��l���֑���p�x�N�g��
    bool isMove = false; 

    [SerializeField, Header("�ړ�����")]
    float moveTime = 1;
    float elapsed = 0; //   �o�ߎ���
    Vector3 startPos;
    Vector3 endPos;

    private int direction = 1;              //�L�����̕����@
    private bool[,] masu = new bool[8, 8];  //�{�[�h�̏��ڂ̃I�Z�����擾�悤�ϐ�
    public int x = 0, y = 0;                //��l���̃{�[�h�ɂ�������W
    private bool masu_check = false;        //�L�����̈ړ���`�F�b�N�p�ϐ��@false = ���@true = ��



    // Start is called before the first frame update
    void Start()
    {
        //p1,p2�̋�ʗp
        if (p_num == 1)
        {
            anim.SetBool("player", true);
            masu_check = false;
            x = 0;y = 0;
        }
        else
        {
            anim.SetBool("player", false);
            masu_check = true;
            x = 7; y = 7;
        }
        //board�f�[�^�̎擾
        for(int i=0;i<8;i++)
        {
            for(int j=0;j<8;j++)
            {
                masu[i,j] = board.getOthel[i,j].frontback;
            }
        }
        

        Debug.Log("" + masu[x, 7 - y]);
        for (int i = 0; i < 8; i++)
        {
            //Debug.Log(i + ":" + board.omoteura[0, i]);
        }
    }

    private void Move()
    {
        elapsed += Time.deltaTime;
        float t = elapsed / moveTime;

        //��ԏ���
        transform.position = Vector3.Lerp(startPos, endPos, t);

        //�I�����̏�����
        if(t >= 1)
        {
            transform.position = endPos;
            isMove = false;
            elapsed = 0f;

            //�A�j���[�V�����I��
            anim.SetBool("down", false);
            anim.SetBool("up", false);
            anim.SetBool("right", false);
            anim.SetBool("left", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //���l�X�V
        //�R���g���[���[���͂̐�Βl�擾
        Vector2 input_abs = new Vector2(Mathf.Abs(Input.GetAxis("HorizontalL_P" + p_num)), Mathf.Abs(Input.GetAxis("VerticalL_P" + p_num)));
        Vector2 input= new Vector2(Input.GetAxis("HorizontalL_P" + p_num), Input.GetAxis("VerticalL_P" + p_num));

        //board�f�[�^�̎擾
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                masu[i, j] = board.getOthel[i, j].frontback;
            }
        }

        //Debug.Log("" + masu[x, 7 - y]);
        //for (int i = 0; i < 8; i++)
        //{
        //    Debug.Log(i + ":" + board.get(8, 8)[0, i]);
        //}

        //�X�^�[�g�A�I�����������Ȃ�
        if (!move_stop)
        {
            //�ړ�����
            if (!isMove)
            {
                //�㉺�D�揈��
                if (input_abs.x < input_abs.y)
                {
                    //��ړ�����
                    if (-input.y > 0 && y < 7)
                    {
                        //��ɉ����Ȃ������A�����Ƃ͈Ⴄ�F�̏ꍇ
                        if (y != 7 && masu[x, 7 - y - 1] == masu_check) 
                        {
                            startPos = transform.position;
                            endPos = transform.position + new Vector3(0, move_power, 0);
                            isMove = true;
                            anim.SetBool("up", true);
                            y++;
                        }
                        //board.GetComponent<Boraddata>().omoteura[2,2 ] = false;

                        anim.SetFloat("idleDir" + p_num, 0);
                        direction = 0;
                    }
                    //���ړ�����
                    else if (-input.y < 0 && y > 0)
                    {
                        //��ɂ������Ƃ͈Ⴄ�F�̏ꍇ
                        if (y != 0 && masu[x, 7 - y + 1] == masu_check) 
                        {
                            startPos = transform.position;
                            endPos = transform.position + new Vector3(0, -move_power, 0);
                            isMove = true;
                            anim.SetBool("down", true);
                            y--;
                        }
                        anim.SetFloat("idleDir" + p_num, 1);
                        direction = 1;
                    }
                }
                //���E�D�揈��
                else
                {
                    //�E�D�揈��
                    if (input.x > 0 && x < 7)
                    {
                        //��ɂ������Ƃ͈Ⴄ�F�̏ꍇ
                        if (x != 7 && masu[x + 1, 7 - y] == masu_check) 
                        {
                            startPos = transform.position;
                            endPos = transform.position + new Vector3(move_power, 0, 0);
                            isMove = true;
                            anim.SetBool("right", true);
                            x++;
                        }
                        anim.SetFloat("idleDir" + p_num, 2);
                        direction = 2;
                    }
                    //���D�揈��
                    else if (input.x < 0 && x > 0)
                    {
                        //��ɂ������Ƃ͈Ⴄ�F�̏ꍇ
                        if (x != 0 && masu[x - 1, 7 - y] == masu_check) 
                        {
                            startPos = transform.position;
                            endPos = transform.position + new Vector3(-move_power, 0, 0);
                            isMove = true;
                            anim.SetBool("left", true);
                            x--;
                        }
                        anim.SetFloat("idleDir" + p_num, 3);
                        direction = 3;
                    }
                }
            }
            else
            {
                Move();
            }



            //�Ђ�����Ԃ�����
            if (direction == 0) 
            {
                //�����̐F�Ƌt�̎�
                if (y != 7 && masu[x, 7 - y - 1] != masu_check)
                {
                    //���̐F�������̐F�ɕς���
                    if(Input.GetButtonDown("ButtonA_P"+p_num))
                    {
                        board.ReverseAll(x, 7 - y - 1);
                    }
                }
            }
            if (direction == 1)
            {
                //�����̐F�Ƌt�̎�
                if (y != 0 && masu[x, 7 - y + 1] != masu_check)
                {
                    //���̐F�������̐F�ɕς���
                    if (Input.GetButtonDown("ButtonA_P" + p_num))
                    {
                        board.ReverseAll(x, 7 - y + 1);
                    }
                }
            }
            if (direction == 2)
            {
                //�����̐F�Ƌt�̎�
                if (x != 7 && masu[x + 1, 7 - y] != masu_check)
                {
                    //���̐F�������̐F�ɕς���
                    if (Input.GetButtonDown("ButtonA_P" + p_num))
                    {
                        board.ReverseAll(x + 1, 7 - y);
                    }
                }
            }
            if (direction == 3)
            {
                //�����̐F�Ƌt�̎�
                if (x != 0 && masu[x - 1, 7 - y] != masu_check)
                {
                    //���̐F�������̐F�ɕς���
                    if (Input.GetButtonDown("ButtonA_P" + p_num))
                    {
                        board.ReverseAll(x - 1, 7 - y);
                    }
                }
            }



        }
    }
}
