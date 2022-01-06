using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HitJoy;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Brain))]
[RequireComponent(typeof(EnemySense))]
[RequireComponent(typeof(ActorMouth))]

public class AiController : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;
    public Brain brain;
    public EnemySense enemySense;
    public ActorMouth actorMouth;

    public float blood = 100.0f;
    public float thinkingTime = 1.0f;
    private float thinkingElapsed = 0.0f;

    private List<AiActionBase> actionList = new List<AiActionBase>();
    private ai_action_type curActionType = ai_action_type.ai_action_type_idle;

    private const float thinking_type_immediately = 100.0f;
    private const float thinking_type_wait = 0.0f;

    public ai_action_type CurActionType
    {
        set { curActionType = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        actionList.Clear();
        actionList.AddRange(GetComponents<AiActionBase>());
        foreach (var aiAction in actionList)
        {
            aiAction.SetController(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        thinkingElapsed += Time.deltaTime;
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

        if (thinkingElapsed > thinkingTime)
            actionList[(int)curActionType].OnUpdate();
    }

    public void OnTargetEnterSenseArea()
    {
        CurActionType = ai_action_type.ai_action_type_chase;
        thinkingElapsed = thinking_type_immediately;
    }

    public void OnTargetOutSenseArea() 
    {
        CurActionType = ai_action_type.ai_action_type_idle;
        thinkingElapsed = thinking_type_immediately;
    }

    public void OnTargetEnterAttackArea()
    {
        CurActionType = ai_action_type.ai_action_type_attack;
        thinkingElapsed = thinking_type_wait;
    }

    public void OnTargetOutAttackArea()
    {
        CurActionType = ai_action_type.ai_action_type_idle;
        thinkingElapsed = thinking_type_wait;
    }

    public void OnAttackedByBullet()
    {
        blood -= 20;
        if (blood > 0)
        {
            CurActionType = ai_action_type.ai_action_type_damage;
            thinkingElapsed = thinking_type_immediately;
        }
        else
        {
            CurActionType = ai_action_type.ai_action_type_death;
            thinkingElapsed = thinking_type_immediately;
        }
    }

    public void OnDamageEnd()
    {
        CurActionType = ai_action_type.ai_action_type_idle;
        thinkingElapsed = thinking_type_immediately;
    }

    public void OnDeath()
    {
        actorMouth.PlayDeathSpeek();
        GameWorld.Instance.PlayKilledSoundEffect(transform.position);
        gameObject.layer = LayerMask.NameToLayer(LayerNames.Death);
    }

    public bool IsDeath()
    {
        return curActionType == ai_action_type.ai_action_type_death;
    }
}
