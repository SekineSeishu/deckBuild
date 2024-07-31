using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    [SerializeField] private List<CharacterData> characterList;

    public bool newGet;
    public bool manyHp;
    public bool manyCost;


    public void ChengeSort()
    {
        characterList.Sort((a, b) =>
        {
            return a._hp.CompareTo(b._hp);
        });
        foreach (var characterObject in characterList)
        {
            CharacterData character = characterObject;
            if (character != null)
            {
                Debug.Log(characterObject.name + ": HP = " + character._hp);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
