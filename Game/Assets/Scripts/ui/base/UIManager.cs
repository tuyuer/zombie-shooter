using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIProxyType
{
    CommonTips,
    MainPanel,
    WaveInfoPanel,
    GameOverPanel,
    StorePanel,
}

public class UIProxyData
{
    private UIProxyType _type;
    private string _url;

    private UILayerType _layerType;

    public UIProxyData(UIProxyType t, string url, UILayerType layerType = UILayerType.Normal)
    {
        this._type = t;
        this._url = url;
        this._layerType = layerType;
    }

    public UIProxyType Type
    {
        get { return _type; }
    }

    public UILayerType LayerType
    {
        get { return _layerType; }
    }

    public string Url
    {
        get { return _url; }
        set { _url = value; }
    }

}

public class UIManager : MonoBehaviour
{
    private static GameObject _container = null;
    private static string _name = "Singleton/UIManager";

    private Dictionary<UIProxyType, UIProxyData> _proxyDatas = new Dictionary<UIProxyType, UIProxyData>();
    private UILayers _layers;

    public static UIManager GetInst()
    {
        if (_container == null)
        {
            Debug.Log("Create Singleton.");
            _container = new GameObject();
            _container.name = _name;
            _container.AddComponent<UIManager>();
        }
        return _container.GetComponent<UIManager>();
    }

    void Awake()
    {
        TryCreateLayers();
        RegistPrxoys();
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void TryCreateLayers()
    {
        var layers = FindObjectOfType<UILayers>();
        if (!layers)
        {
            GameObject obj = Resources.Load("ui/common/UILayers") as GameObject;
            if (obj)
            {
                obj = Instantiate(obj);
                layers = obj.GetComponent<UILayers>();
                layers.InitLayers();
            }
        }
        _layers = layers;
    }

    private void RegistPrxoys()
    {
        //tips
        RegistProxy(new UIProxyData(UIProxyType.CommonTips, "ui/common/tips/tipsPanel", UILayerType.Tip));

        //dialog

        //normal
        RegistProxy(new UIProxyData(UIProxyType.MainPanel, "ui/gameScene/mainPanel"));
        RegistProxy(new UIProxyData(UIProxyType.WaveInfoPanel, "ui/gameScene/waveInfoPanel"));
        RegistProxy(new UIProxyData(UIProxyType.GameOverPanel, "ui/gameScene/gameOverPanel"));
    }

    private void RegistProxy(UIProxyData data)
    {
        _proxyDatas.Add(data.Type, data);
    }

    private bool IsProxyRegist(UIProxyType type)
    {
        if (!_proxyDatas.ContainsKey(type))
        {
            Debug.Log("ShowProxy Err: pls regist ui => " + type);
            return false;
        }
        return true;
    }

    public UIProxy ShowProxy(UIProxyType type)
    {
        if (!IsProxyRegist(type))
        {
            return null;
        }

        UIProxyData proxyData;
        var isSuccess = _proxyDatas.TryGetValue(type, out proxyData);
        if (!isSuccess) return null;

        return _layers.AddUI(proxyData);
    }

    public UIProxy GetProxy(UIProxyType type)
    {
        if (!IsProxyRegist(type))
        {
            return null;
        }

        UIProxyData proxyData;
        var isSuccess = _proxyDatas.TryGetValue(type, out proxyData);
        if (!isSuccess) return null;

        var layer = _layers.GetLayer(proxyData.LayerType);
        if (!layer) return null;

        var proxy = layer.GetProxy(type);
        return proxy;
    }

    public void CloseProxy(UIProxyType type)
    {
        if (!IsProxyRegist(type))
        {
            return;
        }

        UIProxyData proxyData;
        var isSuccess = _proxyDatas.TryGetValue(type, out proxyData);
        if (!isSuccess) return;

        var layer = _layers.GetLayer(proxyData.LayerType);
        if (!layer) return;

        layer.CloseProxy(type);
    }

    public void CloseProxy(UIProxy proxy)
    {
        if (!IsProxyRegist(proxy.type))
        {
            return;
        }

        UIProxyData proxyData;
        var isSuccess = _proxyDatas.TryGetValue(proxy.type, out proxyData);
        if (!isSuccess) return;

        var layer = _layers.GetLayer(proxyData.LayerType);
        if (!layer) return;

        layer.CloseProxy(proxy);
    }

    public void CloseAllProxy()
    {
        _layers.CloseAllProxy();
    }
}
