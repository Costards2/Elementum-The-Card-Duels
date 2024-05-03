using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPointController : MonoBehaviour
{
    public static CardPointController instace;
    public CardPlacePoint[] playerCardPoint, enemyCardPoint;

    public float timeBetweenAttacks = .25f;

    private void Awake()
    {
        instace = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayerAttack()
    {
        StartCoroutine(PlayerAttackCo());
    }

    IEnumerator PlayerAttackCo()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);

        for(int i = 0; i < playerCardPoint.Length; i++)
        {
            if (playerCardPoint[i].activeCard != null && enemyCardPoint[i].activeCard != null)
            {
                //Attack
            }

            yield return new WaitForSeconds(timeBetweenAttacks);
        }

        BattleController.instance.AdvanceTurn();
    }
}
