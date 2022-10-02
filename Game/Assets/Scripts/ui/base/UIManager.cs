using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIProxyType
{
    CommonTips
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
            _container.AddComponent(typeof(UIManager));
        }
        return _container.GetComponent<UIManager>();
    }

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        _layers = GameObject.FindObjectOfType<UILayers>();
        RegistPrxoys();

        DontDestroyOnLoad(gameObject);
    }

    private void RegistPrxoys()
    {
        RegistProxy(new UIProxyData(UIProxyType.CommonTips, "ui/common/tips/tips-panel", UILayerType.Tip));
    }

    private void RegistProxy(UIProxyData data)
    {
        _proxyDatas.Add(data.Type, data);
    }

    public UIProxy ShowProxy(UIProxyType type)
    {
        if (!_proxyDatas.ContainsKey(type))
        {
            Debug.Log("ShowProxy Err: pls regist ui => " + type);
            return null;
        }

        UIProxyData proxyData;
        var isSuccess = _proxyDatas.TryGetValue(type, out proxyData);
        if (!isSuccess) return null;

        return _layers.AddUI(proxyData);
    }
}
