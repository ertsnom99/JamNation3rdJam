﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    private const float CROSS_FADE_DURATION = 3f;

    public AudioSource PrimaryAmbient;
    public AudioSource SecondaryAmbient;
    public AudioSource SFXSource;
    public AudioSource SFXSourceAlt;
    
    public AudioClip AMBGame;
    
    public AudioClip SFXLaunchFirwork;
    public AudioClip SFXPath;

    private Dictionary<string, AudioClip> AMBSounds;
    private Dictionary<string, AudioClip> FXSounds;

    void Awake()
    {
        FXSounds = new Dictionary<string, AudioClip>();

        FXSounds.Add("SFXLaunchFirwork", SFXLaunchFirwork);
        FXSounds.Add("SFXPath", SFXPath);

        AMBSounds = new Dictionary<string, AudioClip>();
        
        AMBSounds.Add("AMBGame", AMBGame);
    }

    public void SetPrimaryAmbient(string soundKey)
    {
        PrimaryAmbient.clip = AMBSounds[soundKey];
    }

    public void SetSecondaryAmbient(string soundKey)
    {
        SecondaryAmbient.clip = AMBSounds[soundKey];
    }

    public void PlayPrimaryAmbient()
    {
        PrimaryAmbient.Play();
    }

    public void PausePrimaryAmbient()
    {
        PrimaryAmbient.Pause();

    }

    public void StopPrimaryAmbient()
    {
        PrimaryAmbient.Stop();
    }

    public void RestartPrimaryAmbient()
    {
        PrimaryAmbient.Stop();
        PrimaryAmbient.Play();
    }

    public void PlaySecondaryAmbient()
    {
        SecondaryAmbient.Play();
    }

    public void PauseSecondaryAmbient()
    {
        SecondaryAmbient.Pause();
    }

    public void StopSecondaryAmbient()
    {
        SecondaryAmbient.Stop();
    }

    public void RestartSecondaryAmbient()
    {
        SecondaryAmbient.Stop();
        SecondaryAmbient.Play();
    }

    public void Crossfade()
    {
        StartCoroutine(ChangeCrossFadeModule(PrimaryAmbient, SecondaryAmbient, CROSS_FADE_DURATION));
        AudioSource temp = PrimaryAmbient;
        PrimaryAmbient = SecondaryAmbient;
        SecondaryAmbient = temp;
    }

    private IEnumerator ChangeCrossFadeModule(AudioSource a1, AudioSource a2, float duration)
    {
        var startTime = Time.time;
        var endTime = startTime + duration;
        a2.Play();

        while (Time.time < endTime)
        {
            var i = (Time.time - startTime) / duration;
            a1.volume = (1 - i);
            a2.volume = i;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void PlayOneShotSound(string soundKey)
    {
        AudioSource source = SFXSource.isPlaying ? SFXSourceAlt : SFXSource;
        source.clip = FXSounds[soundKey];
        source.Play();
    }
}