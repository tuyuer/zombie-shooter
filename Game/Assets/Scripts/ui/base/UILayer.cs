using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UILayerType
{
    Normal,
    Dialog,
    Tip,
}


public class UILayer : MonoBehaviour
{
    public UILayerType type = UILayerType.Normal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddUI(UIProxyData data)
    {
        GameObject ui = Resources.Load(data.Url) as GameObject;
        if (ui)
        {
            ui = Instantiate(ui);
            ui.transform.parent = gameObject.transform;
            RectTransform rTransform = ui.transform as RectTransform;

            //left bottom
            rTransform.offsetMin = new Vector2(0.0f, 0.0f);
            //right top
            rTransform.offsetMax = new Vector2(0.0f, 0.0f);
        }
    }
}
