using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopPanel : MonoBehaviour
{
    public Text txtChips;

    // Start is called before the first frame update
    void Start()
    {
        UpdateChips();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateChips();
    }

    void UpdateChips()
    {
        int nChipCount = GameWorld.Instance.backpack.GetElementCount(backpack_element_type.backpack_element_type_gold);
        txtChips.text = "" + nChipCount;
    }

    public void OnCloseClicked()
    {
        gameObject.SetActive(false);
        GameWorld.Instance.waveManager.ResumeWave();
    }

    public void OnBuyClicked()
    {
        if (GameWorld.Instance.player.GetWeaponType() == weapon_type.weapon_type_rifle)
        {
            GameWorld.Instance.uiCanvas.tipsPanel.ShopTips("您已装备自动步枪，请前往战场杀敌！");
            return;
        }

        int nChipCount = GameWorld.Instance.backpack.GetElementCount(backpack_element_type.backpack_element_type_gold);
        if (nChipCount >= 6)
        {
            GameWorld.Instance.player.SetWeapon(weapon_type.weapon_type_rifle);
            GameWorld.Instance.backpack.RemoveElement(backpack_element_type.backpack_element_type_gold, 6);
            GameWorld.Instance.uiCanvas.tipsPanel.ShopTips("您已装备自动步枪，请前往战场杀敌！");
        }
        else
        {
            GameWorld.Instance.uiCanvas.tipsPanel.ShopTips("筹码不足，请您在战场中收集更多筹码！");
        }
    }
}
