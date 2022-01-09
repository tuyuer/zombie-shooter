using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSceneCanvas : MonoBehaviour
{
    public TMP_Text textKills;
    public Image imgBlood;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textKills.text = "" + GameWorld.Instance.killStatistics.KillCount;
        imgBlood.fillAmount = GameWorld.Instance.playerBoard.character.actorBlood.GetFillAmount();
    }
}
