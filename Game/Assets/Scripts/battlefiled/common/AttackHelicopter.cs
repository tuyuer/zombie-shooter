using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HitJoy;

public enum attack_helicopter_state
{
    attack_helicopter_state_waitting,
    attack_helicopter_state_attack,
};


public class AttackHelicopter : MonoBehaviour
{
    public float speed = 1.0f;
    public float rotateSpeed = 2.0f;
    public float flyHeight = 4.0f;
    public bool enableSound = true;
    public AudioSource sound;
    public Weapon[] weaponList;

    private attack_helicopter_state state = attack_helicopter_state.attack_helicopter_state_waitting;
    private List<GameObject> insightEnemys = new List<GameObject>();

    private Vector3 attackPos;
    private Vector3 waittingPos;

    private static int callCount = 0;
    private float leftAngle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        InitWaittingPosition();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case attack_helicopter_state.attack_helicopter_state_attack:
                InAttackState();
                break;
            case attack_helicopter_state.attack_helicopter_state_waitting:
                InWaittingState();
                break;
            default:
                break;
        }

        float soundVolume = enableSound ? 0.2f : 0.0f;
        sound.volume = soundVolume;
    }

    private void OnEnable()
    {
        MessageCenter.AddMessageObserver(this, NotificationDef.NOTIFICATION_ON_CALL_AIR_FORCE, new MessageEvent(OnCallAirForce));
        MessageCenter.AddMessageObserver(this, NotificationDef.NOTIFICATION_ON_EXIT_AIR_FORCE, new MessageEvent(OnExitAirForce));
    }

    private void OnDisable()
    {
        MessageCenter.RemoveAllObservers(this);
    }

    private void OnCallAirForce(System.Object data)
    {
        float delayTime = callCount % 2 == 0 ? 0.01f : 1.0f;
        callCount++;

        Utils.Instance.PerformFunctionWithDelay(delegate ()
        {
            state = attack_helicopter_state.attack_helicopter_state_attack;
            leftAngle = Random.Range(360, 720);
            InitWaittingPosition();
            CalculateAttackPosition();
            enableSound = true;
        }, delayTime);
    }

    private void OnExitAirForce(System.Object data) 
    {
        state = attack_helicopter_state.attack_helicopter_state_waitting;
        CalculateWaittingPosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagDef.Enemy)
        {
            AiController aiController = other.gameObject.GetComponent<AiController>();
            if (!insightEnemys.Contains(other.gameObject) &&
                !aiController.IsDeath())
            {
                insightEnemys.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == TagDef.Enemy)
        {
            AiController aiController = other.gameObject.GetComponent<AiController>();
            if (aiController.IsDeath())
            {
                insightEnemys.Remove(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (insightEnemys.Contains(other.gameObject))
        {
            insightEnemys.Remove(other.gameObject);
        }
    }

    private void InitWaittingPosition() 
    {
        float randomX = Mathf.Sign(UnityEngine.Random.Range(-100, 100)) * UnityEngine.Random.Range(10, 60);
        float randomZ = Mathf.Sign(UnityEngine.Random.Range(-100, 100)) * UnityEngine.Random.Range(10, 60);
        waittingPos = new Vector3(randomX, flyHeight, randomZ);
        transform.position = waittingPos;
    }

    private void CalculateAttackPosition() 
    {
        Vector3 targetPos = GameWorld.Instance.player.transform.position;
        targetPos.y = flyHeight;
        attackPos = targetPos;
    }

    private void CalculateWaittingPosition() 
    {
        waittingPos = transform.position + transform.forward * 60f;
    }

    private void MoveToAttackPosition()
    {
        transform.position = Vector3.Lerp(transform.position, attackPos, Time.deltaTime * speed);
        transform.forward = attackPos - transform.position;
    }

    private void MoveToWaittingPosition() 
    {
        transform.position = Vector3.Lerp(transform.position, waittingPos, Time.deltaTime * speed * 0.1f);
    }

    private bool IsNearDestination() 
    {
        if (state == attack_helicopter_state.attack_helicopter_state_attack)
        {
            if ((transform.position - attackPos).sqrMagnitude < 0.1f)
            {
                return true;
            }
        }
        else if (state == attack_helicopter_state.attack_helicopter_state_waitting)
        {
            if ((transform.position - waittingPos).sqrMagnitude < 1600.0f)
            {
                return true;
            }
        }
        return false;
    }

    public void InAttackState()
    {
        if (IsNearDestination())
        {
            if (leftAngle > 0)
            {
                transform.Rotate(0, rotateSpeed, 0, Space.World);
                leftAngle -= rotateSpeed;
            }
            else
            {
                OnExitAirForce(null);
            }
        }
        else
        {
            MoveToAttackPosition();
        }

        Shoot();
    }

    public void InWaittingState()
    {
        if (IsNearDestination())
        {
            enableSound = false;
        }
        else
        {
            MoveToWaittingPosition();
            Shoot();
        }
    }

    public bool IsHelicopterReady() 
    {
        if (!enableSound)
        {
            return true;
        }

        return false;
    }

    public void Shoot()
    {
        if (weaponList.Length > 0)
        {
            int nIndex = Random.Range(0, weaponList.Length - 1);
            weaponList[nIndex].Shoot();
        }
    }
}
