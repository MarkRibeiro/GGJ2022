using System.Collections;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
		}
		DontDestroyOnLoad(gameObject);

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.volume = s.volume;

			s.source.outputAudioMixerGroup = mixerGroup;
            if(s.playOnAwake){
                Play(s.name);
            }
		}
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		} 

		s.source.Play();
	}


	public void StopSound(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.Stop();
	}

	public void StopAllSounds()
	{
		foreach (Sound s in sounds)
		{
			s.source.Stop();
		}
	}

	public IEnumerator PlayLevelSounds()
	{
		string[] clips = new string[3] {"Inicio fase", "Tema Fase p1", "Tema Fase p2" }; 

		yield return null; 

		for(int i = 0; i < clips.Length; i++)
		{
			Sound s = Array.Find(sounds, item => item.name == clips[i]);

			s.source.Play();	

			while(s.source.isPlaying)
			{
				yield return null;
			}		
		}
	}
}