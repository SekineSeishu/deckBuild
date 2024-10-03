
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
using static SlotManager;

public class SlotManager : MonoBehaviour
{
    public enum numberSort //入手順
    {
        minNumber,
        maxNumber,
    }
    public enum sortGroup //ステータスソート
    {
        none,
        minHp,
        maxHp,
        minCost,
        maxCost
    }

    [Header("現在の表示順")]
    [SerializeField] private numberSort _number;
    [SerializeField] private sortGroup _sortGroup;
    [Space(10), Header("キャラクターデータ")]
    [SerializeField] private List<CharacterData> characterList;
    [SerializeField] private List<CharacterData> myDeck;
    [Space(10), Header("親オブジェクト")]
    [SerializeField] private RectTransform content;
    [Space(5), Header("表示スロットオブジェクト")]
    [SerializeField] private GameObject slotPrefab;
    [Space(5), Header("表示スロットレイアウト")]
    [SerializeField] private GameObject rowPrefab;
    [Space(10), Header("ソートUI")]
    [SerializeField] private GameObject sortMap;
    [SerializeField] private ToggleGroup numberToggleGrop;
    [SerializeField] private ToggleGroup sortToggleGrop;
    private int itemPreRow = 4;
    private GameObject currentRow;
    private int currentItemIndex = 0;
    [Space(10),Header("インベントリの最大容量")]
    [SerializeField]private int maxSlots = 255;

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

    public void SetSlot()//デッキ内のキャラクターを表示する
    {
        ClearSlot();
        for (int i = 0; i < myDeck.Count; i++)
        {
            //一列の最大容量までいったら次の列を生成
            if (currentItemIndex % itemPreRow == 0)
            {
                currentRow = Instantiate(rowPrefab, content);
            }
            GameObject characterSlot = Instantiate(slotPrefab, currentRow.transform);
            characterSlot.GetComponentInChildren<Character>()._data = myDeck[i];
            currentItemIndex++;
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

    public void ClearSlot()//スロット表示一度初期化する
    {
        currentItemIndex = 0;
        foreach (Transform item in content)
        {
            Destroy(item.gameObject);
        }
    }

    //ソート設定画面を開く
    public void OpenSortMapButton()
    {
        sortMap.SetActive(true);
    }

    //条件に応じてソートを行う
    public void ChengeSort()
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
        //ソート後表示する
        SetSlot();
        sortMap.SetActive(false);
    }

    public void MinNumberSort()
    {
        //HPとコストをそれぞれソート
        switch (_sortGroup)
        {
            case sortGroup.none:
                //詳細ソートが設定されていない(none)なら入手順でソート
                myDeck = myDeck
                .OrderBy(character => character._number)// 入手順を昇順でソート
                .ToList();
                break;
            case sortGroup.minHp:
                myDeck = myDeck
                    .OrderBy(character => character._hp)// まずはHP順に昇順でソート
                    .ThenBy(character => character._number) // 同じHPだったら入手順を昇順でソート
                    .ToList();
                Debug.Log("HP:昇順　入手:昇順");
                break;
            case sortGroup.maxHp:
                myDeck = myDeck
                    .OrderByDescending(character => character._hp)
                    .ThenBy(character => character._number)
                    .ToList();
                Debug.Log("HP:降順　入手:昇順");
                break;
            case sortGroup.minCost:
                myDeck = myDeck
                    .OrderBy(character => character._cost)// まずはHP順に降順でソート
                    .ThenBy(character => character._number)// 同じHPだったら入手順を昇順でソート
                    .ToList();
                Debug.Log("COST:昇順　入手:昇順");
                break;
            case sortGroup.maxCost:
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
        switch (_sortGroup)
        {
            case sortGroup.none:
                //詳細ソートが設定されていない(none)なら入手順でソート
                myDeck = myDeck
                .OrderByDescending(character => character._number)// 入手順を降順でソート
                .ToList();
                break;
            case sortGroup.minHp:
                myDeck = myDeck
                    .OrderBy(character => character._hp)// まずはHP順に昇順でソート
                    .ThenByDescending(character => character._number) // 同じHPだったら入手順を降順でソート
                    .ToList();
                break;
            case sortGroup.maxHp:
                myDeck = myDeck
                    .OrderByDescending(character => character._hp)// まずはHP順に降順でソート
                    .ThenBy(character => character._number)// 同じHPだったら入手順を降順でソート
                    .ToList();
                break;
            case sortGroup.minCost:
                myDeck = myDeck
                    .OrderBy(character => character._cost)// まずはCOST順に昇順でソート
                    .ThenByDescending(character => character._number)// 同じHPだったら入手順を降順でソート
                    .ToList();
                break;
            case sortGroup.maxCost:
                myDeck = myDeck
                   .OrderByDescending(character => character._hp)// まずはCOST順に降順でソート
                   .ThenByDescending(character => character._number)// 同じHPだったら入手順を降順でソート
                   .ToList();
                break;
        }

    }
    public void SetNumberSort(numberSort numberSort)
    {
        _number = numberSort;
    }
    public void SetSortGroup(sortGroup sortGroup)
    {
        _sortGroup = sortGroup;
    }

    //Toggleが押されたら対応する条件をセットする
    public void OnMinNumberToggleChanged(bool isOn)
    {
        if (isOn) SetNumberSort(numberSort.minNumber);
    }

    public void OnMaxNumberToggleChanged(bool isOn)
    {
        if (isOn) SetNumberSort(numberSort.maxNumber); 
    }

    public void OnMinHpToggleChanged(bool isOn)
    {
        if (isOn) SetSortGroup(sortGroup.minHp);
        else SetSortGroup(sortGroup.none);
    }

    public void OnMaxHpToggleChanged(bool isOn)
    {
        if (isOn) SetSortGroup(sortGroup.maxHp);
        else SetSortGroup(sortGroup.none);
    }

    public void OnMinCostToggleChanged(bool isOn)
    {
        if (isOn) SetSortGroup(sortGroup.minCost);
        else SetSortGroup(sortGroup.none);
    }

    public void OnMaxCostToggleChanged(bool isOn)
    {
        if (isOn) SetSortGroup(sortGroup.maxCost);
        else SetSortGroup(sortGroup.none);
    }
}
