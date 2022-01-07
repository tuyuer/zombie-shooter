using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiActionPatrol : AiActionBase
{
    [Range(0.1f, 1)]
    public float animatorSpeed = 0.8f;

    [Range(0.1f, 5)]
    public float moveSpeed = 2;

    [Range(1f, 5f)]
    public float obstacleCheckDistance = 3f;

    public LayerMask obstacleLayers;

    private float patrolTime = 0.0f;
    private float nextPatrolTime = 0.0f;

    private Vector3 moveDir = Vector3.zero;
    void Awake()
    {
        actionType = ai_action_type.ai_action_type_patrol;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        BuildPatrol();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        if (CheckObstacle())
        {
            ChangeMoveDir();
            return;
        }

        Transform playerTrans = aiController.enemySense.FindTarget();
        if (playerTrans != null)
        {
            bool isTargetInSense = aiController.enemySense.DetectTargetInSenseView(playerTrans);
            if (!isTargetInSense)
            {
                OnPatrol();
            }
            else
            {
                aiController.OnTargetEnterSenseArea();
            }
        }
        else
        {
            OnPatrol();
        }
    }

    private void OnPatrol()
    {
        patrolTime += Time.deltaTime;
        aiController.animator.SetFloat(AnimatorParameter.ForwardSpeed, animatorSpeed);
        aiController.characterController.Move(moveDir * moveSpeed * Time.deltaTime);

        if (patrolTime > nextPatrolTime)
            BuildPatrol();
    }
    
    private void BuildPatrol()
    {
        patrolTime = 0f;
        nextPatrolTime = Random.Range(3f, 8f);
        moveDir = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        moveDir.Normalize();
        transform.forward = moveDir;
    }

    private void ChangeMoveDir()
    {
        patrolTime = 0f;
        nextPatrolTime = Random.Range(3f, 8f);
        moveDir = new Vector3(-moveDir.x, 0, -moveDir.z);
        moveDir.Normalize();
        transform.forward = moveDir;
    }

    private bool CheckObstacle()
    {
        Vector3 originPos = transform.position + Vector3.up;
        RaycastHit hitInfo;
        if (Physics.Raycast(originPos, moveDir, out hitInfo, obstacleCheckDistance, obstacleLayers))
        {
            return true;
        }
        return false;
    }
}
