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
    
    void Start()
    {
        attackPower = cardSO.attackPower;
        attackPowerString.text = cardSO.attackPower.ToString();

        cardElementSprite = cardSO.cardElement;
        cardElementImage.sprite = cardElementSprite;

        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        
        Material[] materials = _meshRenderer.sharedMaterials;
        materials[1] = cardSO.cardSprite;
        
        _meshRenderer.sharedMaterials = materials;

        handController = FindObjectOfType<HandController>();
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
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
}
