using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController instance;

    private void Awake()
    {
        instance = this; 
    }

    public int startingCardsAmount = 5;

    void Start()
    {
        DeckController.instance.DrawMutipleCards(startingCardsAmount);
    }

    void Update()
    {
        
    }
}
