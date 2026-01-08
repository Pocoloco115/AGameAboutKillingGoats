using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Clips")]
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip goatClip;
    [SerializeField] private AudioClip buttonClip;
    [SerializeField] private AudioClip backgroundMusic;

    private Dictionary<string, AudioClip> sfxLibrary;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            sfxLibrary = new Dictionary<string, AudioClip>
            {
                { "Shoot", shootClip },
                { "Goat", goatClip },
                { "Button", buttonClip }
            };

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene")
        {
            if (!musicSource.isPlaying)
            {
                PlayMusic(backgroundMusic);
            }
        }
        else
        {
            StopMusic();
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    public void PlaySFX(string key)
    {
        if (sfxLibrary.ContainsKey(key))
        {
            sfxSource.PlayOneShot(sfxLibrary[key]);
        }
    }
}
