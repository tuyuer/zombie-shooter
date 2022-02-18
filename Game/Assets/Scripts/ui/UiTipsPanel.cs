using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiTipsPanel : MonoBehaviour
{
    public Text txtTips;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShopTips(string tipsStr)
    {
        txtTips.text = tipsStr;
        gameObject.SetActive(true);
    }

    public void OnClickedConfirm()
    {
        gameObject.SetActive(false);
    }

}
