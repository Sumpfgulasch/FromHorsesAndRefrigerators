using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Action OnAudioFinished;

    public AudioClip[] VoiceOvers;
    private AudioSource voiceOver, music, sfx;
    void Start()
    {
        voiceOver = new AudioSource();
        //voiceOver.clip
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayClipAndFireEvent(AudioClip clip)
    {
        //clip
    }
}