using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    public List<CardScriptableObjects> deckToUse = new List<CardScriptableObjects>();
    private List<CardScriptableObjects> activeCards = new List<CardScriptableObjects>();
    //private List<CardScriptableObjects> cardsInHand = new List<CardScriptableObjects>();

    public int startHandSize;

    public Card cardToSpawn;
    public Transform cardSpawnPoint;

    public enum AIType {Basic, Advanced}
    public AIType enemyAIType = AIType.Basic;

    private void Awake()
    {
        instance = this; 
    }
    
    void Start()
    {
        SetUpDeck();
        //SetUpHand();
    }

    public void SetUpDeck()
    {
        activeCards.Clear();

        List<CardScriptableObjects> tempDeck = new List<CardScriptableObjects>();
        tempDeck.AddRange(deckToUse);

        int interations = 0;

        while (tempDeck.Count > 0 && interations < 500)
        {
            int selected = Random.Range(0, tempDeck.Count);
            activeCards.Add(tempDeck[selected]);
            tempDeck.RemoveAt(selected);
            interations++;
        }
    }

    public void StartAction()
    {
        StartCoroutine(EnemyActionCourotine());
    }

    IEnumerator EnemyActionCourotine()
    {
        if(activeCards.Count == 0)
        {
            //Bring More Cards if the enemy ran out of them (I will probably remove this)
            SetUpDeck();
        }

        //yield return new WaitForSeconds(.5f);

        List<CardPlacePoint> cardPoint = new List<CardPlacePoint>();
        cardPoint.AddRange(CardPointController.instace.enemyCardPoint);

        CardPlacePoint selectedPoint = cardPoint[0];

        switch (enemyAIType)
        {
            case AIType.Basic:

                if (selectedPoint.activeCard == null)
                {

                    Card newCard = Instantiate(cardToSpawn, cardSpawnPoint.position, cardSpawnPoint.rotation);

                    newCard.anim.SetTrigger("CardPlaced");

                    newCard.cardSO = activeCards[0];
                    activeCards.RemoveAt(0);
                    newCard.SetUpCard();
                    newCard.MoveToPoint(selectedPoint.transform.position, selectedPoint.transform.rotation);

                    selectedPoint.activeCard = newCard;
                    newCard.assignedPlace = selectedPoint;
                }

                break;

            case AIType.Advanced:

                break;
        } 

        yield return new WaitForSeconds(.5f);
        BattleController.instance.AdvanceTurn();
    }

    //void SetUpHand()
    //{
    //    for (int i = 0; i < startHandSize; i++)
    //    {
    //        if(activeCards.Count == 0)
    //        {
    //            SetUpDeck();
    //        }

    //        cardsInHand.Add(activeCards[0]);
    //        activeCards.RemoveAt(0);
    //    }
    //}
}
