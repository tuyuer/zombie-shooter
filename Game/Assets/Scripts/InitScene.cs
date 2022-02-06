using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitScene : MonoBehaviour
{
    void Awake()
    {
        BuglyAgent.ConfigDebugMode(true);
        BuglyAgent.InitWithAppId("2f1eab0569");
        BuglyAgent.EnableExceptionHandler();

        SceneManager.LoadScene("StartScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
