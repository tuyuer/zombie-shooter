using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectPool : MonoBehaviour
{
    [HideInInspector]
    public List<PoolObject> poolObjects = new List<PoolObject>();

    public simple_object_pool_type poolType;
    public GameObject poolObjectPrefab = null;

    public GameObject FetchObject()
    {
        foreach (var poolObj in poolObjects)
        {
            if (!poolObj.gameObject.activeSelf)
            {
                poolObj.Reset();
                return poolObj.gameObject;
            }
        }

        if (poolObjectPrefab != null)
        {
            GameObject clonedObj = Instantiate(poolObjectPrefab);
            clonedObj.transform.parent = transform;
           
            PoolObject poolObj = clonedObj.GetComponent<PoolObject>();
            poolObjects.Add(poolObj);
            poolObj.onObjectDestroyed += OnObjectDestroyed;
            return clonedObj;
        }

        return null;
    }

    void OnObjectDestroyed(PoolObject poolObj)
    {
        poolObj.gameObject.SetActive(false);
    }
}
