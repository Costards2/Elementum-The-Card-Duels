using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public List<Card> heldCards = new();

    public Transform minPos, maxPos;
    public List<Vector3> cardPosition = new List<Vector3>();


    void Start()
    {
        SetCardPositionsInHand();
    }

    void Update()
    {
        
    }

    public void SetCardPositionsInHand()
    {
        cardPosition.Clear();

        Vector3 distanceBetwenPoints = Vector3.zero;

        if(heldCards.Count > 1)
        {
            distanceBetwenPoints = (maxPos.position - minPos.position) / (heldCards.Count - 1);
        }

        for (int i = 0; i < heldCards.Count; i++)
        {
            cardPosition.Add(minPos.position + (distanceBetwenPoints * i));

            //heldCards[i].transform.position = cardPosition[i];
            //heldCards[i].transform.rotation = minPos.rotation;

            heldCards[i].MoveToPoint(cardPosition[i], minPos.rotation);
            heldCards[i].inHand = true;
            heldCards[i].handPosition = i;
        }
    }
}
