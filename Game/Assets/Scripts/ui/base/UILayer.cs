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
    public List<UIProxy> proxys = new List<UIProxy>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public UIProxy AddUI(UIProxyData data)
    {
        var p = GetProxy(data.Type);
        if (p) return p;

        GameObject ui = Resources.Load(data.Url) as GameObject;
        if (ui)
        {
            ui = Instantiate(ui);
            ui.transform.SetParent(gameObject.transform);
            RectTransform rTransform = ui.transform as RectTransform;

            //left bottom
            rTransform.offsetMin = new Vector2(0.0f, 0.0f);
            //right top
            rTransform.offsetMax = new Vector2(0.0f, 0.0f);

            var proxy = ui.GetComponent<UIProxy>();
            proxy.type = data.Type;
            proxys.Add(proxy);

            return proxy;
        }
        return null;
    }

    public bool ContainsProxy(UIProxy proxy)
    {
        for (int i = 0; i < proxys.Count; i++)
        {
            var p = proxys[i];
            if (p == proxy)
            {
                return true;
            }
        }
        return false;
    }

    public UIProxy GetProxy(UIProxyType type)
    {
        for(int i = 0; i < proxys.Count; i++)
        {
            var proxy = proxys[i];
            if(proxy.type == type)
            {
                return proxy;
            }
        }
        return null;
    }

    public List<UIProxy> GetProxys(UIProxyType type)
    {
        List<UIProxy> all = new List<UIProxy>();
        for (int i = 0; i < proxys.Count; i++)
        {
            var p = proxys[i];
            if(p.type == type)
            {
                all.Add(p);
            }
        }
        return all;
    }

    public void CloseProxy(UIProxyType type)
    {
        var proxy = GetProxy(type);
        if (!proxy) return;

        proxys.Remove(proxy);
        Destroy(proxy.gameObject);
    }

    public void CloseProxy(UIProxy proxy)
    {
        var exist = ContainsProxy(proxy);
        if (!exist)
        {
            Destroy(proxy.gameObject);
            return;
        }

        proxys.Remove(proxy);
        Destroy(proxy.gameObject);
    }

    public void CloseAllProxy()
    {
        var tempProxys = new List<UIProxy>(proxys);
        foreach(var proxy in tempProxys)
        {
            CloseProxy(proxy);
        }
    }
}
