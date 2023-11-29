using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private string volumeType;

    private void Start()
    {
        if (volumeType == "BGM")
        {
            volumeSlider.value = PlayerPrefs.GetFloat("BGMVolume", 1f);
        }
        else if (volumeType == "SFX")
        {
            volumeSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);
        }
    }

    public void SetVolume()
    {
        float volume = volumeSlider.value;
        if (volumeType == "BGM")
        {
            BGMManager.Instance.SetBGMVolume(volume);
            PlayerPrefs.SetFloat("BGMVolume", volume);
        }
        else if (volumeType == "SFX")
        {
            SFXManager.Instance.SetSFXVolume(volume);
            PlayerPrefs.SetFloat("SFXVolume", volume);
        }
    }
}
