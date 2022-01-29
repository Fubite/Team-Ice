using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class othellodate
{
    /// <summary>
    /// �R���X�g���N�^
    /// othell = ���g�̃I�u�W�F�N�g
    /// color = ���݂̐F(true=��,false=��)
    /// </summary>
    public othellodate(GameObject _othell,bool _color)
    {
        othell = _othell;
        mySr = othell.GetComponent<SpriteRenderer>();
        myAnim = othell.GetComponent<Animator>();
        color = _color;
        col = new Color[2];
        col[0] = new Color(1, 1, 1, 1);
        col[1] = new Color(0, 0, 0, 1);
        mySr.color = col[color ? 1 : 0];
        isAnimEnd = true;
    }
    //���g�̃I�u�W�F�N�g
    readonly GameObject othell = null;
    readonly Animator myAnim = null;
    //���g�̐F(true=��,false=��)
    bool color;
    public bool Color => color; //���g�̐F�̃Q�b�^�[
    Color[] col;  //���ۂ̐F
    public SpriteRenderer mySr;    //�X�v���C�g�����_���[
    //�A�j���[�V�������I�����Ă��邩�ǂ���
    bool isAnimEnd;
    public bool IsAnimEnd => isAnimEnd; //�A�j���[�V�����I���t���O�̃Q�b�^�[
    //
    public void Reverse()
    {
        color = !color;
        mySr.color = col[color ? 1 : 0];
        isAnimEnd = false;
    }
    public IEnumerator ReverseAnim()
    {
        if (!myAnim)    //�A�j���[�V�����̎擾�Ɏ��s���Ă���Α��I��
            yield break;
        myAnim.SetTrigger("Reverse");
        yield return new WaitForSeconds(0.2f);
        isAnimEnd = true;
        yield break;
    }
}

public class Hoken : MonoBehaviour
{
    [SerializeField] GameObject othelloPrefab;
    //�I�Z���f�[�^�̔z��
    private othellodate[,] othellodates;
    public othellodate[,] Othellodates => othellodates;

    private void Start()
    {
        othellodates = new othellodate[8, 8];
        for(int y = 0; y < 8; ++y)
            for(int x = 0; x < 8; ++x)
            {
                //�������z�u
                GameObject othello = Instantiate(othelloPrefab);
                othello.transform.position = new Vector3(x, -y, 0);
                othellodates[x, y] = new othellodate(othello, y <= 3); 
            }
        //���̌�V���b�t��
        for (int y = 0; y < 8; ++y)
            for (int x = 0; x < 8; ++x)
            {
                if ((x == 0 && y == 0) || (x == 7 && y == 7))
                    continue;    //�����ʒu�������珈�����Ȃ�
                Vector2 ranPos = new Vector2(Random.Range(0, 8), Random.Range(0, 8));
                if (ranPos == Vector2.zero || ranPos == new Vector2(7, 7))
                    continue;   //�����������ʒu�������珈�����Ȃ�
                //����ւ�����
                othellodates[x, y].Reverse();
                othellodates[(int)ranPos.x, (int)ranPos.y].Reverse();
            }
    }

    /// <summary>
    /// �Ђ�����Ԃ����𐔂���
    /// color = �J�n�n�_�̐F(�Ђ�����Ԃ�����)
    /// x,y = �J�n�n�_
    /// dir = ����
    /// </summary>
    int ChackReverseCount(bool color, int x, int y, Vector2 dir)
    {
        int size = 0;
        int nx = x, ny = y;
        while(true)
        {
            nx += (int)dir.x;
            ny += (int)dir.y;
            if (nx < 0 || 7 < nx || ny < 0 || 7 < ny)
            {
                return 0;   //�ՊO�Ȃ炻���������߂ĂȂ��̂�0��Ԃ��I��
            }
            if (othellodates[nx, ny].Color != color)
            {
                size++; //�J�n�n�_�̐F�ƈقȂ�Ȃ�J�E���g��i�߁A���𒲂ׂ�
            }
            else
            {
                return size;    //�J�n�n�_�Ɠ����F�������Ȃ炱��܂ł̃J�E���g����Ԃ�
            }
        }
    }

    /// <summary>
    /// �Ђ�����Ԃ�����
    /// x,y = �Ђ�����Ԃ����W
    /// </summary>
    public void Reverse(int x, int y)
    {
        othellodates[x, y].Reverse();
        Vector2 dir = new Vector2(-1, -1);  //���ォ��
        for (int nx = x - 1; nx <= x + 1; ++nx)
        {
            for (int ny = y - 1; ny <= y + 1; ++ny)
            {
                if (nx == x && ny == y)
                {
                    dir.y++;
                    continue;   //���g�Ɠ������W�͏������Ȃ�
                }
                if (nx < 0 || 7 < nx || ny < 0 || 7 < ny)
                {
                    dir.y++;
                    continue;   //�ՊO�Ȃ珈�����Ȃ�
                }
                int size = ChackReverseCount(othellodates[x, y].Color, x, y, dir);
                for (int i = 1; i <= size; ++i)
                {
                    othellodates[x + ((int)dir.x * i), y + ((int)dir.y * i)].Reverse();
                }
                dir.y++;
            }
            dir.x++;
            dir.y = -1;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Reverse(4, 4);
        }
    }

}
