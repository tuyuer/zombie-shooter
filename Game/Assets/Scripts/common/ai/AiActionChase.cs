using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiActionChase : AiActionBase
{
    public float chaseSpeed = 3.0f;
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
        aiController.animator.SetFloat(AnimatorParameter.ForwardSpeed, 1.0f);
        if (aiController.enemySense.DetectTargetInAttackField(targetTrans))
        {
            aiController.OnTargetEnterAttackArea();
        }
        else
        {
            aiController.transform.LookAt(targetTrans);
            Vector3 moveDir = (targetTrans.position - aiController.transform.position).normalized;
            aiController.characterController.Move(moveDir * chaseSpeed * Time.deltaTime);
        }
    }
}
