using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;
using System;

public class Card : MonoBehaviour
{
    public CardScriptableObjects cardSO;

    public bool isPlayer;

    public enum Type { Fire, Water, Plant }
    public Type selectedType;
    int scripatableObjectType;

    public int attackPower;
    public TMP_Text attackPowerString;

    private Sprite cardElementSprite;
    public Image cardElementImage;

    public MeshRenderer _meshRenderer;

    private Vector3 targetPoint;
    private Quaternion targetRotation;
    public float moveSpeed = 5f;
    public float rotateSpeed = 540f;

    public bool inHand;
    public int handPosition;

    public HandController handController;

    private bool isSelected;
    private Collider theCollider;
   
    public LayerMask whatIsDeskTop;
    public LayerMask whatIsPlacement;
    private bool justPressed;

    public CardPlacePoint assignedPlace;

    public Card enemyCard;

    public Animator anim;

    Transform child;
    Transform grandChild;

    void Start()
    {
        //Making the enemy card stay on Its position
        if (targetPoint == Vector3.zero)
        {
            targetPoint = this.transform.position;
            targetRotation = this.transform.rotation;
        }

        SetUpCard();

        handController = FindObjectOfType<HandController>(); 
        theCollider = GetComponent<Collider>();

        child = transform.GetChild(0);

        if (child != null)
        {
             grandChild = child.GetChild(0); 
        }
    
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

        if (isSelected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, whatIsDeskTop))
            {
                MoveToPoint(hit.point + new Vector3(0f, 0.2f, 0f), Quaternion.identity);
            }

            if((Input.GetMouseButtonDown(1) || Input.GetMouseButtonUp(0)) && BattleController.instance.battleEnded == false)
            {
                ReturnToHand();
            }

