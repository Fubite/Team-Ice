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

    [Header("��l���̑���ł��邩���Ǘ�")]
    public bool move_stop = false;

    private Vector3 p_vec = new Vector3(0, 0, 0);           //��l���֑���p�x�N�g��
    private Vector3 mem_move_amount = new Vector3(0, 0, 0); //��l���̈ړ��ʋL��
    bool isMove = false; 
    private int move_count = 0;

    [SerializeField, Header("�ړ�����")]
    float moveTime = 1;
    float elapsed = 0; //   �o�ߎ���
    Vector3 startPos;
    Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        
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

            anim.SetBool("down", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //���l�X�V
        //�R���g���[���[���͂̐�Βl�擾
        Vector2 input_abs = new Vector2(Mathf.Abs(Input.GetAxis("HorizontalL_P" + p_num)), Mathf.Abs(Input.GetAxis("VerticalL_P" + p_num)));
        Vector2 input= new Vector2(Input.GetAxis("HorizontalL_P" + p_num), Input.GetAxis("VerticalL_P" + p_num));
        mem_move_amount = this.gameObject.transform.position;
        p_vec = new Vector3(0, 0, 0);

        //�X�^�[�g�A�I�����������Ȃ�
        if (!move_stop)
        {
            //�ړ�����
            if (!isMove)
            {
                //�㉺�D�揈��
                if (input_abs.x < input_abs.y)
                {
                    if (-input.y > 0)//��ړ�����
                    {
                        startPos = transform.position;
                        endPos = transform.position + new Vector3(0, 1, 0);
                        isMove = true;
                    }
                    else if (-input.y < 0)//���ړ�����
                    {
                        startPos = transform.position;
                        endPos = transform.position + new Vector3(0, -1, 0);
                        anim.SetBool("down", true); Debug.Log("bbb");
                        isMove = true;
                    }
                }
                else//���E�D�揈��
                {
                    if (input.x > 0)//�E�D�揈��
                    {
                        startPos = transform.position;
                        endPos = transform.position + new Vector3(1, 0, 0);
                        isMove = true;
                    }
                    else if (input.x < 0)//���D�揈��
                    {
                        startPos = transform.position;
                        endPos = transform.position + new Vector3(-1, 0, 0);
                        isMove = true;
                    }
                }
            }
            else
            {
                Move();
            }
        }
    }
}
