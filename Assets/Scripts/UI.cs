using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using System;

public class UI : MonoBehaviour
{
    public static UI instance;

    //public GameObject[] playerUiPoints = new GameObject[9];
    //public GameObject[] enemyUiPoints = new GameObject[9];

    //public TMP_Text playerText;
    //public TMP_Text EnemyText;

    public GameObject battleEndScreen;
    public TMP_Text battleResulttext;

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

    private void Awake()
    {
        instance = this;

        Time.timeScale = 1;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }

        UnityEngine.Debug.Log(playerPointPlant);
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

        //Old Ui with many objects 
        // switch(playerPointFire)
        // {
        //     case 1:

        //         playerUiPoints[0].SetActive(true);
                
        //         break;

        //     case 2:

        //         playerUiPoints[1].SetActive(true);

        //         break;

        //     case 3:

        //         playerUiPoints[2].SetActive(true);

        //         break;
        // }
        // switch (playerPointWater)
        // {
        //     case 1:

        //         playerUiPoints[3].SetActive(true);

        //         break;

        //     case 2:

        //         playerUiPoints[4].SetActive(true);

        //         break;

        //     case 3:

        //         playerUiPoints[5].SetActive(true);

        //         break;
        // }
        // switch (playerPointPlant)
        // {
        //     case 1:

        //         playerUiPoints[6].SetActive(true);

        //         break;

        //     case 2:

        //         playerUiPoints[7].SetActive(true);

        //         break;

        //     case 3:

        //         playerUiPoints[8].SetActive(true);

        //         break;
        // }

        // switch (enemyPointFire)
        // {
        //     case 1:

        //         enemyUiPoints[0].SetActive(true);

        //         break;

        //     case 2:

        //         enemyUiPoints[1].SetActive(true);

        //         break;

        //     case 3:

        //         enemyUiPoints[2].SetActive(true);

        //         break;
        // }
        // switch (enemyPointWater)
        // {
        //     case 1:

        //         enemyUiPoints[3].SetActive(true);

        //         break;

        //     case 2:

        //         enemyUiPoints[4].SetActive(true);

        //         break;

        //     case 3:

        //         enemyUiPoints[5].SetActive(true);

        //         break;
        // }
        // switch (enemyPointPlant)
        // {
        //     case 1:

        //         enemyUiPoints[6].SetActive(true);

        //         break;

        //     case 2:

        //         enemyUiPoints[7].SetActive(true);

        //         break;

        //     case 3:

        //         enemyUiPoints[8].SetActive(true);

        //         break;
        // }

        //playerText.text = ("P = " + playerPoints);
        //EnemyText.text = ("E = " + enemyPoints);
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
}
