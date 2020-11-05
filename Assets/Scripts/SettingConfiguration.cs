using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SettingConfiguration : MonoBehaviour
{
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    public void SetMusicVolume(float volume) {
        audioMixer.SetFloat("MusicVolume", volume);
    }
}
