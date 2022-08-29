using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAutoPatrolWithPoints : MonoBehaviour
{
    public float moveTime = 5;
    public GameObject[] patrolPoints;

    private float elapsedTime = 0;
    private Vector3 startPos = Vector3.zero;
    private GameObject targetPoint = null;

    // Start is called before the first frame update
    void Start()
    {
        this.NextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime > moveTime)
        {
            elapsedTime = 0;
            this.NextPoint();
            return;
        }

        if (targetPoint != null)
        {
            transform.position = Vector3.Lerp(startPos, targetPoint.transform.position, elapsedTime / moveTime);
        }
    }

    void NextPoint()
    {
        startPos = transform.position;
        int nIndex = Random.Range(0, patrolPoints.Length);
        if(nIndex < patrolPoints.Length)
        {
            GameObject nextTarget = patrolPoints[nIndex];
            if(nextTarget == targetPoint)
            {
                this.NextPoint();
                return;
            }
            targetPoint = nextTarget;
        }
    }
}
