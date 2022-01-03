using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoBehaviour
{
    public SimpleObjectPool bloodPool = null;

    private static GameWorld _instance = null;

    public static GameWorld Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        _instance = this;
    }

    public void SpawnBlood(Vector3 position, Quaternion rotation)
    {
        GameObject fetchedObj = bloodPool.FetchObject();
        fetchedObj.transform.position = position;
        fetchedObj.transform.rotation = rotation;
    }
}
