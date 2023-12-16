using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField, Header("Sound Settings"), Range(0f, 1f)]
    float masterVolume = 1.0f;

    [SerializeField]
    Sound[] musicSounds, sfxSounds;

    [SerializeField]
    AudioSource musicSource, sfxSource;

    public float MasterVolume { get => masterVolume;
        set
        {
            masterVolume = value;
            musicSource.volume= masterVolume * 0.5f;
            sfxSource.volume = masterVolume;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance= this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
      if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            PlayMusic("MenuMusic");
        }
      else if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            PlayMusic("GameMusic");
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if(s == null)
        {
            Debug.Log(" Sound Not Found");

        }
        else
        {
            musicSource.volume = 0.5f * masterVolume;
            musicSource.clip = s.audioClip;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {


    }

    public void PlaySFX(string name, float volume)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log(" Sound Not Found");

        }
        else
        {
           
                sfxSource.PlayOneShot(s.audioClip,volume * masterVolume);
            
            
            
        }

    }
    public void PauseSFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log(" Sound Not Found");

        }
        else
        {

            sfxSource.clip = s.audioClip;
            sfxSource.Pause();

        }

    }

    public void StopSFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log(" Sound Not Found");

        }
        else
        {
            
                sfxSource.clip = s.audioClip;
                sfxSource.Stop();
            
        }

    }
}
