using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;
    [Header("SFX")]
    [SerializeField] private AudioClip ButtonStart;
    [SerializeField] private AudioClip ButtonClick;
    [SerializeField] private AudioClip GunShot;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }

    public void StartButton()
    {
        audioSource.clip = ButtonStart;
        audioSource.loop = false;
        audioSource.Play();
    }
    public void ClickButton()
    {
        audioSource.clip = ButtonClick;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void GunShotSound()
    {
        audioSource.clip = GunShot;
        audioSource.loop = false;
        audioSource.Play();
    }
    public void SetSFXVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
