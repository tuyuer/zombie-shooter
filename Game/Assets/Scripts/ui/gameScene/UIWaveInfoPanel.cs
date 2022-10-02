using System;
using System.Collections;
using System.Collections.Generic;
using HitJoy;
using TMPro;
using UnityEngine;

public class UIWaveInfoPanel : MonoBehaviour
{
    public TMP_Text tmpWaves;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        MessageCenter.AddMessageObserver(this, NotificationDef.NOTIFICATION_ON_WAVE_PREPARE_BEGIN, new MessageEvent(OnWavePrepareBegin));
        MessageCenter.AddMessageObserver(this, NotificationDef.NOTIFICATION_ON_WAVE_PREPARE_UPDATE, new MessageEvent(OnWavePrepareUpdate));
        MessageCenter.AddMessageObserver(this, NotificationDef.NOTIFICATION_ON_WAVE_PREPARE_END, new MessageEvent(OnWavePrepareEnd));
    }

    private void OnDisable()
    {
        MessageCenter.RemoveAllObservers(this);
    }

    public void OnWavePrepareBegin(System.Object data)
    {
        //MessageObject msgObj = data as MessageObject;
        //waveInfoPanel.gameObject.SetActive(true);
        //tmpWaves.text = "WAVE " + msgObj.nParameter;

        //GameWorld.Instance.PlaySound(sound_type.sound_type_wave_begin);
        //Utils.Instance.PerformFunctionWithDelay(delegate ()
        //{
        //    GameWorld.Instance.PlaySound(sound_type.sound_type_wave_countdown);
        //}, 3.0f);
    }

    public void OnWavePrepareUpdate(System.Object data)
    {
        //MessageObject msgObj = data as MessageObject;
        //float prepareElapsedTime = msgObj.fParameter;
        //float wavePrepareTime = msgObj.fParameter2;
        //int leftTime = (int)Mathf.Ceil(wavePrepareTime - prepareElapsedTime);
        //if (leftTime < 4)
        //{
        //    tmpWaves.text = Convert.ToString(leftTime);
        //}
    }

    public void OnWavePrepareEnd(System.Object data)
    {
        //tmpWaves.text = "GO";
        //GameWorld.Instance.PlaySound(sound_type.sound_type_wave_go);

        //Utils.Instance.PerformFunctionWithDelay(delegate ()
        //{
        //    waveInfoPanel.gameObject.SetActive(false);
        //}, 1.0f);
    }
}
