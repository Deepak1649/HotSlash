using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float health = 100f;
    public Animator anim;

    public enum State
    {
        Idle = 0,
        Move = 1,
        Attack = 2,
        Death = 3,
    }

    public State playerState;

    public Enemy target;

    public Manager manager;

    private void Start()
    {
        manager = FindObjectOfType<Manager>();
    }

    public void Update()
    {
        currentPlayerState();
    }

    private void currentPlayerState()
    {
       switch(playerState)
        {
            case State.Idle:
                    
                break; 
            case State.Move:

                break;
            case State.Attack:

                break;
            case State.Death:

                break;
        } 
    }

    public void Move()
    {
        target.transform.LookAt(base.transform.position);

        anim.SetTrigger("attack");
    }

    public void MoveComplete()
    {
        manager.EnemyTurn();
    }

    public void PlayerTurn()
    {
        if(playerState != State.Death)
        {
            playerState = State.Idle;
        }
        
        
    }

    public void PlayerDamaged()
    {
        if(playerState != State.Death)
        {
            if(health <= 0)
            {
                playerState = State.Death;
                anim.SetTrigger("death");
                manager.PlayerLose();
            }
        }
    }

}
