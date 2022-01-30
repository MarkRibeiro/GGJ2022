using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioConfig : MonoBehaviour
{
    private int firstPlayInt;
    public Slider geralSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    private float geralFloat;
    private float musicFloat;
    private float sfxFloat;
    private AudioManager _am;
    // Start is called before the first frame update
    void Start()
    {
        _am = GetComponent<AudioManager>();

        geralFloat = 0.5f;
        musicFloat = 0.5f;
        sfxFloat = 0.5f;
        geralSlider.value = geralFloat;
        musicSlider.value = musicFloat;
        sfxSlider.value = sfxFloat;
    }

    public void UpdateSound()
    {
        foreach(Sound s in _am.sounds)
        {
            s.source.volume = geralSlider.value;
            if(s.isSFX){
                s.source.volume *= sfxSlider.value;
            }
            else{
                s.source.volume *= musicSlider.value;
            }
        }
    }
}