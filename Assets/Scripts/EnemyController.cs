using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    public List<CardScriptableObjects> deckToUse = new List<CardScriptableObjects>();
    private List<CardScriptableObjects> activeCards = new List<CardScriptableObjects>();

    public Card cardToSpawn;
    public Transform cardSpawnPoint;

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
        yield return new WaitForSeconds(.5f);

        //BattleController.instance.AdvanceTurn();
    }
}
