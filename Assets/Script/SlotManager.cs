
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public enum numberSort
    {
        minNumber,
        maxNumber,
    }
    public enum sortGrop
    {
        minHp,
        maxHp,
        minCost,
        maxCost
    }

    [SerializeField] private numberSort _number;
    [SerializeField] private sortGrop _sortGrop;
    [SerializeField] private List<CharacterData> characterList;
    [SerializeField] private List<CharacterData> myDeck;
    [SerializeField] private List<Transform> firstSlotPosition;
    [SerializeField] private List<Transform> slotsPosition;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject slotPositionPrefab;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject sortMap;
    [SerializeField] private ToggleGroup numberToggleGrop;
    [SerializeField] private ToggleGroup sortToggleGrop;
    private float nextPage = -285;
    private int maxSlots = 255;

    public bool newGet;
    public bool manyHp;
    public bool manyCost;


    void Start()
    {
        sortMap.SetActive(false);
        for (int i = 0; i < maxSlots; i++) 
        {
            int characterNuber =UnityEngine.Random.Range(0, characterList.Count);
            CharacterData character = ScriptableObject.CreateInstance<CharacterData>();
            character._cost = characterList[characterNuber]._cost;
            character._hp = characterList[characterNuber]._hp;
            character._image = characterList[characterNuber]._image;
            character._number = i;
            myDeck.Add(character);
        }
        ListNumberSort();
        SetSlot();
    }

    public void SetSlot()
    {
        ClearSlot();
        for (int i = 0; i < myDeck.Count; i++)
        {
            GameObject characterSlot = Instantiate(slotPrefab, slotsPosition[i]);
            characterSlot.GetComponentInChildren<Character>()._data = myDeck[i];
            nextSlot(slotsPosition[i]);
        }
    }

    public void ListNumberSort()//入手順に並び替える
    {
        switch (_number)
        {
            case numberSort.minNumber:
               myDeck = myDeck.OrderBy(character => character._number).ToList();   // 入手順に昇順でソート
                break;
            case numberSort.maxNumber:
                myDeck = myDeck.OrderByDescending(character => character._number).ToList(); // 入手順に降順でソート
                break;
        }
    }

    public void nextSlot(Transform slotposition)//次のスロットポジションを作る
    {
        GameObject newSlot = Instantiate(slotPositionPrefab, content);
        newSlot.transform.position = new Vector3(slotposition.transform.position.x, 
                                                                      slotposition.transform.position.y + nextPage, 
                                                                      slotposition.transform.position.z);
        slotsPosition.Add(newSlot.transform);
    }

    public void ClearSlot()//スロット表示一度初期化する
    {
        for (int i = 0;i < slotsPosition.Count;i++)
        {
            if (i <= firstSlotPosition.Count)
            {
                foreach (Transform child in slotsPosition[i])
                {
                    Destroy(child.gameObject);
                }
            }
            else
            {
                Destroy(slotsPosition[i].gameObject);
            }
        }
        slotsPosition.Clear();
        slotsPosition = new List<Transform>(firstSlotPosition);
    }

    public void OpenSortMapButton()
    {
        sortMap.SetActive(true);
    }

    public void SrotButton()
    {
        string numberLabel = numberToggleGrop.ActiveToggles()
            .First(t => t.name == "Label").GetComponentInChildren<Text>().text;
        string sortLabel = sortToggleGrop.ActiveToggles()
            .First(t => t.name == "Label").GetComponentInChildren<Text>().text;
        if (numberLabel != null)
        {
            if (Enum.TryParse<numberSort>(numberLabel, out numberSort selectNumber))
            {
                _number = selectNumber;
            }
            if (Enum.TryParse<sortGrop>(sortLabel, out sortGrop sort))
            {
                _sortGrop = sort;
            }
        }
        ChengeSort();
        sortMap.SetActive(false);
    }

    public void ChengeSort()//HPを比較して並び替える
    {
        switch (_number)//入手順の並びとそれぞれの昇順で切り替える
        {
            case numberSort.minNumber:
                MinNumberSort();
                break;
            case numberSort.maxNumber:
                MaxNumberSort();
                break;
        }
        SetSlot();
        foreach (var characterObject in myDeck)
        {
            CharacterData character = characterObject;
            if (character != null)
            {
                Debug.Log(characterObject.name + ": HP = " + character._hp);
            }
        }
    }

    public void MinNumberSort()
    {
        //HPとコストをそれぞれソート
        switch (_sortGrop)
        {
            case sortGrop.minHp:
                myDeck = myDeck
                    .OrderBy(character => character._hp)// まずはHP順に昇順でソート
                    .ThenBy(character => character._number) // 同じHPだったら入手順を昇順でソート
                    .ToList();
                Debug.Log("HP:昇順　入手:昇順");
                break;
            case sortGrop.maxHp:
                myDeck = myDeck
                    .OrderByDescending(character => character._hp)
                    .ThenBy(character => character._number)
                    .ToList();
                Debug.Log("HP:降順　入手:昇順");
                break;
            case sortGrop.minCost:
                myDeck = myDeck
                    .OrderBy(character => character._cost)// まずはHP順に降順でソート
                    .ThenBy(character => character._number)// 同じHPだったら入手順を昇順でソート
                    .ToList();
                Debug.Log("COST:昇順　入手:昇順");
                break;
            case sortGrop.maxCost:
                myDeck = myDeck
                   .OrderByDescending(character => character._hp)// まずはCOST順に昇順でソート
                   .ThenBy(character => character._number)// 同じCOSTだったら入手順を昇順でソート
                   .ToList();
                Debug.Log("COST:降順　入手:昇順");
                break;
        }
    }

    public void MaxNumberSort()
    {
        //HPとコストをそれぞれソート
        switch (_sortGrop)
        {
            case sortGrop.minHp:
                myDeck = myDeck
                    .OrderBy(character => character._hp)// まずはHP順に昇順でソート
                    .ThenByDescending(character => character._number) // 同じHPだったら入手順を降順でソート
                    .ToList();
                break;
            case sortGrop.maxHp:
                myDeck = myDeck
                    .OrderByDescending(character => character._hp)// まずはHP順に降順でソート
                    .ThenBy(character => character._number)// 同じHPだったら入手順を降順でソート
                    .ToList();
                break;
            case sortGrop.minCost:
                myDeck = myDeck
                    .OrderBy(character => character._cost)// まずはCOST順に昇順でソート
                    .ThenByDescending(character => character._number)// 同じHPだったら入手順を降順でソート
                    .ToList();
                break;
            case sortGrop.maxCost:
                myDeck = myDeck
                   .OrderByDescending(character => character._hp)// まずはCOST順に降順でソート
                   .ThenByDescending(character => character._number)// 同じHPだったら入手順を降順でソート
                   .ToList();
                break;
        }
    }
}
