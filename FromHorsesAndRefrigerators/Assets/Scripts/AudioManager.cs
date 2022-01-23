using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Action OnAudioFinished;

    [HideInInspector] public AudioSource voiceOver; //, music, sfx;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //instance = this;
        voiceOver = this.AddComponent<AudioSource>();
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