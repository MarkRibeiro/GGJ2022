using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class StopAllThenPlay : MonoBehaviour
{
    public string[] sounds;

    private void Start() {
        Assert.IsNotNull(AudioManager.instance);
        AudioManager.instance.StopAllSounds();
        foreach (string sound in sounds)
        {
            AudioManager.instance.Play(sound);
        }

    }
}
