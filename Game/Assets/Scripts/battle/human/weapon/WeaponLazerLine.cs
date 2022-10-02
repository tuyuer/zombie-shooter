using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLazerLine : MonoBehaviour
{
    public LineRenderer lineRender;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLength(float length)
    {
        lineRender.SetPosition(1, Vector3.forward * length);
    }
}
