using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardScriptableObjects cardSO;

    public enum Type { Fire, Water, Plant }
    public Type selectedType;

    public int attackPower;

    public Image cardSprite;

    void Start()
    {
        attackPower = cardSO.attackPower;
        cardSprite.sprite = cardSO.cardSprite;
    }

    void Update()
    {

    }
}
