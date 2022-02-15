using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiActionChase : AiActionBase
{
    public float chaseSpeed = 3.0f;
    public float nearByChaseSpeed = 6.0f;
    void Awake()
    {
        actionType = ai_action_type.ai_action_type_chase;
    }

    public override void OnUpdate()
    {
        Transform playerTrans = aiController.enemySense.FindTarget();
        if (playerTrans != null)
        {
            bool isTargetInSense = aiController.enemySense.DetectTargetInSenseView(playerTrans);
            if (isTargetInSense)
            {
                ChaseTarget(playerTrans);
            }
            else
            {
                aiController.OnTargetOutSenseArea();
            }
        }
    }

    private void ChaseTarget(Transform targetTrans)
    {
        if (aiController.enemySense.DetectTargetInAttackField(targetTrans))
        {
            aiController.animator.SetFloat(AnimatorParameter.ForwardSpeed, 0f);
            aiController.OnTargetEnterAttackArea();
        }
        else
        {
            aiController.animator.SetFloat(AnimatorParameter.ForwardSpeed, 1.0f);
            aiController.transform.LookAt(targetTrans);
            Vector3 moveDir = (targetTrans.position - aiController.transform.position).normalized;

            float moveSpeed = chaseSpeed;
            if (aiController.enemySense.DetectTargetNearBy(targetTrans))
            {
                moveSpeed = nearByChaseSpeed;
            }

            aiController.characterController.Move(moveDir * moveSpeed * Time.deltaTime);
        }
    }
}
