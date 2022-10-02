using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UiTipsPanel : UIProxy
{
    public GameObject tips;
    public TMP_Text tmpTips;
    public float speed = 100;

    private float leftTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        leftTime -= Time.deltaTime;
        if (leftTime < 0) Close();

        Vector3 newPos = tips.transform.position;
        newPos.y += Time.deltaTime * speed;
        tips.transform.position = newPos;
    }

    public void Show(string tips)
    {
        tmpTips.text = tips;
    }
}
