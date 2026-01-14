using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource exclusiveSource;

    [Header("Clips")]
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip goatClip;
    [SerializeField] private AudioClip buttonClip;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip emptyShootClip;
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioClip reloadClip;

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
                { "Button", buttonClip },
                { "Hit", hitClip },
                { "EmptyShoot", emptyShootClip },
                { "Explosion", explosionClip },
                { "Reload", reloadClip }
            };

            SceneManager.sceneLoaded += OnSceneLoaded;
            PlayMusic(menuMusic);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(exclusiveSource.isPlaying)
        {
            exclusiveSource.Stop();
        }

        if (scene.name == "SampleScene")
        {
            if (musicSource.clip != backgroundMusic)
            {
                PlayMusic(backgroundMusic);
            }
            else if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }
        else
        {
            if (musicSource.clip != menuMusic)
            {
                PlayMusic(menuMusic);
            }
            else if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }
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
    public void PlaySFXAtPosition(string key, Vector3 position)
    {
        if (sfxLibrary.ContainsKey(key))
        {
            AudioSource.PlayClipAtPoint(sfxLibrary[key], position);
        }
    }
    public void PlaySFXExclusive(string key)
    {
        if (sfxLibrary.ContainsKey(key))
        {
            exclusiveSource.Stop();
            exclusiveSource.PlayOneShot(sfxLibrary[key]);
        }
    }
    public AudioClip GetSFXClip(string key)
    {
        if (sfxLibrary.ContainsKey(key))
        {
            return sfxLibrary[key];
        }
        return null;
    }
    public void StopSFX()
    {
        sfxSource.Stop();
    }
}
