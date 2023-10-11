using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;
    [Header("SFX")]
    [SerializeField] private AudioClip ButtonStart;
    [SerializeField] private AudioClip ButtonClick;
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
    public void MuteSFX()
    {
        audioSource.mute = !audioSource.mute;
    }
}
