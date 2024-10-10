using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardBase : MonoBehaviour
{
    public CharacterData _data;
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _setText;

    // Start is called before the first frame update
    void Start()
    {
        _setText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //初期設定
    public void firstSetUp()
    {
        _icon.sprite = _data._image;
        setCharacter();
    }

    //キャラクターがセットされている状態
    public void setCharacter()
    {
        if (_data._set)
        {
            _setText.gameObject.SetActive(true);
            _setText.text = "set!";
        }
        else
        {
            _setText.gameObject.SetActive(false);
        }
    }
}
