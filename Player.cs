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
        Death = 2,
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

    private void Start()
    {
        manager = FindObjectOfType<Manager>();
        cam = Camera.main;
        linePosition = new Vector3[2];
        linePosOffset = Vector3.up * .5f;
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
        }
    }

    private void currentPlayerState(State state)
    {
        playerState = state;

    }

    public void Move(Transform SelectedTarget)
    {
        target.transform.LookAt(base.transform.position);
        base.transform.position = Vector3.MoveTowards(base.transform.position, SelectedTarget.position, 2f);
        base.transform.position = SelectedTarget.transform.position;
      
        // anim.SetTrigger("attack");
        Debug.Log("Called");
        MoveComplete();
    }

    public void MoveComplete()
    {
        manager.EnemyTurn();
        target.EnemyDamaged();
        
    }

    public void PlayerTurn()
    {
        if (playerState != State.Death)
        {
            currentPlayerState(State.Idle);
        }


    }

    public void PlayerDamaged()
    {
        if (playerState != State.Death)
        {
            if (health <= 0)
            {
                currentPlayerState(State.Death);
                anim.SetTrigger("death");
                manager.PlayerLose();
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
        lineRenderer.enabled = false;
        if ((bool)target)
        {
            Move(target.transform);
            currentPlayerState(State.Move);
        }
    }

    private void DragUpdate()
    {
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
                lineTargetPos = position + normalized;
                highlight.transform.position = position + linePosOffset;
                highlight.SetActive(true);
            }
        }
        linePosition[0] = base.transform.position + linePosOffset;
        linePosition[1] = linePosOffset + lineTargetPos;
        lineRenderer.SetPositions(linePosition);
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