            if (Input.GetMouseButtonUp(0) && !justPressed && (BattleController.instance.currentFase == BattleController.TurnOrder.playerActive) && BattleController.instance.battleEnded == false)
            {
                if (Physics.Raycast(ray, out hit, 100f, whatIsPlacement))
                {
                    CardPlacePoint selectedPoint = hit.collider.GetComponent<CardPlacePoint>();

                    if (selectedPoint.activeCard == null && selectedPoint.isPlayerPoint)
                    {
                        selectedPoint.activeCard = this;
                        assignedPlace = selectedPoint;

                        MoveToPoint(selectedPoint.transform.position,Quaternion.identity); // Quaternion.Euler(0, 0.04f, 180f), This way a make the card Rotate preventing the enemy seeing it, however i decided to do all of this in the Animator (I regret It a little) 

                        anim.SetTrigger("CardPlaced");

                        inHand = false;
                        isSelected = false;

                        handController.RemoveCardFromHand(this);

                        //Automatically advance turn 
                        BattleController.instance.AdvanceTurn();
                    }
                    else
                    {
                        ReturnToHand();
                    }
                }
                else
                {
                    ReturnToHand();
                }
            }
        }

        justPressed = false;
    }

    public void SetUpCard()
    {
        attackPower = cardSO.attackPower;
        attackPowerString.text = cardSO.attackPower.ToString();

        scripatableObjectType = Convert.ToInt32(cardSO.selectedType);
        switch (scripatableObjectType)
        {
            case 0:

                selectedType = Type.Fire; 
                break;
                
            case 1:

                selectedType = Type.Water; 
                break;

            case 2:

                selectedType = Type.Plant; 
                break;
        }


        cardElementSprite = cardSO.cardElement;
        cardElementImage.sprite = cardElementSprite;

        _meshRenderer = GetComponentInChildren<MeshRenderer>();

        Material[] materials = _meshRenderer.sharedMaterials;
        materials[1] = cardSO.cardSprite;

        _meshRenderer.sharedMaterials = materials;
    }

    public void MoveToPoint(Vector3 pointToMoveTo, Quaternion rotationToMatch)
    {
        targetPoint = pointToMoveTo;
        targetRotation = rotationToMatch;
    }

    private void OnMouseOver()
    {
        if(inHand && isPlayer && BattleController.instance.battleEnded == false)
        {
            grandChild.GetComponent<CardOutline>().OutlineOn();
            MoveToPoint(handController.cardPosition[handPosition] + new Vector3(0f, 0.25f, .5f), Quaternion.identity);
        }
    }

    private void OnMouseExit()
    {
        if (inHand && isPlayer)
        {
            grandChild.GetComponent<CardOutline>().OutlineOff();
            MoveToPoint(handController.cardPosition[handPosition], handController.minPos.rotation);
        }
    }

    //private void OnMouseDown()
    //{
    //    if (inHand && BattleController.instance.currentFase == BattleController.TurnOrder.playerActive && isPlayer && finished)
    //    {
    //        isSelected = true;
    //        theCollider.enabled = false;
    //        justPressed = true;
    //    }
    //}

    private void OnMouseDrag()
    {
        if (inHand && BattleController.instance.currentFase == BattleController.TurnOrder.playerActive && isPlayer && BattleController.instance.canAttackAgain == true && BattleController.instance.battleEnded == false)
        {
            isSelected = true;
            theCollider.enabled = false;
            justPressed = true;
        }
    }

    public void ReturnToHand()
    {
        isSelected = false;
        theCollider.enabled = true;

        MoveToPoint(handController.cardPosition[handPosition],handController.minPos.rotation);
    }

    public void AttackCard(int powerAmount, Type element, GameObject card) // Add a better "win controller", Create a Battle turn Controller because all the win or loose turn code is done in the card script and change some of this Ifs for swicth
    {
        if(selectedType == element)
        {
           if(this.attackPower > powerAmount) 
           {
                if (selectedType == Type.Fire)
                {
                    BattleController.instance.UpdatePlayerPointFire();
                }
                else if (selectedType == Type.Water)
                {
                    BattleController.instance.UpdatePlayerPointWater();
                }
                else if (selectedType == Type.Plant)
                {
                    BattleController.instance.UpdatePlayerPointPlant();
                }
           } 
           else if(this.attackPower < powerAmount)
           {
                if (element == Type.Water)
                {
                    BattleController.instance.UpdateEnemyPointWater();
                }
                else if (element == Type.Plant)
                {
                    BattleController.instance.UpdateEnemyPointPlant();
                }
                else if (element == Type.Fire)
                {
                    BattleController.instance.UpdateEnemyPointFire();
                }
            }
           else
           {
                Debug.Log("Tie");
           }
        }
        else if(selectedType != element) 
        { 
            if(selectedType == Type.Fire && element == Type.Water)
            {
                BattleController.instance.UpdateEnemyPointWater();
            }
            else if(selectedType == Type.Water && element == Type.Plant)
            {
                BattleController.instance.UpdateEnemyPointPlant();
            }
            else if (selectedType == Type.Plant && element == Type.Fire)
            {
                BattleController.instance.UpdateEnemyPointFire();
            }
            else if (selectedType == Type.Fire && element == Type.Plant)
            {
                BattleController.instance.UpdatePlayerPointFire();
            }
            else if (selectedType == Type.Water && element == Type.Fire)
            {
                BattleController.instance.UpdatePlayerPointWater();
            }
            else if (selectedType == Type.Plant && element == Type.Water)
            {
                BattleController.instance.UpdatePlayerPointPlant();
            }
        }

        enemyCard = card.GetComponent<Card>();

        StartCoroutine(WaitToAttackAndDiscard());

        Destroy(card, 5f);
        Destroy(gameObject, 5f);
    }

    IEnumerator WaitToAttackAndDiscard()
    {
        yield return new WaitForSeconds(.25f);

        anim.SetTrigger("Attack");
        enemyCard.anim.SetTrigger("Attack");

        yield return new WaitForSeconds(1.3f);

        UI.instance.UpdatePointsUI();

        anim.SetTrigger("AfterAttack"); //insteaf of using an after attack I could have made it all in attack but i decided to do this way to "play" a little more
        enemyCard.anim.SetTrigger("AfterAttack");

        yield return new WaitForSeconds(1f);

        MoveToPoint(BattleController.instance.discardPoint.position, BattleController.instance.discardPoint.rotation);
        anim.SetTrigger("Jump");
        enemyCard.MoveToPoint(BattleController.instance.discardPoint.position, BattleController.instance.discardPoint.rotation);
        enemyCard.anim.SetTrigger("Jump");

        BattleController.instance.CheckPoints(); //Will Check the points and end the battle if either the player or the enemy won
    }
}
