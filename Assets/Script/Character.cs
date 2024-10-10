using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class Character : MonoBehaviour
{
    public CharacterData _data;
    public int number;
    public int cost;
    public int hp;
    public Image cardImage;
    public CardBase Base;

    // Start is called before the first frame update
    void Start()
    {
        //キャラクターデータを受け取る
        number = _data._number;
        cost = _data._cost;
        hp = _data._hp;
        cardImage.sprite = _data._image;
        Base._data = _data;
        Base.firstSetUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
