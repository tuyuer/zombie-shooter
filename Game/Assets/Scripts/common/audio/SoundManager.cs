using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HitJoy;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    private static SoundManager instance;
    public static SoundManager Instance
    {
        get { return instance; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {

    }

    public void PlaySound(sound_type soundType)
    {
        SoundConfig soundConfig = ConfigManager.Instance.GetConfigByKey(EnumConfigGenre.ENUM_CONFIG_SOUND, (int)soundType) as SoundConfig;
        if (soundConfig != null)
        {
            AudioClip audioClip = Resources.Load(soundConfig.m_strPath) as AudioClip;
            audioSource.PlayOneShot(audioClip);
        }
    }
}
