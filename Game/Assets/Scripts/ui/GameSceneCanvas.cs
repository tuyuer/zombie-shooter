using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HitJoy;
using System;
using UnityEngine.SceneManagement;

public class GameSceneCanvas : MonoBehaviour
{
    public TMP_Text tmpBullets;
    public TMP_Text tmpKills;
    public TMP_Text tmpChips;
    public TMP_Text tmpWaves;
    public Image imgBlood;

    public GameObject waveInfoPanel;
    public GameObject gameOverPanel;
    public UIShopPanel shopPanel;
    public UiTipsPanel tipsPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        MessageCenter.AddMessageObserver(this, NotificationDef.NOTIFICATION_ON_WAVE_PREPARE_BEGIN, new MessageEvent(OnWavePrepareBegin));
        MessageCenter.AddMessageObserver(this, NotificationDef.NOTIFICATION_ON_WAVE_PREPARE_UPDATE, new MessageEvent(OnWavePrepareUpdate));
        MessageCenter.AddMessageObserver(this, NotificationDef.NOTIFICATION_ON_WAVE_PREPARE_END, new MessageEvent(OnWavePrepareEnd));

        MessageCenter.AddMessageObserver(this, NotificationDef.NOTIFICATION_ON_PLAYER_DEATH, new MessageEvent(OnPlayerDeath));
        MessageCenter.AddMessageObserver(this, NotificationDef.NOTIFICATION_ON_OPEN_SHOP_PANEL, new MessageEvent(OnOpenShop));
    }

    private void OnDisable()
    {
        MessageCenter.RemoveAllObservers(this);
    }

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

    public void OnWavePrepareBegin(System.Object data)
    {
        MessageObject msgObj = data as MessageObject;
        waveInfoPanel.gameObject.SetActive(true);
        tmpWaves.text = "WAVE " + msgObj.nParameter;

        GameWorld.Instance.PlaySound(sound_type.sound_type_wave_begin);
        Utils.Instance.PerformFunctionWithDelay(delegate ()
        {
            GameWorld.Instance.PlaySound(sound_type.sound_type_wave_countdown);
        }, 3.0f);
    }

    public void OnWavePrepareUpdate(System.Object data)
    {
        MessageObject msgObj = data as MessageObject;
        float prepareElapsedTime = msgObj.fParameter;
        float wavePrepareTime = msgObj.fParameter2;
        int leftTime = (int)Mathf.Ceil(wavePrepareTime - prepareElapsedTime);
        if (leftTime < 4)
        {
            tmpWaves.text = Convert.ToString(leftTime);
        }
    }

    public void OnWavePrepareEnd(System.Object data)
    {
        tmpWaves.text = "GO";
        GameWorld.Instance.PlaySound(sound_type.sound_type_wave_go);

        Utils.Instance.PerformFunctionWithDelay(delegate ()
        {
            waveInfoPanel.gameObject.SetActive(false);
        }, 1.0f);
    }

    public void OnPlayerDeath(System.Object data)
    {
        gameOverPanel.gameObject.SetActive(true);
    }

    public void OnOpenShop(System.Object data)
    {
        shopPanel.gameObject.SetActive(true);
        GameWorld.Instance.waveManager.PauseWave();
    }

    public void OnBtnRetryClicked()
    {
        SceneManager.LoadScene("StartScene");
    }
}
