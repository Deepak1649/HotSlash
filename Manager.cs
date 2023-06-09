using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Manager : MonoBehaviour
{
    public Player player;
    public Enemy[] enemies;


    private void Start()
    {
        enemies = Resources.FindObjectsOfTypeAll(typeof(Enemy)) as Enemy[];
    }
}
