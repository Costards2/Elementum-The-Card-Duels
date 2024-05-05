using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI instance;

    public TMP_Text playerText;
    public TMP_Text EnemyText;

    public int playerPoints;
    public int enemyPoints;

    private void Awake()
    {
        instance = this;

        playerPoints = 0;
        enemyPoints = 0;

        playerText.text = ("P = " + playerPoints);
        EnemyText.text = ("E = " + enemyPoints);
    }

    public void UpdatePointsUI()
    {
        playerPoints = BattleController.instance.playerPoints;
        enemyPoints = BattleController.instance.enemyPoints;
        playerText.text = ("P = " + playerPoints);
        EnemyText.text = ("E = " + enemyPoints);
    }
}
