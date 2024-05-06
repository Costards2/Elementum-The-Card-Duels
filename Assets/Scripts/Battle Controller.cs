using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController instance;
    
    public enum TurnOrder { playerActive, CardsAttack, enemyActive }
    public TurnOrder currentFase;

    public int cardsToDrawPerTurn = 1;

    public int startingCardsAmount = 5;

    public Transform discardPoint;

    public int playerPointFire = 0;
    public int playerPointWater = 0;
    public int playerPointPlant = 0;

    public int enemyPointFire = 0;
    public int enemyPointWater = 0;
    public int enemyPointPlant = 0;

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

        Debug.Log(currentFase);
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

                StartCoroutine(CardDrawDelay());//or you can use: DeckController.instance.DrawnCardToHand();

                break;

            case TurnOrder.enemyActive:

                Debug.Log("Enemy Place Card");
                //AdvanceTurn();

                break;

            case TurnOrder.CardsAttack:
                
                Debug.Log("Cards Attack");
                CardPointController.instace.PlayerAttack();   
                AdvanceTurn();

                break;

            default:
                
                Debug.Log("None of the states");
                
                break;
        }

        UI.instance.UpdatePointsUI();
    }

    public void EndPlayerTurn()
    {
        AdvanceTurn();
    }

    IEnumerator CardDrawDelay()
    {
        yield return new WaitForSeconds(2f);
        DeckController.instance.DrawMutipleCards(cardsToDrawPerTurn);
    }

    public void UpdatePlayerPointFire()
    {
        playerPointFire++;
    }
    public void UpdatePlayerPointWater()
    {
        playerPointWater++;
    }
    public void UpdatePlayerPointPlant()
    {
        playerPointPlant++;
    }

    public void UpdateEnemyPointFire()
    {
        enemyPointFire++;
    }
    public void UpdateEnemyPointWater()
    {
        enemyPointWater++;
    }
    public void UpdateEnemyPointPlant()
    {
        enemyPointPlant++;
    }
}
