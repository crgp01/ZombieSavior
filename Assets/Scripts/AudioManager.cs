﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public SoundController[] soundList;

    void Awake() {
        foreach (SoundController sound in soundList)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
        }
    }

    public void Play(string name) {
        SoundController soundFinded = Array.Find(soundList, sound => sound.soundName == name);
        soundFinded.source.Play();
    }
}
