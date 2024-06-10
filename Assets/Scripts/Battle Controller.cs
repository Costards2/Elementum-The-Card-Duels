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

    public bool battleEnded;
    public AudioClip[] audioClips = new AudioClip[2];
    public AudioSource audioSource;

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

        audioSource = GetComponent<AudioSource>();
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

        if(playerWon)
        {
            UI.instance.battleResulttext.text = "YOU WON";
        }
        else
        {
            UI.instance.battleResulttext.text = "YOU LOST";
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
            audioSource.PlayOneShot(audioClips[0]);
        }
        else
        {
            audioSource.PlayOneShot(audioClips[1]);
        }
    }
}
