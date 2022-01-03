using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiActionDeath : AiActionBase
{
    public float handleTime = 1.0f;
    private float handleElapsed = 0f;
    void Awake()
    {
        actionType = ai_action_type.ai_action_type_death;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        int nIndex = Random.Range(0, 3);
        aiController.animator.SetTrigger("Death" + nIndex);
        handleElapsed = 0f;
    }

    public override void OnUpdate()
    {

    }
}
