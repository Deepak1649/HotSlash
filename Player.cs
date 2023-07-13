using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public float speed;
    public float health = 100f;
    public Animator anim;

    public enum State
    {
        Idle = 0,
        Move = 1,
        Death = 2,
        Menu = 3,
    }

    public State playerState;

    public Enemy target;

    public Manager manager;

    public Camera cam;

    public bool isDragging;

    public LineRenderer lineRenderer;

    public GameObject highlight;

    public Vector3[] linePosition;
    public Vector3 linePosOffset;
    public Vector3 lineTargetPos;

    public LayerMask GroundMask;
    public LayerMask selectionMask;

    private Vector3 killedByLookPos;
    private Player player;
    public int attackCount= 6;

    private void Start()
    {
        manager = FindObjectOfType<Manager>();
        player = GetComponent<Player>();
        cam = Camera.main;
        linePosition = new Vector3[2];
        //linePosOffset = Vector3.up * .05f;
    }

    public void Update()
    {
        highlight.SetActive(false); 
        switch (playerState)
        {
            case State.Idle:
                OnIdle();
                break;
            case State.Move:

                break;
            
            case State.Death:

                break;
            case State.Menu:

                break;
        }
    }

    private void currentPlayerState(State state)
    {
        playerState = state;
        if(playerState == State.Move)
        {
            manager.PlayParticles();
        }else{
            manager.PauseParticles();
        }

    }

    public void Move(Transform SelectedTarget)
    {
        target.transform.LookAt(base.transform.position);
        base.transform.LookAt(target.transform.position);
        anim.SetTrigger("Move");

        //base.transform.position = Vector3.MoveTowards(base.transform.position, SelectedTarget.transform.position , Time.deltaTime * 4);
        //base.transform.position = SelectedTarget.transform.position;
        iTween.MoveTo(base.gameObject, iTween.Hash("position", SelectedTarget.position,"speed",7f,"oncomplete","MoveComplete","easetype",iTween.EaseType.linear));
        
        Debug.Log("Move Called");  
    }

   
    public void KilledBy(Enemy enemyObject)
    {
        killedByLookPos = enemyObject.transform.position;
    }

    public void MoveComplete()
    {
        
        Attack();
        target.EnemyKilled();
        manager.EnemyTurn();
        
        
    }

    public void Attack(){
        int num = Random.Range(0,attackCount-1);
        anim.SetTrigger("Attack" + num);
        manager.camerashake(1f);
    }

    public void PlayerTurn()
    {if (manager.EnemyCounter() == 0)
        {
            manager.PlayerWin();
        }
        target = null;
        if (playerState != State.Death)
        {
            anim.SetTrigger("Idle");
            currentPlayerState(State.Idle);
        }


    }

    public void PlayerDamaged()
    {
        if (playerState != State.Death)
        {
            health -= 100f;
            if (health <= 0)
            {
                currentPlayerState(State.Death);
                anim.SetTrigger("Death0");
                Debug.Log("Dead");
                manager.PlayerLose();
                base.transform.LookAt(killedByLookPos);
                iTween.MoveBy(base.gameObject, iTween.Hash("z", -1.5f, "time", 0.5f, "islocal", true));
                
            }
        }
    }



    private void StartDrag()
    {
        isDragging = true;
        lineRenderer.enabled = true;
    }

    private void StopDrag()
    {
        isDragging = false;
        
        if ((bool)target)
        {
            Move(target.transform.GetChild(0).gameObject.transform);
            currentPlayerState(State.Move);
        }
        lineRenderer.enabled = false;
    }

//this is not on update how it is called as an update
    private void DragUpdate()
    {
        
        target = null;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100f, GroundMask))
        {
            lineTargetPos = hitInfo.point;
            
            Enemy component = hitInfo.transform.GetComponent<Enemy>();
            if ((bool)component)
            {
                target = component;
                Vector3 position = target.transform.position;
                Vector3 normalized = (base.transform.position - position).normalized;
                lineTargetPos = position;
                highlight.transform.position = position + linePosOffset;
                highlight.SetActive(true);
               
            }
        }
        linePosition[0] = base.transform.GetChild(2).transform.position ;
        // Debug.Log(base.transform.GetChild(2).transform.localPosition);
        Debug.Log(lineTargetPos);
        linePosition[1] = lineTargetPos;
        //base.transform.LookAt(lineTargetPos);
        lineRenderer.SetPositions(linePosition);
        // lineRenderer.useWorldSpace = false;
        lineRenderer.enabled = true;

    }

    private void OnIdle()
    {
        if (!Input.GetMouseButton(0))
        {
            StopDrag();
            return;
        }
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, 100f, selectionMask) && (bool)hitInfo.transform.GetComponent<Player>() && !isDragging)
        {
            StartDrag();
        }
        if (isDragging)
        {
            DragUpdate();
        }
    }


}