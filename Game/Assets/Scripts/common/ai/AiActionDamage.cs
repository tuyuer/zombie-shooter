using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiActionDamage : AiActionBase
{
    public float handleTime = 1.0f;
    private float handleElapsed = 0f;

    private NavMeshAgent navAgent;

    void Awake()
    {
        actionType = ai_action_type.ai_action_type_damage;
        navAgent = GetComponent<NavMeshAgent>();
    }

    public override void OnEnter()
    {
        base.OnEnter();

        navAgent.speed = 0f;
        navAgent.isStopped = true;

        int nIndex = Random.Range(0, 1);
        aiController.animator.SetTrigger("Damage" + nIndex);
        aiController.actorMouth.PlayDamageSpeek();
        handleElapsed = 0f;
    }

    public override void OnUpdate()
    {
        handleElapsed += Time.deltaTime;
        if (handleElapsed > handleTime)
        {
            aiController.OnDamageEnd();
        }
    }
}
