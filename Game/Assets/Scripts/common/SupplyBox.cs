using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HitJoy;

public class SupplyBox : MonoBehaviour
{
    public GameObject canvas;

    private void Awake()
    {
        canvas.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagDef.Player)
        {
            canvas.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == TagDef.Player)
        {
            canvas.SetActive(false);
        }
    }

    public void OnUseClicked()
    {
        MessageCenter.PostMessage(NotificationDef.NOTIFICATION_ON_SUPPLY_BOX_USE_BUTTON_CLICKED);
    }
}
