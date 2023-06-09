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

    public Enemy target;

    public Manager manager;

    private void Start()
    {
        manager = FindObjectOfType<Manager>();
    }
}
