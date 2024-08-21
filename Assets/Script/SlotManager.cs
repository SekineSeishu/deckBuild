using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    [SerializeField] private List<CharacterData> characterList;
    [SerializeField] private List<CharacterData> myDeck;
    [SerializeField] private List<Transform> firstSlotPosition;
    [SerializeField] private List<Transform> slotsPosition;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject slotPositionPrefab;
    [SerializeField] private GameObject slotPrefab;
    private float nextPage = -285;
    private int maxSlots = 255;

    public bool newGet;
    public bool manyHp;
    public bool manyCost;


    void Start()
    {
        for (int i = 0; i < maxSlots; i++) 
        {
            myDeck.Add(characterList[Random.Range(0, characterList.Count)]);
        }
        SetSlot();
    }

    void Update()
    {

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
    public void nextSlot(Transform slotposition)
    {
        GameObject newSlot = Instantiate(slotPositionPrefab, content);
        newSlot.transform.position = new Vector3(slotposition.transform.position.x, 
                                                                      slotposition.transform.position.y + nextPage, 
                                                                      slotposition.transform.position.z);
        slotsPosition.Add(newSlot.transform);
    }

    public void ClearSlot()
    {
        for(int i = 0;i < slotsPosition.Count;i++)
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

    public void ChengeHPSort()
    {
        myDeck.Sort((a, b) => b._hp - a._hp);
        /*{
            return a._hp.CompareTo(b._hp);
        });*/
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
}
