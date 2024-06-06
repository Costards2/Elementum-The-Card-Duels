using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAtTheEnd : MonoBehaviour
{
    public float videoTime;

    void Start()
    {
        StartCoroutine(MainMenu());
    }

    IEnumerator MainMenu()
    {
        yield return new WaitForSeconds(videoTime);

        MenuManager.instance.MainMenu();
    }
}
