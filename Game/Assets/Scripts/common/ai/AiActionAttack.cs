using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiActionAttack : AiActionBase
{
    public float coldTime = 2.0f;
    private float coldElapsed = 0f;

    private NavMeshAgent navAgent;

    void Awake()
    {
        coldElapsed = coldTime;
        actionType = ai_action_type.ai_action_type_attack;
        navAgent = GetComponent<NavMeshAgent>();
    }

    public override void OnEnter()
    {
        base.OnEnter();

        navAgent.speed = 0f;
        navAgent.isStopped = true;
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
            coldElapsed = 0;
            aiController.transform.LookAt(targetTrans);
            aiController.animator.SetTrigger(AnimatorParameter.AttackO);
            Utils.Instance.PerformFunctionWithDelay(delegate ()
            {
                aiController.OnAttackTarget(targetTrans);
            }, coldTime / 2);
        }
    }
}
