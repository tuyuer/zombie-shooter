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
    public GameObject gameOverPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        MessageCenter.AddMessageObserver(this, NotificationDef.NOTIFICATION_ON_PLAYER_DEATH, new MessageEvent(OnPlayerDeath));
    }

    private void OnDisable()
    {
        MessageCenter.RemoveAllObservers(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerDeath(System.Object data)
    {
        gameOverPanel.gameObject.SetActive(true);
    }


    public void OnBtnRetryClicked()
    {
        SceneManager.LoadScene("StartScene2");
    }
}
