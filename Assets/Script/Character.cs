using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public CharacterData _data;
    public int cost;
    public int hp;
    public Sprite _image;
    public Image cardImage;
    public Image baceImage;

    // Start is called before the first frame update
    void Start()
    {
        cost = _data._cost;
        hp = _data._hp;
        _image = _data._image;
        cardImage.sprite = _image;
        baceImage.sprite = _image;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
