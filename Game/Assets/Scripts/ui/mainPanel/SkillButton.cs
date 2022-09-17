using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SkillProgress
{
    public float coldTime = 2.0f;
    private float progress = 0;

    public SkillProgress()
    {
        progress = coldTime;
    }

    public void Reset()
    {
        progress = 0;
    }

    public bool IsReady()
    {
        return progress >= coldTime;
    }

    public void Step(float time)
    {
        progress += time;
    }

    public float Progress
    {
        get { return Mathf.Clamp(progress / coldTime, 0, 1); }
    }
}

public class SkillButton : MonoBehaviour
{
    public SkillProgress progress;

    public Image icon;

    void Awake()
    {
        icon = this.GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        progress.Step(Time.deltaTime);
        icon.fillAmount = progress.Progress;
    }

    public bool IsReady()
    {
        return icon.fillAmount >= 0.99;
    }

    public void TriggerSkill()
    {
        if (!IsReady()) return;

        progress.Reset();
    }
}
