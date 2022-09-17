using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HitJoy;

public class UiMainPanel : MonoBehaviour
{
    public int casCost = 2;

    public SkillButton airforceBtn = null;

    // Update is called once per frame
    void Update()
    {

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
            GameWorld.Instance.uiCanvas.tipsPanel.Show("NEED 2 CHIPS!");
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
