using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBlood : MonoBehaviour
{
    [Range(0, 100)]
    public int totolBlood = 100;
    private int curBlood = 100;

    public void Awake()
    {
        curBlood = totolBlood;
    }

    public void OnDamage(int nDamage = 10)
    {
        curBlood -= nDamage;
        curBlood = Mathf.Max(0, curBlood);
    }

    public void OnRemedy(int nRemedy = 10)
    {
        curBlood += nRemedy;
        curBlood = Mathf.Min(totolBlood, curBlood);
    }

    public float GetFillAmount()
    {
        return curBlood * 1.0f / totolBlood;
    }
}
