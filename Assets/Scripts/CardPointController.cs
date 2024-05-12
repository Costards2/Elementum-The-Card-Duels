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
                playerCardPoint[i].activeCard.AttackCard(enemyCardPoint[i].activeCard.attackPower, enemyCardPoint[i].activeCard.selectedType, enemyCardPoint[i].activeCard.gameObject);
                //maybe place the attack trigger anim here

                //Debug.Log("PlayerAttackCO");
            }

            //yield return new WaitForSeconds(timeBetweenAttacks);
        }

        BattleController.instance.AdvanceTurn();
    }
}
