using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    void Awake()
    {
        Time.timeScale = 1;
        instance = this;
    }

    void Start()
    {
        AudioManager.instance.playMenuMusic();
    }
  
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void JogarEasy()
    {
        SceneManager.LoadScene("Rato");
    }

     public void JogarBasic()
    {
        SceneManager.LoadScene("Bruxa");
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
