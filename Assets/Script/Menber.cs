using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menber : MonoBehaviour
{
    //メンバーの総コストによる処理
    public AllMenber menberController;
    //現在のスロットの状態
    public bool onMenber;
    private Character menber;
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
        if (menberChack())
        {
            if (nowMemberCount == 1)//スロットにキャラクターがいればこのスロットにキャラクターのコストを追加して計算
            {
                if (!onMenber)
                {
                    menberCost = menber.cost;
                    menberController.GetmenberCost();
                    onMenber = true;
                }
            }
            else if(nowMemberCount == 0)//スロットにキャラクターがいなければこのスロットのコストを０にして計算
            {
                menberCost = 0;
                menberController.GetmenberCost();
                onMenber = false;
            }
        }
    }

    public bool menberChack()//メンバースロットの子を確認する
    {
        if (transform.childCount != nowMemberCount)
        {
            Debug.Log("変化しました");
            nowMemberCount = transform.childCount;
            menber = GetComponentInChildren<Character>();
            return true;
        }
        return false;
    }
}
