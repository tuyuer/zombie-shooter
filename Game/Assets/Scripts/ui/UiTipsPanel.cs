using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UiTipsPanel : MonoBehaviour
{
    public TMP_Text tmpTips;
    public Image tipsBg;

    private float leftTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        leftTime -= Time.deltaTime;
        tipsBg.gameObject.SetActive(leftTime > 0);
    }

    public void Show(string tips)
    {
        tmpTips.text = tips;
        leftTime = 2;
    }
}
