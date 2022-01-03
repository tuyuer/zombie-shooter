using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiActionIdle : AiActionBase
{
    void Awake()
    {
        actionType = ai_action_type.ai_action_type_idle;
    }

    public override void OnUpdate()
    {
        Transform playerTrans = aiController.enemySense.FindTarget();
        if (playerTrans != null)
        {
            bool isTargetInSense = aiController.enemySense.DetectTargetInSenseView(playerTrans);
            if (!isTargetInSense)
            {
                Idle();
            }
            else
            {
                aiController.OnTargetEnterSenseArea();
            }
        }
    }

    private void Idle()
    {
        aiController.animator.SetFloat(AnimatorParameter.ForwardSpeed, 0f);
    }
}
