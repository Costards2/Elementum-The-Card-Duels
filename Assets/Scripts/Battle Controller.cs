using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

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
    private bool tie;

    public bool battleEnded;

    public Sprite[] endScreens = new Sprite[3];

    public int turns;
    private int playerPoints;
    private int enemyPoints;

    private void Awake()
    {
        Time.timeScale = 1;
        instance = this;
    }

    void Start()
    {
        playerWon = false;
        enemyWon = false;
        tie = false;

        battleEnded = false;

        canAttackAgain = true;
        DeckController.instance.DrawMutipleCards(startingCardsAmount);

        AudioManager.instance.playBattleMusic();

        turns = 0;
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

                    //Make the player place the card and dont bug the fases
                    CardPointController.instace.playerCardPoint[0].activeCard = null;
                    CardPointController.instace.enemyCardPoint[0].activeCard = null;
        
                    if(!battleEnded)
                    {
                        StartCoroutine(CardDrawDelay());//or you can use: DeckController.instance.DrawnCardToHand();
                    }

                    break;

                case TurnOrder.enemyActive:

                    EnemyController.instance.StartAction();

                    break;

                case TurnOrder.CardsAttack:

                    canAttackAgain = false;
                    StartCoroutine(CardAttackDelay());
                    UI.instance.UpdatePointsUI();

                    turns++;

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
        if(turns < 18)
        {
            if ((playerPointFire == 3 || playerPointWater == 3 || playerPointPlant == 3) /*|| (playerPointFire > 0  && playerPointWater > 0 && playerPointPlant >0)*/)
            {
                playerWon = true;
                EndBattle();
            }
            else if ((enemyPointFire == 3 || enemyPointWater == 3 || enemyPointPlant == 3) /*|| (enemyPointFire > 0 && enemyPointWater > 0 && enemyPointPlant > 0)*/)
            {
                enemyWon = true;
                EndBattle();
            }
        }
        else if (turns >= 18)
        {
            playerPoints = playerPointFire + playerPointPlant + playerPointWater;
            enemyPoints = enemyPointFire + enemyPointPlant + enemyPointWater;

            if(playerPoints > enemyPoints)
            {
                playerWon = true;
            } 
            else if(playerPoints < enemyPoints)
            {
                enemyWon = true;
            }
            else if (playerPoints == enemyPoints)
            {
                tie = true;
            }
            
            EndBattle();
        }
    }

    IEnumerator CardAttackDelay()
    {
        yield return new WaitForSeconds(.5f);
        CardPointController.instace.PlayerAttack();
    }

    private void EndBattle()
    {
        battleEnded = true;

        if(playerWon)
        {
            UI.instance.victoryOrLoss.sprite = endScreens[0];
        }
        else if(enemyWon)
        {
            UI.instance.victoryOrLoss.sprite = endScreens[1];
        }
        else if(tie)
        {
            UI.instance.victoryOrLoss.sprite = endScreens[1];
        }

        StartCoroutine(EmpityHand());
        StartCoroutine(ShowResult());
    }

    //Made this enumarator so the hand wouldn't be emptied righ away 
    IEnumerator EmpityHand()
    {
        yield return new WaitForSeconds(2.75f);

        HandController.instance.EmpityHand();   
    }

    IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(3.25f);

        UI.instance.battleEndScreen.SetActive(true);

        if(playerWon)
        {
            AudioManager.instance.playVictoryMusic();
        }
        else
        {
           AudioManager.instance.playDefeatMusic();
        }
    }
}
