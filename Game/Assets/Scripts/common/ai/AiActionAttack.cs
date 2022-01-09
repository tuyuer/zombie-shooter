using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiActionAttack : AiActionBase
{
    public float coldTime = 2.0f;
    private float coldElapsed = 0f;
    void Awake()
    {
        coldElapsed = coldTime;
        actionType = ai_action_type.ai_action_type_attack;
    }

    public override void OnUpdate()
    {
        coldElapsed += Time.deltaTime;

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
                if (!stateInfo.IsName(AnimatorStateName.AttackX) &&
                    !stateInfo.IsName(AnimatorStateName.AttackO) &&
                    coldElapsed > coldTime)
                {
                    aiController.OnTargetOutAttackArea();
                }
            }
        }
    }

    private void AttackTarget(Transform targetTrans)
    {
        if (coldElapsed > coldTime)
        {
            aiController.transform.LookAt(targetTrans);
            aiController.animator.SetTrigger(AnimatorParameter.AttackO);
            coldElapsed = 0;
        }

        //apply attack
        if (coldElapsed < coldTime / 2 &&
            coldElapsed + Time.deltaTime > coldTime / 2)
        {
            aiController.OnAttackTarget(targetTrans);
        }
    }
}
