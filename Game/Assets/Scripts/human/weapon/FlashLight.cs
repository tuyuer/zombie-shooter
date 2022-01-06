using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLB;

namespace HitJoy
{
    public class FlashLight : MonoBehaviour
    {
        public Light spotLight;
        public VolumetricLightBeam lightBeam;
        private void Awake()
        {
            spotLight = GetComponent<Light>();
            lightBeam = GetComponent<VolumetricLightBeam>();
        }

        public void TurnOn()
        {
            spotLight.enabled = true;
            lightBeam.enabled = true;
        }

        public void TurnOff()
        {
            spotLight.enabled = false;
            lightBeam.enabled = false;
        }
    }
}
