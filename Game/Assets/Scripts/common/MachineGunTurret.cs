using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunTurret : MonoBehaviour
{
    public float weaponRotateSpeed = 1;

    public Weapon weapon;

    private List<GameObject> insightEnemys = new List<GameObject>();
    private AiController lockedEnemy = null;

    void Update()
    {
        if (lockedEnemy == null)
        {
            lockedEnemy = GetClosestEnemyInSight();
        }

        if (lockedEnemy != null)
        {
            if (lockedEnemy.IsDeath() ||
                !IsEnemyInSight(lockedEnemy.gameObject))
            {
                lockedEnemy = null;
                return;
            }

            RotateToTarget(lockedEnemy.transform);
            CheckAndShoot();
        }
    }

    void OnTriggerEnter(Collider other)
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

    void OnTriggerStay(Collider other)
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

    void OnTriggerExit(Collider other)
    {
        if (insightEnemys.Contains(other.gameObject))
        {
            insightEnemys.Remove(other.gameObject);
        }
    }

    AiController GetClosestEnemyInSight()
    {
        float closestDistance = GlobalDef.MAX_INT_VALUE;
        foreach (var item in insightEnemys)
        {
            float distanceSqr = (transform.position - item.transform.position).sqrMagnitude;
            if (distanceSqr < closestDistance)
            {
                closestDistance = distanceSqr;
                return item.GetComponent<AiController>();
            }
        }

        return null;
    }

    bool IsEnemyInSight(GameObject enemyObj)
    {
        foreach (var item in insightEnemys)
        {
            if (item == enemyObj)
            {
                return true;
            }
        }
        return false;
    }


    void RotateToTarget(Transform target)
    {
        Vector3 targetPos = target.position;
        targetPos.y = weapon.transform.position.y;
        Vector3 forwardDir = targetPos - weapon.transform.position;
        weapon.transform.forward = Vector3.Lerp(weapon.transform.forward, forwardDir, Time.deltaTime * weaponRotateSpeed);
    }

    void CheckAndShoot()
    {
        if (weapon.IsEnemyFront)
        {
            weapon.Shoot();
        }
    }
}
