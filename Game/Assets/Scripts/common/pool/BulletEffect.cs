using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : PoolObject
{
    public float speed = 100;
    public AudioClip[] hitSounds;

    private Rigidbody rigidbody;
    private TrailRenderer trailRender;

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
        //play hit sound
        if (hitSounds.Length > 0)
        {
            GameWorld.Instance.PlaySound(transform.position, hitSounds[Random.Range(0, hitSounds.Length - 1)]);
        }

        //add blood
        GameWorld.Instance.SpawnBlood(transform.position, Quaternion.Inverse(transform.rotation));

        //target damage
        AiController aiController = collision.gameObject.GetComponent<AiController>();
        if (aiController != null)
            aiController.OnAttackedByBullet();

        //destroy
        DestroySelf();
    }

    public override void DestroySelf()
    {
        base.DestroySelf();
        trailRender.Clear();
    }
}
