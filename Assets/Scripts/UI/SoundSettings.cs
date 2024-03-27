using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public enum VolumeParametrs
{
    MasterVolume,
    MusicVolume,
    SoundsVolume
}
public class SoundSettings : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private VolumeParametrs volumeParam;
    private void Start()
    {
        SetVolume(PlayerPrefs.GetFloat(volumeParam.ToString(), 100));
    }
    
    public void SetVolumeFromSlider()
    {
        SetVolume(slider.value);
    }

    public void SetVolume(float value)
    {
        if(value < 1) { 
            value = .001f;
        }

        RefreshSlider(value);

        PlayerPrefs.SetFloat(volumeParam.ToString(), value);

        masterMixer.SetFloat(volumeParam.ToString(), Mathf.Log10(value / 100) * 20f);
    }

    public void RefreshSlider(float value)
    {
        slider.value = value;
    } 
}
