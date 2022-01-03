using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HitJoy;

public class AiController : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;
    public Brain brain;
    public EnemySense enemySense;

    private List<AiActionBase> actionList = new List<AiActionBase>();

    private ai_action_type curActionType = ai_action_type.ai_action_type_idle;
    public ai_action_type CurActionType
    {
        set { curActionType = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        actionList.Add(new AiActionIdle());
        actionList.Add(new AiActionChase());
        actionList.Add(new AiActionAttack());

        foreach (var aiAction in actionList)
        {
            aiAction.SetController(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var aiAction in actionList)
        {
            if (aiAction.ActionState == ai_action_state.ai_action_state_running &&
                curActionType != aiAction.ActionType)
            {
                aiAction.OnExit();
            }
        }

        foreach (var aiAction in actionList)
        {
            if (aiAction.ActionState != ai_action_state.ai_action_state_running &&
                curActionType == aiAction.ActionType)
            {
                aiAction.OnEnter();
            }
        }

        actionList[(int)curActionType].OnUpdate();
    }

    public void OnTargetEnterSenseArea()
    {
        CurActionType = ai_action_type.ai_action_type_chase;
    }

    public void OnTargetOutSenseArea() 
    {
        CurActionType = ai_action_type.ai_action_type_idle;
    }

    public void OnTargetEnterAttackArea()
    {
        CurActionType = ai_action_type.ai_action_type_attack;
    }

    public void OnTargetOutAttackArea()
    {
        CurActionType = ai_action_type.ai_action_type_idle;
    }
}
