using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Othellodata
{
    [Header("true=��")] public bool frontback;//���̃I�Z���̗��\
    GameObject Object;//prefab�̃I�u�W�F�N�g
    public Vector3 objpos { get { return Object.transform.position; } }//�Q�b�^�[�Ƃ������z�@�ڂ�����c#�v���p�e�B�������ł�
    SpriteRenderer spriteRenderer;//�摜�f�[�^
    Animator anim;
    public Othellodata(GameObject gameobject,bool boolean)
    {
        this.Object = gameobject;
        this.frontback = boolean;
        spriteRenderer = this.Object.GetComponent<SpriteRenderer>();
        anim = Object.GetComponent<Animator>();
    }
    public void reverse()//�Ђ�����Ԃ��֐�
    {
        if (frontback)
        {
            frontback = false;
            //spriteRenderer.color = Color.white;
        }
        else
        {
            frontback = true;
            //spriteRenderer.color = Color.black;
        }
        anim.SetTrigger("Reverse");
    }
}