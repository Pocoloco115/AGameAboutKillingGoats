using UnityEngine;
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

            PlayMusic(backgroundMusic);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(string key)
    {
        if (sfxLibrary.ContainsKey(key))
        {
            sfxSource.PlayOneShot(sfxLibrary[key]);
        }
    }
}
