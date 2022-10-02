using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiActionIdle : AiActionBase
{
    void Awake()
    {
        actionType = ai_action_type.ai_action_type_idle;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Idle();
    }

    public override void OnUpdate()
    {
        Transform playerTrans = aiController.enemySense.FindTarget();
        if (playerTrans != null)
        {
            bool isTargetInSense = aiController.enemySense.DetectTargetInSenseView(playerTrans);
            if (!isTargetInSense)
            {
                aiController.OnTargetOutSenseArea();
            }
            else
            {
                aiController.OnTargetEnterSenseArea();
            }
        }
        else
        {
            aiController.OnTargetOutSenseArea();
        }
    }

    private void Idle()
    {
        aiController.animator.SetFloat(AnimatorParameter.ForwardSpeed, 0f);
    }
}
