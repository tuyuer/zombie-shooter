using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HitJoy;

public class Character : MonoBehaviour
{
    public AudioSource runSound;
    public Weapon[] allWeapons;

    private Weapon weapon = null;
    public Weapon CurrentWeapon
    {
        get { return weapon; }
    }

    private ActorBlood characterBlood;
    public ActorBlood Blood
    {
        get { return characterBlood; }
    }

    private Blackboard blackBoard;

    void Awake()
    {
        characterBlood = GetComponent<ActorBlood>();
        blackBoard = GetComponent<Blackboard>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetWeapon(weapon_type.weapon_type_rifle);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsAlive())
            return;

        if (GlobalDef.ENABLE_STICKJOY)
        {
            if (GameWorld.Instance.aimstick.IsHolding)
            {
                if (weapon != null)
                {
                    weapon.Shoot();
                }
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                if (weapon != null)
                {
                    weapon.Shoot();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetWeapon(weapon_type.weapon_type_pistol);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetWeapon(weapon_type.weapon_type_rifle);
        }
    }

    public void OnDeath()
    {
        blackBoard.animator.SetTrigger(AnimatorParameter.Die);
        SetWeaponLayerWeight("", 0);
        MessageCenter.PostMessage(NotificationDef.NOTIFICATION_ON_PLAYER_DEATH);
    }

    public void OnDamage() 
    {
        MessageCenter.PostMessage(NotificationDef.NOTIFICATION_ON_PLAYER_DAMAGE);
    }

    public bool IsAlive()
    {
        return characterBlood.GetFillAmount() > 0;
    }

    public void SetWeapon(weapon_type weaponType)
    {
        foreach (Weapon item in allWeapons)
        {
            item.gameObject.SetActive(false);
            if (item.weaponType == weaponType)
            {
                weapon = item;
            }
        }
        weapon.gameObject.SetActive(true);
        SetWeaponLayerWeight("", 0);
        switch (weaponType)
        {
            case weapon_type.weapon_type_pistol:
                SetWeaponLayerWeight(AnimatorLayerNames.PistolLayer, 1);
                break;
            case weapon_type.weapon_type_rifle:
                SetWeaponLayerWeight(AnimatorLayerNames.RifleLayer, 1);
                break;
            case weapon_type.weapon_type_shortgun:
                SetWeaponLayerWeight(AnimatorLayerNames.RifleLayer, 1);
                break;
            default:
                break;
        }
        SetWeaponRigs(weaponType);
    }

    public void EnterRunState()
    {
        if (runSound != null &&
            !runSound.gameObject.activeSelf)
        {
            runSound.gameObject.SetActive(true);
        }
    }

    public void ExitRunState()
    {
        if (runSound != null &&
            runSound.gameObject.activeSelf)
        {
            runSound.gameObject.SetActive(false);
        }
    }

    private void SetWeaponRigs(weapon_type weaponType) 
    {
        WeaponRigs weaponRigs = GetComponent<WeaponRigs>();
        if (weaponRigs == null)
            return;

        switch (weaponType)
        {
            case weapon_type.weapon_type_pistol:
                weaponRigs.WeaponPistol();
                break;
            case weapon_type.weapon_type_rifle:
                weaponRigs.WeaponRifle();
                break;
            case weapon_type.weapon_type_shortgun:
                weaponRigs.WeaponRifle();
                break;
            default:
                break;
        }
    }

    public void SetWeaponLayerWeight(string layerName, float value)
    {
        if (layerName.Length == 0)
        {
            int nLayerIndex = blackBoard.animator.GetLayerIndex(AnimatorLayerNames.PistolLayer);
            blackBoard.animator.SetLayerWeight(nLayerIndex, 0);

            nLayerIndex = blackBoard.animator.GetLayerIndex(AnimatorLayerNames.RifleLayer);
            blackBoard.animator.SetLayerWeight(nLayerIndex, 0);
        }
        else
        {
            int nLayerIndex = blackBoard.animator.GetLayerIndex(layerName);
            blackBoard.animator.SetLayerWeight(nLayerIndex, value);
        }
    }
}
