using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalScene : MonoBehaviour
{
    public static FinalScene instance;
    public GameObject battleEndScreenFinal;


    private void Awake()
    {
        instance = this;
    }

    public void FinalSceneActivate()
    {
        battleEndScreenFinal.SetActive(true);
    }

    public void PlayFinalScene()
    {
        SceneManager.LoadScene("Cinematic Vitoria");
    }
}
