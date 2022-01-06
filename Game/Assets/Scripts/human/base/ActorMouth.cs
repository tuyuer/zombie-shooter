using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorMouth : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] damageSpeeks;
    public AudioClip[] randomSpeeks;
    public AudioClip[] deathSpeeks;

    public void PlayDamageSpeek()
    {
        if (damageSpeeks.Length > 0)
        {
            audioSource.PlayOneShot(damageSpeeks[Random.Range(0, damageSpeeks.Length - 1)]);
        }
    }

    public void PlayRandomSpeek()
    {

    }

    public void PlayDeathSpeek()
    {
        if (deathSpeeks.Length > 0)
        {
            audioSource.PlayOneShot(deathSpeeks[Random.Range(0, deathSpeeks.Length - 1)]);
        }
    }
}
