using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioConfig : MonoBehaviour
{
    public Slider geralSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public static float geralFloat;
    public static float musicFloat;
    public static float sfxFloat;
    public static AudioManager _am;
    public static AudioManager am
    {
        get
        {
            if (_am == null)
            {
                _am = FindObjectOfType<AudioManager>();
            }
            return _am;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        geralSlider.value = geralFloat;
        musicSlider.value = musicFloat;
        sfxSlider.value = sfxFloat;

        geralSlider.onValueChanged.AddListener(SetGeralVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);
    }

    public void SetGeralVolume(float vol)
    {
        geralFloat = vol;
        UpdateSound();
    }

    public void SetMusicVolume(float vol)
    {
        musicFloat = vol;
        UpdateSound();
    }

    public void SetSfxVolume(float vol)
    {
        sfxFloat = vol;
        UpdateSound();
    }

    public static void UpdateSound()
    {
        foreach (Sound s in am.sounds)
        {
            s.source.volume = geralFloat;
            if (s.isSFX)
            {
                s.source.volume *= sfxFloat;
            }
            else
            {
                s.source.volume *= musicFloat;
            }
        }
    }
}