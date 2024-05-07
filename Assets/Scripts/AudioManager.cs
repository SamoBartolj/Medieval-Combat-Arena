using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource lightAttackAudio;
    public AudioSource clickAudio;


    public void PlayLightAttackAudio()
    {
        lightAttackAudio.Play();
    }

    public void PlayClickAudio()
    {
        clickAudio.Play();
    }
}
