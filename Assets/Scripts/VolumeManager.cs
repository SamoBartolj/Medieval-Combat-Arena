using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public Slider musicSlider;
    public Slider effectsSlider;

    public AudioSource musicAudioSource;
    public AudioSource[] effectsAudioSource;

    public string musicPrefsKey = "MusicVolume";
    public string effectsPrefsKey = "EffectsVolume";

    void Start()
    {
        if (PlayerPrefs.HasKey(musicPrefsKey) && PlayerPrefs.HasKey(effectsPrefsKey))
        {
            float savedMusicValue = PlayerPrefs.GetFloat(musicPrefsKey);
            musicSlider.value = savedMusicValue;

            float savedEffectsValue = PlayerPrefs.GetFloat(effectsPrefsKey);
            effectsSlider.value = savedEffectsValue;

            SetVolume(savedMusicValue, savedEffectsValue);
        }
    }

    public void SaveSliderValue()
    {
        //Music Volume
        float musicVolume = musicSlider.value;
        PlayerPrefs.SetFloat(musicPrefsKey, musicVolume);
        PlayerPrefs.Save();

        //Effects Volume
        float effectsVolume = effectsSlider.value;
        PlayerPrefs.SetFloat(effectsPrefsKey, effectsVolume);
        PlayerPrefs.Save();

        SetVolume(musicVolume, effectsVolume);
    }

    void SetVolume(float musicVolume, float effectsVolume)
    {
        for(int i = 0; i < effectsVolume; i++)
        {
            effectsAudioSource[i].volume = effectsVolume;
        }
        musicAudioSource.volume = musicVolume;
    }
}
