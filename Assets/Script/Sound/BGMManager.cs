using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;
    [Header("BGM")]
    [SerializeField] private AudioClip Menu;
    [SerializeField] private AudioClip Indoor;
    [SerializeField] private AudioClip Outdoor;
    [SerializeField] private AudioClip Pelabuhan;
    [SerializeField] private AudioClip BalaiKota;
    [SerializeField] private AudioClip Penyerbuan;
    [SerializeField] private AudioClip GedungInternatio;
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
    }
    public void PlayBGMMenu()
    {
        audioSource.clip = Menu;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void PlayBGMIndoor()
    {
        audioSource.clip = Indoor;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void PlayBGMOutdoor()
    {
        audioSource.clip = Outdoor;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void PlayBGMPelabuhan()
    {
        audioSource.clip = Pelabuhan;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void PlayBGMBalaiKota()
    {
        audioSource.clip = BalaiKota;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void PlayBGMPenyerbuan()
    {
        audioSource.clip = Penyerbuan;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void PlayBGMGedungInternatio()
    {
        audioSource.clip = GedungInternatio;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void MuteBGM()
    {
        audioSource.mute = !audioSource.mute;
    }
}
