using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;
    public float health = 100f;
    public Animator anim;
    private float moveDistance = 8f;
    private float attackDistance = 4f;
    private float moveSpeed = .3f;
    private Manager gameManager;
    private Collider enemyCollider;

    public enum state
    {
        Idle = 0,
        Move = 1,
        Attack = 2,
        Death = 3,
    }

    public enum type
    {
        Samurai = 0,
        Ninja = 1,

    }

    public state EnemyState;
    public type EnemyType;


    private void Awake()
    {
        player = UnityEngine.Object.FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
        base.transform.LookAt(player.transform.position);
        gameManager = GetComponent<Manager>();

    }

    private void Update()
    {

    }


    public void EnemyMove()
    {
        if(player.playerState == Player.State.Death)
        {
            return;
        }

        if (EnemyState == state.Death)
        {
            EnemyKilled();
            return;
        }
       


        float dist = Vector3.Distance(base.transform.position, player.transform.position);
        if (dist < attackDistance)
        {
            attack();
        }
        else if (dist < moveDistance)
        {
            move();
        }
    }

    private void attack()
    {
        base.transform.LookAt(player.transform.position);
        base.transform.position = Vector3.MoveTowards(base.transform.position, player.transform.position, Time.deltaTime);
        // anim.SetTrigger("attack");
        Debug.Log("Enemy Attack called");
        player.PlayerDamaged();

    }

    public void EnemyDamaged()
    {
        EnemyState = state.Death;

    }

    private void move()
    {
        base.transform.position = Vector3.MoveTowards(base.transform.position, player.transform.position, 4f);
        Debug.Log("Enemy move called");

    }

    public void EnemyKilled()
    {

    }

}