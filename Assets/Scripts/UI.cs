using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.Diagnostics;
using System;
using Unity.VisualScripting;

public class UI : MonoBehaviour
{
    public static UI instance;

    //public GameObject[] playerUiPoints = new GameObject[9];
    //public GameObject[] enemyUiPoints = new GameObject[9];

    //public TMP_Text playerText;
    //public TMP_Text EnemyText;

    public GameObject battleEndScreen;
    //public TMP_Text battleResulttext;

    //VALUES
    public int playerPointFire = 0;
    public int playerPointWater = 0;
    public int playerPointPlant = 0;

    public int enemyPointFire = 0;
    public int enemyPointWater = 0;
    public int enemyPointPlant = 0;

    //TEXT
    public TextMeshProUGUI playerPointFireText;
    public TextMeshProUGUI playerPointWaterText;
    public TextMeshProUGUI playerPointPlantText;

    public TextMeshProUGUI enemyPointFireText;
    public TextMeshProUGUI enemyPointWaterText;
    public TextMeshProUGUI enemyPointPlantText;

    public GameObject pauseScreen;

    public Image victoryOrLoss;

    public Slider volumeSlider;
    private string allVolume = "MasterVolume";

    private void Awake()
    {
        instance = this;

        Time.timeScale = 1;
    }

    void Start()
    {
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
            PauseUnpause();
        }

    }
    public void UpdatePointsUI()
    {
        playerPointFire = BattleController.instance.playerPointFire;
        playerPointWater = BattleController.instance.playerPointWater;
        playerPointPlant = BattleController.instance.playerPointPlant;

        enemyPointFire = BattleController.instance.enemyPointFire;
        enemyPointWater = BattleController.instance.enemyPointWater;
        enemyPointPlant = BattleController.instance.enemyPointPlant;

        playerPointFireText.text = (playerPointFire.ToString());
        playerPointWaterText.text = (playerPointWater.ToString());
        playerPointPlantText.text = (playerPointPlant.ToString());

        enemyPointFireText.text = (enemyPointFire.ToString());
        enemyPointWaterText.text = (enemyPointWater.ToString());
        enemyPointPlantText.text = (enemyPointPlant.ToString());

    }

    public void MainMenu() 
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ReStartLevel() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseUnpause()
    {
        if(pauseScreen.activeSelf == false)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void SetVolume(float volume)
    {
        float dB = Mathf.Log10(volume) * 20;
        AudioManager.instance.audioMixer.SetFloat(allVolume, dB);
    }

    public void SetVolumeFromSlider(float volume)
    {
        SetVolume(volume);
    }
}
