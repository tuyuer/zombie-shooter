using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HitJoy;

public class UiMainPanel : MonoBehaviour
{
    public Button btnCas;
    public Button btnAuxiliary;

    public int casCost = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int nGoldCount = GameWorld.Instance.backpack.GetElementCount(backpack_element_type.backpack_element_type_gold);
        btnCas.gameObject.SetActive(nGoldCount >= casCost && IsAirForceReady());

        btnAuxiliary.gameObject.SetActive(nGoldCount >= casCost && GameWorld.Instance.player.AutoAim.IsRunning());
    }

    private bool IsAirForceReady() 
    {
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
        if (IsAirForceReady())
        {
            MessageCenter.PostMessage(NotificationDef.NOTIFICATION_ON_CALL_AIR_FORCE);
            GameWorld.Instance.backpack.RemoveElement(backpack_element_type.backpack_element_type_gold, casCost);
        }
    }

    public void OnCallAutoAim() 
    {
        GameWorld.Instance.player.StartAutoAim();
    }
}
