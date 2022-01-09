using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillStatistics : MonoBehaviour
{
    private int nKillCount = 0;
    public int KillCount
    {
        get { return nKillCount; }
    }

    public void Increase()
    {
        nKillCount++;
    }
}
