using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //CrashTestStartSceneCanvas();
    }

    public void OnBtnPlayClicked()
    {
        SceneManager.LoadScene("StoryScene2");
    }

    void CrashTestStartSceneCanvas()
    {
        GameObject go = null;
        go.name = "";
    }
}
