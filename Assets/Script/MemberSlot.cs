using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberSlot : MonoBehaviour
{
    //�����o�[�̑��R�X�g�ɂ�鏈��
    public AllMenber menberController;
    //���݂̃X���b�g�̏��
    public bool onMenber;
    public Character menber;
    //�L�����N�^�[�̓���ւ��m�FUI
    private GameObject chengeUI;
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
        if (transform.childCount != nowMemberCount)
        {
            menberChack();
        }
    }

    public void menberChack()//�����o�[�X���b�g�̎q���m�F����
    {
        Debug.Log("�ω����܂���");
        nowMemberCount = transform.childCount;
        menber = GetComponentInChildren<Character>();

        if (nowMemberCount == 0)
        {
            menberCost = 0;
            menberController.GetmenberCost();
            onMenber = false;
        }
        else
        {
            menberCost = menber.cost;
            menberController.GetmenberCost();
            onMenber = true;
        }
    }
}
