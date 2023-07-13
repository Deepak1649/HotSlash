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
    private ParticleSystem[] particleSystems;
    
    public GameObject Camera;

    private void Start()
    {
        uiManager = UnityEngine.Object.FindObjectOfType<UIManager>();
        enemies = UnityEngine.Object.FindObjectsOfType<Enemy>();
        particleSystems = UnityEngine.Object.FindObjectsOfType<ParticleSystem>();
        PauseParticles();
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

    public int NonNinjaEnemyCount()
    {
        List<Enemy> list =new List<Enemy>();
        for (int i=0; i<enemies.Length; i++)
        {
            if(enemies[i].EnemyState!=Enemy.state.Dead && enemies[i].EnemyType == Enemy.type.Samurai)
            {
                Debug.Log(2);
                list.Add(enemies[i]);
            }
        }
        
        return list.Count;  
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

    public void PlayParticles()
    {
        for(int i =0;i<particleSystems.Length; i++){
            particleSystems[i].Play();
        }
    }

    public void PauseParticles()
    {
        for(int i =0;i<particleSystems.Length; i++){
            particleSystems[i].Pause();
        }
    }

    public void camerashake(float _amount){
        iTween.ShakePosition(Camera.gameObject, iTween.Hash("amount", Vector3.one*_amount, "time",0.5f));
    }
}