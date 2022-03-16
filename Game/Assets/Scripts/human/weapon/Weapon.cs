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
    public WeaponClip weaponClip;

    public weapon_type weaponType = weapon_type.weapon_type_pistol;
    public bool enableLaser = true;
    public bool enableSound = true;

    private SimpleObjectPool bulletPool = null;

    private int laserLineMask;

    private bool isEnemyFront = false;
    public bool IsEnemyFront
    {
        get { return isEnemyFront; }
    }

    void Start()
    {
        int moveableLayer = LayerMask.NameToLayer(LayerNames.Moveable);
        int obstableLayer = LayerMask.NameToLayer(LayerNames.Obstacle);
        laserLineMask = (1 << moveableLayer) | (1 << obstableLayer);

        switch (weaponType)
        {
            case weapon_type.weapon_type_pistol:
                bulletPool = GameWorld.Instance.GetBulletPoolByType(simple_object_pool_type.simple_object_pool_type_bullet_turret);
                break;
            case weapon_type.weapon_type_rifle:
                bulletPool = GameWorld.Instance.GetBulletPoolByType(simple_object_pool_type.simple_object_pool_type_bullet_turret);
                break;
            case weapon_type.weapon_type_shortgun:
                bulletPool = GameWorld.Instance.GetBulletPoolByType(simple_object_pool_type.simple_object_pool_type_bullet_shortgun);
                break;
            case weapon_type.weapon_type_gun_turret:
                bulletPool = GameWorld.Instance.GetBulletPoolByType(simple_object_pool_type.simple_object_pool_type_bullet_turret);
                break;
            default:
                break;
        }

        weaponClip.Setup();
    }

    void Update()
    {
        UpdateLaser();
        weaponClip.OnUpdate(Time.deltaTime);
    }

    void UpdateLaser()
    {
        if (laserLine == null)
            return;

        if (!enableLaser)
        {
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(laserLine.transform.position, laserLine.transform.forward, out hit, 100, laserLineMask))
        {
            Vector3 vecOffset = hit.point - laserLine.transform.position;
            laserLine.SetLength(vecOffset.magnitude);
            isEnemyFront = true;
        }
        else
        {
            laserLine.SetLength(200);
            isEnemyFront = false;
        }
    }

    void FireMuzzleFlash()
    {
        muzzleFlash.gameObject.SetActive(false);
        muzzleFlash.gameObject.SetActive(true);
    }

    public void Shoot()
    {
        if (weaponClip.IsBulletReady)
        {
            if (bulletPool == null)
            {
                Debug.LogError("bulletPool is null !!!");
                return;
            }

            GameObject bulletObj = bulletPool.FetchObject();
            BulletEffect bullet = bulletObj.GetComponent<BulletEffect>();

            Vector3 moveDir = transform.forward;
            bullet.Shoot(spawnPoint.position, moveDir);
            FireMuzzleFlash();
            weaponClip.Shoot();

            if (enableSound)
            {
                weaponSound.Play();
            }
        }
    }
}
