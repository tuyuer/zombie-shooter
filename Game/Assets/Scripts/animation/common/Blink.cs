using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Blink : MonoBehaviour
{
    //time
    public float blinkInTime = 1.0f;
    public float blinkOutTime = 1.0f;

    //alpha
    public float minAlpha = 0;
    public float maxAlpha = 1;

    private CanvasGroup _canvasGroup;
    private Sign _sign = Sign.Positive;

    void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }


    // Update is called once per frame
    void Update()
    {
        float alpha = _canvasGroup.alpha;
        if(_sign == Sign.Positive)
        {
            float nextAlpha = alpha + Time.deltaTime / blinkInTime;
            bool reachMax = nextAlpha >= maxAlpha;
            alpha = reachMax ? maxAlpha : nextAlpha;
            _sign = reachMax ? Sign.Negative : Sign.Positive; 
        }
        else if(_sign == Sign.Negative)
        {
            float nextAlpha = alpha - Time.deltaTime / blinkOutTime;
            bool reachMin = nextAlpha <= minAlpha;
            alpha = reachMin ? minAlpha : nextAlpha;
            _sign = reachMin ? Sign.Positive : Sign.Negative;
        }
        _canvasGroup.alpha = alpha;

        TipsUtil.ShowTips("hello boy");
    }

}
