using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolObject
{
    public float speed = 100;
    private Rigidbody rigidbody = null;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
    }

    public void Shoot(Vector3 startPos, Vector3 moveDir)
    {
        transform.position = startPos;
        transform.forward = moveDir;
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(moveDir * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        DestroySelf();
    }
}
