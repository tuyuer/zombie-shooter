using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HitJoy;
using UnityEngine.Assertions;

public class GameWorld : MonoBehaviour
{
    [HideInInspector]
    public Character player;
    [HideInInspector]
    public Joystick joystick = null;
    [HideInInspector]
    public Joystick aimstick = null;

    public bool isGameScene = false;
    public WaveManager waveManager;

    public SimpleObjectPool[] bulletPools;
    public SimpleObjectPool bloodPool = null;
    public SimpleObjectPool soundPool = null;

    public AudioClip killedEffect = null;

    public KillStatistics killStatistics;
    public Backpack backpack;

    private static GameWorld _instance = null;

    public static GameWorld Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        Application.targetFrameRate = 60;
        _instance = this;
    }

    void Start()
    {
        if(isGameScene) SetupGame();

        SetupPlayer();
        SetupWave();
    }

    void OnEnable()
    {
        MessageCenter.AddMessageObserver(this, NotificationDef.NOTIFICATION_ON_PLAYER_DEATH, new MessageEvent(OnPlayerDeath));
    }

    void OnDisable()
    {
        MessageCenter.RemoveAllObservers(this);
    }

    void SetupGame() 
    {
        //打开页面
        var uiManager = UIManager.GetInst();
        UiMainPanel mainPanel = uiManager.ShowProxy(UIProxyType.MainPanel) as UiMainPanel;

        //设置joystick, aimstick
        joystick = mainPanel.joystick;
        aimstick = mainPanel.aimstick;
    }

    void SetupPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag(TagDef.Player);
        Assert.IsNotNull(playerObj);

        player = playerObj.GetComponent<Character>();
        Assert.IsNotNull(player);
    }

    void SetupWave()
    {
        waveManager.StartWork();
    }

    void OnPlayerDeath(System.Object data)
    {
        UIManager.GetInst().CloseAllProxy();
        UIManager.GetInst().ShowProxy(UIProxyType.GameOverPanel);
    }

    public void SpawnBlood(Vector3 position, Quaternion rotation)
    {
        GameObject fetchedObj = bloodPool.FetchObject();
        fetchedObj.transform.position = position;
        fetchedObj.transform.rotation = rotation;
    }

    public void PlaySound(sound_type soundType)
    {
        SoundConfig soundConfig = ConfigManager.Instance.GetConfigByKey(EnumConfigGenre.ENUM_CONFIG_SOUND, (int)soundType) as SoundConfig;
        if (soundConfig != null)
        {
            Debug.Log("soundType=>>>" + soundType + ",PlaySound=>>>" + soundConfig.m_strPath);
            AudioClip audioClip = Resources.Load(soundConfig.m_strPath) as AudioClip;
            PlaySound(Camera.main.transform.position, audioClip);
        }
    }

    public void PlaySound(Vector3 position, AudioClip audioClip)
    {
        GameObject fetchedObj = soundPool.FetchObject();
        fetchedObj.transform.position = position;

        SoundEffect soundEffect = fetchedObj.GetComponent<SoundEffect>();
        if (soundEffect != null)
        {
            soundEffect.PlayAudioClip(audioClip);
        }
    }

    public void PlayKilledSoundEffect(Vector3 position)
    {
        PlaySound(position, killedEffect);
    }

    public SimpleObjectPool GetBulletPoolByType(simple_object_pool_type poolType)
    {
        foreach (var bulletPool in bulletPools)
        {
            if (bulletPool.poolType == poolType)
            {
                return bulletPool;
            }
        }
        return null;
    }
}
