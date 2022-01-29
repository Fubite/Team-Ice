using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer
{
    /// <summary>
    /// �Đ��t���O
    /// </summary>
    bool play = false;
    public bool IsPlay => play;

    /// <summary>
    /// ���ݎ���
    /// </summary>
    float currentTime = 0;
    public float CurrentTime => currentTime; 

    /// <summary>
    /// �o�ߎ���
    /// </summary>
    float deltaTime = 0; 
    public float DeltaTime => deltaTime;

    /// <summary>
    /// �^�C�}�[�J�n
    /// </summary>
    public void Start()
    {
        Reset();    //���������s��
        play = true;
    }
    
    /// <summary>
    /// �^�C�}�[�X�V
    /// ����������g�p����Ȃ�FixedUpdate�͓������Ȃ�
    /// </summary>
    public void Update()
    {
        //�Đ����łȂ���΍X�V���Ȃ�
        if (!play) return;

        deltaTime = Time.deltaTime;
        currentTime += deltaTime;
    }

    /// <summary>
    /// �^�C�}�[�X�V(FixedUpdate �Ŏg�p����p)
    /// ����������g�p����Ȃ�Update�͓������Ȃ�
    /// </summary>
    public void FixedUpdate()
    {
        //�Đ����łȂ���΍X�V���Ȃ�
        if (!play) return;

        deltaTime = Time.fixedDeltaTime;
        currentTime += deltaTime;
    }

    /// <summary>
    /// �^�C�}�[���Z�b�g
    /// </summary>
    public void Reset() { SetCurrentTime(0); }

    /// <summary>
    /// �^�C�}�[�ݒ�
    /// </summary>
    /// <param name="t"></param �Z�b�g���鎞��>
    public void SetCurrentTime(float t)
    {
        deltaTime = t < currentTime ? 0 : (t - currentTime);
        currentTime = t;
    }

    /// <summary>
    /// �^�C�}�[�ꎞ��~
    /// </summary>
    public void Pause() { play = false; }

    /// <summary>
    /// �^�C�}�[�ĊJ
    /// </summary>
    public void Resume() { play = true; }
}
