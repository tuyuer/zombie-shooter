using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAutoAim : MonoBehaviour
{
    public float rotateSpeed = 10.0f;

    private List<GameObject> insightEnemys = new List<GameObject>();
    private AiController lockedEnemy = null;

    private float leftTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lockedEnemy = GetClosestEnemyInSight();

        if (lockedEnemy != null)
        {
            if (lockedEnemy.IsDeath() ||
                !IsEnemyInSight(lockedEnemy.gameObject))
            {
                lockedEnemy = null;
                return;
            }

            RotateToTarget(lockedEnemy.transform);
        }
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

    private AiController GetClosestEnemyInSight()
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

    private bool IsEnemyInSight(GameObject enemyObj)
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

    private void RotateToTarget(Transform target)
    {
        if (!IsRunning())
        {
            return;
        }

        Vector3 posOffset = target.position - transform.position;
        posOffset.y = 0;

        transform.forward = Vector3.Lerp(transform.forward, posOffset, Time.deltaTime * rotateSpeed);
    }

    public bool IsRunning()
    {
        if (leftTime < 0)
        {
            return false;
        }

        return true;
    }

    public void StartAutoAim()
    {
        leftTime = 10f;
    }
}
