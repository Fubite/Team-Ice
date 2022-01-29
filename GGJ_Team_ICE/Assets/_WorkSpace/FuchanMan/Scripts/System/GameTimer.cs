using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer
{
    /// <summary>
    /// 再生フラグ
    /// </summary>
    bool play = false;
    public bool IsPlay => play;

    /// <summary>
    /// 現在時間
    /// </summary>
    float currentTime = 0;
    public float CurrentTime => currentTime; 

    /// <summary>
    /// 経過時間
    /// </summary>
    float deltaTime = 0; 
    public float DeltaTime => deltaTime;

    /// <summary>
    /// タイマー開始
    /// </summary>
    public void Start()
    {
        Reset();    //初期化を行う
        play = true;
    }
    
    /// <summary>
    /// タイマー更新
    /// ※こちらを使用するならFixedUpdateは動かさない
    /// </summary>
    public void Update()
    {
        //再生中でなければ更新しない
        if (!play) return;

        deltaTime = Time.deltaTime;
        currentTime += deltaTime;
    }

    /// <summary>
    /// タイマー更新(FixedUpdate で使用する用)
    /// ※こちらを使用するならUpdateは動かさない
    /// </summary>
    public void FixedUpdate()
    {
        //再生中でなければ更新しない
        if (!play) return;

        deltaTime = Time.fixedDeltaTime;
        currentTime += deltaTime;
    }

    /// <summary>
    /// タイマーリセット
    /// </summary>
    public void Reset() { SetCurrentTime(0); }

    /// <summary>
    /// タイマー設定
    /// </summary>
    /// <param name="t"></param セットする時間>
    public void SetCurrentTime(float t)
    {
        deltaTime = t < currentTime ? 0 : (t - currentTime);
        currentTime = t;
    }

    /// <summary>
    /// タイマー一時停止
    /// </summary>
    public void Pause() { play = false; }

    /// <summary>
    /// タイマー再開
    /// </summary>
    public void Resume() { play = true; }
}
