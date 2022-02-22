using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Opsive.UltimateCharacterController.Objects;
using UnityEngine.SceneManagement;

public class StorySignalEvents : MonoBehaviour
{
    public GameObject timelineCaptions;
    public GameObject timelineShoot1;

    public MuzzleFlash muzzleFlash = null;

    public void OnStoryCaptionsEnd()
    {
        timelineCaptions.SetActive(false);
        timelineShoot1.SetActive(true);
    }

    public void OnSoldierPistolMuzzleFlash()
    {
        muzzleFlash.ShowEffect();
    }

    public void OnEnterGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
