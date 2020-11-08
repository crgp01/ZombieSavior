using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SettingConfiguration : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixer SFXaudioMixer;

    public void SetMusicVolume(float volume) {
        audioMixer.SetFloat("MusicVolume", volume);
    }
    public void SetSFXVolume(float volume) {
        SFXaudioMixer.SetFloat("SFXVolume", volume);
    }
}
