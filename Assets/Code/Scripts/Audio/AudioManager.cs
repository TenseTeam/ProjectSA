using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource backgroundMusicSource;
    public AudioSource ambienceSource;
    public AudioSource oneShotVoiceSource;
    public AudioSource oneShotShadowSource;

    public AudioMixer audioMixer;

    public AudioClip backgroundMusicClip;
    public AudioClip[] oneShotVoiceClips;
    public AudioClip[] oneShotShadowClips;
    public AudioClip ambienceClip;

    [Range(0, 100)] public float voicePlayChance = 30f; 
    [Range(0, 100)] public float shadowPlayChance = 30f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    

    void Start()
    {
        PlayBackgroundMusic();
        PlayAmbience();
        TryPlayRandomOneShot();
        TryPlayShadowOneShot();
        //oneShotAmbienceSource.clip = oneShotAmbienceClips[Random.Range(0, oneShotAmbienceClips.Length)];
        //oneShotAmbienceSource.PlayOneShot(oneShotAmbienceSource.clip);
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusicClip != null)
        {
            backgroundMusicSource.clip = backgroundMusicClip; 
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
    }

    public void PlayAmbience()
    {
        if (ambienceClip != null)
        {
            ambienceSource.clip = ambienceClip;
            ambienceSource.loop = true;
            ambienceSource.Play();
        }
    }

    public void PlayRandomOneShot()
    {
        if (oneShotVoiceClips.Length > 0)
        {
            int randomIndex = Random.Range(0, oneShotVoiceClips.Length);
            oneShotVoiceSource.clip = oneShotVoiceClips[randomIndex];
            oneShotVoiceSource.loop = true;
            oneShotVoiceSource.Play();
        }
    }

    public void PlayShadowOneShot()
    {
        if (oneShotVoiceClips.Length > 0)
        {
            int randomIndex = Random.Range(0, oneShotShadowClips.Length);
            oneShotShadowSource.clip = oneShotShadowClips[randomIndex];
            oneShotShadowSource.loop = true;
            oneShotShadowSource.Play();
        }
    }

    public void TryPlayRandomOneShot()
    {
        if (Random.Range(0f, 100f) <= voicePlayChance)
        {
            PlayRandomOneShot();
        }
    }

    public void TryPlayShadowOneShot()
    {
        if (Random.Range(0f, 100f) <= shadowPlayChance)
        {
            PlayShadowOneShot();
        }
    }
}