// ============================================
// 6. AudioManager.cs - 오디오 관리
// ============================================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private Dictionary<string, AudioClip> musicClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // 리소스에서 오디오 클립 로드
        LoadAudioClips();
    }

    private void LoadAudioClips()
    {
        // 음악 로드
        AudioClip[] music = Resources.LoadAll<AudioClip>("Audio/Music");
        foreach (AudioClip clip in music)
        {
            musicClips[clip.name] = clip;
        }

        // 효과음 로드
        AudioClip[] sfx = Resources.LoadAll<AudioClip>("Audio/SFX");
        foreach (AudioClip clip in sfx)
        {
            sfxClips[clip.name] = clip;
        }
    }

    public void PlayMusic(string musicName, float fadeDuration = 1f)
    {
        if (!musicClips.ContainsKey(musicName))
        {
            Debug.LogWarning($"Music not found: {musicName}");
            return;
        }

        StartCoroutine(CrossfadeMusic(musicClips[musicName], fadeDuration));
    }

    public void PlaySound(string soundName, float volume = 1f)
    {
        if (!sfxClips.ContainsKey(soundName))
        {
            Debug.LogWarning($"Sound not found: {soundName}");
            return;
        }

        sfxSource.PlayOneShot(sfxClips[soundName], volume);
    }

    private IEnumerator CrossfadeMusic(AudioClip clip, float duration)
    {
        // Fade out
        float elapsed = 0f;
        float startVolume = musicSource.volume;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0, elapsed / duration);
            yield return null;
        }

        // 음악 변경
        musicSource.clip = clip;
        musicSource.Play();

        // Fade in
        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0, 1, elapsed / duration);
            yield return null;
        }

        musicSource.volume = 1f;
    }
}
