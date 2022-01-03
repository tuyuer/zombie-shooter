using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolObject
{
    public float speed = 100;
    private Rigidbody rigidbody = null;
    private TrailRenderer trailRender = null;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        trailRender = GetComponent<TrailRenderer>();
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
        GameWorld.Instance.SpawnBlood(transform.position, Quaternion.Inverse(transform.rotation));
        DestroySelf();
    }

    public override void DestroySelf()
    {
        base.DestroySelf();
        trailRender.Clear();
    }
}
