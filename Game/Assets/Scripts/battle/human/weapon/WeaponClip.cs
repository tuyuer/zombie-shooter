using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponClip
{
    public int clipSize = 10;

    public float reloadTime = 2.0f;

    public float bulletReadyTime = 0.1f;

    private float bulletReadyLeftTime = -1f;

    public bool IsBulletReady
    {
        get { return bulletReadyLeftTime < 0f && LeftBullet > 0; }
    }

    private int leftBullet = 10;
    public int LeftBullet
    {
        get { return leftBullet; }
    }

    private float reloadLeftTime = -1f;

    public bool IsReloading
    {
        get { return reloadLeftTime > 0; }
    }

    public void Setup()
    {
        leftBullet = clipSize;
        bulletReadyLeftTime = -1f;
        reloadLeftTime = -1f;
    }

    public void Shoot()
    {
        if (leftBullet > 0)
        {
            bulletReadyLeftTime = bulletReadyTime;
            leftBullet -= 1;

            if (leftBullet <= 0)
            {
                Reload();
            }
        }
    }

    public void OnUpdate(float dt)
    {
        OnPrepareBullet(dt);
        OnPrepareClip(dt);
    }

    private void OnPrepareBullet(float dt)
    {
        bulletReadyLeftTime -= dt;
    }

    private void OnPrepareClip(float dt)
    {
        if (reloadLeftTime < 0)
            return;

        reloadLeftTime -= dt;
        if (reloadLeftTime < 0)
            Setup();
    }

    private void Reload()
    {
        if (IsReloading)
        {
            return;
        }

        reloadLeftTime = reloadTime;
    }
}
