using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiActionDamage : AiActionBase
{
    public float handleTime = 1.0f;
    private float handleElapsed = 0f;
    void Awake()
    {
        actionType = ai_action_type.ai_action_type_damage;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        int nIndex = Random.Range(0, 1);
        aiController.animator.SetTrigger("Damage" + nIndex);
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
