using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menber : MonoBehaviour
{
    //�����o�[�̑��R�X�g�ɂ�鏈��
    public AllMenber menberController;
    //���݂̃X���b�g�̏��
    public bool onMenber;
    private Character menber;
    private int nowMemberCount;
    //�X���b�g�̃R�X�g���
    public int menberCost;
    void Start()
    {
        onMenber = false;
        nowMemberCount = 0;
    }


    void Update()
    {
        if (menberChack())
        {
            if (nowMemberCount == 1)//�X���b�g�ɃL�����N�^�[������΂��̃X���b�g�ɃL�����N�^�[�̃R�X�g��ǉ����Čv�Z
            {
                if (!onMenber)
                {
                    menberCost = menber.cost;
                    menberController.GetmenberCost();
                    onMenber = true;
                }
            }
            else if(nowMemberCount == 0)//�X���b�g�ɃL�����N�^�[�����Ȃ���΂��̃X���b�g�̃R�X�g���O�ɂ��Čv�Z
            {
                menberCost = 0;
                menberController.GetmenberCost();
                onMenber = false;
            }
        }
    }

    public bool menberChack()//�����o�[�X���b�g�̎q���m�F����
    {
        if (transform.childCount != nowMemberCount)
        {
            Debug.Log("�ω����܂���");
            nowMemberCount = transform.childCount;
            menber = GetComponentInChildren<Character>();
            return true;
        }
        return false;
    }
}
