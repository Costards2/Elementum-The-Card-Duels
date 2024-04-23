using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card", order = 1)]

public class CardScriptableObjects : ScriptableObject
{
    public enum Type { Fire, Water, Plant }
    public Type selectedType;

    public int attackPower;

    public string cardName;

    public Sprite cardSprite; 
}
