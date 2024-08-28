
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

    public void nextSlot(Transform slotposition)//���̃X���b�g�|�W�V���������
    {
        GameObject newSlot = Instantiate(slotPositionPrefab, content);
        newSlot.transform.position = new Vector3(slotposition.transform.position.x, 
                                                                      slotposition.transform.position.y + nextPage, 
                                                                      slotposition.transform.position.z);
        slotsPosition.Add(newSlot.transform);
    }

    public void ClearSlot()//�X���b�g�\����x����������
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

    public void ChengeSort()//HP���r���ĕ��ёւ���
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
        //HP�ƃR�X�g�����ꂼ��\�[�g
        switch (_sortGrop)
        {
            case sortGrop.minHp:
                myDeck = myDeck
                    .OrderBy(character => character._hp)// �܂���HP���ɏ����Ń\�[�g
                    .ThenBy(character => character._number) // ����HP����������菇�������Ń\�[�g
                    .ToList();
                Debug.Log("HP:�����@����:����");
                break;
            case sortGrop.maxHp:
                myDeck = myDeck
                    .OrderByDescending(character => character._hp)
                    .ThenBy(character => character._number)
                    .ToList();
                Debug.Log("HP:�~���@����:����");
                break;
            case sortGrop.minCost:
                myDeck = myDeck
                    .OrderBy(character => character._cost)// �܂���HP���ɍ~���Ń\�[�g
                    .ThenBy(character => character._number)// ����HP����������菇�������Ń\�[�g
                    .ToList();
                Debug.Log("COST:�����@����:����");
                break;
            case sortGrop.maxCost:
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
        switch (_sortGrop)
        {
            case sortGrop.minHp:
                myDeck = myDeck
                    .OrderBy(character => character._hp)// �܂���HP���ɏ����Ń\�[�g
                    .ThenByDescending(character => character._number) // ����HP����������菇���~���Ń\�[�g
                    .ToList();
                break;
            case sortGrop.maxHp:
                myDeck = myDeck
                    .OrderByDescending(character => character._hp)// �܂���HP���ɍ~���Ń\�[�g
                    .ThenBy(character => character._number)// ����HP����������菇���~���Ń\�[�g
                    .ToList();
                break;
            case sortGrop.minCost:
                myDeck = myDeck
                    .OrderBy(character => character._cost)// �܂���COST���ɏ����Ń\�[�g
                    .ThenByDescending(character => character._number)// ����HP����������菇���~���Ń\�[�g
                    .ToList();
                break;
            case sortGrop.maxCost:
                myDeck = myDeck
                   .OrderByDescending(character => character._hp)// �܂���COST���ɍ~���Ń\�[�g
                   .ThenByDescending(character => character._number)// ����HP����������菇���~���Ń\�[�g
                   .ToList();
                break;
        }
    }
}
