using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController instance;

    public enum TurnOrder { playerActive, enemyActive, CardsAttack }
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

    public bool canAttackAgain; //This makes the player wait a little to attack again

    private bool playerWon;
    private bool enemyWon;

    public bool battleEnded;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerWon = false;
        enemyWon = false;

        battleEnded = false;

        canAttackAgain = true;
        DeckController.instance.DrawMutipleCards(startingCardsAmount);
    }

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.U))
        //{
        //    AdvanceTurn();
        //}

        //Debug.Log(currentFase);
    }

    public void AdvanceTurn()
    {
        if (!battleEnded)
        {
            currentFase++;

            if ((int)currentFase >= System.Enum.GetValues(typeof(TurnOrder)).Length)
            {
                currentFase = 0;
            }

            switch (currentFase)
            {
                case TurnOrder.playerActive:

                    CardPointController.instace.playerCardPoint[0].activeCard = null;
                    CardPointController.instace.enemyCardPoint[0].activeCard = null;
                    StartCoroutine(CardDrawDelay());//or you can use: DeckController.instance.DrawnCardToHand();

                    break;

                case TurnOrder.enemyActive:

                    EnemyController.instance.StartAction();

                    break;

                case TurnOrder.CardsAttack:

                    canAttackAgain = false;
                    StartCoroutine(CardAttackDelay());

                    break;

                default:

                    Debug.Log("None of the states");

                    break;
            }
        }
        //UI.instance.UpdatePointsUI();
    }

    IEnumerator CardDrawDelay()
    {
        yield return new WaitForSeconds(2f);
        canAttackAgain = true;
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

    public void CheckPoints()
    {
        if ((playerPointFire == 3 || playerPointWater == 3 || playerPointPlant == 3) || (playerPointFire > 0  && playerPointWater > 0 && playerPointPlant >0))
        {
            playerWon = true;
            EndBattle();
        }
        else if ((enemyPointFire == 3 || enemyPointWater == 3 || enemyPointPlant == 3) || (enemyPointFire > 0 && enemyPointWater > 0 && enemyPointPlant > 0))
        {
            enemyWon = true;
            EndBattle();
        }
    }

    IEnumerator CardAttackDelay()
    {
        yield return new WaitForSeconds(.5f);
        CardPointController.instace.PlayerAttack();
    }

    void EndBattle()
    {
        battleEnded = true;
        Debug.Log("Ended");
    }
}
