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
    void Update()//総コストの値によって表示文字の色を変化させる
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

    public void saveButton()//ボタンを押した際の総コストによる処理
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

    public void GetmenberCost()//一度コストを初期化したのちそれぞれのメンバースロットのコストの合計を調べる
    {
        allCost = 0;
        foreach (var menber in menberList)
        {
            allCost += menber.menberCost;
        }
    }
}
