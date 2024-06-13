using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public Slider volumeSlider;
    private string allVolume = "MasterVolume";
    public GameObject configScreen;

    void Awake()
    {
        Time.timeScale = 1;
        instance = this;
    }

    void Start()
    {
        AudioManager.instance.playMenuMusic();

         float currentVolume;
        
        if ( AudioManager.instance.audioMixer.GetFloat(allVolume, out currentVolume))
        {
            volumeSlider.value = Mathf.Pow(10, currentVolume / 20);
        }
        else
        {
            UnityEngine.Debug.LogError($"Parameter {allVolume} not found in AudioMixer");
        }
        volumeSlider.onValueChanged.AddListener(SetVolumeFromSlider);
    }
  
    private void Update()
    {
     

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            configScreen.SetActive(true);
        }

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

    public void SetVolume(float volume)
    {
        if (volume <= 0.0001f)
        {
            volume = 0.0001f; 
        }

        float dB = Mathf.Log10(volume) * 20;
        AudioManager.instance.audioMixer.SetFloat(allVolume, dB);
    }

    public void SetVolumeFromSlider(float volume)
    {
        SetVolume(volume);
    }
}
