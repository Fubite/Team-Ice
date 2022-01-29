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
    public GameObject board;

    [Header("��l���̑���ł��邩���Ǘ�")]
    public bool move_stop = false;

    private Vector3 p_vec = new Vector3(0, 0, 0);           //��l���֑���p�x�N�g��
    bool isMove = false; 

    [SerializeField, Header("�ړ�����")]
    float moveTime = 1;
    float elapsed = 0; //   �o�ߎ���
    Vector3 startPos;
    Vector3 endPos;

    private int direction = 1;      //�L�����̕����@


    private bool[,] masu = new bool[8, 8];

    private int x=0,y = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (p_num == 1)
        {
            anim.SetBool("player", true);
        }
        else
        {
            anim.SetBool("player", false);
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
        masu = board.GetComponent<Boraddata>().bord;

        Debug.Log("" + masu[x, 7 - y]);

        //�X�^�[�g�A�I�����������Ȃ�
        if (!move_stop)
        {
            //�ړ�����
            if (!isMove)
            {
                //�㉺�D�揈��
                if (input_abs.x < input_abs.y)
                {
                    if (-input.y > 0 && y < 7)//��ړ�����
                    {
                        //��ɂ������Ƃ͈Ⴄ�F�̏ꍇ
                        if (masu[x, 7 - y - 1] == true)
                        {
                            startPos = transform.position;
                            endPos = transform.position + new Vector3(0, 1, 0);
                            isMove = true;
                            anim.SetBool("up", true);
                            anim.SetFloat("idleDir" + p_num, 0);
                            direction = 0;
                            y++;
                        }
                        
                    }
                    else if (-input.y < 0 && y > 0)//���ړ�����
                    {
                        //��ɂ������Ƃ͈Ⴄ�F�̏ꍇ
                        if (masu[x, 7 - y + 1] == true)
                        {
                            startPos = transform.position;
                            endPos = transform.position + new Vector3(0, -1, 0);
                            isMove = true;
                            anim.SetBool("down", true);
                            anim.SetFloat("idleDir" + p_num, 1);
                            direction = 1;
                            y--;
                        }
                    }
                }
                else//���E�D�揈��
                {
                    if (input.x > 0 && x < 7)//�E�D�揈��
                    {
                        //��ɂ������Ƃ͈Ⴄ�F�̏ꍇ
                        if (masu[x - 1, 7 - y] == true)
                        {
                            startPos = transform.position;
                            endPos = transform.position + new Vector3(1, 0, 0);
                            isMove = true;
                            anim.SetBool("right", true);
                            anim.SetFloat("idleDir" + p_num, 2);
                            direction = 2;
                            x++;
                        }
                    }
                    else if (input.x < 0 && x > 0)//���D�揈��
                    {
                        //��ɂ������Ƃ͈Ⴄ�F�̏ꍇ
                        if (masu[x + 1, 7 - y] == true)
                        {
                            startPos = transform.position;
                            endPos = transform.position + new Vector3(-1, 0, 0);
                            isMove = true;
                            anim.SetBool("left", true);
                            anim.SetFloat("idleDir" + p_num, 3);
                            direction = 3;
                            x--;
                        }
                    }
                }
            }
            else
            {
                Move();
            }



            //�Ђ�����Ԃ�����
            if(direction==0)
            {

            }


        }
    }
}
