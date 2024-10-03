using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AllMenber : MonoBehaviour
{
    public int allCost;
    public int maximumCost;
    public TMP_Text costText;
    public TMP_Text saveText;
    public List<Menber> menberList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()//���R�X�g�̒l�ɂ���ĕ\�������̐F��ω�������
    {
        costText.text = "Cost:" + allCost;
        if (allCost >= 10)
        {
            costText.color = Color.red;
        }
        else
        {
            costText.color = Color.white;
        }
    }

    public void saveButton()//�{�^�����������ۂ̑��R�X�g�ɂ�鏈��
    {
        if (allCost >= maximumCost)
        {
            saveText.text = "CostOver!";
            saveText.color = Color.red;
        }
        else if (allCost == 0)
        {
            saveText.text = "notMenber";
            saveText.color = Color.red;
        }
        else
        {
            saveText.text = "saveComplete!";
            saveText.color = Color.green;
        }

    }

    public void GetmenberCost()//��x�R�X�g�������������̂����ꂼ��̃����o�[�X���b�g�̃R�X�g�̍��v�𒲂ׂ�
    {
        allCost = 0;
        foreach (var menber in menberList)
        {
            allCost += menber.menberCost;
        }
    }
}
