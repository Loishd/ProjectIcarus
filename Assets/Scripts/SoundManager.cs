using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("SFX List")]
    public AudioClip birdSfx;
    public AudioClip deathSfx;
    public AudioClip wingflapSfx;
    public AudioClip hotWingSfx;
    public AudioClip wetWingSfx;
    public AudioClip windHitSfx;
    public AudioClip cloudHitSfx;
    public AudioClip coinSfx;
    public AudioClip dashSfx;

    [Header("Music List")]
    [SerializeField] private List<AudioClip> musicList = new List<AudioClip>();

    [Header("Audio Mixer")]
    [SerializeField] AudioMixer mixer;

    float masterVolume = 1f;
    float musicVolume = 0.05f;
    float sfxVolume = 1f;

    private float lastPlayTime;
    public float minTimeBetweenSFX = 0.05f;

    private int currentTrack = 0;

    void Awake()
    {
        // Singleton + DontDestroy
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Force 2D audio
        if (musicSource != null) musicSource.spatialBlend = 0f;
        if (sfxSource != null) sfxSource.spatialBlend = 0f;
    }

    void Start()
    {
        LoadVolume();
        PlayCurrentMusic(); // 🔥 auto play
    }

    void Update()
    {
        // กัน null
        if (ScoreManager.Instance != null && ScoreManager.Instance.isPause) return;
    }

    // ================= MUSIC =================
    public void PlayCurrentMusic()
    {
        if (musicList.Count == 0 || musicSource == null) return;

        musicSource.Stop();
        musicSource.clip = musicList[currentTrack];
        musicSource.loop = true;
        musicSource.Play();
    }

    public void NextTrack()
    {
        currentTrack++;
        if (currentTrack >= musicList.Count)
            currentTrack = 0;

        PlayCurrentMusic();
    }

    public void PreviousTrack()
    {
        currentTrack--;
        if (currentTrack < 0)
            currentTrack = musicList.Count - 1;

        PlayCurrentMusic();
    }

    // ================= SFX =================
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource == null || clip == null) return;

        // ถ้าเวลาที่ผ่านไปยังไม่ถึงค่าที่กำหนด ไม่ต้องเล่น
        if (Time.time - lastPlayTime < minTimeBetweenSFX) return;

        sfxSource.PlayOneShot(clip);
        lastPlayTime = Time.time;
    }

    // ================= VOLUME =================
    float ToDB(float value)
    {
        return Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
    }

    public void SetMasterVolume(float value)
    {
        masterVolume = value;
        musicSource.volume = musicVolume * masterVolume;
        sfxSource.volume = sfxVolume * masterVolume;

        if (mixer != null)
        {
            mixer.SetFloat("MasterVolume", ToDB(masterVolume));
        }
        else
        {
            ApplyFallbackVolume();
        }

        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        musicSource.volume = musicVolume;

        if (mixer != null)
        {
            mixer.SetFloat("MusicVolume", ToDB(musicVolume));
        }
        else
        {
            ApplyFallbackVolume();
        }

        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        sfxSource.volume = sfxVolume;

        if (mixer != null)
        {
            mixer.SetFloat("SFXVolume", ToDB(sfxVolume));
        }
        else
        {
            ApplyFallbackVolume();
        }

        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    // ================= LOAD =================
    void LoadVolume()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.05f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        SetMasterVolume(masterVolume);
        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);
    }

    // ================= FALLBACK =================
    void ApplyFallbackVolume()
    {
        if (musicSource != null)
            musicSource.volume = musicVolume * masterVolume;

        if (sfxSource != null)
            sfxSource.volume = sfxVolume * masterVolume;
    }

    void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}