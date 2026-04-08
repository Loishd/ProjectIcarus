using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("SFX List")]
    public AudioClip jumpSfx;
    public AudioClip coinSfx;

    [Header("Music List")]
    [SerializeField] private List<AudioClip> musicList = new List<AudioClip>();

    private int currentTrack = 0;

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Force 2D audio
        musicSource.spatialBlend = 0f;
        sfxSource.spatialBlend = 0f;
        SetMusicVolume(0.05f);
        SetSFXVolume(1);
    }

    void Start()
    {
        PlayCurrentMusic();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextTrack();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousTrack();
        }
    }

    void PlayCurrentMusic()
    {
        if (musicList.Count == 0) return;

        musicSource.Stop();
        musicSource.clip = musicList[currentTrack];
        musicSource.loop = true;
        musicSource.Play();
    }

    void NextTrack()
    {
        currentTrack++;

        if (currentTrack >= musicList.Count)
            currentTrack = 0;

        PlayCurrentMusic();
    }

    void PreviousTrack()
    {
        currentTrack--;

        if (currentTrack < 0)
            currentTrack = musicList.Count - 1;

        PlayCurrentMusic();
    }

    // SFX
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // Volume control
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}