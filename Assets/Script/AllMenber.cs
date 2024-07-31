using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AllMenber : MonoBehaviour
{
    public int allCost;
    public TMP_Text costText;
    public List<Menber> menberList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        costText.text = "Cost:" + allCost;
    }

    public void GetmenberCost()
    {
        allCost = 0;
        foreach (var menber in menberList)
        {
            allCost += menber.menberCost;
        }
    }
}
