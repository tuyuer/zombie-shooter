using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnPoint : MonoBehaviour
{
    public GameObject objectToSpawn;
    public int spawnTimes = 10;
    public int maxAliveCount = 20;
    public float spawnColdTime = 2.0f;
    private float spawnElapsedTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnElapsedTime = spawnColdTime;
    }

    // Update is called once per frame
    void Update()
    {
        spawnElapsedTime += Time.deltaTime;
        if (spawnElapsedTime > spawnColdTime)
        {
            SpawnObject();
            spawnElapsedTime = 0f;
        }
    }

    private void SpawnObject()
    {
        if (objectToSpawn != null)
        {
            AiController[] childrenComps = GetComponentsInChildren<AiController>();
            int nAliveCount = 0;
            foreach (var aiController in childrenComps)
            {
                if (!aiController.IsDeath())
                {
                    nAliveCount++;
                }
            }
            if (nAliveCount > maxAliveCount)
                return;
            
            GameObject clonedObj = Instantiate(objectToSpawn);
            clonedObj.transform.parent = transform;
            clonedObj.transform.position = transform.position;
        }
    }

    private void OnDrawGizmos()
    {
        Color originColor = Gizmos.color;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
        Gizmos.color = originColor;
    }
}
