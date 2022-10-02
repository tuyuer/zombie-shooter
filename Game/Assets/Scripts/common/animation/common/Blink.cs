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
    private sign_type _sign = sign_type.sign_type_positive;

    void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }


    // Update is called once per frame
    void Update()
    {
        float alpha = _canvasGroup.alpha;
        if(_sign == sign_type.sign_type_positive)
        {
            float nextAlpha = alpha + Time.deltaTime / blinkInTime;
            bool reachMax = nextAlpha >= maxAlpha;
            alpha = reachMax ? maxAlpha : nextAlpha;
            _sign = reachMax ? sign_type.sign_type_negative : sign_type.sign_type_positive; 
        }
        else if(_sign == sign_type.sign_type_negative)
        {
            float nextAlpha = alpha - Time.deltaTime / blinkOutTime;
            bool reachMin = nextAlpha <= minAlpha;
            alpha = reachMin ? minAlpha : nextAlpha;
            _sign = reachMin ? sign_type.sign_type_positive : sign_type.sign_type_negative;
        }
        _canvasGroup.alpha = alpha;
    }

}
