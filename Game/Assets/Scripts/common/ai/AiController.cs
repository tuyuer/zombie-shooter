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
    public EnemySense enemySense;
    public ActorMouth actorMouth;

    public float blood = 100.0f;
    public float deathDelayRemvoveTime = 30.0f;
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
        AiActionBase[] aiBaseList = GetComponents<AiActionBase>();
        actionList.Clear();

        for (int i = 0; i < (int)ai_action_type.ai_action_type_count; i++)
        {
            for (int j = 0; j < aiBaseList.Length; j++)
            {
                if (i == (int)aiBaseList[j].ActionType)
                {
                    actionList.Add(aiBaseList[j]);
                    break;
                }
            }
        }

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
        {
            actionList[(int)curActionType].OnUpdate();
        }

        LockYPosition();
    }

    private void LockYPosition()
    {
        if (transform.position.y > 0.1f)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

    public void OnTargetEnterSenseArea()
    {
        CurActionType = ai_action_type.ai_action_type_chase;
        thinkingElapsed = thinking_type_immediately;
    }

    public void OnTargetOutSenseArea() 
    {
        CurActionType = ai_action_type.ai_action_type_patrol;
        thinkingElapsed = thinking_type_immediately;
    }

    public void OnTargetEnterAttackArea()
    {
        CurActionType = ai_action_type.ai_action_type_attack;
        thinkingElapsed = thinking_type_immediately;
    }

    public void OnTargetOutAttackArea()
    {
        CurActionType = ai_action_type.ai_action_type_idle;
        thinkingElapsed = thinking_type_wait;
    }

    public void OnAttackTarget(Transform targetTrans) 
    {
        if (enemySense.DetectTargetInArea(targetTrans, 120.0f, 2.0f))
        {
            Character character = targetTrans.GetComponent<Character>();
            if (character != null)
            {
                character.Blood.OnDamage(10);
            }
        }
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
        GameWorld.Instance.killStatistics.KillEnemy(gameObject);

        Destroy(gameObject, deathDelayRemvoveTime);
    }

    public bool IsDeath()
    {
        return curActionType == ai_action_type.ai_action_type_death;
    }
}
