using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    public static DeckController instance;

    public List<CardScriptableObjects> deckToUse = new List<CardScriptableObjects>();

    private List<CardScriptableObjects> activeCards = new List<CardScriptableObjects>();

    public Card cardToSpawn;

    public float waitBetweenDrawingCards = .25f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SetUpDeck();
    }

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.T)) 
        //{
        //    DrawnCardToHand();
        //}
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

    public void DrawnCardToHand()
    {
        //Restart the Deck
        if(activeCards.Count == 0)
        {
            SetUpDeck();
        }

        Card newCard = Instantiate(cardToSpawn, transform.position, transform.rotation);
        newCard.cardSO = activeCards[0];
        newCard.SetUpCard();

        activeCards.RemoveAt(0);

        HandController.instance.AddCardToHand(newCard);
    }

    public void DrawMutipleCards(int amountToDraw)
    {
       StartCoroutine(DrawMutipleCouroutine(amountToDraw));
    }

    IEnumerator DrawMutipleCouroutine(int amountToDraw)
    {
        for (int i = 0; i < amountToDraw; i++)
        {
            DrawnCardToHand();

            yield return new WaitForSeconds(waitBetweenDrawingCards);
        }
    }
}
