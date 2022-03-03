using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HitJoy;
using Opsive.UltimateCharacterController.Objects;

public class Weapon : MonoBehaviour
{
    public Transform spawnPoint = null;
    public Transform muzzleFlash = null;
    public AudioSource weaponSound = null;
    public WeaponLazerLine laserLine = null;

    public weapon_type weaponType = weapon_type.weapon_type_pistol;

    //µØº–¥Û–°
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
            
        RaycastHit hit;
        if (Physics.Raycast(laserLine.transform.position, laserLine.transform.forward, out hit, 100, laserLineMask))
        {
            Vector3 vecOffset = hit.point - laserLine.transform.position;
            laserLine.SetLength(vecOffset.magnitude);
        }
        else
        {
            laserLine.SetLength(200);
        }
    }

    void FireMuzzleFlash()
    {
        muzzleFlash.gameObject.SetActive(false);
        muzzleFlash.gameObject.SetActive(true);
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
            FireMuzzleFlash();
            weaponSound.Play();

            isReady = false;
            reloadElapsedTime = GlobalDef.ZERO_FLOAT_VALUE;
        }
    }
}
