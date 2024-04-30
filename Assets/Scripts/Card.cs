using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardScriptableObjects cardSO;

    public enum Type { Fire, Water, Plant }
    public Type selectedType;

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

    void Start()
    {
        SetUpCard();

        handController = FindObjectOfType<HandController>(); 
        theCollider = GetComponent<Collider>();
    }

    void SetUpCard()
    {
        attackPower = cardSO.attackPower;
        attackPowerString.text = cardSO.attackPower.ToString();

        cardElementSprite = cardSO.cardElement;
        cardElementImage.sprite = cardElementSprite;

        _meshRenderer = GetComponentInChildren<MeshRenderer>();

        Material[] materials = _meshRenderer.sharedMaterials;
        materials[1] = cardSO.cardSprite;

        _meshRenderer.sharedMaterials = materials;
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

            if(Input.GetMouseButtonDown(1))
            {
                ReturnToHand();
            }

            if (Input.GetMouseButtonDown(0) && !justPressed)
            {
                if (Physics.Raycast(ray, out hit, 100f, whatIsPlacement))
                {
                    CardPlacePoint selectedPoint = hit.collider.GetComponent<CardPlacePoint>();

                    if (selectedPoint.activeCard == null && selectedPoint.isPlayerPoint)
                    {
                        selectedPoint.activeCard = this;
                        assignedPlace = selectedPoint;

                        MoveToPoint(selectedPoint.transform.position, Quaternion.identity);

                        inHand = false;
                        isSelected = false;

                        handController.RemoveCardFromHand(this);
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

    public void MoveToPoint(Vector3 pointToMoveTo, Quaternion rotationToMatch)
    {
        targetPoint = pointToMoveTo;
        targetRotation = rotationToMatch;
    }

    private void OnMouseOver()
    {
        if(inHand)
        {
            MoveToPoint(handController.cardPosition[handPosition] + new Vector3(0f, 0.25f, .5f), Quaternion.identity);
        }
    }

    private void OnMouseExit()
    {
        if (inHand)
        {
            MoveToPoint(handController.cardPosition[handPosition], handController.minPos.rotation);
        }
    }

    private void OnMouseDown()
    {
        if (inHand)
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
}
