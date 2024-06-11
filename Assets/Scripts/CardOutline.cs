using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CardOutline : MonoBehaviour
{
    public void OutlineOn()
    {
        //gameObject.layer = LayerMask.NameToLayer("Outline");
        this.gameObject.layer = LayerMask.NameToLayer("Outline");
    }

    public void OutlineOff()
    {
        //gameObject.layer = LayerMask.NameToLayer("Default");
        this.gameObject.layer = LayerMask.NameToLayer("Default");
    }
}
