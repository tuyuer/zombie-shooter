using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoBehaviour
{
    public SimpleObjectPool bloodPool = null;
    public SimpleObjectPool soundPool = null;

    public AudioClip killedEffect = null;

    public Joystick joystick = null;
    public Joystick aimstick = null;

    private static GameWorld _instance = null;

    public static GameWorld Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        _instance = this;
    }

    public void SpawnBlood(Vector3 position, Quaternion rotation)
    {
        GameObject fetchedObj = bloodPool.FetchObject();
        fetchedObj.transform.position = position;
        fetchedObj.transform.rotation = rotation;
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
}
