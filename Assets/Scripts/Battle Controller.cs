using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController instance;
    
    public enum TurnOrder { playerActive, playerCardAttacks, enemyActive, enemyCardAttacks }
    public TurnOrder currentFase;

    public int cardsToDrawPerTurn = 1;

    public int startingCardsAmount = 5;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        DeckController.instance.DrawMutipleCards(startingCardsAmount);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            AdvanceTurn();
        }

        //Debug.Log((int)currentFase);
    }

    public void AdvanceTurn()
    {
        currentFase++;

        if((int)currentFase >= System.Enum.GetValues(typeof(TurnOrder)).Length) 
        {
            currentFase = 0;
        }

        switch (currentFase)
        {
            case TurnOrder.playerActive:

                DeckController.instance.DrawMutipleCards(cardsToDrawPerTurn); // or you can use: DeckController.instance.DrawnCardToHand();

                break;

            case TurnOrder.playerCardAttacks:
                
                Debug.Log("Skipping playerCardAttacks");
                //AdvanceTurn();

                break;

            case TurnOrder.enemyActive:

                Debug.Log("Skipping enemyActive");
                AdvanceTurn();

                break;

            case TurnOrder.enemyCardAttacks:

                Debug.Log("Skipping enemyCardAttacks");
                AdvanceTurn();

                break;

            default:
                
                Debug.Log("None of the states");
                
                break;
        }
    }

    public void EndPlayerTurn()
    {
        AdvanceTurn();
    }
}
