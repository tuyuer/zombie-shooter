using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void SpawnObject()
    {
        //if (objectToSpawn != null)
        //{
        //    AiController[] childrenComps = GetComponentsInChildren<AiController>();
        //    int nAliveCount = 0;
        //    foreach (var aiController in childrenComps)
        //    {
        //        if (!aiController.IsDeath())
        //        {
        //            nAliveCount++;
        //        }
        //    }
        //    if (nAliveCount > maxAliveCount)
        //        return;
            
        //    GameObject clonedObj = Instantiate(objectToSpawn);
        //    clonedObj.transform.parent = transform;
        //    clonedObj.transform.position = transform.position;
        //}
    }

    private void OnDrawGizmos()
    {
        Color originColor = Gizmos.color;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
        Gizmos.color = originColor;
    }
}
