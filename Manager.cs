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


    private void Start()
    {
        enemies = Resources.FindObjectsOfTypeAll(typeof(Enemy)) as Enemy[];
    }

    public int EnemyCounter()
    {
        List<Enemy> list = new List<Enemy>();
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].EnemyState != Enemy.state.Death)
            {
                list.Add(enemies[i]);
            }
        }
        return list.Count;
    }

    public void EnemyTurn()
    {
        if (EnemyCounter() == 0)
        {
            PlayerWin();
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].EnemyMove();
        }
        player.PlayerTurn();

    }

    private void PlayerWin()
    {
        GameWinPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PlayerLose()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}