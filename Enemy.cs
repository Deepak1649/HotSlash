using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;
    public float health = 100f;
    public Animator anim;
    public float moveDistance = 8f;
    public float attackDistance = 4f;
    public float moveSpeed = .3f;
    private Manager gameManager;
    private Collider enemyCollider;
    private float moveAmount = 3f;

    [SerializeField]
    public GameObject[] bloodPrefabs;
    public GameObject[] bloodAirPrefabs;
    public GameObject bloodAir;
    public GameObject hitpos;
    public float dist;
    public int killAnim=1;

    public enum state
    {
        Idle = 0,
        Dead = 1,
        Block = 2,
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
        hitpos=base.transform.GetChild(0).gameObject;
        player = UnityEngine.Object.FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
        base.transform.LookAt(player.transform.position);
        gameManager = GetComponent<Manager>();
        enemyCollider = GetComponent<Collider>();
    }


    public void EnemyMove()
    {
        if(player.playerState == Player.State.Death)
        {
            return;
        }

        if (EnemyState == state.Dead)
        {
           
            return;
        }
        if (EnemyState == state.Block)
        {
            EnemyState = state.Idle;
            return;
        }



         dist = Vector3.Distance(base.transform.position, player.transform.position);
        if (dist <= attackDistance)
        {
            attack();
        }
        else if (dist <= moveDistance)
        {
            move();
        }
        else
        {
            anim.SetTrigger("Idle");        
        }
    }

    private void attack()
    {
        base.transform.LookAt(player.transform.position);
        //base.transform.position = player.transform.position - new Vector3(1, 0, 1);
        anim.SetTrigger("Attack");
        iTween.MoveTo(base.gameObject, iTween.Hash("position", player.transform.position, "time", 0.3f, "oncompletetarget", base.gameObject, "oncomplete", "AttackComplete", "easetype", iTween.EaseType.easeInCubic));
        Debug.Log("Enemy Attack called");
        player.KilledBy(this);

    }

    public void AttackComplete()
    {
        player.PlayerDamaged();
        
    }

    public void EnemyDamaged()
    {
        EnemyState = state.Dead;

    }

    private void move()
    {
        base.transform.LookAt(player.transform.position);
        //base.transform.position = Vector3.MoveTowards(base.transform.position, player.transform.position, 4f);
        iTween.MoveBy(base.gameObject, iTween.Hash("z", moveAmount, "time", 1f, "oncomplete", "enemymovecomplete"));
        anim.SetTrigger("Move");
        Debug.Log("Enemy move called");

    }

    private void enemymovecomplete()
    {
        anim.SetTrigger("Idle");
    }

    public void EnemyKilled()
    {
        UpdateKillAnim();
        anim.SetTrigger("Death");
        EnemyState = state.Dead;
        enemyCollider.enabled = false;
        AddBlood();
    }

    private void AddBlood()
    {
        Debug.Log("blood");
         int num = UnityEngine.Random.Range(0, bloodPrefabs.Length);
         GameObject gameObject = UnityEngine.Object.Instantiate(bloodPrefabs[num], base.transform.position, base.transform.rotation);
         gameObject.transform.SetParent(base.transform.parent);
            num = UnityEngine.Random.Range(0, bloodAirPrefabs.Length);
         bloodAir = UnityEngine.Object.Instantiate(bloodAirPrefabs[num], base.transform.position, base.transform.rotation);
         bloodAir.transform.SetParent(base.transform);

         
            }

    private void UpdateKillAnim()
    {
        // if (killAnim ==3)
        // {
        //     Destroy(bloodAir.gameObject);
        // }
        // if (killAnim < 3)
        // {
        //     anim.SetTrigger("kill" + killAnim);
        //     iTween.MoveBy(base.gameObject, iTween.Hash("z", -1.5f, "time", 0.5f, "islocal", true));
        //     killAnim++;
        // }
    }

}