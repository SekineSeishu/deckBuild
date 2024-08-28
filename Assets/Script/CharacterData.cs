using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObject/CreateCharacter")]
public class CharacterData : ScriptableObject
{
    public int _cost;
    public int _hp;
    public Sprite _image;
    public int _number;
}
