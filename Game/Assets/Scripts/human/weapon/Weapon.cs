using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HitJoy;
using Opsive.UltimateCharacterController.Objects;

public class Weapon : MonoBehaviour
{
    public Transform spawnPoint = null;
    public MuzzleFlash muzzleFlash = null;
    public AudioSource weaponSound = null;
    public LaserLine laserLine = null;

    public weapon_type weaponType = weapon_type.weapon_type_pistol;

    //���д�С
    public int clipSize = 10;
    public int bulletsInClip = 0;

    [Range(0, 10f)]
    public float reloadTime = 0.5f;
    private float reloadElapsedTime = GlobalDef.ZERO_FLOAT_VALUE;

    private SimpleObjectPool bulletPool = null;
    private bool isReady = true;

    private int laserLineMask;

    void Start()
    {
        int moveableLayer = LayerMask.NameToLayer(LayerNames.Moveable);
        int obstableLayer = LayerMask.NameToLayer(LayerNames.Obstacle);
        laserLineMask = (1 << moveableLayer) | (1 << obstableLayer);

        switch (weaponType)
        {
            case weapon_type.weapon_type_pistol:
                bulletPool = GameWorld.Instance.GetBulletPoolByType(simple_object_pool_type.simple_object_pool_type_bullet_pistol);
                break;
            case weapon_type.weapon_type_rifle:
                bulletPool = GameWorld.Instance.GetBulletPoolByType(simple_object_pool_type.simple_object_pool_type_bullet_rifle);
                break;
            case weapon_type.weapon_type_shortgun:
                bulletPool = GameWorld.Instance.GetBulletPoolByType(simple_object_pool_type.simple_object_pool_type_bullet_shortgun);
                break;
            case weapon_type.weapon_type_gun_turret:
                bulletPool = GameWorld.Instance.GetBulletPoolByType(simple_object_pool_type.simple_object_pool_type_bullet_pistol);
                break;
            default:
                break;
        }
    }

    void Update()
    {
        reloadElapsedTime += Time.deltaTime;
        if (reloadElapsedTime > reloadTime)
        {
            isReady = true;
        }

        UpdateLaser();
    }

    void UpdateLaser()
    {
        if (laserLine == null)
            return;

        laserLine.SetPosition(0, Vector3.zero);

        Debug.DrawRay(laserLine.transform.position, laserLine.transform.forward * 10);
        RaycastHit hit;
        if (Physics.Raycast(laserLine.transform.position, laserLine.transform.forward, out hit, 100, laserLineMask))
        {
            Vector3 vecOffset = hit.point - laserLine.transform.position;
            laserLine.SetPosition(1, Vector3.forward * vecOffset.magnitude);
        }
        else
        {
            Vector3 forwardDir = Vector3.forward;
            Vector3 endPos = forwardDir * 100;
            laserLine.SetPosition(1, endPos);
        }
    }

    public void Shoot()
    {
        if (isReady)
        {
            if (bulletPool == null)
                return;

            GameObject bulletObj = bulletPool.FetchObject();
            BulletEffect bullet = bulletObj.GetComponent<BulletEffect>();

            Vector3 moveDir = transform.forward;
            bullet.Shoot(spawnPoint.position, moveDir);
            muzzleFlash.ShowEffect();
            weaponSound.Play();

            isReady = false;
            reloadElapsedTime = GlobalDef.ZERO_FLOAT_VALUE;
        }
    }
}
