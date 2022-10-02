using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using HitJoy;
using TMPro;

public class UiMainPanel : UIProxy
{
    public TMP_Text tmpBullets;
    public TMP_Text tmpKills;
    public TMP_Text tmpChips;

    public Joystick joystick;
    public Joystick aimstick;

    public Image imgBlood;
    public SkillButton airforceBtn = null;

    public int casCost = 2;

    // Update is called once per frame
    void Update()
    {
        tmpKills.text = "" + GameWorld.Instance.killStatistics.KillCount();
        tmpChips.text = "" + GameWorld.Instance.backpack.GetElementCount(backpack_element_type.backpack_element_type_gold);
        imgBlood.fillAmount = GameWorld.Instance.player.Blood.GetFillAmount();

        Weapon curWeapon = GameWorld.Instance.player.CurrentWeapon;
        if (curWeapon != null)
        {
            int nBulletInClip = curWeapon.weaponClip.LeftBullet;
            int nClipSize = curWeapon.weaponClip.clipSize;
            if (curWeapon.weaponType == weapon_type.weapon_type_pistol)
            {
                tmpBullets.text = "infinity";
            }
            else
            {
                if (nBulletInClip == 0)
                {
                    tmpBullets.text = "loading...";
                }
                else
                {
                    tmpBullets.text = String.Format("{0:D}/{1:D}", nBulletInClip, nClipSize);
                }
            }
        }
    }

    private bool IsAirForceReady() 
    {
        if (!airforceBtn.IsReady()) return false;

        AttackHelicopter[] helicopters = FindObjectsOfType<AttackHelicopter>();
        bool isCASReady = true;
        foreach (var helicopter in helicopters)
        {
            if (!helicopter.IsHelicopterReady())
            {
                isCASReady = false;
                break;
            }
        }
        return isCASReady;
    }

    public void OnCallAirForce()
    {
        var goldCount = GameWorld.Instance.backpack.GetElementCount(backpack_element_type.backpack_element_type_gold);
        if (goldCount < casCost)
        {
            TipsUtil.ShowTips("NEED 2 CHIPS!");
            return;
        }

        if (IsAirForceReady())
        {
            MessageCenter.PostMessage(NotificationDef.NOTIFICATION_ON_CALL_AIR_FORCE);
            GameWorld.Instance.backpack.RemoveElement(backpack_element_type.backpack_element_type_gold, casCost);
            airforceBtn.TriggerSkill();
        }
    }
}
