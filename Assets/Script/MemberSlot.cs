using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberSlot : MonoBehaviour
{
    //メンバーの総コストによる処理
    public AllMenber menberController;
    //現在のスロットの状態
    public bool onMenber;
    public Character menber;
    //キャラクターの入れ替え確認UI
    private GameObject chengeUI;
    private int nowMemberCount;
    //スロットのコスト状態
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

    public void menberChack()//メンバースロットの子を確認する
    {
        Debug.Log("変化しました");
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
