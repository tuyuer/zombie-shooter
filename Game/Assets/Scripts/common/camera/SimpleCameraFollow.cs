using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    public Transform followTarget;
    public float followSpeed = 10.0f;

    private Vector3 posOffset = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position - followTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, followTarget.position + posOffset, Time.deltaTime * followSpeed);
    }
}
