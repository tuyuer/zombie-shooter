using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunTurret : MonoBehaviour
{
    public float weaponRotateSpeed = 1;

    public Weapon weapon;

    private List<GameObject> insightEnemys = new List<GameObject>();
    private GameObject lockedEnemy = null;

    void Update()
    {
        if (!IsEnemyInSight(lockedEnemy))
        {
            lockedEnemy = GetClosestEnemyInSight();
            if (lockedEnemy != null)
            {
                RotateToTarget(lockedEnemy.transform);
            }
        }
        weapon.Shoot();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagDef.Enemy)
        {
            insightEnemys.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == TagDef.Enemy)
        {
            insightEnemys.Remove(other.gameObject);
        }
    }

    GameObject GetClosestEnemyInSight()
    {
        GameObject retObj = null; ;
        float closestDistance = GlobalDef.MAX_INT_VALUE;
        foreach (var item in insightEnemys)
        {
            float distanceSqr = (transform.position - item.transform.position).sqrMagnitude;
            if (distanceSqr < closestDistance)
            {
                closestDistance = distanceSqr;
                retObj = item;
            }
        }

        return retObj;
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
}
