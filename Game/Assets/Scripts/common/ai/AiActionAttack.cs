using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiActionAttack : AiActionBase
{
    void Awake()
    {
        actionType = ai_action_type.ai_action_type_attack;
    }

    public override void OnUpdate()
    {
        Transform playerTrans = aiController.enemySense.FindTarget();
        if (playerTrans != null)
        {
            bool isInAttackArea = aiController.enemySense.DetectTargetInAttackField(playerTrans);
            if (isInAttackArea)
            {
                AttackTarget(playerTrans);
            }
            else
            {
                AnimatorStateInfo stateInfo = aiController.animator.GetCurrentAnimatorStateInfo(0);
                if (stateInfo.IsName(AnimatorStateName.Locomotion))
                {
                    aiController.OnTargetOutAttackArea();
                }
            }
        }
    }

    private void AttackTarget(Transform targetTrans)
    {
        aiController.transform.LookAt(targetTrans);
        aiController.animator.SetTrigger(AnimatorParameter.AttackO);
    }
}
