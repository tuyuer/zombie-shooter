using System;
using System.Collections;
using System.Collections.Generic;
using HitJoy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameOverPanel : UIProxy
{
    public Button btnRetry;

    // Start is called before the first frame update
    void Start()
    {
        btnRetry.onClick.AddListener(delegate () {
            UIManager.GetInst().CloseAllProxy();
            SceneManager.LoadScene("StartScene2");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
