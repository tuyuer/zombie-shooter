using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILayers : MonoBehaviour
{
    private UILayer[] _layers = null;

    void Awake()
    {
        _layers = GetComponentsInChildren<UILayer>();
    }

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
        UILayer layer = GetLayer(data.LayerType);
        if (layer) layer.AddUI(data);
    }

    public UILayer GetLayer(UILayerType type)
    {
        for(int i = 0; i < _layers.Length; i++)
        {
            if(_layers[i].type == type)
            {
                return _layers[i];
            }
        }
        return null;
    }
}
