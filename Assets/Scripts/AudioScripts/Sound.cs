using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;
    
    public bool loop;
    public float start = 0;

    [HideInInspector]
    public AudioSource source;

}
