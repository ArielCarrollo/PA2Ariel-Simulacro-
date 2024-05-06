using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicSetting : MonoBehaviour
{
    public GameObject MusicSettings;
    private bool isMuted = false;
    public void MostrarMusicSettings()
    {
        if (MusicSettings.activeSelf == false)
        {
            MusicSettings.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            MusicSettings.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public AudioMixer audioMixer;
    public AudioSettings audioSettings;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        audioSettings.masterVolume = volume;
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        audioSettings.musicVolume = volume;
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        audioSettings.sfxVolume = volume;
    }
    public void ToggleMute()
    {
        isMuted = !isMuted;
        if (isMuted)
        {
            audioMixer.SetFloat("Master", -80);
            audioMixer.SetFloat("Music", -80);
            audioMixer.SetFloat("SFX", -80);
        }
        else
        {
            audioMixer.SetFloat("Master", Mathf.Log10(audioSettings.masterVolume) * 20);
            audioMixer.SetFloat("Music", Mathf.Log10(audioSettings.musicVolume) * 20);
            audioMixer.SetFloat("SFX", Mathf.Log10(audioSettings.sfxVolume) * 20);
        }
    }
}
