using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAtTheEnd : MonoBehaviour
{
    public float videoTime;
    public bool secretCinematic;

    void Start()
    {
        if(secretCinematic)
        {
            StartCoroutine(Ailu());
        }
        else
        {
            StartCoroutine(MainMenu());
        }
    }

    IEnumerator MainMenu()
    {
        yield return new WaitForSeconds(videoTime);

        MenuManager.instance.MainMenu();
    }

    IEnumerator Ailu()
    {
        yield return new WaitForSeconds(videoTime);

        MenuManager.instance.JogarAilu();
    }
}
