using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    public AudioSource warningSource;

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
    private bool isDeathSoundPlayed = false;
    private Coroutine fadeCoroutine;
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

    public void PlayWarning(AudioClip clip)
    {
        if (clip == null) return;
        warningSource.clip = clip;
        warningSource.Play();
    }

    // Method สำหรับสั่ง "ดับเสียง" ทันที
    public void StopWarning()
    {
        if (warningSource.isPlaying)
        {
            warningSource.Stop();
        }
    }

    public void PlayWarningFade(AudioClip clip, float duration)
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        warningSource.clip = clip;
        warningSource.Play();
        fadeCoroutine = StartCoroutine(FadeSource(warningSource, 0.3f, duration));
    }

    public void StopWarningFade(float duration)
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeSource(warningSource, 0f, duration, true));
    }

    private IEnumerator FadeSource(AudioSource source, float targetVolume, float duration, bool stopAtEnd = false)
    {
        float startVolume = source.volume;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            yield return null;
        }

        source.volume = targetVolume;
        if (stopAtEnd && targetVolume <= 0) source.Stop();
    }

    public void StopWarningImmediate()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);

        warningSource.Stop();
        warningSource.volume = 0f; // รีเซ็ต Volume กลับเป็น 0 สำหรับครั้งถัดไป

        // อย่าลืมรีเซ็ตตัวแปรใน HeightSystem ด้วย ถ้าจะให้เกิดใหม่แล้วเล่นใหม่ได้
    }

    // ================= SFX =================
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource == null || clip == null) return;

        // 🛡️ ถ้าเป็นเสียงตาย และเคยเล่นไปแล้วในรอบนี้ ให้ "เงียบ" ไปเลย
        if (clip == deathSfx)
        {
            if (isDeathSoundPlayed) return; // ถ้าเคยดังแล้ว จบ ไม่ต้องเล่นซ้ำ
            isDeathSoundPlayed = true;    // ถ้ายังไม่เคยดัง ให้จำไว้ว่าดังแล้วนะ
        }

        // ส่วนของเสียงทั่วไป (เช่น เหรียญ หรือ Dash) ให้เว้นระยะนิดหน่อยกันเสียงแตก
        if (clip != deathSfx && Time.time - lastPlayTime < minTimeBetweenSFX) return;

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

    public void ResetDeathSound()
    {
        isDeathSoundPlayed = false;
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