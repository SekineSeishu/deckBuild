
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
    public enum numberSort
    {
        minNumber,
        maxNumber,
    }
    public enum sortGroup
    {
        none,
        minHp,
        maxHp,
        minCost,
        maxCost
    }

    [SerializeField] private numberSort _number;
    [SerializeField] private sortGroup _sortGroup;
    [SerializeField] private List<CharacterData> characterList;
    [SerializeField] private List<CharacterData> myDeck;
    [SerializeField] private List<Transform> firstSlotPosition;
    [SerializeField] private List<Transform> slotsPosition;
    [SerializeField] private RectTransform content;
    [SerializeField] private GameObject slotPositionPrefab;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject sortMap;
    [SerializeField] private ToggleGroup numberToggleGrop;
    [SerializeField] private ToggleGroup sortToggleGrop;
    private int itemPreRow = 4;
    [SerializeField] private GameObject rowPrefab;
    private GameObject currentRow;
    private int currentItemIndex = 0;
    private float nextPage = -285;
    [SerializeField]private int maxSlots = 255;

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
            if (currentItemIndex % itemPreRow == 0)
            {
                currentRow = Instantiate(rowPrefab, content);
            }
            GameObject characterSlot = Instantiate(slotPrefab, currentRow.transform);
            characterSlot.GetComponentInChildren<Character>()._data = myDeck[i];
            currentItemIndex++;
        }
    }

    public void ListNumberSort()//���菇�ɕ��ёւ���
    {
        switch (_number)
        {
            case numberSort.minNumber:
               myDeck = myDeck.OrderBy(character => character._number).ToList();   // ���菇�ɏ����Ń\�[�g
                break;
            case numberSort.maxNumber:
                myDeck = myDeck.OrderByDescending(character => character._number).ToList(); // ���菇�ɍ~���Ń\�[�g
                break;
        }
    }

    public void ClearSlot()//�X���b�g�\����x����������
    {
        currentItemIndex = 0;
        foreach (Transform item in content)
        {
            Destroy(item.gameObject);
        }
    }

    //�\�[�g�ݒ��ʂ��J��
    public void OpenSortMapButton()
    {
        sortMap.SetActive(true);
    }

    //�����ɉ����ă\�[�g���s��
    public void ChengeSort()
    {
        switch (_number)//���菇�̕��тƂ��ꂼ��̏����Ő؂�ւ���
        {
            case numberSort.minNumber:
                MinNumberSort();
                break;
            case numberSort.maxNumber:
                MaxNumberSort();
                break;
        }
        //�\�[�g��\������
        SetSlot();
        sortMap.SetActive(false);
    }

    public void MinNumberSort()
    {
        //HP�ƃR�X�g�����ꂼ��\�[�g
        switch (_sortGroup)
        {
            case sortGroup.none:
                //�ڍ׃\�[�g���ݒ肳��Ă��Ȃ�(none)�Ȃ���菇�Ń\�[�g
                myDeck = myDeck
                .OrderBy(character => character._number)// ���菇�������Ń\�[�g
                .ToList();
                break;
            case sortGroup.minHp:
                myDeck = myDeck
                    .OrderBy(character => character._hp)// �܂���HP���ɏ����Ń\�[�g
                    .ThenBy(character => character._number) // ����HP����������菇�������Ń\�[�g
                    .ToList();
                Debug.Log("HP:�����@����:����");
                break;
            case sortGroup.maxHp:
                myDeck = myDeck
                    .OrderByDescending(character => character._hp)
                    .ThenBy(character => character._number)
                    .ToList();
                Debug.Log("HP:�~���@����:����");
                break;
            case sortGroup.minCost:
                myDeck = myDeck
                    .OrderBy(character => character._cost)// �܂���HP���ɍ~���Ń\�[�g
                    .ThenBy(character => character._number)// ����HP����������菇�������Ń\�[�g
                    .ToList();
                Debug.Log("COST:�����@����:����");
                break;
            case sortGroup.maxCost:
                myDeck = myDeck
                   .OrderByDescending(character => character._hp)// �܂���COST���ɏ����Ń\�[�g
                   .ThenBy(character => character._number)// ����COST����������菇�������Ń\�[�g
                   .ToList();
                Debug.Log("COST:�~���@����:����");
                break;
        }
    }

    public void MaxNumberSort()
    {
        //HP�ƃR�X�g�����ꂼ��\�[�g
        switch (_sortGroup)
        {
            case sortGroup.none:
                //�ڍ׃\�[�g���ݒ肳��Ă��Ȃ�(none)�Ȃ���菇�Ń\�[�g
                myDeck = myDeck
                .OrderByDescending(character => character._number)// ���菇���~���Ń\�[�g
                .ToList();
                break;
            case sortGroup.minHp:
                myDeck = myDeck
                    .OrderBy(character => character._hp)// �܂���HP���ɏ����Ń\�[�g
                    .ThenByDescending(character => character._number) // ����HP����������菇���~���Ń\�[�g
                    .ToList();
                break;
            case sortGroup.maxHp:
                myDeck = myDeck
                    .OrderByDescending(character => character._hp)// �܂���HP���ɍ~���Ń\�[�g
                    .ThenBy(character => character._number)// ����HP����������菇���~���Ń\�[�g
                    .ToList();
                break;
            case sortGroup.minCost:
                myDeck = myDeck
                    .OrderBy(character => character._cost)// �܂���COST���ɏ����Ń\�[�g
                    .ThenByDescending(character => character._number)// ����HP����������菇���~���Ń\�[�g
                    .ToList();
                break;
            case sortGroup.maxCost:
                myDeck = myDeck
                   .OrderByDescending(character => character._hp)// �܂���COST���ɍ~���Ń\�[�g
                   .ThenByDescending(character => character._number)// ����HP����������菇���~���Ń\�[�g
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
