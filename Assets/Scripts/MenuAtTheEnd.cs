using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAtTheEnd : MonoBehaviour
{
    public float videoTime;
    public GameObject text;
    public bool secretCinematic;

    void Start()
    {
        StartCoroutine(MainMenu());

        if(secretCinematic)
        {
            StartCoroutine(PlayNextPatch());
        }
    }

    IEnumerator MainMenu()
    {
        yield return new WaitForSeconds(videoTime);

        MenuManager.instance.MainMenu();
    }

    IEnumerator PlayNextPatch()
    {
        yield return new WaitForSeconds(7f);

        text.SetActive(true);
    }
}
