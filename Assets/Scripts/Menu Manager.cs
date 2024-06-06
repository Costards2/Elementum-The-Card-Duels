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
    }
      public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Jogar()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void Pular()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Config()
    {
        //painelMenu.SetActive(true);
        //paineBasic.SetActive(false);
    }

    public void Voltar()
    {
        //painelMenu.SetActive(false);
        //paineBasic.SetActive(true);
    }

    public void Sair()
    {
        Application.Quit();
    }
}
