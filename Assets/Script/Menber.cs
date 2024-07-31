using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menber : MonoBehaviour
{
    public AllMenber menberController;
    public bool onMenber;
    private Character menber;
    private int nowMemberCount;
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
            if (nowMemberCount == 1)
            {
                if (!onMenber)
                {
                    menberCost = menber.cost;
                    menberController.GetmenberCost();
                    onMenber = true;
                }
            }
            else if(nowMemberCount == 0)
            {
                menberCost = 0;
                menberController.GetmenberCost();
                onMenber = false;
            }
        }
    }

    public bool menberChack()
    {
        if (transform.childCount != nowMemberCount)
        {
            Debug.Log("•Ï‰»‚µ‚Ü‚µ‚½");
            nowMemberCount = transform.childCount;
            menber = GetComponentInChildren<Character>();
            return true;
        }
        return false;
    }
}
