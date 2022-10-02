using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAutoShoot : MonoBehaviour
{
    private Character charactor;
    private AiAutoAim autoAim;

    void Awake()
    {
        charactor = GetComponent<Character>();
        autoAim = GetComponent<AiAutoAim>();
    }

    void Update()
    {
        if (autoAim.IsTargetLocked)
        {
            charactor.Shoot();
        }
    }
}
