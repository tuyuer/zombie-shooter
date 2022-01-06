using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HitJoy;

public class FlashLightLightoffArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagDef.Flashlight)
        {
            FlashLight flashLight = other.gameObject.GetComponent<FlashLight>();
            if (flashLight != null)
            {
                flashLight.TurnOff();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == TagDef.Flashlight)
        {
            FlashLight flashLight = other.gameObject.GetComponent<FlashLight>();
            if (flashLight != null)
            {
                flashLight.TurnOn();
            }
        }
    }
}
