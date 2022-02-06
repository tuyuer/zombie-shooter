using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBugly : MonoBehaviour
{
    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CrashTest()
    {
        GameObject go = null;
        go.name = "";
    }
}
