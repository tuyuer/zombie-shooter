using UnityEngine;
using System.Collections;

public class UIInstance : MonoBehaviour
{
    private int _instanceId = 10000000;
    public int InstanceId
    {
        get { return _instanceId; }
    }


    void Awake()
    {
        _instanceId++;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
