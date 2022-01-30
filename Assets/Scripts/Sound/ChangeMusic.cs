using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ChangeMusic : MonoBehaviour
{
    public string music;

    private void Start() {
        Assert.IsNotNull(AudioManager.instance);
        AudioManager.instance.StopAllSounds();
        AudioManager.instance.Play(music);

    }
}
