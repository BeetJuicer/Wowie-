using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
            instance = this;     
        else
        {
            Destroy(gameObject);
            return; 
        }

        //DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;// * PlayerPrefs.GetFloat("MusicVolume");
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
    }

    public void Mute()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = 0f;
        }
    }
    
    public void Unmute()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume;// * PlayerPrefs.GetFloat("MusicVolume");
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");//logwarning
            return;
        }
        
        s.source.Play();
    }
    
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");//logwarning
            return;
        }

        s.source.Stop();
    }
}
