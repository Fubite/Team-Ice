using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FadeManager : SingletonMonoBehaviour<FadeManager>
{
    [SerializeField, Header("�t�F�[�h�̃J���[")] Color fadeColor = Color.black;
    [SerializeField, Header("�t�F�[�h���鎞��(�����l)")] float fadeTime = 1.0f;
    [SerializeField, Header("�t�F�[�h�ׂ̈�Image")] Image fadeImage;

    public bool IsFade { get { return fadeCoroutine != null; } }

    //�������Ă���R���[�`��
    Coroutine fadeCoroutine;
    Coroutine fadeActionCoroutine;
    private new void Awake()
    {
        base.Awake();
        if (this != Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    /// <summary>
    /// �t�F�[�h�{��:
    /// fadeinTime =  �t�F�[�h�ɂ����鎞��,
    /// isIn =      true �t�F�[�h�C��, false �t�F�[�h�A�E�g
    /// </summary>
    public void Fade(float _fadeTime = -1.0f, bool _isIn = true)
    {
        if (fadeCoroutine != null)
        {
            FadeWarning();
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeCoroutine(_fadeTime, _isIn));
    }

    /// <summary>
    /// �t�F�[�h������ɃA�N�V�������s:
    /// action =    ���s�������A�N�V����,
    /// fadeinTime =  �t�F�[�h�ɂ����鎞��,
    /// isIn =      true �t�F�[�h�C��, false �t�F�[�h�A�E�g
    /// </summary>
    public void FadeAction(System.Action _action, float _fadeTime = -1.0f, bool _isIn = false)
    {
        if (fadeActionCoroutine != null)
        {
            FadeWarning();
            StopCoroutine(fadeActionCoroutine);
        }
        fadeActionCoroutine = StartCoroutine(FadeActionCoroutine(_action, _fadeTime, _isIn));
    }
    /// <summary>
    /// �t�F�[�h���Ȃ���V�[���؂�ւ�:
    /// sceneName =   �؂�ւ���̃V�[��,
    /// fadeinTime =    �t�F�[�h�ɂ����鎞��
    /// </summary>
    public void FadeSceneChange(string _sceneName, float _fadeTime = -1.0f)
    {
        FadeAction(() => StartCoroutine(FadeSceneChangeCoroutine(_sceneName, _fadeTime)), _fadeTime);
    }
    //�t�F�[�h���Ȃ���V�[���؂�ւ�(in out)
    IEnumerator FadeSceneChangeCoroutine(string _sceneName, float _fadeTime = -1.0f)
    {
        yield return SceneManager.LoadSceneAsync(_sceneName);
        Fade(_fadeTime);
    }
    //�t�F�[�h������ɃA�N�V�������s
    IEnumerator FadeActionCoroutine(System.Action _action, float _fadeTime, bool _isIn)
    {
        //�t�F�[�h���Ȃ��~
        if (fadeCoroutine != null)
        {
            FadeWarning();
            StopCoroutine(fadeCoroutine);
        }
        //�t�F�[�h
        fadeCoroutine = StartCoroutine(FadeCoroutine(_fadeTime, _isIn));
        //�t�F�[�h���Ă���̂�҂�
        yield return fadeCoroutine;
        fadeActionCoroutine = null;
        //�A�N�V�������s
        _action?.Invoke();
    }
    //�t�F�[�h�{��
    IEnumerator FadeCoroutine(float _fadeTime, bool _isIn)
    {
        //�t�F�[�h�^�C����0�ȉ��Ȃ珉���ݒ���g��
        if (_fadeTime <= 0)
        {
            _fadeTime = fadeTime;
        }
        //�I���̃J���[��ݒ肷��
        Color endColor = fadeColor;

        float startTime = Time.time, div, rate;
        //isIn��true�Ȃ�t�F�[�h�C��false�Ȃ�A�E�g
        fadeColor.a = System.Convert.ToInt32(_isIn);
        endColor.a = System.Convert.ToInt32(!_isIn);
        //��������t�F�[�h�J���[�ɂ���
        fadeImage.color = fadeColor;
        while (true)
        {
            //�R���[�`���J�n����̎���
            div = Time.time - startTime;
            //0�`1�܂ł̎��Ԍo��
            rate = div / _fadeTime;
            //�t�F�[�h
            fadeImage.color = Color.Lerp(fadeColor, endColor, rate);
            //rate��1�ُ�Ȃ�I��
            if (rate >= 1)
            {
                fadeCoroutine = null;
                yield break;
            }
            yield return null;
        }
    }

    void FadeWarning()
    {
#if UNITY_EDITOR
        Debug.LogWarning("�t�F�[�h���Ƀt�F�[�h���Ăяo���Ȃ��ł��������B");
#endif
    }
}
