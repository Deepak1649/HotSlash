using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;
    public float health;
    public Animator anim;
    private float moveDistance =8f;
    private float attackDistance = 4f;
    private float moveSpeed= .3f;
    


    private void Awake()
    {
        player = FindObjectOfType(typeof(Player)) as Player;
        anim = GetComponent<Animator>();
        base.transform.LookAt(player.transform.position);

    }

    private void Update()
    {

        EnemyMove();
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
        iTween.MoveBy(base.gameObject, iTween.Hash("z", moveSpeed, "time", 0.2f));
    }

   
}
