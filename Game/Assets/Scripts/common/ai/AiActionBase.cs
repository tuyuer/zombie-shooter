using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ai_action_type
{
    ai_action_type_idle,
    ai_action_type_chase,
    ai_action_type_attack,
};

public enum ai_action_state
{
    ai_action_state_running,
    ai_action_state_stopped,
};


public class AiActionBase
{
    protected AiController aiController;

    protected ai_action_type actionType = ai_action_type.ai_action_type_idle;
    public ai_action_type ActionType
    {
        get { return actionType; }
    }

    protected ai_action_state actionState = ai_action_state.ai_action_state_stopped;
    public ai_action_state ActionState
    {
        get { return actionState; }
        set { actionState = value; }
    }

    public AiActionBase() 
    {
        
    }

    public void SetController(AiController ac)
    {
        aiController = ac;
    }


    public virtual void OnEnter()
    {
        ActionState = ai_action_state.ai_action_state_running;
    }

    public virtual void OnExit() 
    {
        ActionState = ai_action_state.ai_action_state_stopped;
    }

    public virtual void OnUpdate() 
    { 

    }
}
