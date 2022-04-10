using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManagerScript instance;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name) {
        Sound s  = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null) return;
        s.source.PlayDelayed(s.start);
    }
}
