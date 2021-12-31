using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public SimpleObjectPool bulletPool = null;
    public Transform spawnPoint = null;

    public float coldTime = 0.1f;
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
            bullet.Shoot(spawnPoint.position, spawnPoint.forward);

            isReady = false;
            coldElapsedTime = 0f;
        }
    }
}
