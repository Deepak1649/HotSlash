using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;
    public float health =100f;
    public Animator anim;
    private float moveDistance =8f;
    private float attackDistance = 4f;
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
        float dist = Vector3.Distance(base.transform.position,player.transform.position);
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
        
    }

    private void move()
    {
        base.transform.LookAt(player.transform.position);
        
    }

   
}
