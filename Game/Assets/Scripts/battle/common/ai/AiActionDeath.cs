using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiActionDeath : AiActionBase
{
    private NavMeshAgent navAgent;

    void Awake()
    {
        actionType = ai_action_type.ai_action_type_death;
        navAgent = GetComponent<NavMeshAgent>();
    }

    public override void OnEnter()
    {
        base.OnEnter();

        navAgent.speed = 0f;
        navAgent.isStopped = true;
        navAgent.enabled = false;

        int nIndex = Random.Range(0, 3);
        aiController.animator.SetTrigger("Death" + nIndex);
        aiController.OnDeath();
    }

    public override void OnUpdate()
    {

    }
}
