using UnityEngine;
using System.Collections;

public class TipsUtil : MonoBehaviour
{
    private static GameObject _container = null;
    private static string _name = "Singleton/TipsUtil";


    public static TipsUtil GetInst()
    {
        if (_container == null)
        {
            Debug.Log("Create Singleton.");
            _container = new GameObject();
            _container.name = _name;
            _container.AddComponent(typeof(TipsUtil));
        }
        return _container.GetComponent<TipsUtil>();
    }

    public static void ShowTips(string tips)
    {
        UIProxy proxy = UIManager.GetInst().ShowProxy(UIProxyType.CommonTips);
        if (proxy)
        {
            UiTipsPanel tipsPanel = proxy as UiTipsPanel;
            if (tipsPanel)
            {
                tipsPanel.Show(tips);
            }
        }
    }
}
