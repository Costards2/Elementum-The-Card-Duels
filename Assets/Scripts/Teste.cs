using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Teste : MonoBehaviour
{

    private void OnMouseOver()
    {
        this.transform.position = new Vector3(this.transform.position.x, 0.25f, this.transform.position.z);
        //if (inHand)
        //{
        //    MoveToPoint(handController.cardPosition[handPosition] + new Vector3(0f, 0.25f, .5f), Quaternion.identity);
        //}
    }

    private void OnMouseExit()
    {
        this.transform.position = new Vector3(this.transform.position.x, 0f, this.transform.position.z); ;
        //if (inHand)
        //{
        //    MoveToPoint(handController.cardPosition[handPosition], handController.minPos.rotation);
        //}
    }

    //private void OnMouseDown()
    //{
    //    if (inHand && BattleController.instance.currentFase == BattleController.TurnOrder.playerActive)
    //    {
    //        isSelected = true;
    //        theCollider.enabled = false;
    //        justPressed = true;
    //    }
    //}

    //public void ReturnToHand()
    //{
    //    isSelected = false;
    //    theCollider.enabled = true;

    //    MoveToPoint(handController.cardPosition[handPosition], handController.minPos.rotation);
    //}
}
