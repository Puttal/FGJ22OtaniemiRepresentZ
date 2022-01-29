using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource EffectsSource;
    public AudioSource MusicSource;

    public AudioClip clickSFX;
    public AudioClip menuMusic;

    // Play a single clip through the sound effects source.
    public void Play(AudioClip clip)
    {
        EffectsSource.clip = clip;
        EffectsSource.Play();
    }

    // Play a single clip through the music source.
    public void PlayMusic(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    public void PlayFromResources(string path, string prefix = "Sounds/") {
        AudioClip clipToPlay = Resources.Load<AudioClip>(prefix + path);
        Play(clipToPlay);
    }

    public void Click()
    {
        Play(clickSFX);
    }

    public void StartMusic ()
    {
        PlayMusic(menuMusic);
    }
}
