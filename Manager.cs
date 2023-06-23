using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Manager : MonoBehaviour
{
    public Player player;
    public Enemy[] enemies;
    public GameObject GameWinPanel;
    public GameObject GameOverPanel;
    private float EnemyTurnTime = 0.5f;
    private UIManager uiManager;


    private void Start()
    {
        uiManager = UnityEngine.Object.FindObjectOfType<UIManager>();
        enemies = UnityEngine.Object.FindObjectsOfType<Enemy>();
    }

    public int EnemyCounter()
    {
        List<Enemy> list = new List<Enemy>();
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].EnemyState != Enemy.state.Dead)
            {
                list.Add(enemies[i]);
            }
        }
        return list.Count;
    }

    public void EnemyTurn()
    {
        StartCoroutine(EnemyTurnRoutine());
    }

    private IEnumerator EnemyTurnRoutine()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].EnemyMove();
        }
        yield return new WaitForSeconds(0.5f);
        player.PlayerTurn();
    }


    public void PlayerWin()
    {
        uiManager.Win();
        player.enabled = false;
    }

    public void PlayerLose()
    {

        player.enabled = false;
        uiManager.Fail();
    }
}