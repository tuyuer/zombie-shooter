using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HitJoy;
using DG.Tweening;


public class Utils
{
    private Utils()
    {
    }

    private static Utils instance = null;

    public static Utils Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Utils();
                instance.Init();
            }

            return instance;
        }
    }

    public void Init()
    {

    }

    public void PerformFunctionWithDelay(TweenCallback callback, float delayTime)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(callback);
        sequence.SetDelay(delayTime);
    }

}
