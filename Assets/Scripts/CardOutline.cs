using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CardOutline : MonoBehaviour
{
    public void OutlineOn()
    {
        Debug.Log("LayerOn");
        //gameObject.layer = LayerMask.NameToLayer("Outline");
        this.gameObject.layer = LayerMask.NameToLayer("Outline");
    }

    public void OutlineOff()
    {
        Debug.Log("LayerOff");
        //gameObject.layer = LayerMask.NameToLayer("Default");
        this.gameObject.layer = LayerMask.NameToLayer("Default");
    }
}
