using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILayers : MonoBehaviour
{
    private Dictionary<UILayerType, UILayer> _layers = new Dictionary<UILayerType, UILayer>();

    public void InitLayers()
    {
        var layers = GetComponentsInChildren<UILayer>();
        for (int i = 0; i < layers.Length; i++)
        {
            var layer = layers[i];
            _layers.Add(layer.type, layer);
        }
        DontDestroyOnLoad(gameObject);
    }

    public UIProxy AddUI(UIProxyData data)
    {
        UILayer layer = GetLayer(data.LayerType);
        if (layer) return layer.AddUI(data);

        return null;
    }

    public UILayer GetLayer(UILayerType type)
    {
        UILayer layer;
        var success = _layers.TryGetValue(type, out layer);
        if (!success) return null;

        return layer;
    }

    public void CloseAllProxy()
    {
        foreach (var item in _layers)
        {
            UILayer layer = item.Value;
            layer.CloseAllProxy();
        }
    }
}
