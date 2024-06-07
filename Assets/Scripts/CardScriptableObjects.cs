using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Card", menuName = "Card", order = 1)]

public class CardScriptableObjects : ScriptableObject
{
    public enum Type { Fire, Water, Plant }
    public Type selectedType;

    public int attackPower;

    public string cardName;

    public Material cardSprite;

    public Material cardDissolve;

    public Sprite cardElement;

    public AudioClip attackClip;
}
