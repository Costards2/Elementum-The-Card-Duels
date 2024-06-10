using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    void Start()
    {
        instance = this;
        AudioManager.instance.StopMusic();
        AudioManager.instance.playMenuMusic();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void JogarEasy()
    {
        SceneManager.LoadScene("Gameplay 1");
    }

     public void JogarBasic()
    {
        SceneManager.LoadScene("Gameplay 2");
    }

    public void Pular()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Config()
    {
        
    }

    public void Voltar()
    {
        
    }

    public void Sair()
    {
        Application.Quit();
    }
}
