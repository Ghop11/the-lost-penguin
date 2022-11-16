using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    private void Awake()
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

    public AudioSource[] music;
    public AudioSource[] sfx;


    public void PlayMusic(int trackNumber)
    {
        StopMusic();
        if (trackNumber < music.Length)
        {
            music[trackNumber].volume = 0.1f;
            music[trackNumber].Play();
            music[7].Play();
        }
    }
    
    public void PlayMusicEnv(int trackNumber, float volume)
    {
        StopMusic();
        if (trackNumber < music.Length)
        {
            music[trackNumber].volume = volume;
            music[trackNumber].Play();
            music[7].Play();
        }
    }

    public void PlayOnlyWind()
    {
        StopMusic();
        music[7].Play();
    }
    
    public void PlayMusicWithVolume(int trackNumber, float volume)
    {
        StopMusic();
        if (trackNumber < music.Length)
        {
            music[trackNumber].volume = volume;
            music[trackNumber].Play();
            music[7].Play(); // wind always plays
        }
    }

    public void StopMusic()
    {
        foreach (AudioSource track in music)
        {
            track.Stop();
        }
    }


    public void PlaySFX(int fx, float volume)
    {
        if (fx < sfx.Length)
        {
            sfx[fx].Stop();
            sfx[fx].volume = volume;
            sfx[fx].Play();
        }
    }
    
    public void PlaySFXWithPitch(int fx, float volume, float pitch)
    {
        if (fx < sfx.Length)
        {
            sfx[fx].Stop();
            sfx[fx].volume = volume;
            sfx[fx].pitch = pitch;
            sfx[fx].Play();
        }
    }

    public void StopThisSFX(int fx)
    {
        sfx[fx].Stop();
    }

}
