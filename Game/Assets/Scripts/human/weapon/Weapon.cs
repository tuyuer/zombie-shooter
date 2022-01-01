using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HitJoy;
using Opsive.UltimateCharacterController.Objects;

public class Weapon : MonoBehaviour
{
    public Blackboard blackboard;
    public SimpleObjectPool bulletPool = null;
    public Transform spawnPoint = null;
    public MuzzleFlash muzzleFlash = null;
    public AudioSource weaponSound = null;

    public float coldTime = 0.5f;
    private float coldElapsedTime = 0f;
    private bool isReady = true;

    void Update()
    {
        coldElapsedTime += Time.deltaTime;
        if (coldElapsedTime > coldTime)
        {
            isReady = true;
        }
    }

    public void Shoot()
    {
        if (isReady)
        {
            GameObject bulletObj = bulletPool.FetchObject();
            Bullet bullet = bulletObj.GetComponent<Bullet>();

            Vector3 moveDir = blackboard.characterAim.aimTarget.transform.position - spawnPoint.position;

            bullet.Shoot(spawnPoint.position, moveDir);
            muzzleFlash.ShowEffect();
            weaponSound.Play();

            isReady = false;
            coldElapsedTime = 0f;
        }
    }
}
